using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public List<Collider2D> detectedObjects = new List<Collider2D>();
    public Collider2D col;
    public List<GameObject> detectedObj;

        // Call this method when an object is destroyed
        public void ObjectDestroyed(GameObject obj)
        {
            detectedObj.Remove(obj);
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
