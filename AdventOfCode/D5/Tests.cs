using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.XPath;
using Xunit;

namespace AdventOfCode.D5;

public class Tests {
	[Fact]
	public async Task CanParseExampleStacks() {
		var stacks = Part1.GetStacks("D5/example.txt");

		Assert.Equal(5, stacks.InputStartLine);

		Assert.Collection(stacks.Stacks, item => {
			Assert.Equal('N', item.Pop());
			Assert.Equal('Z', item.Pop());
			Assert.Empty(item);
		}, item => {
			Assert.Equal('D', item.Pop());
			Assert.Equal('C', item.Pop());
			Assert.Equal('M', item.Pop());
			Assert.Empty(item);
		}, item => {
			Assert.Equal('P', item.Pop());
			Assert.Empty(item);
		});
	}

	[Fact]
	public async Task CanPerformOperations() {
		var stacks = Part1.GetStacks("D5/example.txt");
		stacks = Part1.ApplyOperations("D5/example.txt", stacks);
		var charList = new List<char>();
		foreach (var stack in stacks.Stacks) {
			if (stack.TryPeek(out char x))
				charList.Add(x);
		}

		Assert.Equal("CMZ", new string(charList.ToArray()));
	}

	[Fact]
	public async Task InputPart1() {
		var stacks = Part1.GetStacks("D5/input.txt");
		stacks = Part1.ApplyOperations("D5/input.txt", stacks);

		var charList = new List<char>();
		foreach (var stack in stacks.Stacks) {
			if (stack.TryPeek(out char x))
				charList.Add(x);
		}

		Assert.Equal("QNNTGTPFN", new string(charList.ToArray()));
	}


	[Fact]
	public async Task ExamplePart2() {
		var stacks = Part2.GetStacks("D5/example.txt");
		stacks = Part2.ApplyOperations("D5/example.txt", stacks);
		var charList = new List<char>();
		foreach (var stack in stacks.Stacks) {
			if (stack.TryPeek(out char x))
				charList.Add(x);
		}

		Assert.Equal("MCD", new string(charList.ToArray()));
	}

	[Fact]
	public async Task InputPart2() {
		var stacks = Part2.GetStacks("D5/input.txt");
		stacks = Part2.ApplyOperations("D5/input.txt", stacks);
		var charList = new List<char>();
		foreach (var stack in stacks.Stacks) {
			if (stack.TryPeek(out char x))
				charList.Add(x);
		}

		Assert.Equal("GGNPJBTTR", new string(charList.ToArray()));
	}
}