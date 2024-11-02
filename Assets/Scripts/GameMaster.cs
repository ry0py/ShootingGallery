using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject[] Managers;

    [SerializeField] private GameObject[] UIs;
    [SerializeField] private UnityEvent finishEvent;
    [SerializeField] private TextMeshProUGUI[] rankingText = new TextMeshProUGUI[3];
    private GameObject timeManager;
    private GameObject scoreManager;
    private TimeManager timeManagerScript;
    private ShootingScoreManager scoreManagerScript;
    private bool isFinish = false;
    private bool isPlaying = false;
    string[] ranking = { "aランキング1位", "aランキング2位", "aランキング3位"};
    int[] rankingValue = new int[3];
    private int score;
    void Start()
    {
        // rankingValue ={0,0,0};
        foreach (GameObject manager in Managers)
        {
            manager.SetActive(false);
            if(manager.name == "TimeManager")
            {
                timeManager = manager;
            }
            if(manager.name ==  "ScoreManager")
            {
                scoreManager = manager; //本当はこの中のプロパティをstaticにして、こんな変なことしなくてもいいようにすべき？
            }
        }
        timeManagerScript = timeManager.GetComponent<TimeManager>();
        scoreManagerScript = scoreManager.GetComponent<ShootingScoreManager>();
    }
    void Update()
    {
        if(!isPlaying)
        {
            if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                ActiveManager();
                isPlaying = true;
            }
        }
        if(!isFinish){
            // if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            // {
            //     ResetGame();
            // }
            if(timeManagerScript.TimeLimit <= 0){
                OnGameFinish();
                isFinish = true;
            }
        }
    }
    public void ActiveManager(){
        foreach (GameObject manager in Managers)
        {
            manager.SetActive(true);
        }
        foreach (GameObject UI in UIs)
        {
            UI.SetActive(true);
            if(UI.name == "ResultCanvas")
            {
                UI.SetActive(false);
            }
            if(UI.name == "GameStartCanvas")
            {
                UI.SetActive(false);
            }
        }
        Gun.SetGameStart();
    }

    private void OnGameFinish()
    {
        foreach (GameObject UI in UIs)
            {
                UI.SetActive(false);
                if(UI.name == "ResultCanvas")
                {
                    UI.SetActive(true);
                }
            }
            foreach (GameObject manager in Managers)
            {
                manager.SetActive(false);
            }
        finishEvent?.Invoke();
        GetPrevRanking();

		SetNewRanking(scoreManagerScript.Score);

		for (int i = 0; i < rankingText.Length; i++)
		{
			rankingText[i].text = rankingValue[i].ToString();
		}
        PlayerPrefs.Save();
    }
    private void GetPrevRanking()
    {
        for(int i = 0; i < 3; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }
    // なんかコードが変
    private void SetNewRanking(int score)
    {
        for (int i = 0; i < ranking.Length; i++)
		{
				//取得した値とRankingの値を比較して入れ替え
				if (score>rankingValue[i])
				{
                    var change = rankingValue[i];
					rankingValue[i] = score;
                    score = change; // 引数をそのまま変更してるけどいいのか？
				}
		}
        for(int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }

    private void ResetGame()
    {
        foreach (GameObject UI in UIs)
        {
            UI.SetActive(false);
            if(UI.name == "GameStartCanvas")
            {
                UI.SetActive(true);
            }
        }
        timeManagerScript.ResetTimer();
        scoreManagerScript.ResetScore();
        isFinish = false;
        isPlaying = false;
    }
}

