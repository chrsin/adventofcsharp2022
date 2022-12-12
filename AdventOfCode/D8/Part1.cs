using System.IO;

namespace AdventOfCode.D8;

public class Part1 {
	public static int[,] BuildTreeMap(string filename) {
		var lines = File.ReadAllLines(filename);

		int width = lines[0].Length;
		int height = lines.Length;
		int[,] map = new int[width, height];
		for (var y = 0; y < height; y++) {
			for (var x = 0; x < width; x++) {
				var value = int.Parse($"{lines[y][x]}");
				map[x, y] = value;
			}
		}

		return map;
	}

	public static int SumVisibleTrees(int[,] treemap) {
		int width = GetWidth(treemap);
		int height = GetHeight(treemap);
		int visibleTrees = 0;
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				if (IsVisible(treemap, x, y))
					visibleTrees++;
			}
		}

		return visibleTrees;
	}

	public static bool IsVisible(int[,] treeMap, int x, int y) {
		int value = treeMap[x, y];
		return IsVisible(treeMap, x, y, Direction.Up, value) || IsVisible(treeMap, x, y, Direction.Down, value) ||
		       IsVisible(treeMap, x, y, Direction.Left, value) || IsVisible(treeMap, x, y, Direction.Right, value);
	}

	public static bool IsVisible(int[,] treeMap, int x, int y, Direction direction, int originalValue) {
		var adjacentValue = 0;
		int nextX = x;
		int nextY = y;
		switch (direction) {
			case Direction.Up:
				if (y == 0)
					return true;

				nextY = y - 1;
				break;
			case Direction.Down:
				if (y + 1 == GetHeight(treeMap))
					return true;

				nextY = y + 1;
				break;
			case Direction.Left:
				if (x == 0)
					return true;

				nextX = x - 1;
				break;
			case Direction.Right:
				if (x + 1 == GetWidth(treeMap))
					return true;

				nextX = x + 1;
				break;
		}

		adjacentValue = treeMap[nextX, nextY];
		if (adjacentValue >= originalValue)
			return false;

		return IsVisible(treeMap, nextX, nextY, direction, originalValue);
	}

	public static int GetWidth(int[,] treeMap) {
		return treeMap.GetLength(0);
	}

	public static int GetHeight(int[,] treeMap) {
		return treeMap.GetLength(1);
	}
}

public enum Direction {
	Up,
	Down,
	Left,
	Right
}