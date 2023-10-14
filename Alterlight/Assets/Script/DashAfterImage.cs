using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAfterImage : MonoBehaviour
{
    [SerializeField] private Transform Wolfy;

    private SpriteRenderer sr;
    private SpriteRenderer playerSR;

    private Color color;

    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;

    [SerializeField]
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        Wolfy = GameObject.Find("Wolfy").transform;
        playerSR = Wolfy.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        sr.sprite = playerSR.sprite;
        transform.position = Wolfy.position;
        transform.rotation = Wolfy.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        sr.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            DashAIPooling.Instance.AddToPool(gameObject);
        }
    }

}