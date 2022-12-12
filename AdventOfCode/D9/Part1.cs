using System;
using System.IO;
using System.Net;

namespace AdventOfCode.D9;

public class Part1 {
	public const int BoardSize = 10000;
	public static int[,] InitializeBoard() {
		var board = new int[BoardSize, BoardSize];

		for (int y = 0; y < board.GetLength(1); y++) {
			for (int x = 0; x < board.GetLength(0); x++) {
				board[x, y] = 0;
			}
		}

		return board;
	}

	public static int[,] ApplyMoves(int[,] board, string filename) {
		int startX = BoardSize / 2;
		int startY = BoardSize / 2 - 1;

		var lines = File.ReadAllLines(filename);

		Rope rope = new Rope(startX, startY);
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

				int currentValue = board[rope.TailX, rope.TailY];
				board[rope.TailX, rope.TailY] = currentValue + 1;
			}
		}

		return board;
	}
	
	

	public static int GetNumberOfTailVisitedFields(int[,] board) {
		int sum = 0;
		foreach (int value in board) {
			if (value > 0)
				sum++;
		}

		return sum;
	}
}

public enum Direction {
	Up,
	Down,
	Left,
	Right
}

public class Rope {
	public Rope(int x, int y) {
		HeadX = x;
		TailX = x;
		HeadY = y;
		TailY = y;
	}

	public void Move(Direction direction) {
		switch (direction) {
			case Direction.Up:
				HeadY--;
				break;
			case Direction.Down:
				HeadY++;
				break;
			case Direction.Left:
				HeadX--;
				break;
			case Direction.Right:
				HeadX++;
				break;
		}

		if (!IsTailAdjacent()) {
			MoveTailBehind(direction);
		}
	}

	public bool IsTailAdjacent() {
		return Math.Abs(HeadX - TailX) <= 1 && Math.Abs(HeadY - TailY) <= 1;
	}

	public void MoveTailBehind(Direction movingDirection) {
		switch (movingDirection) {
			case Direction.Up:
				TailX = HeadX;
				TailY = HeadY + 1;
				break;
			case Direction.Down:
				TailX = HeadX;
				TailY = HeadY - 1;
				break;
			case Direction.Left:
				TailX = HeadX + 1;
				TailY = HeadY;
				break;
			case Direction.Right:
				TailX = HeadX - 1;
				TailY = HeadY;
				break;
		}
	}

	public int HeadX { get; set; }
	public int HeadY { get; set; }

	public int TailX { get; set; }
	public int TailY { get; set; }
}