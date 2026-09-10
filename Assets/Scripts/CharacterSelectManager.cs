using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private string gameplayScene = "Gameplay";
    [SerializeField] private string mainMenuScene = "MainMenu";

    [Header("Character Panels")]
    [SerializeField] private GameObject panel_Char1;
    [SerializeField] private GameObject panel_Char2;

    [Header("Selected Highlight")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = new Color(1f, 0.9f, 0.3f);

    private int selectedCharacter = -1;

    public void SelectCharacter(int characterIndex)
    {
        selectedCharacter = characterIndex;
        GameData.SelectedCharacter = selectedCharacter;
        SceneManager.LoadScene(gameplayScene);
    }

    private void SetPanelHighlight(GameObject panel, bool isSelected)
    {
        Image img = panel.GetComponent<Image>();
        if (img != null)
            img.color = isSelected ? selectedColor : normalColor;
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}

public static class GameData
{
    public static int SelectedCharacter = 0;
    public static float HighScore = 0f;
    public static float CurrentScore = 0f;
    public static bool IsNewHighScore = false;
}