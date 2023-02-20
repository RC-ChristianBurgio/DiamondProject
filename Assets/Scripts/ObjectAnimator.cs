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
        objectTransform = this.gameObject.transform;
    }

    void FixedUpdate()
    {
        objectTransform.position = objectTransform.position + new Vector3(0, Time.deltaTime * animationSpeed, 0);

        objectTransform.Rotate(0, rotationSpeed, 0);
        if (objectTransform.position.y <= positionLimits[0] || objectTransform.position.y >= positionLimits[1])
        {
            animationSpeed *= -1;
        }

    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

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
                Invoke("DestroyObject", 0.3f);
            }
            yield return null;
        }
    }

     
   
}
