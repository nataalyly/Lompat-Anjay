using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayButton();
        });
    }
}