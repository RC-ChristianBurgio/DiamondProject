using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Object", order = 1)]
public class SO_Object : ScriptableObject
{
    public Color customColor;
    public Material material;
    public int pointsValue;
    public GameObject collectedVFX;
    public AudioClip collectedSFX;
    public Mesh mesh;

    // Add a mesh & material here in the future which will be set onto the object (diamond, gold, rings etc)

}
