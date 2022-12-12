using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D8; 

public class Tests {

	[Fact]
	public async Task CanSolveExamplePart1() {
		var map = Part1.BuildTreeMap("D8/example.txt");
		var sum = Part1.SumVisibleTrees(map);
		
		Assert.Equal(21, sum);
	}
	
	[Fact]
	public async Task CanSolveInputPart1() {
		var map = Part1.BuildTreeMap("D8/input.txt");
		var sum = Part1.SumVisibleTrees(map);
		
		Assert.Equal(1840, sum);
	}

	[Fact]
	public async Task CanSolveExamplePart2() {
		var map = Part2.BuildTreeMap("D8/example.txt");
		var highest = Part2.GetHighestScenicScore(map);
		
		Assert.Equal(8, highest);
	}

	[Fact]
	public async Task CanSolveInputPart2() {
		var map = Part2.BuildTreeMap("D8/input.txt");
		var highest = Part2.GetHighestScenicScore(map);
		
		Assert.Equal(405769, highest);
	}

}