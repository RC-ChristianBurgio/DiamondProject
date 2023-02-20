using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #REVIEW: @Christian - I'd like to change the structure of this class to be what's called 'Bottom -Up'.
// Let's discuss in detail once you read this.
public class ObjectController : MonoBehaviour
{
    public SO_Object ObjectAttributes;

    // Object references
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private int points; 
    
    [SerializeField] private ObjectAnimator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private UIPointCounter pointCounter;
    
    private void Start()
    {
        mesh.material.SetColor("_EmissionColor", ObjectAttributes.customColor);
        points = ObjectAttributes.pointsValue;
    }
    void Update()
    {
        if (Input.GetKeyDown("space")) // alternatively, use the unity event system and input system
        {
            StartCoroutine(animator.Shrink());
            loadAndPlayAudioClip();
            Instantiate(ObjectAttributes.collectedVFX);
            UIPointCounter p = Instantiate(pointCounter, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.8f, 
            gameObject.transform.position.z), Quaternion.identity); // Surely theres a better way to pass through values??
            p.targetPoints = ObjectAttributes.pointsValue;

        } 
    }

    private void loadAndPlayAudioClip()
    {
        audioSource.clip = ObjectAttributes.collectedSFX;
        audioSource.PlayOneShot(ObjectAttributes.collectedSFX);
    }
}