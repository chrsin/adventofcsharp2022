using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode.D6; 

public class Part1 {
	public static int FindMessageMarker(string input, int markerLength) {
		var lastMessages = ImmutableQueue.Create<char>();

		for (int index = 0; index < input.Length; index++) {
			char c = input[index];
			if (lastMessages.Count() == markerLength)
				lastMessages = lastMessages.Dequeue();

			lastMessages = lastMessages.Enqueue(c);
			bool found = HasDuplicates(lastMessages);

			if (!found && lastMessages.Count() == markerLength)
				return index + 1;
		}

		return -1;
	}

	public static bool HasDuplicates(ImmutableQueue<char> queue) {
		while (queue.Count() != 0) {
			queue = queue.Dequeue(out char c);
			bool found = queue.Contains(c);
			if (found)
				return true;
		}

		return false;
	}
}