using UnityEngine;
using TMPro;
public class ShootingScoreManager : MonoBehaviour
{
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
            // UpdateScoreText();
        }
    }
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
