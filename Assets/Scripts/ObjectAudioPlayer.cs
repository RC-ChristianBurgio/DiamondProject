using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAudioPlayer : EventHandler
{
    public AudioSource audioSource;

    private void Start() 
    {
        objectController = gameObject.GetComponentInParent<ObjectController>(); // As this is on a seperate game object, we cant override the parent start function 
        audioSource = gameObject.GetComponent<AudioSource>();
        objectController.onCollected.AddListener(AudioEventTrigger);
    }

    private void OnDestroy()
    {
        objectController.onCollected.RemoveListener(AudioEventTrigger);
    }
    private void AudioEventTrigger()
    {
        audioSource.clip = objectController.ObjectAttributes.collectedSFX;
        audioSource.PlayOneShot(objectController.ObjectAttributes.collectedSFX);
    }
}
