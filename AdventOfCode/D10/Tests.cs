using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D10; 

public class Tests {

	[Fact]
	public async Task CanSolveExample() {

		Assert.Equal(13140, Part1.ProcessInput("D10/example.txt"));
	}
	
	[Fact]
	public async Task CanSolveInput() {

		Assert.Equal(15020, Part1.ProcessInput("D10/input.txt"));
	}

	[Fact]
	public async Task CanSolveExamplePart2() {
		Part2.ProcessInput("D10/input.txt", @"C:\tmp\output.txt");
	}
}