using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCDCounter : MonoBehaviour
{
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillCDText;

    WolfyDash wolfyDash;
    float cooldownTimeLeft;

    private void Start()
    {
        wolfyDash = GameObject.Find("Wolfy").GetComponent<WolfyDash>();
    }

    private void Update()
    {
        if (wolfyDash.isDashing || cooldownTimeLeft > 0)
        {
            if (wolfyDash.isDashing)
            {
                cooldownTimeLeft = wolfyDash.dashCoolDown; // start cooldown when dash starts
            }
            else
            {
                cooldownTimeLeft -= Time.deltaTime; // decrease cooldown time
            }

            // darken the skill image
            skillImage.color = new Color(0.5f, 0.5f, 0.5f);
            // show remaining cooldown text
            skillCDText.text = Mathf.Round(cooldownTimeLeft).ToString();
        }
        else
        {
            // reset the skill image color
            skillImage.color = Color.white;
            // hide the cooldown text
            skillCDText.text = "";
        }
    }
}
