using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public static Dictionary<string, StatContainer> StatManagers = new Dictionary<string, StatContainer>();
    public static string activeCharacter;

    public void Awake()
    {
        activeCharacter = "Wolfy";
        StatContainer Wolfy = Resources.Load<StatContainer>("Character/Wolfy");
        StatContainer Cyrus = Resources.Load<StatContainer>("Character/Cyrus");
        if (Wolfy != null && Cyrus != null)
        {
            Debug.Log("Loaded StatManager successfully.");
            StatManagers["Wolfy"] = Wolfy;
            StatManagers["Cyrus"] = Cyrus;
        }
        else if (Wolfy == null || Cyrus == null)
        {
            Debug.LogError("1 in 2 character failed to load.");
        }
    }

    private void Start()
    {

    }

    public static float getStatValue(string statName)
    {
        if (!StatManagers.ContainsKey(activeCharacter))
        {
            Debug.LogError("Active character not found: " + activeCharacter);
            return 0f;
        }

        StatContainer statContainer = StatManagers[activeCharacter];
        switch (statName)
        {
            case "Health":
                return statContainer.Health;
            case "Stamina":
                return statContainer.Stamina;
            case "Damage":
                return statContainer.Damage;
            case "Defense":
                return statContainer.Defense;
            case "Speed":
                return statContainer.Speed;
            case "critRate":
                return statContainer.critRate;
            case "critDamage":
                return statContainer.critDamage;
            default:
                Debug.LogWarning("Invalid stat name: " + statName);
                return 0f;
        }
    }

}