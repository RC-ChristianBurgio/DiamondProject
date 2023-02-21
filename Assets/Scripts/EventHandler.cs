using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    [HideInInspector] public ObjectController objectController;

    public virtual void Start()
    {
        objectController = gameObject.GetComponent<ObjectController>();
    }

    // Removed the initialization of the variable for ObjectTransform

    // #REVIEW: @Christian - I would like to see you separate each of the pieces of functionality into their own components:
    // - Float
    // - Rotate
    // - Shrink
    // Let's discuss the Single Responsibility Principle when you read this.

    // Removed floating movement from here into a sine script

    // Removed rotation movement from here into a seperate script


    // Removed Destroy Object function

    // #REVIEW: Let's explore achieving this with a timer as opposed to evaluation of magnitude, I will also show you
    // how we can then use this with what's called animation curve to make it a bit more dynamic.

}
