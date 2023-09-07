using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public List<Collider2D> detectedObjects = new List<Collider2D>();
    public Collider2D col;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Player")
        {
            detectedObjects.Add(collision);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectedObjects.Remove(collision);
        }
    }
}
