using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerSwitch : MonoBehaviour
{
    public WolfyMovement WolfyController;
    public CyrusMovement CyrusController;

    public CinemachineVirtualCamera cam;

    public GameObject PlayerWolfy;
    public GameObject PlayerCyrus;

    public GameObject MiniWolfy;
    public GameObject MiniCyrus;
    Animator anim;
    public bool WolfyActive;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam.Follow = WolfyController.transform;
        cam.LookAt = WolfyController.transform;
        WolfyActive = true;
        // disable gameObject PlayerCyrus
        PlayerCyrus.SetActive(false);
        PlayerWolfy.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
            PlayerCyrus.SetActive(true);
            PlayerWolfy.SetActive(false);
            MiniCyrus.SetActive(false);
            MiniWolfy.SetActive(true);
        }
        else if (WolfyActive == false)
        {
            cam.Follow = WolfyController.transform;
            cam.LookAt = WolfyController.transform;
            WolfyController.enabled = true;
            CyrusController.enabled = false;
            WolfyActive = true;
            PlayerCyrus.SetActive(false);
            PlayerWolfy.SetActive(true);
            MiniCyrus.SetActive(true);
            MiniWolfy.SetActive(false);
        }
    }
}