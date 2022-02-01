using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Scripts;
using Players;
using UnityEditor;

public class Field : MonoBehaviour {
	
	// данные для дебага
	// private string _savePath;  // путь для сохранения матрицы в json
	// private string saveFileName = "data.json";  // имя файла json для сохранения матрицы
	// private void SaveToFile() {
	// 	/* метод сохранения текущего состояния матрицы в файл */
	//
	// 	int[] tmpMassiv = new int[9]; // временный одномерный массив
	// 	for (int i = 0; i < 9; i++) {
	// 		// добавление матрицы в одномерный массив
	// 		tmpMassiv[i] = this._fieldMatrix[i / 3, i % 3];
	// 	}
	//
	// 	FieldMatrix fieldMatrix = new FieldMatrix {
	// 		vectorMatrix = tmpMassiv // инициализация поля стркутуры
	// 	};
	//
	// 	string json = JsonUtility.ToJson(fieldMatrix, prettyPrint: true); // сохранение структуры в json
	// 	try {
	// 		File.WriteAllText(this._savePath, contents: json); // сохрание json в файл
	// 	}
	// 	catch (Exception e) {
	// 		{
	// 			/* pass */
	// 		}
	// 	}
	// }
	// private void Awake() {
	// 	// this._savePath = Path.Combine(Application.dataPath, saveFileName);  // определение пути для сохранения матрицы
	// }

	private int[,] _fieldMatrix = new int[3, 3];  // матрица поля

	private Player[] _players = new Player[] {new Cross(), new Circle()};  // игроки
	private int _playerIndex = 0;  // индекс текущего игрока

	private int _cross = 0;
	private int _circle = 1;
	

	private void Start() {
		this.GiveIdToCells();  // присвоение номера каждой клетке
		this.FillMatrix();  // заполнение матрицы -1
	}

	private void Update() {
		if (this.IsWinner(this._cross)) {  // если победил крестик
			print("cross win");
		} else if (this.IsWinner(this._circle)) {  // иначе если победил нолик
			print("circle win");
		} else if (this.IsMatrixFilled()) {  // иначе если никто не победил, но все клетки заняты
			print("draw");
		}

		// if (Input.GetKey(KeyCode.P)) {
		// 	this.SaveToFile(); // сохранение в файл текущего состояния матрицы
		// }
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
		
		int currentPlayer = this._players[this._playerIndex++ % 2].Draw(); // получение числа текущего игрока/переход к следующему игроку
		this._fieldMatrix[cellNumber / 3, cellNumber % 3] = currentPlayer; // занесение в матрицу числа текущего игрока по номеру переданной клетки
		return currentPlayer; // возвращение числа текущего игрока
	}

	private void GameOver() {
		/* окончание игры */
		
		foreach (Transform child in this.transform) {
			child.GetComponent<CellButton>().isGameOver = true;  // отметка конца игры в каждом предке поля
		}
	}

	private bool IsMatrixFilled() {
		/* проверка заполненности матрицы поля */
		
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (this._fieldMatrix[i, j] == -1) {  // если элемент матрицы равен -1
					return false;  // матрица имеет пустые клетки -> возвращаем false
				}
			}
		}
		// если вышли из цикла, значит на поле все клетки заняты
		this.GameOver();  // завершаем игру
		return true;
	}

	private bool IsWinner(int player) {
		/* Проверка победителя */
		/*
		 * gorizontal win
		 * [0,0] [0,1] [0,2]
		 * [1,0] [1,1] [1,2]
		 * [2,0] [2,1] [2,2]
		 *
		 * vertical win
		 * [0,0] [1,0] [2,0]
		 * [0,1] [1,1] [2,1]
		 * [0,2] [1,2] [2,2]
		 *
		 * diagonal win
		 * [0,0] [1,1] [2,2]
		 * [0,2] [1,1] [2,0]
		 */

		string winCombination = "";
		
		switch (player) {
			case 0: {  // если игрок крестик
				winCombination = "000";
				break;
			}
			case 1: {  // если игрок нолик
				winCombination = "111";
				break;
			}
		}

		List<string> winCheckList = new List<string>();  // список со всеми возможными комбинациями
		winCheckList.Add($"{this._fieldMatrix[0,0]}{this._fieldMatrix[0,1]}{this._fieldMatrix[0,2]}");
		winCheckList.Add($"{this._fieldMatrix[1,0]}{this._fieldMatrix[1,1]}{this._fieldMatrix[1,2]}");
		winCheckList.Add($"{this._fieldMatrix[2,0]}{this._fieldMatrix[2,1]}{this._fieldMatrix[2,2]}");
		winCheckList.Add($"{this._fieldMatrix[0,0]}{this._fieldMatrix[1,0]}{this._fieldMatrix[2,0]}");
		winCheckList.Add($"{this._fieldMatrix[0,1]}{this._fieldMatrix[1,1]}{this._fieldMatrix[2,1]}");
		winCheckList.Add($"{this._fieldMatrix[0,2]}{this._fieldMatrix[1,2]}{this._fieldMatrix[2,2]}");
		winCheckList.Add($"{this._fieldMatrix[0,0]}{this._fieldMatrix[1,1]}{this._fieldMatrix[2,2]}");
		winCheckList.Add($"{this._fieldMatrix[0,2]}{this._fieldMatrix[1,1]}{this._fieldMatrix[2,0]}");

		foreach (string combination in winCheckList) {  // для каждой комбинации в списке возможных выигрышных комбинаций
			if (combination == winCombination) {  // если комбинация в списке равна выигрышной комбинации
				this.GameOver();  // завершаем игру
				return true;  // выходим из функции
			}
		}
		// если вышли из цикла, значит в списке нет ни одной выигрышной комбинации
		return false;
	}
}