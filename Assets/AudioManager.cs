using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioSource _backSource;
    [SerializeField] private AudioSource _effectSource;
    
    [SerializeField] private AudioClip _pickUpMoney;
    
    public void PlayBackSound(AudioClip clip) {
        _backSource.clip = clip;
        _backSource.Play();
    }

    public void StopBackSound() {
        _backSource.Stop();
    }

    public void PlayPickUpEffect() {
        if(_pickUpMoney == null) return;

        _effectSource.clip = _pickUpMoney;
        _effectSource.Play();
    }
}
