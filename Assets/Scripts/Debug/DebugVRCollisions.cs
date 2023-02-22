using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugVRCollisions : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    public GameObject handPosition;
    
    void Update()
    {
        debugText.text = handPosition.transform.position.ToString();
    }
}
