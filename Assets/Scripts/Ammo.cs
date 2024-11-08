using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Ammo : MonoBehaviour
{
    ShootingScoreManager scoreManager;
    GameObject scoreManagerObj = null;

    private float timeToDestroy = 2.0f;

    void Start()
    {
        // ShootingScoreManager scoreManager = FindObjectOfType<ShootingScoreManager>();
        scoreManagerObj = GameObject.Find("ScoreManager");
        scoreManager = scoreManagerObj.GetComponent<ShootingScoreManager>();
        Destroy(gameObject, timeToDestroy);
    }
    void OnCollisionEnter(Collision other)
    {
        if(scoreManager == null)
        {
            scoreManagerObj = GameObject.Find("ScoreManager");
            scoreManager = scoreManagerObj.GetComponent<ShootingScoreManager>();
        }
        // Targetの種類を増やすときはここに追加する
        if(other.gameObject.CompareTag("NormalTarget"))
        {
            scoreManager.Score += 10;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("RainbowTarget"))
        {
            scoreManager.Score += 50;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
    // private IEnumerator DestroyBullet(){
    //     yield return new WaitForSeconds(timeToDestroy);
    //     Destroy(gameObject);
    // }
}
