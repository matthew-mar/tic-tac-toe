using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Scripts;
using Players;

public class Field : MonoBehaviour {

	private int[,] _fieldMatrix = new int[3, 3];

	private string savePath;
	private string saveFileName = "data.json";

	private Player[] _players = new Player[] {new Cross(), new Circle()};
	private int _playerIndex = 0;

	private void Awake() {
		savePath = Path.Combine(Application.dataPath, saveFileName);
		this.GiveIdToCells();
		this.FillMatrix();
	}
	
	private void Update() {
		if (Input.GetKey(KeyCode.P)) {
			this.SaveToFile();
		}
	}

	private void SaveToFile() {

		int[] tmp_massiv = new int[9];
		for (int i = 0; i < 9; i++) {
			tmp_massiv[i] = this._fieldMatrix[i / 3, i % 3];
		}
  
		FieldMatrix fieldMatrix = new FieldMatrix {
			Massiv = tmp_massiv
		};

		string json = JsonUtility.ToJson(fieldMatrix, prettyPrint: true);
		try {
			File.WriteAllText(savePath, contents: json);
		}
		catch (Exception e) {
			print("Hello world");
		}
	}
	
	private void GiveIdToCells() {
		for (int i = 0; i < 9; i++) {
			this.transform.GetChild(i).GetComponent<CellButton>().SetCellId(i);
		}
	}

	private void FillMatrix() {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				this._fieldMatrix[i, j] = -1;
			}
		}
	}

	public int DrawOnField(int cellNumber) {
		int currentPlayer = this._players[this._playerIndex++ % 2].Draw();
		this._fieldMatrix[cellNumber / 3, cellNumber % 3] = currentPlayer;
		return currentPlayer;
	}
}
