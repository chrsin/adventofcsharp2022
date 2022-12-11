using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D7;

public class Tests {
	[Fact]
	public async Task CanParseExamplePart1() {
		var tree = Part1.GenerateTree("D7/example.txt");

		Assert.Equal(95437, tree.SumDirectoriesLessThan(100000));
	}

	[Fact]
	public async Task CanParseInputPart1() {
		var tree = Part1.GenerateTree("D7/input.txt");

		Assert.Equal(1453349, tree.SumDirectoriesLessThan(100000));
	}

	[Fact]
	public async Task CanCalculateExamplePart2() {
		var tree = Part1.GenerateTree("D7/example.txt");
		var totalSize = tree.GetSize();

		Assert.Equal(48381165, totalSize);

		var deviceSize = 70_000_000;
		var freeSpace = deviceSize - totalSize;
		var requiredSize = 30_000_000;

		var needsDeleting = requiredSize - freeSpace;

		Assert.Equal(8381165, needsDeleting);

		Assert.Equal(24933642, tree.FindSmallestDirectoryAbove(needsDeleting));
	}

	[Fact]
	public async Task CanCalculateInputPart2() {
		var tree = Part1.GenerateTree("D7/input.txt");
		var totalSize = tree.GetSize();

		var deviceSize = 70_000_000;
		var freeSpace = deviceSize - totalSize;
		var requiredSize = 30_000_000;

		var needsDeleting = requiredSize - freeSpace;
		Assert.Equal(2948823, tree.FindSmallestDirectoryAbove(needsDeleting));
	}
}