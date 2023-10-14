using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAIPooling : MonoBehaviour
{
     [SerializeField] private GameObject DashPrefab;

    private Queue<GameObject> DashQueue = new Queue<GameObject>();

    public static DashAIPooling Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Initialize(1);
    }

    private void Initialize(int initialCount)
    {
        for (int i = 0; i < initialCount; i++)
        {
            var instanceToAdd = Instantiate(DashPrefab);
            instanceToAdd.transform.SetParent(transform); // Set the parent of the instance to the DashPooler object
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        DashQueue.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (DashQueue.Count == 0)
        {
            Initialize(1);
        }

        var instance = DashQueue.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
