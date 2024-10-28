using UnityEngine;

public class BrokenTarget : MonoBehaviour
{
    private Animator animator;
    private float timeToFadeout = 1.0f;
    private float time = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DestroyBrokenTarget()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>=timeToFadeout)
        {
            animator.SetTrigger("Fadeout");
        }
    }
}
