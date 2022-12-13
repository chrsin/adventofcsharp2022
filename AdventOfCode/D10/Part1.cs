using System;
using System.IO;
using System.Net;

namespace AdventOfCode.D10;

public class Part1 {
	public static int ProcessInput(string filename) {
		var lines = File.ReadAllLines(filename);

		int x = 1;
		int lineIndex = 0;
		string line = lines[lineIndex++];

		IOperation operation = GetOperation(x, line);
		int summedResult = 0;

		for (int cycle = 1; cycle <= 220; cycle++) {
			x = operation.X;
			if ((cycle - 20) % 40 == 0)
				summedResult += cycle * x;
			
			if(!operation.RunCycle())
				continue;

			operation = GetOperation(operation.X, lines[lineIndex++]);
		}

		return summedResult;
	}

	public static IOperation GetOperation(int x, string input) {
		var pair = input.Split(' ');
		if (pair[0] == "addx") {
			return new AddX(x, int.Parse(pair[1]));
		}

		return new Noop(x);
	}
}

public interface IOperation {
	public bool RunCycle();

	public int X { get; }
}

public class Noop : IOperation {
	private int _missingCycles;

	public Noop(int x) {
		X = x;
		_missingCycles = 1;
	}

	public bool RunCycle() {
		--_missingCycles;
		if (_missingCycles == 0)
			return true;
		if (_missingCycles < 0)
			throw new InvalidOperationException("Cycle ran too long");
		
		return false;
	}

	public int X { get; }
}

public class AddX : IOperation {
	private int _missingCycles;
	private int _add;

	public AddX(int x, int add) {
		X = x;
		_add = add;
		_missingCycles = 2;
	}

	public bool RunCycle() {
		--_missingCycles;
		if (_missingCycles == 0) {
			X += _add;
			return true;
		}

		if (_missingCycles < 0)
			throw new InvalidOperationException("Cycle ran too long");

		return false;
	}

	public int X { get; private set; }
}