using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellButton : MonoBehaviour {

	[SerializeField] private Manager _gameManager;
	private Field _field;
	private int _cellId;  // номер клетки в матрице
	private bool _isEmpty = true;  // флаг пустой клетки


	private void Start() {
		// this._gameManager = FindObjectOfType<Manager>();
		this._field = this.transform.parent.GetComponent<Field>();
	}
	
	public void Click() { 
		/* Метод нажатия кнопки клетки */
		
		if (!this._gameManager.IsGameOver()) {  // если игра не окнчена
			if (this._isEmpty) {  // если клетка пустая
				int currentPlayer = this._gameManager.GetCurrentPlayer(inc: true);
				this.transform.GetChild(currentPlayer).gameObject.SetActive(true);  // активация на клетке рисунка, соответствующего номеру игрока
				this._field.DrawOnField(this._cellId, currentPlayer);
				this._isEmpty = false;  // клетка не пустая
			}
		}
	}

	public void SetCellId(int id) {
		/* Метод присваивает номер клетке */
		this._cellId = id;
	}

	public void ClearCell() {
		foreach (Transform child in this.transform) {
			child.gameObject.SetActive(false);
		}
		this._isEmpty = true;
	}
}
