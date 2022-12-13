using System;
using System.IO;
using System.Net;
using System.Text;

namespace AdventOfCode.D10;

public class Part2 {
	public static void ProcessInput(string filename, string output) {
		var lines = File.ReadAllLines(filename);

		int x = 1;
		int lineIndex = 0;
		string line = lines[lineIndex++];

		IOperation operation = GetOperation(x, line);
		int summedResult = 0;

		StringBuilder stringBuilder = new StringBuilder();

		try {
			for (int cycle = 0; cycle <= 240; cycle++) {
				x = operation.X;
				WriteToFile(stringBuilder, x, cycle);

				if (!operation.RunCycle())
					continue;

				operation = GetOperation(operation.X, lines[lineIndex++]);
			}
		}
		catch {
			
		}

		File.WriteAllText(output, stringBuilder.ToString());
	}

	public static void WriteToFile(StringBuilder stringBuilder, int x, int cycle) {
		int lineIndex = cycle % 40;
		if (lineIndex == 0 && cycle != 0)
			stringBuilder.Append("\n");
		string pixelValue = Math.Abs(x - lineIndex) <= 1 ? "#" : ".";
		stringBuilder.Append(pixelValue);
	}

	public static IOperation GetOperation(int x, string input) {
		var pair = input.Split(' ');
		if (pair[0] == "addx") {
			return new AddX(x, int.Parse(pair[1]));
		}

		return new Noop(x);
	}
}