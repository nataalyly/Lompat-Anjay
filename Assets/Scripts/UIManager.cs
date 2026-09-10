using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtHighScore;
    [SerializeField] private GameObject pauseButton;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI txtFinalScore;
    [SerializeField] private TextMeshProUGUI txtFinalHighScore;
    [SerializeField] private TextMeshProUGUI txtNewHighScore;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        panelGameOver.SetActive(false);
        txtNewHighScore.gameObject.SetActive(false);

        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        txtHighScore.text = "Best: " + Mathf.FloorToInt(highScore).ToString();
    }

    void Update()
    {
        txtScore.text = "Score: " + Mathf.FloorToInt(GameData.CurrentScore).ToString();
        txtHighScore.text = "Best: " + Mathf.FloorToInt(GameData.HighScore).ToString();
    }

    public void ShowGameOver()
    {
        HideHUD();
        Time.timeScale = 0f;
        panelGameOver.SetActive(true);

        txtFinalScore.text = "Score: " + Mathf.FloorToInt(GameData.CurrentScore).ToString();
        txtFinalHighScore.text = "Best: " + Mathf.FloorToInt(GameData.HighScore).ToString();

        if (GameData.IsNewHighScore)
            txtNewHighScore.gameObject.SetActive(true);
    }

    public void HideHUD()
    {
        txtScore.gameObject.SetActive(false);
        txtHighScore.gameObject.SetActive(false);
        if (pauseButton != null)
            pauseButton.SetActive(false);
    }

    public void ShowHUD()
    {
        txtScore.gameObject.SetActive(true);
        txtHighScore.gameObject.SetActive(true);
        if (pauseButton != null)
            pauseButton.SetActive(true);
    }
}