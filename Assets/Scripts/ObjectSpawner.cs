using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Essence.Hand.StaticGesture;
using Wave.Essence.Hand;
using System.Linq;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawning Properties")]
    [SerializeField] int nodesToSpawn;
    [SerializeField] float circleRadius;
    [SerializeField] SO_Object[] objectsInCirculation;
    [SerializeField] List<SpawnNode> spawnLocations;
    [SerializeField] SpawnNode spawnNode;
    [SerializeField] GameObject Object;
    private float circleOffset = 0.1f;

    [Header("Spawned Objects")]
    [SerializeField] List<GameObject> currentObjects;
    private Vector3 ObjectSpawnLocation;

    [Header("VR Properties")]
    public string defaultHand = WXRGestureHand.GetSingleHandGesture(true);


    void Update()
    {
        //if(HandManager.Instance.GetHandGesture(false) == HandManager.GestureType.ThumbUp)
        //{
        //    StartGameVR();
        //}    

        if (Input.GetKeyDown(KeyCode.S) /*&& currentObjects.Count < 3*/)
        {
            StartGameVR();
        }
    }

    public void StartGameVR()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < nodesToSpawn; i++)
        {
            Vector3 pos = RandomCircle(center, circleRadius);
            var newNode = Instantiate(spawnNode, pos, Quaternion.identity);
            spawnLocations.Add(newNode);
            circleOffset += 0.1f;
        }
        InvokeRepeating(nameof(spawnObjectLoop), 1f, 1.5f); // @Jack is this an okay use-case for InvokeRepeating? 
    }

    private void spawnObject()
    {
        int randomAttribute = Random.Range(0, objectsInCirculation.Length);
        if (GetRandomLocation(out int randomSpawnLocation))
        {
            var newObject = Instantiate(Object, spawnLocations[randomSpawnLocation].transform.position, Quaternion.identity);
            newObject.GetComponent<ObjectController>().ObjectAttributes = objectsInCirculation[randomAttribute];
            currentObjects.Add(newObject);
        }
    }

    private void spawnObjectLoop() // Should i just put all the spawnObject logic here? kinda seems redundant but didnt want to cluster things
    {
        if (currentObjects.Count == 3) return;
        else {
            spawnObject();
        }
    }


    #region Utils
    private Vector3 RandomCircle(Vector3 center, float radius)
    {
        float angle = circleOffset * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + 0.25f;
        pos.z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return pos;
    }

    private bool GetRandomLocation(out int randomSpawnLocation)
    {
        randomSpawnLocation = Random.Range(0, spawnLocations.Count);
        if (spawnLocations[randomSpawnLocation].currentlyOccupied == false)
        {
            return true;
        } else
        {
            return false;
        }
    }


    #endregion
}
