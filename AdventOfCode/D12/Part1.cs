using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.D12;

public class Part1 {
	public static List<Direction> AllDirections = new List<Direction> {
		Direction.Right, Direction.Down, Direction.Up, Direction.Left
	};

	public static HeightMap GenerateHeightMap(string filename) {
		var lines = File.ReadAllLines(filename);

		Coordinate[,] map = new Coordinate[lines[0].Length, lines.Length];

		Coordinate? startCoordinate = null;

		Coordinate? endCoordinate = null;

		for (int y = 0; y < lines.Length; y++) {
			string line = lines[y];
			for (int x = 0; x < lines[y].Length; x++) {
				char c = line[x];

				if (c == 'S') {
					startCoordinate = new Coordinate(x, y, 0);
					map[x, y] = startCoordinate;
				}
				else if (c == 'E') {
					endCoordinate = new Coordinate(x, y, 25);
					map[x, y] = endCoordinate;
				}
				else {
					map[x, y] = new Coordinate(x, y, c - 97);
				}
			}
		}

		HeightMap result = new HeightMap(map, startCoordinate!, endCoordinate!);

		foreach (Coordinate coordinate in map) {
			foreach (var direction in AllDirections) {
				var next = result.GetNextCoord(direction, coordinate);
				if (next is not null)
					coordinate.ValidCoordinates.Add(next);
			}
		}

		return result;
	}

	public static int CalculateShortestPath(HeightMap map) {
		StepPath(map, map.Start, 0);
		return map.End.FewestSteps;
	}

	public static int? StepPath(HeightMap map, Coordinate current, int currentStep) {
		if (Equals(current, map.End)) {
			current.FewestSteps = Math.Min(currentStep, current.FewestSteps);
			return currentStep;
		}

		if (current.FewestSteps < currentStep) {
			return null;
		}

		current.FewestSteps = currentStep;
		currentStep++;

		int? least = null;
		foreach (Coordinate coordinate in current.ValidCoordinates) {
			if (coordinate.FewestSteps > currentStep) {
				var result = StepPath(map, coordinate, currentStep);
				if (result.HasValue)
					least = least.HasValue ? Math.Min(least.Value, result.Value) : result.Value;
			}
		}

		return least;
	}
}

public class HeightMap {
	public HeightMap(Coordinate[,] map, Coordinate start, Coordinate end) {
		Map = map;
		Start = start;
		End = end;
	}

	public Coordinate[,] Map { get; }
	public Coordinate Start { get; }
	public Coordinate End { get; }


	public Coordinate? GetNextCoord(Direction direction, Coordinate coordinate) {
		Coordinate? newCoord = null;
		if (direction == Direction.Up) {
			if (coordinate.Y == 0)
				return null;

			newCoord = Map[coordinate.X, coordinate.Y - 1];
		}

		if (direction == Direction.Down) {
			if (coordinate.Y == Map.GetLength(1) - 1)
				return null;

			newCoord = Map[coordinate.X, coordinate.Y + 1];
		}

		if (direction == Direction.Left) {
			if (coordinate.X == 0)
				return null;

			newCoord = Map[coordinate.X - 1, coordinate.Y];
		}

		if (direction == Direction.Right) {
			if (coordinate.X == Map.GetLength(0) - 1)
				return null;
			newCoord = Map[coordinate.X + 1, coordinate.Y];
		}

		if (newCoord is null) {
			return null;
		}

		return coordinate.Height - newCoord.Height >= -1 ? newCoord : null;
	}
}

public enum Direction {
	Up,
	Down,
	Left,
	Right
}

public class Coordinate {
	protected bool Equals(Coordinate other) {
		return X == other.X && Y == other.Y;
	}

	public override bool Equals(object? obj) {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Coordinate)obj);
	}

	public override int GetHashCode() {
		return HashCode.Combine(X, Y);
	}

	public Coordinate(int x, int y, int height) {
		X = x;
		Y = y;
		Height = height;
		FewestSteps = int.MaxValue;
		ValidCoordinates = new List<Coordinate>(4);
	}

	public List<Coordinate> ValidCoordinates { get; }
	public int X { get; }
	public int Y { get; }
	public int Height { get; }
	public int FewestSteps { get; set; }
}