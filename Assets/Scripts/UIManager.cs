using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject titleScreenPanel;
    public GameObject inGameHudPanel;
    public GameObject endingPanel;

    [Header("In-Game HUD")]
    public Text floorText;

    [Header("Fade Screen")]
    public Image fadeScreen;

    void Start()
    {
        // GameManagerの状態に基づいてUIを初期化
        if (GameManager.Instance != null)
        {
            switch (GameManager.Instance.currentState)
            {
                case GameManager.GameState.Title:
                    ShowTitleScreen();
                    break;
                case GameManager.GameState.Playing:
                    ShowInGameHUD();
                    break;
                case GameManager.GameState.Ending:
                    ShowEndingScreen();
                    break;
            }
        }
    }

    public void ShowTitleScreen()
    {
        titleScreenPanel.SetActive(true);
        inGameHudPanel.SetActive(false);
        endingPanel.SetActive(false);
    }

    public void ShowInGameHUD()
    {
        titleScreenPanel.SetActive(false);
        inGameHudPanel.SetActive(true);
        endingPanel.SetActive(false);
    }

    public void ShowEndingScreen()
    {
        titleScreenPanel.SetActive(false);
        inGameHudPanel.SetActive(false);
        endingPanel.SetActive(true);
    }

    public void UpdateFloorText(int floor)
    {
        if (floor < 0)
        {
            floorText.text = "B" + (-floor) + "F";
        }
        else
        {
            floorText.text = floor + "F";
        }
    }

    // --- Button Clicks ---
    public void OnStartButtonClick()
    {
        GameManager.Instance.currentState = GameManager.GameState.Playing;
        ShowInGameHUD();
        // ここでゲームのメインシーンに遷移するなどの処理
    }

    public void FadeToBlack(float duration)
    {
        if (fadeScreen != null)
        {
            fadeScreen.color = new Color(0, 0, 0, 0);
            fadeScreen.gameObject.SetActive(true);
            fadeScreen.CrossFadeAlpha(1, duration, false);
        }
    }
}
