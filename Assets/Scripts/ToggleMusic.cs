using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] Image musicIconContainer;
    [SerializeField] Sprite musicOnSprite;
    [SerializeField] Sprite musicOffSprite;

    private void Start()
    {
        var mute = musicAudioSource.mute;
        musicIconContainer.sprite = mute ? musicOffSprite : musicOnSprite;
    }

    public void ToggleMusicMute()
    {
        var mute = !musicAudioSource.mute;
        musicAudioSource.mute = mute;
        
        musicIconContainer.sprite = mute ? musicOffSprite : musicOnSprite;
    }
}
