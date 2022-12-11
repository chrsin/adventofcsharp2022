using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.D5;

public class Part1 {
	public static StackFileResult GetStacks(string filename) {
		var lines = File.ReadAllLines(filename);

		List<int> columnIndexes = new List<int>();
		int headerLine = 0;
		for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++) {
			string line = lines[lineNumber];

			if (Regex.IsMatch(line, @"^(\s*(\[\w\])\s*)*$"))
				continue;

			headerLine = lineNumber;

			for (int index = 0; index < line.Length; index++) {
				if (!int.TryParse($"{line[index]}", out int number))
					continue;

				columnIndexes.Add(index);
			}

			break;
		}

		List<Stack<Char>> result = new List<Stack<char>>(columnIndexes.Count);
		foreach (var columnIndex in columnIndexes) {
			result.Add(new Stack<char>());
		}

		for (int lineNumber = headerLine - 1; lineNumber >= 0; lineNumber--) {
			var line = lines[lineNumber];

			for (int index = 0; index < columnIndexes.Count; index++) {
				int columnIndex = columnIndexes[index];
				var c = line[columnIndex];
				if (c == ' ')
					continue;

				var columnStack = result[index];
				columnStack.Push(c);
			}
		}

		return new StackFileResult(headerLine + 2, result);
	}

	public static StackFileResult ApplyOperations(string filename, StackFileResult stacks) {
		var lines = File.ReadAllLines(filename);

		for (int index = stacks.InputStartLine; index < lines.Length; index++) {
			var line = lines[index];
			var match = Regex.Match(line, @"^[^\d]*(\d+).*(\d).*(\d)$");
			int popCount = int.Parse(match.Groups[1].Value);
			var from = stacks.Stacks[int.Parse(match.Groups[2].Value) - 1];
			var to = stacks.Stacks[int.Parse(match.Groups[3].Value) - 1];

			for (int i = 0; i < popCount; i++) {
				if (from.TryPop(out char x)) {
					to.Push(x);
				}
			}
		}

		return stacks;
	}
}

public record StackFileResult(int InputStartLine, List<Stack<char>> Stacks);