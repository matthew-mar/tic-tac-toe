using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	[SerializeField] private GameObject _menuCanvas;
	[SerializeField] private GameObject _gameCanvas;

	public void StartGame() {
		this._menuCanvas.SetActive(false);  // выключение канваса меню
		this._gameCanvas.SetActive(true);  // включение канваса игры
		this.GetComponent<Manager>().StartGame();
	}

	public void Exit() {
		Application.Quit();
	}
}
