using System;
using System.IO;

namespace AdventOfCode.D8;

public class Part2 {
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

	public static int GetHighestScenicScore(int[,] treemap) {
		int width = GetWidth(treemap);
		int height = GetHeight(treemap);

		int highest = int.MinValue;

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				highest = Math.Max(highest, GetScenicScore(treemap, x, y));
			}
		}

		return highest;
	}

	public static int GetScenicScore(int[,] treeMap, int x, int y) {
		int value = treeMap[x, y];
		return GetScenicScore(treeMap, x, y, Direction.Up, value, 0) *
		       GetScenicScore(treeMap, x, y, Direction.Down, value, 0) *
		       GetScenicScore(treeMap, x, y, Direction.Left, value, 0) *
		       GetScenicScore(treeMap, x, y, Direction.Right, value, 0);
	}

	public static int GetScenicScore(int[,] treeMap, int x, int y, Direction direction, int originalValue,
		int startScore) {
		var adjacentValue = 0;
		int nextX = x;
		int nextY = y;
		switch (direction) {
			case Direction.Up:
				if (y == 0)
					return startScore;

				nextY = y - 1;
				break;
			case Direction.Down:
				if (y + 1 == GetHeight(treeMap))
					return startScore;

				nextY = y + 1;
				break;
			case Direction.Left:
				if (x == 0)
					return startScore;

				nextX = x - 1;
				break;
			case Direction.Right:
				if (x + 1 == GetWidth(treeMap))
					return startScore;

				nextX = x + 1;
				break;
		}

		adjacentValue = treeMap[nextX, nextY];
		if (adjacentValue >= originalValue)
			return startScore + 1;

		return GetScenicScore(treeMap, nextX, nextY, direction, originalValue, startScore + 1);
	}

	public static int GetWidth(int[,] treeMap) {
		return treeMap.GetLength(0);
	}

	public static int GetHeight(int[,] treeMap) {
		return treeMap.GetLength(1);
	}
}