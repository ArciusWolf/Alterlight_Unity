using UnityEngine;
using UnityEngine.Events;

namespace BlazeAISpace 
{
    [AddComponentMenu("Alert Tag Behaviour/Alert Tag Behaviour")]
    public class AlertTagBehaviour : MonoBehaviour
    {
        public bool checkLocation;

        [Tooltip("Animation name to play the moment the AI sees the alert tag. Will play before moving to location (if checked). If this is empty, it'll be ignored.")]
        public string onSightAnim;

        [Min(0), Tooltip("The amount of time to pass playing the animation on sight.")]
        public float onSightDuration = 3f;

        [Tooltip("If check location is true, this animation will play when reaching the location. If check location is false, the animation will play instantly.")]
        public string reachedLocationAnim;
        
        [Min(0), Tooltip("The amount of time to pass playing the reached location animation.")]
        public float reachedLocationDuration = 2f;
        
        [Min(0), Tooltip("The transition time from current animation to either move/finished animations.")]
        public float animT = 0.25f;
        
        [Tooltip("Set your audios in the audio scriptable in the General Tab in Blaze AI.")]
        public bool playAudio;
        public int audioIndex;
        
        [Tooltip("Will call other agents to the location of the alert object.")]
        public bool callOtherAgents;
        [Min(0)]
        public float callRange = 10f;
        [Tooltip("Shows the call range as a white wire sphere in scene view.")]
        public bool showCallRange;
        public LayerMask otherAgentsLayers;
        [Tooltip("If enabled, the call will pass through colliders. If disabled, the call won't pass through the layers set in [Layers To Detect] in Vision.")]
        public bool callPassesColliders;
        [Tooltip("If enabled, the AI will call others not to the exact point but a randomized position within the destination radius. This is to avoid having the AIs all move to the exact same point.")]
        public bool randomizeCallPosition;

        public UnityEvent onStateEnter;
        public UnityEvent onStateExit;


        #region BEHAVIOUR VARS

        BlazeAI blaze;
        AlertStateBehaviour alertStateBehaviour;
        
        bool audioPlayed;
        bool calledAgents;
        bool onSightDone;
        bool isOffMeshBypassed;
        
        float _durationTimer;
        float onSightTimer = 0;

        #endregion
    
        #region UNITY METHODS

        void Start()
        {
            blaze = GetComponent<BlazeAI>();
            alertStateBehaviour = GetComponent<AlertStateBehaviour>();
            

            // force shut if not the same state
            if (blaze.state != BlazeAI.State.sawAlertTag) {
                enabled = false;
            }
        }

        void OnDrawGizmosSelected()
        {
            if (!showCallRange || !callOtherAgents) {
                return;
            }

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, callRange);
        }

        void OnDisable()
        {
            audioPlayed = false;
            calledAgents = false;
            onSightDone = false;
            isOffMeshBypassed = false;

            _durationTimer = 0;
            onSightTimer = 0;

            onStateExit.Invoke();
        }

        void OnEnable()
        {
            onStateEnter.Invoke();
        }

        void Update()
        {
            // check if alert state behaviour isn't added
            if (alertStateBehaviour == null) {
                Debug.Log("Alert State Behaviour must be added for Alert Tag behaviour to function. Please add the alert state behaviour.");
                return;
            }


            if (OffMeshToBypass()) {
                return;
            }


            // play audio
            if (playAudio) {
                PlayAudio();
            }

            
            // call nearby agents
            if (callOtherAgents) {
                CallAgents();
            }

            
            // play the on sight anim -> don't continue until it returns true
            if (!onSightDone) {
                if (!PlayOnSightAnim()) {
                    return;
                }
            }
        

            // see if location needs to be checked
            if (!checkLocation) {
                blaze.animManager.Play(reachedLocationAnim, animT);
                DurationTimer();
                return;
            }


            // go to location if check location is true
            if (blaze.MoveTo(blaze.sawAlertTagPos, alertStateBehaviour.moveSpeed, alertStateBehaviour.turnSpeed, alertStateBehaviour.moveAnim, alertStateBehaviour.animT)) {
                blaze.animManager.Play(reachedLocationAnim, animT);
                DurationTimer();
            }
        }

