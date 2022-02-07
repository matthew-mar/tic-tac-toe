using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winner : MonoBehaviour {

	[SerializeField] private GameObject _winnerCanvas;
	[SerializeField] private Text _winner;
	private SoundManager _soundManager;


	private void Start() {
		this._soundManager = this.GetComponent<SoundManager>();
	}

	public void SetWinner(string winner) {
		if (!this._winnerCanvas.activeSelf) {
			this._soundManager.WinSoundPlay(draw: winner == "Draw");
		}
		this._winnerCanvas.SetActive(true);
		this._winner.text = winner;
	}

	public void Hide() {
		this._winnerCanvas.SetActive(false);
	}
}
