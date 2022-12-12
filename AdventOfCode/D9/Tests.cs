using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D9; 

public class Tests {

	[Fact]
	public async Task CanSolveExamplePart1() {
		int[,] board = Part1.InitializeBoard();

		board = Part1.ApplyMoves(board, "D9/example.txt");

		int visitedFields = Part1.GetNumberOfTailVisitedFields(board);
		Assert.Equal(13, visitedFields);
	}
	
	[Fact]
	public async Task CanSolveInputPart1() {
		int[,] board = Part1.InitializeBoard();

		board = Part1.ApplyMoves(board, "D9/input.txt");

		int visitedFields = Part1.GetNumberOfTailVisitedFields(board);
		Assert.Equal(5779, visitedFields);
	}

	[Fact]
	public async Task CanSolveExamplePart2() {
		Assert.Equal(1, Part2.ApplyMovesAndGetScore("D9/example.txt"));
	}
	
	[Fact]
	public async Task CanSolveExample2Part2() {
		Assert.Equal(36, Part2.ApplyMovesAndGetScore("D9/example2.txt"));
	}
	
	[Fact]
	public async Task CanSolveInput2Part2() {
		Assert.Equal(2331, Part2.ApplyMovesAndGetScore("D9/input.txt"));
	}
}