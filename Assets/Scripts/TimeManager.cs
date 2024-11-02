using System;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeLimit = 120f; //[s]
    private float initialTimer;
    public float TimeLimit{
        get{
            return timeLimit;
        }
        set{
            if(value > 0){
                timeLimit = value;
            }
            else{
                timeLimit = 0;
            }
        }
    }

    void Start(){
        initialTimer = timeLimit;
    }

    void Update(){
        timeLimit -= Time.deltaTime;
        if(timeLimit <= 0){
            timeLimit = 0;
        }
        var timeSpan = TimeSpan.FromSeconds(timeLimit);
        timerText.text = timeSpan.ToString("mm':'ss");
    }

    public void ResetTimer(){
        timeLimit = initialTimer;
    }
}
