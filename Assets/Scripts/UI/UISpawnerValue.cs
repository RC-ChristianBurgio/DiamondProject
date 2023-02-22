using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UISpawnerValue : MonoBehaviour
{
    [SerializeField] private ObjectSpawner spawner;
    [SerializeField] private TextMeshProUGUI valueText;
    private void Start()
    {
        if (spawner != null)
        {
            spawner.updateObjectsUI.AddListener(GetObjectCount);
        }
      
    }
    public void GetObjectCount(int count)
    {
        valueText.text = count.ToString();
    }

}
