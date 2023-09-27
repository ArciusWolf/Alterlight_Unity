using UnityEngine;
using System.Collections.Generic;

namespace BlazeAISpace
{
    [System.Serializable]
    public class Vision
    {
        [System.Serializable] public struct normalVision {
            [Range(0f, 360f)]
            public float coneAngle;
            [Min(0f)]
            public float sightRange;
            
            public normalVision (float angle, float range) {
                coneAngle = angle;
                sightRange = range;
            }
        }

        [System.Serializable] public struct alertVision {
            [Range(0f, 360f)]
            public float coneAngle;
            [Min(0f)]
            public float sightRange;

            public alertVision (float angle, float range) {
                coneAngle = angle;
                sightRange = range;
            }
        }

        [System.Serializable] public struct attackVision {
            [Range(0f, 360f)]
            [Tooltip("Always better to have this at 360 in order for the AI to have 360 view when in attack state.")]
            public float coneAngle;
            [Min(0f), Tooltip("Will be automatically set if cover shooter enabled based on Distance From Enemy property.")]
            public float sightRange;
            [Tooltip("If false, the AI will only apply attack vision when in attack state and there's a clear enemy. If an enemy has been lost for a frame or so, it'll apply the alert vision until a target is seen again. This makes vision more realistic but more bound to lose it's target especially if the target goes through the AI collider. If, however, this property is enabled, the attack vision will always be applied in attack state no matter there's a clear enemy or not. Making losing enemies impossible until they're out of range.")]
            public bool alwaysApply;

            public attackVision (float angle, float range, bool forcedApply=true) {
                coneAngle = angle;
                sightRange = range;
                alwaysApply = forcedApply;
            }
        }

        [System.Serializable] public struct AlertTags {
            [Tooltip("The tag name you want to react to.")]
            public string alertTag;
            [Tooltip("The behaviour script to enable when seeing this alert tag.")]
            public MonoBehaviour behaviourScript;
            [Tooltip("When the AI sees an object with an alert tag it'll immediately change it to this value. In order not to get alerted by it again. If this value is empty it'll fall back to 'Untagged'.")]
            public string fallBackTag;
        }
        

        [Header("ENEMY/ALERT LAYERS & TAGS")]
        [Tooltip("Add all the layers you want to detect around the world. Any layer not added will be seen through. Recommended not to add other Blaze agents layers in order for them not to block the view from your target. Also no need to set the enemy layers here.")]
        public LayerMask layersToDetect = Physics.AllLayers;
        [Tooltip("Set the layers of the hostiles and alerts. Hostiles are the enemies you want to attack. Alerts are objects when seen will turn the AI to alert state.")]
        public LayerMask hostileAndAlertLayers;

        [Space(5)]

        [Tooltip("The tag names of hostile gameobjects (player) that this agent should attack. This needs to be set in order to identify enemies.")]
        public string[] hostileTags;
        [Tooltip("Tags that will make the agent become in alert state such as tags of dead bodies or an open door. This is optional.")]
        public AlertTags[] alertTags;


        [Space(10)]
        [Header("VISION POSITION & HEIGHT")]
        [Tooltip("This is where the vision ray will start from. Place on the eyes of the AI. Enable [Show Vision] below to draw the vision cone in the scene view for easy placement. If [Max Sight Level] is set to 0. This Y point will be the maximum Y position the AI can detect and anything bigger will not be detected or seen.")]
        public Vector3 visionPosition = new Vector3(0, 1, 0);
        [Min(0f), Tooltip("The maximum Y position the AI can detect. If any target's Y position is bigger than this, it won't be detected or seen. Enable [Show Max Sight Level] below to show as a green rectangle in the scene view.")]
        public float maxSightLevel;
        [Tooltip("If enabled, and the AI has a detected target it'll not check for it's height in order not lose it. Making the target unable to escape the vision by simply going higher. If disabled, the target can escape the AI by simply going higher.")]
        public bool checkMaxSightOnTargetLock = false;
        [Tooltip("The minimum Y position the AI can detect. If any target's Y position is lower than this, it won't be detected or seen. Enable [Show Min Sight Level] below to show as a red rectangle in the scene view.")]
        public float minSightLevel = -1;
        [Tooltip("If enabled, then the [Min Sight Level] will act as the minimum sight level and anything lower will not be detected. If disabled, no minimum detection level will be applied and anything below the Vision Position Y point (including max sight level) will be detected. More info in the docs, Vision section.")]
        public bool useMinLevel = true;


