using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Essence.Hand.StaticGesture;
using Wave.Essence.Hand;
using System.Linq;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
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

    [Header("VR Properties")]
    public string defaultHand = WXRGestureHand.GetSingleHandGesture(true);
    private bool gameStarted = false;

    // UI Reference
    [SerializeField] UIScoreValue uiScore;

    [HideInInspector] public UnityEvent<int> updateObjectsUI = new UnityEvent<int>();

    void Update()
    {
        if (gameStarted == false)
        {
            if (HandManager.Instance.GetHandGesture(false) == HandManager.GestureType.ThumbUp)
            {
                StartGameVR();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGameVR();
        }
    }

    public void StartGameVR()
    {
        gameStarted = true;
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

            if (objectScript != null)
            {
                objectScript.onUpdateUI.AddListener(uiScore.GetScore);
            }


        }
        else return;
    }



    private void spawnObjectLoop() // Should i just put all the spawnObject logic here? kinda seems redundant but didnt want to cluster things
    {
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
