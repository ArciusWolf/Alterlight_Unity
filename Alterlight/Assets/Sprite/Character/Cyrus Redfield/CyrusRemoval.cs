using UnityEngine;

public class CyrusRemoval : MonoBehaviour
{
    public void RemovePlayer()
    {
        Destroy(gameObject, 3);
    }
}