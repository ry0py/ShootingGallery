using UnityEngine;
using TMPro;

public class OVRDebugManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){
            text.text = text.text + "PrimaryIndexTrigger";
        }
    }
}
