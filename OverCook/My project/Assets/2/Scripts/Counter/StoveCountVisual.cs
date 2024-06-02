using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCountVisual : MonoBehaviour
{
    [SerializeField] private StoveCount stoveCounter;
    [SerializeField] private GameObject storeOnHameObgect;
    [SerializeField] private GameObject particlesHameObgect;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCount.OnStatateChangedEventArgs e)
    {
        bool showVisual = e.state == StoveCount.State.Frying || e.state == StoveCount.State.Fried;
        storeOnHameObgect.SetActive(showVisual);
        particlesHameObgect.SetActive(showVisual);
    }
}
