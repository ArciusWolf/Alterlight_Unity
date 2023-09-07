using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DMG_Text : MonoBehaviour
{
    public float timeLast = 0.5f;
    public float timeElapsed = 0f;
    public float floatSpeed = 50;
    Color startingColor;
    public TextMeshProUGUI textMeshPro;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        startingColor = textMeshPro.color;
        textMeshPro.color = startingColor;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        rectTransform.anchoredPosition += new Vector2(0, floatSpeed * Time.deltaTime);
        //rectTransform.position += floatDirection * floatSpeed * Time.deltaTime;

        //textMeshPro.color = new Color(startingColor.r, startingColor.g, startingColor.b, Mathf.Lerp(startingColor.a, 0, timeElapsed / timeLast));
        textMeshPro.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeLast));

        if (timeElapsed >= timeLast)
        {
            Destroy(gameObject);
        }
    }
}
