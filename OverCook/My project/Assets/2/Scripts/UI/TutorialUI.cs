using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyUp;
    [SerializeField] private TextMeshProUGUI keyDown;
    [SerializeField] private TextMeshProUGUI keyLeft;
    [SerializeField] private TextMeshProUGUI keyRight;
    [SerializeField] private TextMeshProUGUI keyInteract;
    [SerializeField] private TextMeshProUGUI keyInteractAlt;
    [SerializeField] private TextMeshProUGUI keyPause;

    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        KitcheGameManager.Instance.OnStateChange += KitcheGameManager_OnStateChange;

        UpdateVisual();

        Show();
    }

    private void KitcheGameManager_OnStateChange(object sender, System.EventArgs e)
    {
        if (KitcheGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual() {
        keyUp.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyDown.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyLeft.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyRight.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteract.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAlt.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternat);
        keyPause.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
       gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
