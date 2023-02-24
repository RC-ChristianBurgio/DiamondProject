using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenCollected : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var objectMono = GetComponent<Object>();
        if (objectMono == null)
            return;


        objectMono.onCollected.AddListener(DestroyObjectFromServer);
    }

    private void DestroyObjectFromServer()
    {
        // Check if on server
        // Trigger mirror destruction
        NetworkServer.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
