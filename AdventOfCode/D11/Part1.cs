using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.D11;

public class Part1 {
	public static MonkeyBarrel GetExampleMonkeys() {
		var barrel = new MonkeyBarrel();

		barrel.Monkeys.Add(new Monkey(new[] { 79, 98 }, c => c * 19, c => c % 23 == 0));
		barrel.Monkeys.Add(new Monkey(new[] { 54, 65, 75, 74 }, c => c + 6, c => c % 19 == 0));
		barrel.Monkeys.Add(new Monkey(new[] { 79, 60, 97 }, c => c * c, c => c % 13 == 0));
		barrel.Monkeys.Add(new Monkey(new[] { 74 }, c => c + 3, c => c % 17 == 0));

		barrel.Monkeys[0].SetMonkeys(barrel.Monkeys[2], barrel.Monkeys[3]);
		barrel.Monkeys[1].SetMonkeys(barrel.Monkeys[2], barrel.Monkeys[0]);
		barrel.Monkeys[2].SetMonkeys(barrel.Monkeys[1], barrel.Monkeys[3]);
		barrel.Monkeys[3].SetMonkeys(barrel.Monkeys[0], barrel.Monkeys[1]);

		return barrel;
	}

	public static MonkeyBarrel GetInputMonkeys() {
		var barrel = new MonkeyBarrel();

		barrel.Monkeys.Add(new Monkey(
			new[] { 72, 64, 51, 57, 93, 97, 68 },
			old => old * 19, 
			c => c % 17 == 0));
		barrel.Monkeys.Add(new Monkey(
			new[] { 62 }, 
			old => old * 11, 
			c => c % 3 == 0));
		barrel.Monkeys.Add(new Monkey(
			new[] { 57, 94, 69, 79, 72 }, 
			old => old + 6, 
			c => c % 19 == 0));
		barrel.Monkeys.Add(new Monkey(
			new[] { 80, 64, 92, 93, 64, 56 }, 
			old => old + 5, 
			c => c % 7 == 0));
		barrel.Monkeys.Add(new Monkey(
			new[] { 70, 88, 95, 99, 78, 72, 65, 94 }, 
			old => old + 7, 
			c => c % 2 == 0));
		barrel.Monkeys.Add(new Monkey(
			new[] { 57, 95, 81, 61 }, 
			old => old * old, 
			c => c % 5 == 0));
		barrel.Monkeys.Add(new Monkey(
			new[] { 79, 99 }, 
			old => old + 2, 
			c => c % 11 == 0));
		barrel.Monkeys.Add(new Monkey(
			new[] { 68, 98, 62 }, 
			old => old + 3, 
			c => c % 13 == 0));

		barrel.Monkeys[0].SetMonkeys(
			barrel.Monkeys[4], barrel.Monkeys[7]);
		barrel.Monkeys[1].SetMonkeys(
			barrel.Monkeys[3], barrel.Monkeys[2]);
		barrel.Monkeys[2].SetMonkeys(
			barrel.Monkeys[0], barrel.Monkeys[4]);
		barrel.Monkeys[3].SetMonkeys(barrel.Monkeys[2], barrel.Monkeys[0]);
		barrel.Monkeys[4].SetMonkeys(barrel.Monkeys[7], barrel.Monkeys[5]);
		barrel.Monkeys[5].SetMonkeys(barrel.Monkeys[1], barrel.Monkeys[6]);
		barrel.Monkeys[6].SetMonkeys(barrel.Monkeys[3], barrel.Monkeys[1]);
		barrel.Monkeys[7].SetMonkeys(barrel.Monkeys[5], barrel.Monkeys[6]);

		return barrel;
	}
}

public class MonkeyBarrel {
	public MonkeyBarrel() {
		Monkeys = new List<Monkey>();
	}

	public void ArseAround(int timesToArseAround) {
		for (int index = 0; index < timesToArseAround; index++) {
			foreach (var monkey in Monkeys) {
				monkey.HaveABlastAnnoyingThePlayer();
			}
		}
	}

	public int CalculateMonkeyBusiness(int fromNumberOfMonkeys) {
		int total = 1;
		foreach (var score in Monkeys.Select(m => m.GetScore()).OrderByDescending(s => s)
			         .Take(fromNumberOfMonkeys)) {
			total *= score;
		}

		return total;
	}

	public List<Monkey> Monkeys { get; }
}

public class Monkey {
	private readonly Func<int, int> _operation;
	private readonly Func<int, bool> _evaluation;
	private Monkey _evaluateTrueMonkey;
	private Monkey _evaluateFalseMonkey;

	private int _score = 0;

	public Queue<int> MonkeyItems { get; }

	public Monkey(int[] startingItems, Func<int, int> operation, Func<int, bool> evaluation) {
		_operation = operation;
		_evaluation = evaluation;
		MonkeyItems = new Queue<int>();
		foreach (var item in startingItems) {
			MonkeyItems.Enqueue(item);
		}
	}

	public int GetScore() {
		return _score;
	}

	public void SetMonkeys(Monkey evaluateTrueMonkey, Monkey evaluateFalseMonkey) {
		_evaluateTrueMonkey = evaluateTrueMonkey;
		_evaluateFalseMonkey = evaluateFalseMonkey;
	}

	public void GiveItem(int item) {
		MonkeyItems.Enqueue(item);
	}

	public static int RelievePlayer(int worry) {
		return Convert.ToInt32(Math.Floor(worry / 3m));
	}

	public void HaveABlastAnnoyingThePlayer() {
		while (MonkeyItems.TryDequeue(out int item)) {
			int worry = _operation(item);
			_score++;

			worry = RelievePlayer(worry);

			if (_evaluation(worry))
				_evaluateTrueMonkey.GiveItem(worry);
			else
				_evaluateFalseMonkey.GiveItem(worry);
		}
	}
}