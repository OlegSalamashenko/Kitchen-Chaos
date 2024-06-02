using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDelivered;

    private void Start()
    {
        KitcheGameManager.Instance.OnStateChange += KitcheGameManager_OnStateChange;
        Hide();
    }

    private void KitcheGameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (KitcheGameManager.Instance.IsGameOver())
        {
            Show();

            recipesDelivered.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
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
