using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] SO_Object[] pickupsToSpawn;
    [SerializeField] GameObject Object;

    [SerializeField] List<GameObject> currentObjects;
    [SerializeField] private Vector3 spawnLocation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) /*&& currentObjects.Count < 3*/)
        {
            spawnObject();
        }
    }



    public void spawnObject()
    {
        int r = Random.Range(0, pickupsToSpawn.Length);
        var newObject = Instantiate(Object, spawnLocation, Quaternion.identity);
        newObject.GetComponent<ObjectController>().ObjectAttributes = pickupsToSpawn[r]; // I hate using getComponent, heaps inefficient 
        currentObjects.Add(newObject);
    }
}
