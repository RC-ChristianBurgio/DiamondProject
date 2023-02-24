using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Essence.Hand.StaticGesture;
using Wave.Essence.Hand;
using System.Linq;
using UnityEngine.Events;
using Mirror;

public class ObjectSpawner : NetworkBehaviour
{
    [Header("Spawn Information")]
    [SerializeField] SO_Object[] objectVariations;
    [SerializeField] List<SpawnNode> spawnLocations;
    [SerializeField] SpawnNode spawnNode;
    [SerializeField] GameObject Object;

    [Header("Spawning Properties")]
    [SerializeField] int nodesToSpawn;
    [SerializeField] float circleRadius;
    private float circleOffset = 0.1f;
    [SerializeField] float spawnDelay;
    [SerializeField] int spawnCap;

    [Header("Spawned Objects")]
    public List<GameObject> currentObjects;

    // Game object reference
    [SerializeField] private StartMatchService matchStarter;

    // UI Reference
    [SerializeField] UIScoreValue uiScore;

    [HideInInspector] public UnityEvent<int> updateObjectsUI = new UnityEvent<int>();

    private void Start()
    {
        NetworkClient.RegisterPrefab(Object);

        if (matchStarter)
            matchStarter.spawnerStart.AddListener(GenerateSpawnNodes);
    }

    public void GenerateSpawnNodes()
    {
        if (!isServer) return;

        Vector3 center = transform.position;
            for (int i = 0; i < nodesToSpawn; i++)
            {
                Vector3 pos = RandomCircle(center, circleRadius);
                var newNode = Instantiate(spawnNode, pos, Quaternion.identity);
                spawnLocations.Add(newNode);
                circleOffset += 0.1f;
            }
        InvokeRepeating(nameof(spawnObjectLoop), 1f, spawnDelay); // @Jack is this an okay use-case for InvokeRepeating? 
    }

    private void spawnObject()
    {
        if (!isServer) return;
        
        int randomAttribute = Random.Range(0, objectVariations.Length);
        if (GetRandomLocation(out int randomSpawnLocation))
        {
            var newObject = Instantiate(Object, spawnLocations[randomSpawnLocation].transform.position, Quaternion.identity);
            newObject.GetComponent<Object>().ObjectAttributes = objectVariations[randomAttribute];
            currentObjects.Add(newObject);


            var objectScript = newObject.GetComponent<Object>();
            objectScript.spawnNode = spawnLocations[randomSpawnLocation];
            objectScript.masterSpawner = this;
            spawnLocations[randomSpawnLocation].currentlyOccupied = true;
            
            // UI
            updateObjectsUI?.Invoke(currentObjects.Count);

            if (objectScript)
                objectScript.onUpdateUI.AddListener(uiScore.GetScore);
            NetworkServer.Spawn(newObject);


        }
    }


    private void spawnObjectLoop() // Should i just put all the spawnObject logic here? kinda seems redundant but didnt want to cluster things
    {
        if (!isServer) return;

        if (currentObjects.Count >= spawnCap) {
            Debug.Log("List is full");
                return;
        }
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
