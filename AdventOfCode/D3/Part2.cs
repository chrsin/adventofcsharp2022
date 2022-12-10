using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.D3; 

public class Part2 {

	public static int CalculateBadgeSum(string filename) {
		var input = File.ReadAllText(filename);

		var backpacks = input.Split("\n");

		var backpackSum = 0;

		for (int index = 0; index < backpacks.Length; index = index + 3) {
			backpackSum += GetBadgePriority(backpacks[index], backpacks[index + 1], backpacks[index + 2]);
		}

		return backpackSum;
	}

	private static int GetBadgePriority(string backpack1, string backpack2, string backpack3) {

		foreach (var c in backpack1) {
			if(backpack2.IndexOf(c) == -1)
				continue;
			
			if(backpack3.IndexOf(c) == -1)
				continue;

			return GetPriority(c);
		}

		throw new NotImplementedException("This should not happen");
	}

	public static int GetPriority(char c) {
		var cv = (int)c;
		return cv switch {
			>= 97 => cv - 96,
			_ =>  cv - 64 + 26
		};
	}
}