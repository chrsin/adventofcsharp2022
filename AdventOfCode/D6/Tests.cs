using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D6;

public class Tests {
	[Theory]
	[InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
	[InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
	[InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
	[InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
	public async Task CanParseExamples(string input, int expectedIndex) {
		Assert.Equal(expectedIndex, Part1.FindMessageMarker(input, 4));
	}

	[Fact]
	public async Task CanParseInputPart1() {
		Assert.Equal(1282, Part1.FindMessageMarker(File.ReadAllText("D6/input.txt"), 4));
	}

	[Fact]
	public async Task CanParseInputPart2() {
		Assert.Equal(3513, Part1.FindMessageMarker(File.ReadAllText("D6/input.txt"), 14));
	}
}