using System;
using UnityEngine;


  class EmoteSoundPlayer : MonoBehaviour {
    AudioSource audioSource;
    [SerializeField]
    bool playSound = true;

    void Awake() {
      audioSource = GetComponent<AudioSource>();
     // audioSource.volume = SoundManager.GetVolume();
    }

    public void PlayEmoteSound() {
      if (!playSound) {
        return;
      }

      playSound = false;
      audioSource.Play();
    }
  }

