using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Scripts;
using Players;
using UnityEditor;

public class Field : MonoBehaviour {

	private int[,] _fieldMatrix = new int[3, 3];  // матрица поля
	

	private void Awake() {
		this.GiveIdToCells();
	}

	private void GiveIdToCells() {
		for (int i = 0; i < 9; i++) {
			// каждому предку поля присваивается номер i
			this.transform.GetChild(i).GetComponent<CellButton>().SetCellId(i);
		}
	}

	public void ResetField() {
		/* начальное заполнение матрицы */

		for (int i = 0; i < 9; i++) {
			this._fieldMatrix[i / 3, i % 3] = -1;
			this.transform.GetChild(i).GetComponent<CellButton>().ClearCell();
		}
	}

	public void DrawOnField(int cellNumber, int player) {
		/* рисование на поле, 1 аргумент - номер дочерней клетки, 2 аргумент - текущий игрок */

		this._fieldMatrix[cellNumber / 3, cellNumber % 3] = player;
	}

	public bool IsMatrixFilled() {
		/* проверка заполненности матрицы поля */
		
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (this._fieldMatrix[i, j] == -1) {  // если элемент матрицы равен -1
					return false;  // матрица имеет пустые клетки -> возвращаем false
				}
			}
		}
		return true;
	}

	public int[,] GetFieldMatrix() => _fieldMatrix;
}
