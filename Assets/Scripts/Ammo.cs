using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Ammo : MonoBehaviour
{
    ShootingScoreManager scoreManager;
    GameObject scoreManagerObj;

    private float timeToDestroy = 2.0f;

    void Awake()
    {
        // ShootingScoreManager scoreManager = FindObjectOfType<ShootingScoreManager>();
        scoreManagerObj = GameObject.Find("ScoreManager");
        scoreManager = scoreManagerObj.GetComponent<ShootingScoreManager>();
        Destroy(gameObject, timeToDestroy);
    }
    void OnCollisionEnter(Collision other)
    {
        // Targetの種類を増やすときはここに追加する
        if(other.gameObject.tag == "Target" || other.gameObject.CompareTag("Target"))
        {
            scoreManager.Score += 10;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

    }
    // private IEnumerator DestroyBullet(){
    //     yield return new WaitForSeconds(timeToDestroy);
    //     Destroy(gameObject);
    // }
}
