using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnObjectPrefab;
    [SerializeField] private int rowCount;
    [SerializeField] private int columnCount;
    [SerializeField] private float offset;
    
    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int a = 0; a < columnCount; a++)
            {
                Vector3 objectPos = new Vector3(i * offset, 0f , a * offset);

                GameObject ob = Instantiate(spawnObjectPrefab, objectPos, Quaternion.identity, this.transform);
            }
        }
    }
}
