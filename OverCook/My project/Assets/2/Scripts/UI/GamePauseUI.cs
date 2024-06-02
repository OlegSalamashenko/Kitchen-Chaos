using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
   
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainManuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {

        
        resumeButton.onClick.AddListener(() =>
        {
            KitcheGameManager.Instance.TogglePauseGame();
        });
        mainManuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }
    private void Start()
    {
        KitcheGameManager.Instance.OnGamePaused += KitcheGameManager_OnGamePaused;
        KitcheGameManager.Instance.OnGameUnpaused += KitcheGameManager_OnGameUnpaused;
        Hide();
    }

    private void KitcheGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KitcheGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