        [Space(10)]
        [Header("SET VISION RANGE AND ANGLE FOR EACH STATE")]
        public normalVision visionDuringNormalState = new normalVision(90f, 10f);
        public alertVision visionDuringAlertState = new alertVision(100f, 15f);
        public attackVision visionDuringAttackState = new attackVision(360f, 20f);


        [Space(10)]
        [Header("SYNC VISION-HEAD ROTATION (OPTIONAL)"), Tooltip("OPTIONAL: add the head object, this will be used for updating both the rotation of the vision according to the head and the sight level automatically. If empty, the rotation will be according to the body, projecting forwards.")]
        public Transform head;


        [Space(10)]
        [Header("VISION PULSE"), Range(1, 30), Tooltip("Vision systems run once every certain amount of frames to favor performance. Here you can set the amount of frames to pass before running vision on each cycle. The lower the number, the more accurate but expensive. The higher the number, the less accurate but better for performance. Remember the amount of frames passing is basically neglibile, so the accuracy isn't that big of a measure.")]
        public int pulseRate = 10;


        [Space(10)]
        [Header("USE MULTI-RAYS"), Tooltip("Using multi-rays gives better accuracy and takes more of performance. It fires multiple rays to many corners of all colliders of the potential target and decides from the results whether it's enough to be considered 'seen'. While using single ray (setting this to off) is better for performance and fires a single raycast to the center of the main collider. For example if only the head of your player is exposed while the rest of the body is hidden behind a tree, the AI will not react. As the center of the player is hidden by the tree. Take note: multi rays may cause issues in VR so it's best to disable this if you're using VR.")]
        public bool multiRayVision = true;


        [Space(10)]
        [Header("SHOW VISION")]
        [Tooltip("Show the vision cone of normal state in scene view for easier debugging.")]
        public bool showNormalVision = true;
        [Tooltip("Show the vision cone of alert state in scene view for easier debugging.")]
        public bool showAlertVision = true;
        [Tooltip("Show the vision cone of attack state in scene view for easier debugging.")]
        public bool showAttackVision = true;
        [Tooltip("Shows the maximum sight level as a green rectangle.")]
        public bool showMaxSightLevel = true;
        [Tooltip("Shows the minimum sight level as a red rectangle.")]
        public bool showMinSightLevel = true;


        // show the vision spheres in level-editor screen
        public void ShowVisionSpheres(Transform visionTransform, Transform charTransform) 
        {
            if (showNormalVision) {
                DrawVisionCone(visionTransform, charTransform, visionDuringNormalState.coneAngle, visionDuringNormalState.sightRange, Color.white);
            }
            
            if (showAlertVision) {
                DrawVisionCone(visionTransform, charTransform, visionDuringAlertState.coneAngle, visionDuringAlertState.sightRange, Color.white);
            }

            if (showAttackVision) {
                DrawVisionCone(visionTransform, charTransform, visionDuringAttackState.coneAngle, visionDuringAttackState.sightRange, Color.red);
            }

            if (showMaxSightLevel) {
                // all the passed arguments are being ignored
                DrawVisionCone(visionTransform, charTransform, visionDuringAttackState.coneAngle, visionDuringAttackState.sightRange, Color.red, true);
            }

            if (showMinSightLevel) {
                // all the passed arguments are being ignored
                DrawVisionCone(visionTransform, charTransform, visionDuringAttackState.coneAngle, visionDuringAttackState.sightRange, Color.red, true);
            }
        }


