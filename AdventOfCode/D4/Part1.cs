using System;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace AdventOfCode.D4; 

public class Part1 {
	public static int GetFullyContainedPairCount(string filename) {
		var lines = File.ReadAllLines(filename);

		var containedCount = 0;

		foreach (var line in lines) {
			var pair = line.Split(',');

			var firstNumbers = pair[0].Split('-');

			var firstRange = new Range(int.Parse(firstNumbers[0]), int.Parse(firstNumbers[1]));

			var lastNumbers = pair[1].Split('-');
			var lastRange = new Range(int.Parse(lastNumbers[0]), int.Parse(lastNumbers[1]));

			if (ContainsRange(firstRange, lastRange) || ContainsRange(lastRange, firstRange))
				containedCount++;
		}

		return containedCount;
	}

	public static bool ContainsRange(Range first, Range last) {
		return first.Start.Value <= last.Start.Value && first.End.Value >= last.End.Value;
	}
}