using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    public void changeLevel(string levelName)
    {
        LevelManager.instance.LoadLevel(levelName);
    }
}
