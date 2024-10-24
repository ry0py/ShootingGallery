using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingSceneManager : MonoBehaviour
{
    private float holdTime = 0.0f;
    public float resetTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(OVRInput.GetDown(OVRInput.Button.One)){
        //     holdTime += Time.deltaTime;
        //     if(holdTime >= resetTime){
        //         ResetScene();
        //     }
        // }
    }
    // シーンをリセットするメソッド
    void ResetScene()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
}
