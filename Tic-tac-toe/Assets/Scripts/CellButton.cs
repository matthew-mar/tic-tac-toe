using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellButton : MonoBehaviour {
	
	private int _cellId;
	
	public void Click() {
		print(this._cellId);
		int currentPlayer = this.transform.parent.GetComponent<Field>().DrawOnField(this._cellId);
		this.transform.GetChild(currentPlayer).gameObject.SetActive(true);
	}

	public void SetCellId(int id) {
		this._cellId = id;
	}
}
