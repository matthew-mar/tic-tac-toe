using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Scripts;
using Players;
using UnityEditor;

public class Field : MonoBehaviour {

	private int[,] _fieldMatrix = new int[3, 3]; // матрица поля

	private string _savePath; // путь для сохранения матрицы в json
	private string saveFileName = "data.json"; // имя файла json для сохранения матрицы

	private Player[] _players = new Player[] {new Cross(), new Circle()}; // игроки
	private int _playerIndex = 0; // индекс текущего игрока

	private List<string> _crossWinCombinations = new List<string>() {
		"0-1-1-10-1-1-10",
		"-1-10-10-10-1-1",
		"000-1-1-1-1-1-1",
		"-1-1-1000-1-1-1",
		"-1-1-1-1-1-1000",
		"0-1-10-1-10-1-1",
		"-10-1-10-1-10-1",
		"-1-10-1-10-1-10"
	};
	
	private List<string> _circleWinCombinations = new List<string>() {
		"1-1-1-11-1-1-11",
		"-1-11-11-11-1-1",
		"111-1-1-1-1-1-1",
		"-1-1-1111-1-1-1",
		"-1-1-1-1-1-1111",
		"1-1-11-1-11-1-1",
		"-11-1-11-1-11-1",
		"-1-11-1-11-1-11"
	};

	private void Awake() {
		this._savePath = Path.Combine(Application.dataPath, saveFileName); // определение пути для сохранения матрицы
		this.GiveIdToCells(); // присвоение номера каждой клетке
		this.FillMatrix(); // заполнение матрицы -1
	}

	private void Update() {
		// print(this.IsWinner());

		if (this.IsMatrixFilled()) {
			print("Draw");
			this.GameOver();
		} else if (this.IsWinner(1, this._crossWinCombinations)) {
			print("Cross won");
			this.GameOver();
		} else if (this.IsWinner(0, this._circleWinCombinations)) {
			print("Circle won");
			this.GameOver();
		}

		if (Input.GetKey(KeyCode.P)) {
			this.SaveToFile(); // сохранение в файл текущего состояния матрицы
		}
	}

	private void SaveToFile() {
		/* метод сохранения текущего состояния матрицы в файл */

		int[] tmpMassiv = new int[9]; // временный одномерный массив
		for (int i = 0; i < 9; i++) {
			// добавление матрицы в одномерный массив
			tmpMassiv[i] = this._fieldMatrix[i / 3, i % 3];
		}

		FieldMatrix fieldMatrix = new FieldMatrix {
			Massiv = tmpMassiv // инициализация поля стркутуры
		};

		string json = JsonUtility.ToJson(fieldMatrix, prettyPrint: true); // сохранение структуры в json
		try {
			File.WriteAllText(this._savePath, contents: json); // сохрание json в файл
		}
		catch (Exception e) {
			{
				/* pass */
			}
		}
	}

	private void GiveIdToCells() {
		for (int i = 0; i < 9; i++) {
			// каждому предку поля присваивается номер i
			this.transform.GetChild(i).GetComponent<CellButton>().SetCellId(i);
		}
	}

	private void FillMatrix() {
		/* начальное заполнение матрицы */

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				this._fieldMatrix[i, j] = -1;
			}
		}
	}

	public int DrawOnField(int cellNumber) {
		/* рисование на поле, аргумент - номер дочерней клетки */

		print(cellNumber);
		
		int currentPlayer = this._players[this._playerIndex++ % 2].Draw(); // получение числа текущего игрока/переход к следующему игроку
		this._fieldMatrix[cellNumber / 3, cellNumber % 3] = currentPlayer; // занесение в матрицу числа текущего игрока по номеру переданной клетки
		return currentPlayer; // возвращение числа текущего игрока
	}

	private void GameOver() {
		foreach (Transform child in this.transform) {
			child.GetComponent<CellButton>().isGameOver = true;
		}
	}

	private bool IsMatrixFilled() {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (this._fieldMatrix[i, j] == -1) {
					return false;
				}
			}
		}
		return true;
	}

	private bool IsWinner(int enemyNumber, List<string> winCombinations) {
		string vector = "";
		for (int i = 0; i < 9; i++) {
			int n = this._fieldMatrix[i / 3, i % 3];
			if (n == enemyNumber) {
				n = -1;
			}
			vector += $"{n}";
		}
		return winCombinations.Contains(vector);
	}
}