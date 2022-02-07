using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	[SerializeField] private Animator _mainMenuButtonsAnimator;
	[SerializeField] private Animator _fieldAnimator;
	[SerializeField] private Animator _gameCanvasAnimator;
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
		this._fieldAnimator.SetBool("showField", true);
	}

	public void GameCanvasDown() {
		this._gameCanvasAnimator.SetBool("downGameCanvas", true);
	}
}
