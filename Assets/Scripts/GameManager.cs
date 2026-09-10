using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("References")]
    [SerializeField] private Transform player;

    private float startY;
    private float currentScore;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        startY = player.position.y;
        currentScore = 0f;
    }

    void Update()
    {
        if (isGameOver) return;

        float height = player.position.y - startY;
        if (height > currentScore)
        {
            currentScore = height;
            GameData.CurrentScore = currentScore;

            float savedHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
            GameData.HighScore = Mathf.Max(currentScore, savedHighScore);
        }

        float bottomOfCamera = Camera.main.transform.position.y - Camera.main.orthographicSize;
        if (player.position.y < bottomOfCamera)
            GameOver();
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        AudioManager.Instance.PlayGameOver();

        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        if (currentScore > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", currentScore);
            PlayerPrefs.Save();
            GameData.IsNewHighScore = true;
        }
        else
        {
            GameData.IsNewHighScore = false;
        }

        GameData.HighScore = PlayerPrefs.GetFloat("HighScore", 0f);
        UIManager.Instance.ShowGameOver();
    }

    public float GetScore() => currentScore;
}
