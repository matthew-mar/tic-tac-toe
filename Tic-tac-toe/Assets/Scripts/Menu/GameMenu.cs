using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

	[SerializeField] private GameObject _gameCanvas;
	[SerializeField] private GameObject _menuCanvas;

	[SerializeField] private Manager _gameManager;
	
	
	public void ToMenu() {
		this._gameCanvas.SetActive(false);
		this._menuCanvas.SetActive(true);
	}

	public void Reset() {
		this._gameManager.StartGame();
	}
}
