using System;
using System.Collections.Generic;

namespace Assets.Scripts {
	
	[System.Serializable]
	public struct FieldMatrix {
		public int[] vectorMatrix;  // одномерное отображение матрицы поля, для записи в json
	}
}