using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// #REVIEW: @Christian - I'd like to change the structure of this class to be what's called 'Bottom -Up'.
// Let's discuss in detail once you read this.
public class ObjectController : MonoBehaviour
{
    public SO_Object ObjectAttributes;

    // Object references
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private UIPointCounter pointCounter;

    [Header("Properties")]
    [SerializeField] private float timeToDestroy = 3f;
    [SerializeField] private float uiCounterHeightOffset = 0.8f;

    [HideInInspector]public UnityEvent<float> onShrink = new UnityEvent<float>();
    [HideInInspector]public UnityEvent onCollected = new UnityEvent();

    private Vector3 CurrentPosition => transform.position;

    private void Start()
    {
        mesh.material.SetColor("_EmissionColor", ObjectAttributes.customColor);
    }
    void Update()
    {
        if (Input.GetKeyDown("space")) Collected();
    }

    private void Collected()
    {
        onShrink?.Invoke(timeToDestroy);
        onCollected?.Invoke();
        StartCoroutine(DestroyProcess_CR());
        Instantiate(ObjectAttributes.collectedVFX);
        UIPointCounter pointCounterGo = Instantiate(pointCounter, TargetVectorOffset(), Quaternion.identity);
        pointCounterGo.targetPoints = ObjectAttributes.pointsValue;
    }

   private IEnumerator DestroyProcess_CR()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }

    #region TOOLS

    private Vector3 TargetVectorOffset()
    {
        return new Vector3(CurrentPosition.x, CurrentPosition.y + uiCounterHeightOffset, CurrentPosition.z);
    }

    #endregion TOOLS
}