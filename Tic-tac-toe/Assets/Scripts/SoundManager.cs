using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClip[] _playersSounds = new AudioClip[2];
    [SerializeField] private AudioClip _menuSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _drawSound;

    private Manager _gameManager;
    private AudioSource _audioSource;


    private void Start() {
        this._gameManager = this.GetComponent<Manager>();
        this._audioSource = this.GetComponent<AudioSource>();
    }

    public void PlayerSoundPlay() {
        int playerIndex = this._gameManager.GetCurrentPlayer(inc: false);
        this._audioSource.PlayOneShot(this._playersSounds[playerIndex]);
    }

    public void MenuSoundPlay() {
        this._audioSource.PlayOneShot(this._menuSound);
    }

    public void WinSoundPlay(bool draw) {
        if (draw) {
            this._audioSource.PlayOneShot(this._drawSound);
        } else {
            this._audioSource.PlayOneShot(this._winSound);   
        }
    }
}