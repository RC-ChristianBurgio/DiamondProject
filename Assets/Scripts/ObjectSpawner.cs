using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] SO_Object[] pickupsToSpawn;
    [SerializeField] GameObject Object;

    [SerializeField] List<GameObject> currentObjects;

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
        var newObject = Instantiate(Object, new Vector3(0, 0.3f, 0), Quaternion.identity);
        newObject.GetComponent<ObjectController>().ObjectAttributes = pickupsToSpawn[r]; // I hate using getComponent, heaps inefficient 
        currentObjects.Add(newObject);
    }
}
