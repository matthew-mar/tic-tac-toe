using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellButton : MonoBehaviour {
	
	private int _cellId;
	public bool isGameOver = false;
	private bool _isFilled = false;
	
	public void Click() {
		if (!this.isGameOver) {
			if (!this._isFilled) {
				print(this._cellId);
				int currentPlayer = this.transform.parent.GetComponent<Field>().DrawOnField(this._cellId);
				this.transform.GetChild(currentPlayer).gameObject.SetActive(true);
				this._isFilled = true;
			}
		}
	}

	public void SetCellId(int id) {
		this._cellId = id;
	}
}
