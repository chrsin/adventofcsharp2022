﻿using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D12; 

public class Tests {
	[Fact]
	public async Task CanSolveExamplePart1() {
		var map = Part1.GenerateHeightMap("D12/example.txt");

		int steps = Part1.CalculateShortestPath(map);
		Assert.Equal(31, steps);
	}
	
	[Fact]
	public async Task CanSolveInputPart1() {
		var map = Part1.GenerateHeightMap("D12/input.txt");

		int steps = Part1.CalculateShortestPath(map);
		Assert.Equal(31, steps);
	}
}