using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject helpPanel;
    public GameObject gameScene;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject snake;
    public GameObject food;
    public Button mainMenuButton;

	private bool isPlaying;
	private static int bestScore = 0;
    private int score;
    private SnakeBehavior snakeBehavior;
    private FoodBehavior foodBehavior;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		snakeBehavior = snake.GetComponent<SnakeBehavior>();
		foodBehavior = food.GetComponent<FoodBehavior>();
		Show(false, true, false);
        isPlaying = false;
        gameOverText.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MainMenuButton_Click()
    {
		PauseGame();
		Show(false, true, false);
	}

    public void StartGame()
    {
        Show(true, false, false);
        score = 0;
		scoreText.text = "Score: " + score;
		Time.timeScale = 1f;
        isPlaying = false;
        gameOverText.gameObject.SetActive(false);
    }

    public void HelpButton_Click()
    {
        Show(false, false, true);
    }

    public void BackToStartPanel()
    {
        Show(false, true, false);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

	public void EndGame()
	{
        gameOverText.gameObject.SetActive(true);
        bestScore = Mathf.Max(bestScore, score);
        bestScoreText.text = "Best: " + bestScore;
        Time.timeScale = 0f;
        StartCoroutine(DoWait());
	}

    private IEnumerator DoWait()
    {
        yield return new WaitForSecondsRealtime(2);
        ResetGame();
        StartGame();
    }

    private void ResetGame()
    {
        snakeBehavior.Reset();
        foodBehavior.RandomizePosition();
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void SetIsPlaying(bool value)
    {
        isPlaying = value;
    }

    private void Show(bool showGameScene, bool showStartPanel, bool showHelpPanel)
    {
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		gameScene.SetActive(showGameScene);
		startPanel.SetActive(showStartPanel);
        helpPanel.SetActive(showHelpPanel);

        if (!showGameScene)
        {
            snakeBehavior.HiddenSnake();
            mainMenuButton.gameObject.SetActive(false);
        }
        else
        {
            snakeBehavior.ShowSnake();
            mainMenuButton.gameObject.SetActive(true);
        }
	}

	public void QuitButton_Click()
	{
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
