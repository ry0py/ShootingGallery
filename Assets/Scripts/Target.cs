using UnityEngine;

public class Target : MonoBehaviour
{
    // 横の移動幅
    public float amplitude = 5.0f;
    // 移動速度
    // public float speed = 2.0f;
    // public float cycleTime = 2.0f;
    // 初期位置を保存する変数
    private Vector3 startPosition;
    private float timeToDestroy = 2.0f;
    [SerializeField] private Transform brokenTargetPrefab;
    [SerializeField] private Transform burstEffectPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 初期位置を保存
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Mathf.PingPongを使用して、オブジェクトを横方向に行ったり来たりさせる
        float offset = Mathf.PingPong(Time.time, 2* amplitude)-amplitude; // sinでもよさそう
        transform.position = startPosition + new Vector3(offset, 0, 0);
    }
    void OnDestroy()
    {
        Transform brokenTarget = Instantiate(brokenTargetPrefab, transform.position, brokenTargetPrefab.rotation); // brokenTargetを生成
        Instantiate(burstEffectPrefab, transform.position, Quaternion.identity); // brokenTargetを生成
        // brokenTarget.localScale = transform.localScale; // brokenTargetのサイズをTargetと同じにする
        foreach(Rigidbody rb in brokenTarget.GetComponentsInChildren<Rigidbody>())
        {
            rb.AddExplosionForce(1000.0f, transform.position, 10.0f);
        }
        Destroy(gameObject);
    }
}
