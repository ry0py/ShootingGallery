using UnityEngine;
using TMPro;
public class ShootingScoreManager : MonoBehaviour
{
    [SerializeField] AudioClip hitSound;
    private AudioSource audioSource;
    private int score;
    public int Score{ // ここをstaticにしないのは、複数のスコアを管理するため、UpdateScoreText含めすべてをstaticにする必要があり、それは避けたいため
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

    public void ResetScore()
    {
        score = 0;
    }
    // void Update()
    // {
    //     scoreText.text = "Score: " + score.ToString();
    // }
}
