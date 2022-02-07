using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;


public class Manager : MonoBehaviour {

	[SerializeField] private Field _field;
	private Player[] _players = new Player[] {new Cross(), new Circle()};
	private int _playerIndex = 0;
	private int _cross = 0;
	private int _circle = 1;

	private bool _gameStarted = false;
	private bool _gameOver = false;

	private Winner _winner;
	

	private void Start() {
		this._winner = this.GetComponent<Winner>();
	}

	private void Update() {
		if (this._gameStarted) {
			if (this.IsWinner(this._cross)) {
				// print("cross win");
				this._winner.SetWinner("Cross Winner");
			} else if (this.IsWinner(this._circle)) {
				// print("cricle win");
				this._winner.SetWinner("Circle Winner");
			} else if (this._field.IsMatrixFilled()) {
				this.GameOver();
				// print("draw");
				this._winner.SetWinner("Draw");
			}	
		}
	}

	private bool IsWinner(int player) {
		string winCombination = "";
		switch (player) {
			case 0: {
				winCombination = "000";
				break;
			}
			case 1: {
				winCombination = "111";
				break;
			}
		}

		int[,] fieldMatrix = this._field.GetFieldMatrix();
		
		List<string> winCheckList = new List<string>();  // список со всеми возможными комбинациями
		winCheckList.Add($"{fieldMatrix[0,0]}{fieldMatrix[0,1]}{fieldMatrix[0,2]}");
		winCheckList.Add($"{fieldMatrix[1,0]}{fieldMatrix[1,1]}{fieldMatrix[1,2]}");
		winCheckList.Add($"{fieldMatrix[2,0]}{fieldMatrix[2,1]}{fieldMatrix[2,2]}");
		winCheckList.Add($"{fieldMatrix[0,0]}{fieldMatrix[1,0]}{fieldMatrix[2,0]}");
		winCheckList.Add($"{fieldMatrix[0,1]}{fieldMatrix[1,1]}{fieldMatrix[2,1]}");
		winCheckList.Add($"{fieldMatrix[0,2]}{fieldMatrix[1,2]}{fieldMatrix[2,2]}");
		winCheckList.Add($"{fieldMatrix[0,0]}{fieldMatrix[1,1]}{fieldMatrix[2,2]}");
		winCheckList.Add($"{fieldMatrix[0,2]}{fieldMatrix[1,1]}{fieldMatrix[2,0]}");

		foreach (string combination in winCheckList) {  // для каждой комбинации в списке возможных выигрышных комбинаций
			if (combination == winCombination) {  // если комбинация в списке равна выигрышной комбинации
				this.GameOver();
				return true;  // выходим из функции
			}
		}
		// если вышли из цикла, значит в списке нет ни одной выигрышной комбинации
		return false;
	}

	private void GameOver() {
		this._gameOver = true;
		// this._gameStarted = false;
	}

	public bool IsGameOver() {
		return this._gameOver;
	}

	public int GetCurrentPlayer(bool inc) {
		if (inc) {
			return this._playerIndex++ % 2;	
		}
		return this._playerIndex % 2;
	}

	public void StartGame() {
		this._winner.Hide();
		this._field.ResetField();
		this._playerIndex = 0;
		this._gameStarted = true;
		this._gameOver = false;
	}
}
