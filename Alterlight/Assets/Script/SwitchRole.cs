using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSwitch : MonoBehaviour
{
    public WolfyMovement WolfyController;
    public CyrusMovement CyrusController;
    SwordAttack swordAttack;
    public CinemachineVirtualCamera cam;
    Animator anim;
    public bool WolfyActive;

    // Start is called before the first frame update
    void Start()
    {
        swordAttack = GetComponent<SwordAttack>();
        cam.Follow = WolfyController.transform;
        cam.LookAt = WolfyController.transform;
        WolfyActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Switch();
        }

    }

    private void Switch()
    {
        if (WolfyActive)
        {
            cam.Follow = CyrusController.transform;
            cam.LookAt = CyrusController.transform;
            WolfyController.enabled = false;
            CyrusController.enabled = true;
            WolfyActive = false;
        }
        else if (WolfyActive == false)
        {
            cam.Follow = WolfyController.transform;
            cam.LookAt = WolfyController.transform;
            WolfyController.enabled = true;
            CyrusController.enabled = false;
            WolfyActive = true;
        }
    }
    void FixedUpdate()
    {
        if (WolfyActive)
        {
            anim.SetBool("isMove", true);
        } else if (WolfyActive == false)
        {
            anim.SetBool("isMove", true);
        }
    }


}
