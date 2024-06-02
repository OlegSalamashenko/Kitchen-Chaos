using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCountSound : MonoBehaviour
{
    [SerializeField] private StoveCount stoveCount;
    private AudioSource audioSource;


    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCount.OnStateChanged += StoveCount_OnStateChanged;
        stoveCount.OnProgressChanged += StoveCount_OnProgressChanged;
    }

    private void StoveCount_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = stoveCount.IsFried() && e.progressNormalized >= burnShowProgressAmount;

    }

    private void StoveCount_OnStateChanged(object sender, StoveCount.OnStatateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCount.State.Frying || e.state == StoveCount.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCount.transform.position);
            }
        }
        
    }
}
