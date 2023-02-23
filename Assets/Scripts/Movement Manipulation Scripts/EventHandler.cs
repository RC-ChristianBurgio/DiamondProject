using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [HideInInspector] public Object objectController;

    public virtual void Start()
    {
        objectController = gameObject.GetComponent<Object>();
    }
}
