using System;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCode.D11;

public class Tests {
	[Fact]
	public async Task CanSolveExample() {
		var barrel = Part1.GetExampleMonkeys();
		barrel.ArseAround(20);
		Assert.Equal(10605, barrel.CalculateMonkeyBusiness(2));
	}

	[Fact]
	public async Task CanSolveInputPart1() {
		var barrel = Part1.GetInputMonkeys();
		barrel.ArseAround(20);
		Assert.Equal(10605, barrel.CalculateMonkeyBusiness(2));
	}


	[Fact]
	public async Task RoundingDownFuckingWorks() {
		Assert.Equal(3, Monkey.RelievePlayer(11));
	}


	[Fact]
	public async Task WorksForExampleRound1() {
		var barrel = Part1.GetExampleMonkeys();
		barrel.ArseAround(1);

		Assert.Collection(barrel.Monkeys,
			monkey => {
				Assert.Collection(monkey.MonkeyItems, item => { Assert.Equal(20, item); },
					item => { Assert.Equal(23, item); }, item => { Assert.Equal(27, item); },
					item => { Assert.Equal(26, item); });
			},
			monkey => {
				Assert.Collection(monkey.MonkeyItems, item => { Assert.Equal(2080, item); },
					item => { Assert.Equal(25, item); }, item => { Assert.Equal(167, item); },
					item => { Assert.Equal(207, item); }, item => { Assert.Equal(401, item); },
					item => { Assert.Equal(1046, item); });
			}, monkey => { Assert.Empty(monkey.MonkeyItems); }, monkey => { Assert.Empty(monkey.MonkeyItems); });
	}

	[Fact]
	public async Task WorksForExampleRound2() {
		var barrel = Part1.GetExampleMonkeys();
		barrel.ArseAround(2);

		Assert.Collection(barrel.Monkeys,
			monkey => {
				Assert.Collection(monkey.MonkeyItems, item => { Assert.Equal(695, item); },
					item => { Assert.Equal(10, item); }, item => { Assert.Equal(71, item); },
					item => { Assert.Equal(135, item); },
					item => { Assert.Equal(350, item);});
			},
			monkey => {
				Assert.Collection(monkey.MonkeyItems, item => { Assert.Equal(43, item); },
					item => { Assert.Equal(49, item); }, item => { Assert.Equal(58, item); },
					item => { Assert.Equal(55, item); }, item => { Assert.Equal(362, item); });
			}, monkey => { Assert.Empty(monkey.MonkeyItems); }, monkey => { Assert.Empty(monkey.MonkeyItems); });
	}


	[Fact]
	public async Task WorksForExampleRound15() {
		var barrel = Part1.GetExampleMonkeys();
		barrel.ArseAround(15);

		Assert.Collection(barrel.Monkeys,
			monkey => {
				Assert.Collection(monkey.MonkeyItems, item => { Assert.Equal(83, item); },
					item => { Assert.Equal(44, item); }, item => { Assert.Equal(8, item); },
					item => { Assert.Equal(184, item); }, item => { Assert.Equal(9, item); },
					item => { Assert.Equal(20, item); }, item => { Assert.Equal(26, item); },
					item => { Assert.Equal(102, item); });
			},
			monkey => {
				Assert.Collection(monkey.MonkeyItems, item => { Assert.Equal(110, item); },
					item => { Assert.Equal(36, item); });
			}, monkey => { Assert.Empty(monkey.MonkeyItems); }, monkey => { Assert.Empty(monkey.MonkeyItems); });
	}
}