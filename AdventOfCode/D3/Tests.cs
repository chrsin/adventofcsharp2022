using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D3; 

public class Tests {

	[Fact]
	public async Task TestAnswer() {
		var sum = Part2.CalculateBadgeSum("D3/input3.txt");
		Assert.Equal(2510, sum);
	}

	[Fact]
	public void Test1() {
		var sum = Part2.CalculateBadgeSum("D3/example3.txt");
		Assert.Equal(70, sum);
	}

	[Theory]
	[InlineData('r', 18)]
	[InlineData('Z', 52)]
	public async Task TestPriority(char input, int expectedOutput) {
		Assert.Equal(expectedOutput, Part2.GetPriority(input));
	}

}