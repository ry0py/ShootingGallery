using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource bgmSource;
    [SerializeField] private float hurryTime = 15.0f;

    TimeManager timeManager;
    GameObject timeManagerObj = null;
    private void Start()
    {
        bgmSource = GetComponent<AudioSource>();
        timeManagerObj = GameObject.Find("TimeManager");
        timeManager = timeManagerObj.GetComponent<TimeManager>();
        bgmSource.pitch = 1.0f;
    }
    private void Update()
    {
        if(timeManager.TimeLimit <= hurryTime)
        {
            bgmSource.pitch = 1.5f;
        }
    }
}