        // draw vision cone
        void DrawVisionCone(Transform visionTransform, Transform charTransform, float angle, float rayRange, Color color, bool ignore = false)
        {
            if (visionTransform == null) return;


            if (ignore) {
                if (showMaxSightLevel && maxSightLevel > 0f) {
                    Gizmos.color = new Color(0, 1, 0);
                    Gizmos.DrawCube(charTransform.forward + charTransform.position + new Vector3(visionPosition.x, maxSightLevel, visionPosition.z), new Vector3(0.5f, 0.05f, 0.5f));
                }

                if (showMinSightLevel) {
                    Gizmos.color = new Color(1, 0, 0);
                    Gizmos.DrawCube(charTransform.forward + charTransform.position + new Vector3(visionPosition.x, minSightLevel, visionPosition.z), new Vector3(0.5f, 0.05f, 0.5f));
                }

                return;
            }


            if (angle >= 360f) {   
                Gizmos.color = color;
                Gizmos.DrawWireSphere(visionTransform.position + new Vector3(visionPosition.x, visionPosition.y + maxSightLevel, visionPosition.z), rayRange);
                return;
            }
            

            float halfFOV = angle / 2.0f;

            Quaternion leftRayRotation1 = Quaternion.AngleAxis(-halfFOV, visionTransform.up);
            Quaternion rightRayRotation1 = Quaternion.AngleAxis(halfFOV, visionTransform.up);

            Vector3 leftRayDirection1 = leftRayRotation1 * visionTransform.forward;
            Vector3 rightRayDirection1 = rightRayRotation1 * visionTransform.forward;

            Vector3 npcSight = charTransform.position + visionPosition;

            Gizmos.color = Color.white;
            Gizmos.DrawRay(npcSight, leftRayDirection1 * rayRange);
            Gizmos.DrawRay(npcSight, rightRayDirection1 * rayRange);

            Gizmos.DrawLine(npcSight + rightRayDirection1 * rayRange, npcSight + leftRayDirection1 * rayRange);
        }


        // return the index of the passed alert tag -> if exists
        public int GetAlertTagIndex(string alertTag)
        {
            for (int i=0; i<alertTags.Length; i++) {
                // check if alert tag is empty
                if (alertTags[i].alertTag.Length <= 0) {
                    continue;
                }

                // check if alert tag equals the paramater
                if (alertTags[i].alertTag != alertTag) {
                    continue;
                }
                
                return i;
            }

            return -1;
        }


        // disable all the behaviour scripts of alert tags
        public void DisableAllAlertBehaviours()
        {
            for (int i=0; i<alertTags.Length; i++) {
                if (alertTags[i].behaviourScript != null) {
                    alertTags[i].behaviourScript.enabled = false;
                }
            }
        }


        // check if any tag in hostile and alert are equal
        public void CheckHostileAndAlertItemEqual(bool dialogue=false)
        {
            for (int i=0; i<hostileTags.Length; i++) 
            {
                for (int x=0; x<alertTags.Length; x++) {
                    if (hostileTags[i].Length > 0 && hostileTags[i] == alertTags[x].alertTag) {
                        #if UNITY_EDITOR
                        if (UnityEditor.EditorUtility.DisplayDialog("Same tag in Hostile and Alert",
                            "You can't have the same tag name in both Hostile and Alert. The tag name in Alert Tags will be removed when out of focus or you can continue typing by double clicking the text.", "Ok")) {
                        }
                        #endif 

                        alertTags[x].alertTag = "";
                    }
                }
            }
        }
        

        public void Validate()
        {
            if (minSightLevel > visionPosition.y) 
            {
                minSightLevel = visionPosition.y - 1;

                #if UNITY_EDITOR
                if (UnityEditor.EditorUtility.DisplayDialog("Min Sight Level is too large",
                    "Min Sight Level can't be as big as the Y axis of Vision Position. It'll automatically be decremented.", "Ok")) {
                }
                #endif 
            }

            if (maxSightLevel < visionPosition.y) {
                maxSightLevel = visionPosition.y;
            }
        }
    }
}