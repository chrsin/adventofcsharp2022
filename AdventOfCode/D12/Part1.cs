using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AdventOfCode.D12;

public class Part1 {
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

		return new HeightMap(map, startCoordinate!, endCoordinate!);
	}

	public static int CalculateShortestPath(HeightMap map) {
		StepPath(map, map.Start, new Stack<Coordinate>(10000));
		return map.End.FewestSteps;
	}

	public static Stack<Coordinate> StepPath(HeightMap map, Coordinate current, Stack<Coordinate> previous) {
		if (Equals(current, map.End)) {
			current.FewestSteps = Math.Min(previous.Count, current.FewestSteps);
			return previous;
		}

		if (current.FewestSteps < previous.Count) {
			return previous;
		}

		current.FewestSteps = previous.Count;
		previous.Push(current);

		Coordinate? nextCoord = map.GetNextCoord(Direction.Down, current, previous.Count);
		if (nextCoord is not null) {
			previous = StepPath(map, nextCoord, previous);
		}

		nextCoord = map.GetNextCoord(Direction.Left, current, previous.Count);
		if (nextCoord is not null) {
			previous = StepPath(map, nextCoord, previous);
		}

		nextCoord = map.GetNextCoord(Direction.Right, current, previous.Count);
		if (nextCoord is not null) {
			previous = StepPath(map, nextCoord, previous);
		}

		nextCoord = map.GetNextCoord(Direction.Up, current, previous.Count);
		if (nextCoord is not null) {
			previous = StepPath(map, nextCoord, previous);
		}

		previous.Pop();
		return previous;
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


	public Coordinate? GetNextCoord(Direction direction, Coordinate coordinate, int currentStepCount) {
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

		if (newCoord is null || newCoord.FewestSteps < currentStepCount) {
			return null;
		}

		return HeightDiff(coordinate, newCoord) <= 1 ? newCoord : null;
	}

	public int HeightDiff(Coordinate coord1, Coordinate coord2) {
		return Math.Abs(coord1.Height - coord2.Height);
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
	}

	public int X { get; }
	public int Y { get; }
	public int Height { get; }
	public int FewestSteps { get; set; }
}