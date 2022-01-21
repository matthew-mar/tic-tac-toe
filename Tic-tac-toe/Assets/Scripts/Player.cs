using System;

namespace Players {
	public abstract class Player {
		public abstract int Draw();
	}

	public class Cross : Player {
		public override int Draw() => 0;
	}

	public class Circle : Player {
		public override int Draw() => 1;
	}
}


