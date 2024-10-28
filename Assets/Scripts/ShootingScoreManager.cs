using UnityEngine;
using TMPro;
public class ShootingScoreManager : MonoBehaviour
{
    [SerializeField] AudioClip hitSound;
    private AudioSource audioSource;
    private int score;
    public int Score{
        get{
            return score;
        }
        set{
            if(value >0){
                score = value;
            }
            else{
                score = 0;
            }
            UpdateScoreText();
        }
    }
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        audioSource = gameObject.GetComponent<AudioSource>();
        scoreText.text = "Score: " + score.ToString();
    }
    private void UpdateScoreText()
    {
        audioSource.PlayOneShot(hitSound);
        scoreText.text = "Score: " + score.ToString();

    }
    // void Update()
    // {
    //     scoreText.text = "Score: " + score.ToString();
    // }
}
