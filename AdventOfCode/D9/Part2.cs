using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace AdventOfCode.D9;

public class Part2 {
	public static int ApplyMovesAndGetScore(string filename) {
		int startX = 0;
		int startY = 0;

		var lines = File.ReadAllLines(filename);

		Rope2 rope = new Rope2(startX, startY, 10);
		foreach (var line in lines) {
			var parts = line.Split(' ');
			int count = int.Parse(parts[1]);
			Direction direction = parts[0] switch {
				"R" => Direction.Right,
				"U" => Direction.Up,
				"L" => Direction.Left,
				"D" => Direction.Down
			};

			for (int c = 0; c < count; c++) {
				rope.Move(direction);
			}
		}

		return rope.GetScore();
	}
}

public class Point {
	protected bool Equals(Point other) {
		return X == other.X && Y == other.Y;
	}

	public override bool Equals(object? obj) {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Point)obj);
	}

	public override int GetHashCode() {
		return HashCode.Combine(X, Y);
	}

	public int X { get; set; }
	public int Y { get; set; }

	public Point(int x, int y) {
		X = x;
		Y = y;
	}
}

public class Rope2 {
	public Rope2(int x, int y, int tails) {
		Points = new List<Point>();
		for (int index = 0; index < tails; index++) {
			Points.Add(new Point(x, y));
		}

		_score = new HashSet<Point>();
	}

	public List<Point> Points { get; set; }
	private ISet<Point> _score;

	public void Move(Direction direction) {
		Point? previous = null;
		for (int index = 0; index < Points.Count; index++) {
			Point current = Points[index];
			if (previous is null) {
				//This is the head
				switch (direction) {
					case Direction.Up:
						current.Y--;
						break;
					case Direction.Down:
						current.Y++;
						break;
					case Direction.Left:
						current.X--;
						break;
					case Direction.Right:
						current.X++;
						break;
				}
			}
			else {
				if (!IsAdjacent(current, previous))
					UpdateTail(current, previous);
			}

			previous = current;
			if (index == Points.Count - 1)
				_score.Add(current);
		}
	}

	private void UpdateTail(Point current, Point previous) {
		int xDif = Math.Abs(previous.X - current.X);
		int yDif = Math.Abs(previous.Y - current.Y);
		if (xDif > 1) {
			if (previous.X > current.X) {
				current.X = previous.X - 1;
			}
			else {
				current.X = previous.X + 1;
			}

			if (yDif <= 1) {
				current.Y = previous.Y;
			}
		}

		if (yDif > 1) {
			if (previous.Y > current.Y) {
				current.Y = previous.Y - 1;
			}
			else {
				current.Y = previous.Y + 1;
			}

			if (xDif <= 1) {
				current.X = previous.X;
			}
		}
	}

	public bool IsAdjacent(Point current, Point previous) {
		return Math.Abs(current.X - previous.X) <= 1 && Math.Abs(current.Y - previous.Y) <= 1;
	}

	public int GetScore() {
		return _score.Count;
	}
}