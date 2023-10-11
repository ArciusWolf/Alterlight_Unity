using Cinemachine;
using UnityEngine;

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

    private bool _isWolfyActive;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SwitchToWolfy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Switch();
        }
    }

    public bool WolfyActive()
    {
        return _isWolfyActive;
    }


    private void Switch()
    {
        if (_isWolfyActive)
        {
            SwitchToCyrus();
        }
        else
        {
            SwitchToWolfy();
        }
    }

    private void SwitchToWolfy()
    {
        SetActiveCharacter(WolfyController, PlayerWolfy, MiniCyrus, "Wolfy");
        _isWolfyActive = true;
    }

    private void SwitchToCyrus()
    {
        SetActiveCharacter(CyrusController, PlayerCyrus, MiniWolfy, "Cyrus");
        _isWolfyActive = false;
    }

    private void SetActiveCharacter(MonoBehaviour activeController, GameObject activePlayer, GameObject activeMini, string characterName)
    {
        cam.Follow = activeController.transform;
        cam.LookAt = activeController.transform;

        WolfyController.enabled = false;
        CyrusController.enabled = false;

        activeController.enabled = true;

        PlayerCyrus.SetActive(false);
        PlayerWolfy.SetActive(false);

        activePlayer.SetActive(true);

        MiniCyrus.SetActive(false);
        MiniWolfy.SetActive(false);

        activeMini.SetActive(true);

        StatManager.activeCharacter = characterName;

        Debug.Log($"Switched to {characterName}");
        Debug.Log($"{characterName}Controller.enabled: {activeController.enabled}");
        Debug.Log($"StatManager.activeCharacter: {StatManager.activeCharacter}");
    }
}
