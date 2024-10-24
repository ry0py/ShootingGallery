using UnityEngine;

public class TargetManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 0.5f;
    private float countTime = 0.0f;
    private Rigidbody rb;
    // private int waveCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   MoveWave();
    }

    void MoveWave()
    {
        countTime += Time.deltaTime;
        while(countTime < 5.0f)
        {
            transform.position += new Vector3(-speed * Time.deltaTime,0, 0 );
            countTime += Time.deltaTime;
        }
        countTime = 0.0f;
        while(countTime < 5.0f)
        {
            transform.position += new Vector3(-speed * Time.deltaTime,0, 0);
            countTime += Time.deltaTime;
        }
        countTime = 0.0f;
    }
}