        #endregion

        #region BEHAVIOUR METHODS

        // play the audio
        void PlayAudio()
        {
            if (!playAudio) {
                return;
            }

            if (audioPlayed) {
                return;
            }

            if (blaze.IsAudioScriptableEmpty()) {
                return;
            }

            if (blaze.PlayAudio(blaze.audioScriptable.GetAudio(AudioScriptable.AudioType.AlertTags, audioIndex))) {
                audioPlayed = true;
            }
        }

        // call agents to position
        void CallAgents()
        {
            if (calledAgents) {
                return;
            }


            Collider[] agentsColl = new Collider[20];
            int agentsNum = Physics.OverlapSphereNonAlloc(transform.position, callRange, agentsColl, otherAgentsLayers);
        
        
            for (int i=0; i<agentsNum; i++) {
                // if this same object then -> skip to next iteration
                if (agentsColl[i].transform.IsChildOf(transform) || transform.IsChildOf(agentsColl[i].transform)) {
                    continue;
                }


                BlazeAI blazeScript = agentsColl[i].transform.GetComponent<BlazeAI>();

                if (blazeScript == null) {
                    continue;
                }
                

                // set the end point that we'll call the other AIs to
                Vector3 point = Vector3.zero;
                Collider objColl = blaze.sawAlertTagObject.GetComponent<Collider>();
                float range = objColl.bounds.size.x + objColl.bounds.size.z;


                if (randomizeCallPosition) {
                    point = blaze.RandomSpherePoint(blaze.sawAlertTagPos, range);
                }
                else {
                    point = blaze.sawAlertTagPos;
                }


                // check there are no obstacles hindering the call
                if (!callPassesColliders) 
                {
                    RaycastHit rayHit;
                    
                    Transform target = agentsColl[i].transform;
                    Collider currentColl = agentsColl[i];

                    Vector3 currentCenter = new Vector3(transform.position.x, transform.position.y + blaze.vision.visionPosition.y, transform.position.z);
                    Vector3 otherAgentCenter = currentColl.ClosestPoint(currentCenter);
                    Vector3 dir = (otherAgentCenter - currentCenter);

                    float rayDistance = Vector3.Distance(otherAgentCenter, transform.position) + 3;

                    if (Physics.Raycast(currentCenter, dir, out rayHit, rayDistance, blaze.vision.layersToDetect)) {
                        if (!rayHit.transform.IsChildOf(target) && !target.IsChildOf(rayHit.transform)) {
                            continue;
                        }
                    }
                }

                
                blazeScript.ChangeState("alert");
                blazeScript.MoveToLocation(point);
            }


            calledAgents = true;
        }

        // play the on sight animation
        bool PlayOnSightAnim()
        {
            if (onSightAnim.Length <= 0 || onSightAnim == null) {
                onSightDone = true;
                return true;
            }

            blaze.animManager.Play(onSightAnim, animT);

            onSightTimer += Time.deltaTime;
            if (onSightTimer >= onSightDuration) {
                onSightDone = true;
                onSightTimer = 0;
                return true;
            }

            return false;
        }

        // the duration before exiting this state
        void DurationTimer()
        {
            _durationTimer += Time.deltaTime;

            if (_durationTimer >= reachedLocationDuration) {
                _durationTimer = 0;
                blaze.ChangeState("alert");
            }
        }

        bool OffMeshToBypass()
        {
            if (blaze.useOffMeshLinks) return false;
            if (!blaze.IsOnOffMeshLink()) return false;

            if (isOffMeshBypassed) return true;
            
            isOffMeshBypassed = true;
            
            blaze.navmeshAgent.Warp(transform.position);
            blaze.ChangeState("alert");
            return true;
        }

        #endregion
    }
}