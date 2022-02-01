using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellButton : MonoBehaviour {
	
	private int _cellId;  // номер клетки в матрице
	public bool isGameOver = false;  // флаг оконченой игры
	private bool _isEmpty = true;  // флаг пустой клетки
	
	public void Click() { 
		/* Метод нажатия кнопки клетки */
		
		if (!this.isGameOver) {  // если игра не окнчена
			if (this._isEmpty) {  // если клетка пустая
				// print(this._cellId);
				int currentPlayer = this.transform.parent.GetComponent<Field>().DrawOnField(this._cellId);  // получение номера текущего игрока и отметка клетки в матрице
				this.transform.GetChild(currentPlayer).gameObject.SetActive(true);  // активация на клетке рисунка, соответствующего номеру игрока
				this._isEmpty = false;  // клетка не пустая
			}
		}
	}

	public void SetCellId(int id) {
		/* Метод присваивает номер клетке */
		this._cellId = id;
	}
}
