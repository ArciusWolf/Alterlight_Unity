using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{

    public Transform Wolfy;
    public Transform Cyrus;

    void LateUpdate()
    {
        Vector3 newPosition = Wolfy.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, Wolfy.eulerAngles.y, 0f);
    }

}