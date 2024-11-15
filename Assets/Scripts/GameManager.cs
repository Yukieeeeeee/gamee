using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SaveScore score_;
    public float currentScore = 0f;
    public UnityEvent OnPlay = new();
    public UnityEvent OnGameOver = new();
    public bool isPlaying = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        string loadedData = SaveEverything.Load("Save");
        if (loadedData != null)
        {

            score_ = JsonUtility.FromJson<SaveScore>(loadedData);
        }
        else {
            score_ = new SaveScore();
        }
        
    }


        private void Update()
    {
        if (isPlaying) {
            currentScore += Time.deltaTime;
        }

    }

    public void StartGame() 
    {
         OnPlay.Invoke();
        isPlaying = true;
        currentScore = 0;
    }


    public void GameOver() {

        
        if (score_.highScore < currentScore) {
            score_.highScore = currentScore;
            string saveString = JsonUtility.ToJson(score_);
            SaveEverything.Save("Save", saveString);
        }
        isPlaying = false;
        OnGameOver.Invoke();

    }
    public string Score() {
        return Mathf.RoundToInt(currentScore).ToString();
    }
    public string HighScore()
    {
        return Mathf.RoundToInt(score_.highScore).ToString();
    }

}

