using System;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D4;

public class Tests {
	[Fact]
	public async Task ExampleInput() {
		Assert.Equal(2, Part1.GetFullyContainedPairCount("D4/example.txt"));
	}

	[Fact]
	public async Task InputPart1() {
		Assert.Equal(515, Part1.GetFullyContainedPairCount("D4/input.txt"));
	}

	[Fact]
	public async Task InputPart2() {
		Assert.Equal(883, Part2.GetFullyContainedPairCount("D4/input.txt"));
	}

	[Theory]
	[InlineData(1, 2, 2, 5, true)]
	[InlineData(5, 9, 6, 6, true)]
	[InlineData(5, 9, 1, 4, false)]
	public async Task ContainsRange(int x1, int x2, int y1, int y2, bool expectedResult) {
		Assert.Equal(expectedResult, Part2.ContainsRange(new Range(x1, x2), new Range(y1, y2)));
	}
}