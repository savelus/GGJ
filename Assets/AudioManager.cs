using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioSource _backSource;
    [SerializeField] private AudioSource _effectSource;
    
    [SerializeField] private AudioClip _pickUpMoney;
    [SerializeField] private AudioClip _changeDirection;
    [SerializeField] private AudioClip _mainTheme;
    
    public void PlayBackSound(AudioClip clip) {
        _backSource.clip = clip;
        _backSource.Play();
    }

    public void StopBackSound() {
        _backSource.Stop();

        _backSource.clip = _mainTheme;
        _backSource.Play();
    }

    public void PickUpEffect() => PlayEffect(_pickUpMoney);
    public void ChangeDirectionEffect() => PlayEffect(_changeDirection);

    private void PlayEffect(AudioClip clip) {
        if(clip == null) return;

        _effectSource.clip = clip;
        _effectSource.Play();
    }
}
