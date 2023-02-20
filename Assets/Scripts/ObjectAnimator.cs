using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectAnimator : MonoBehaviour
{
    [SerializeField] private Transform objectTransform;
    [SerializeField] private float[] positionLimits;
    [SerializeField] private float animationSpeed;
    private float rotationSpeed = 3;
    private Vector3 localScale = new Vector3(0.6f, 0.6f, 0.6f);
    
    private void Start()
    {
        //#REVIEW: @Christian - as this is a MonoBehaviour we inherently have transform available.
        // No need to cache it in a variable.
        objectTransform = this.gameObject.transform;
    }

    // #REVIEW: @Christian - I would like to see you separate each of the pieces of functionality into their own components:
    // - Float
    // - Rotate
    // - Shrink
    // Let's discuss the Single Responsibility Principle when you read this.
    void FixedUpdate()
    {
        // #REVIEW: @Christian - Explore how to achieve this with a Sin wave
        objectTransform.position = objectTransform.position + new Vector3(0, Time.deltaTime * animationSpeed, 0);

        // #REVIEW: @Christian - Let's shift this to another script after our chat about SRP 
        objectTransform.Rotate(0, rotationSpeed, 0);
        
        //#REVIEW: @Christian -  This will become redundant after our chat about Sin waves and their implementation
        if (objectTransform.position.y <= positionLimits[0] || objectTransform.position.y >= positionLimits[1])
        {
            animationSpeed *= -1;
        }
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    // #REVIEW: Let's explore achieving this with a timer as opposed to evaluation of magnitude, I will also show you
    // how we can then use this with what's called animation curve to make it a bit more dynamic.
    public IEnumerator Shrink()
    {
        animationSpeed = 0;
        rotationSpeed = 0;
        while(objectTransform.localScale.magnitude > 0.01)
        {
            Vector3 newScale = Vector3.Lerp(objectTransform.localScale, new Vector3(0,0,0), Time.deltaTime);
            transform.localScale = newScale;
            if(objectTransform.localScale.magnitude < 0.11)
            {
                // #REVIEW: @Christian - No need to invoke with delay when we shift over to a time based coroutine 
                Invoke("DestroyObject", 0.3f);
            }
            
            // #REVIEW: @Christian - Opt for WaitForEndOfFrame() instead of returning null
            yield return null;
        }
    }

     
   
}
