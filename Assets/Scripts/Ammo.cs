using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    ShootingScoreManager scoreManager;
    private float timeToDestroy = 2.0f;
    void Start()
    {
        ShootingScoreManager scoreManager = FindObjectOfType<ShootingScoreManager>(); ;
        // Destroy(gameObject, timeToDestroy);
    }
    private void OnCollisionEnter(Collision other)
    {
        // Targetの種類を増やすときはここに追加する
        if(other.gameObject.tag == "Target")
        {
            Destroy(gameObject);
            scoreManager.Score += 10;
        }
    }
    // private IEnumerator DestroyBullet(){
    //     yield return new WaitForSeconds(timeToDestroy);
    //     Destroy(gameObject);
    // }
}
