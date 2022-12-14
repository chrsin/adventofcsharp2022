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
		Assert.Equal(99852, barrel.CalculateMonkeyBusiness(2));
	}

	[Fact]
	public async Task TestInputMonkeys() {
		var barrel = Part1.GetInputMonkeys();
		var monkey = barrel.Monkeys[0];
		Assert.Equal(19, monkey._operation(1));
		Assert.Equal(38, monkey._operation(2));
		Assert.True(monkey._evaluation(34));
		Assert.False(monkey._evaluation(32));
		
		Assert.Same(barrel.Monkeys[4], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[7], monkey._evaluateFalseMonkey);

		monkey = barrel.Monkeys[1];
		Assert.Equal(11, monkey._operation(1));
		Assert.Equal(22, monkey._operation(2));
		Assert.True(monkey._evaluation(33));
		Assert.False(monkey._evaluation(32));
		
		Assert.Same(barrel.Monkeys[3], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[2], monkey._evaluateFalseMonkey);
		
		monkey = barrel.Monkeys[2];
		Assert.Equal(7, monkey._operation(1));
		Assert.Equal(8, monkey._operation(2));
		Assert.True(monkey._evaluation(38));
		Assert.False(monkey._evaluation(37));
		
		Assert.Same(barrel.Monkeys[0], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[4], monkey._evaluateFalseMonkey);

		monkey = barrel.Monkeys[3];
		Assert.Equal(6, monkey._operation(1));
		Assert.Equal(7, monkey._operation(2));
		Assert.True(monkey._evaluation(14));
		Assert.False(monkey._evaluation(13));
		
		Assert.Same(barrel.Monkeys[2], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[0], monkey._evaluateFalseMonkey);
		
		monkey = barrel.Monkeys[4];
		Assert.Equal(8, monkey._operation(1));
		Assert.Equal(9, monkey._operation(2));
		Assert.True(monkey._evaluation(18));
		Assert.False(monkey._evaluation(17));
		
		Assert.Same(barrel.Monkeys[7], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[5], monkey._evaluateFalseMonkey);
		
		monkey = barrel.Monkeys[5];
		Assert.Equal(1, monkey._operation(1));
		Assert.Equal(4, monkey._operation(2));
		Assert.True(monkey._evaluation(25));
		Assert.False(monkey._evaluation(24));
		
		Assert.Same(barrel.Monkeys[1], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[6], monkey._evaluateFalseMonkey);
		
		monkey = barrel.Monkeys[6];
		Assert.Equal(3, monkey._operation(1));
		Assert.Equal(4, monkey._operation(2));
		Assert.True(monkey._evaluation(22));
		Assert.False(monkey._evaluation(21));
		
		Assert.Same(barrel.Monkeys[3], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[1], monkey._evaluateFalseMonkey);
		
		monkey = barrel.Monkeys[7];
		Assert.Equal(4, monkey._operation(1));
		Assert.Equal(5, monkey._operation(2));
		Assert.True(monkey._evaluation(26));
		Assert.False(monkey._evaluation(21));
		
		Assert.Same(barrel.Monkeys[5], monkey._evaluateTrueMonkey);
		Assert.Same(barrel.Monkeys[6], monkey._evaluateFalseMonkey);
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