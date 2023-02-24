using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Wave.Essence.Hand;
[RequireComponent(typeof(MeshRenderer))] // This forces unity to put this component on
public class Object : MonoBehaviour
{
    public SO_Object ObjectAttributes;

    // Object references
    private MeshRenderer meshRenderer;
    private MeshFilter meshAsset;
    [SerializeField] private UIPointCounter pointCounter;
    [HideInInspector] public SpawnNode spawnNode;
    [HideInInspector] public ObjectSpawner masterSpawner;

    [Header("Properties")]
    [SerializeField] private float expiryCountdown;
    [SerializeField] private float blinkStart = 3;
    [SerializeField] private float collectedTimeSequence = 3f;
    [SerializeField] private float uiCounterHeightOffset = 0.8f;
    private bool collected = false;

    [HideInInspector]public UnityEvent<float> onShrink = new UnityEvent<float>();
    [HideInInspector]public UnityEvent onCollected = new UnityEvent();
    [HideInInspector]public UnityEvent onExpiry = new UnityEvent();
    [HideInInspector]public UnityEvent<int> onUpdateUI = new UnityEvent<int>();


    private Vector3 CurrentPosition => transform.position;

    private void Start()
    {
        //meshAsset = GetComponent<MeshFilter>();
        //meshRenderer = GetComponent<MeshRenderer>();
        //meshRenderer.material = ObjectAttributes.material;
        //meshRenderer.material.SetColor("_EmissionColor", ObjectAttributes.customColor);
        //meshAsset.mesh = ObjectAttributes.mesh;
      
    }

    private void Awake()
    {
        StartCoroutine(ObjectExpiryCountdown());

    }

    private void OnTriggerEnter(Collider hands) // Could use tags, get component or find by name here...
    {
        if (hands.tag == "Hands" && collected == false) // Marshal suggested using an interface
        {
            Collected();
        }
    }


    private IEnumerator ObjectExpiryCountdown()
    {
        onShrink?.Invoke(expiryCountdown);
        // Create a script that will make the mesh/material blink
        yield return new WaitForSeconds(expiryCountdown - blinkStart);
        onExpiry?.Invoke();
        yield return new WaitForSeconds(blinkStart);
        StartCoroutine(DestroyProcess_CR());
    }

    private void Collected()
    {
        collected = true;


        StopCoroutine(ObjectExpiryCountdown()); // Not sure if i have to even stop this? 
        
        // Fire animations
        onShrink?.Invoke(collectedTimeSequence);
        onCollected?.Invoke();
        
        // Start destruction 
        //StartCoroutine(DestroyProcess_CR());

        // Spawn Particle FX
        Instantiate(ObjectAttributes.collectedVFX, gameObject.transform);
        
        // UI
        UIPointCounter pointCounterGo = Instantiate(pointCounter, TargetVectorOffset(), Quaternion.LookRotation(TargetVectorOffset() - Camera.main.transform.position).normalized);
        pointCounterGo.targetPoints = ObjectAttributes.pointsValue;

        onUpdateUI?.Invoke(ObjectAttributes.pointsValue);

    }

    private IEnumerator DestroyProcess_CR()
    {
        yield return new WaitForSeconds(collectedTimeSequence);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {

        masterSpawner.currentObjects.Remove(gameObject);
        spawnNode.currentlyOccupied = false;
    }

    #region TOOLS

    private Vector3 TargetVectorOffset()
    {
        return new Vector3(CurrentPosition.x, CurrentPosition.y + uiCounterHeightOffset, CurrentPosition.z);
    }

    

    #endregion TOOLS
}