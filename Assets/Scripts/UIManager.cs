using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI yourScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenu;
    GameManager gm;


    void Start()
    {
        gm = GameManager.Instance;
        gm.OnGameOver.AddListener(ActivateGameOverUI);
    }


    public void PlayButtonHandler() {
        gm.StartGame();
    }


    private void OnGUI()
    {
        scoreUI.text = gm.Score();
    }

    public void ActivateGameOverUI() {
    
        gameOverUI.SetActive(true);

        yourScoreUI.text = "Рахунок: " + gm.Score();
        highScoreUI.text = "Рекорд: " + gm.HighScore();
    }



}
