using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounterVisual : MonoBehaviour
{
    [SerializeField] private BaceCounter baceCounter;
    [SerializeField] private GameObject[] VisualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baceCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject visualGameObject in VisualGameObject)
        {
            visualGameObject.SetActive(true);
        }
       

    }
    private void Hide() {
        foreach (GameObject visualGameObject in VisualGameObject)
        {
            visualGameObject.SetActive(false);
        }
    }
}
