using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	[SerializeField] private Animator _mainMenuButtonsAnimator;
	[SerializeField] private Animator _fieldAnimator;
	[SerializeField] private Animator _gameCanvasAnimator;
	[SerializeField] private GameObject _gameMenuButtonsBackground;
	private Menu _menu;


	private void Start() {
		this._menu = this.GetComponent<Menu>();
	}

	private void StartGame() {
		this._menu.StartGame();
	}
	
	public void AnimationStart() {
		this._mainMenuButtonsAnimator.SetBool("isStartGame", true);
		Invoke("StartGame", 1f);
	}

	public void AnimationReload() {
		this._mainMenuButtonsAnimator.SetBool("isStartGame", false);
	}

	public void ShowField() {
		this.GameButtonsShow();
		this._fieldAnimator.SetBool("showField", true);
		
	}

	public void GameCanvasDown() {
		this._gameCanvasAnimator.gameObject.SetActive(true);
		Invoke("GameCanvasOff", 1f);
	}

	private void GameCanvasOff() {
		this._gameCanvasAnimator.gameObject.SetActive(false);
	}

	private void GameButtonsShow() {
		this._gameMenuButtonsBackground.SetActive(true);
		Invoke("GameButtonsBackgroundOff", 1f);
	}

	private void GameButtonsBackgroundOff() {
		this._gameMenuButtonsBackground.SetActive(false);
	}
}
