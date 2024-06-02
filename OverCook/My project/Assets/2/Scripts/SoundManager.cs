using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsValume";
    public static SoundManager Instance { get; private set; }
    [SerializeField]  private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;
    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME,1f);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaceCounter.OnAnyObjectPlacedHere += BaceCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnEnyObjectTrashed += TrashCounter_OnEnyObjectTrashed;
    }

    private void TrashCounter_OnEnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlayeSound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaceCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaceCounter baceCounter = sender as BaceCounter;
        PlayeSound(audioClipRefsSO.objectDrop, baceCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlayeSound(audioClipRefsSO.objectPickup,Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlayeSound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCount deliveryCount = DeliveryCount.Instance;
        PlayeSound(audioClipRefsSO.deliveryFail, deliveryCount.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCount deliveryCount = DeliveryCount.Instance;
        PlayeSound(audioClipRefsSO.deliverySuccess, deliveryCount.transform.position);
    }
    private void PlayeSound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlayeSound(audioClipArray[Random.Range(0, audioClipArray.Length)],position,volume);  
    }

    private void PlayeSound(AudioClip audioClip, Vector3 position, float volumemultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,position,volumemultiplier * volume);
    }

    public void PlayFootStepsSound(Vector3 position,float volume)
    {
        PlayeSound(audioClipRefsSO.footstep,position,volume);
    }

    public void PlayContDownSound()
    {
        PlayeSound(audioClipRefsSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlayeSound(audioClipRefsSO.warning, position);
    }


    public void ChangeVolume() { 
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() 
    { 
        return volume; 
    }

}
