using UnityEngine;

public class WolfyRemoval : MonoBehaviour
{
    public void RemovePlayer()
    {
        Destroy(gameObject, 3);
    }
}