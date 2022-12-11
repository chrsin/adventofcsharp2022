using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace AdventOfCode.D7;

public class Part1 {
	public static DirectoryNode GenerateTree(string filename) {
		var stdout = File.ReadAllLines(filename);

		DirectoryNode rootNode = new DirectoryNode(null, "/");
		DirectoryNode currentDir = rootNode;
		foreach (var line in stdout) {
			if (line.StartsWith("$")) {
				//This is a command
				string command = line.Substring(2);
				if (command.StartsWith("cd")) {
					string dirName = command.Substring(3);
					if (dirName == "/")
						currentDir = rootNode;
					else if (dirName == "..") {
						currentDir = currentDir.Parent;
					}
					else {
						var newDir =
							currentDir.Children.SingleOrDefault(x => x is DirectoryNode && x.Name == dirName) as
								DirectoryNode ??
							new DirectoryNode(currentDir, dirName);

						currentDir = newDir;
					}
				}
			}
			else {
				if (line.StartsWith("dir")) {
					var dirName = line.Substring(4);
					var thisDir = currentDir.Children.SingleOrDefault(x => x is DirectoryNode && x.Name == dirName);
					if (thisDir is null)
						currentDir.Children.Add(new DirectoryNode(currentDir, dirName));
				}
				else {
					var fileParts = line.Split(' ');
					var size = int.Parse(fileParts[0]);
					var fileName = fileParts[1];

					var file = currentDir.Children.SingleOrDefault(x => x.Name == filename);
					if (file is null) {
						currentDir.Children.Add(new FileNode(currentDir, fileName, size));
					}
				}
			}
		}

		return rootNode;
	}
}

public interface ITreeNode {
	public int GetSize();
	public DirectoryNode? Parent { get; }
	public string Name { get; }
	public List<ITreeNode> Children { get; }
}

public class FileNode : ITreeNode {
	private readonly int _size;

	public FileNode(DirectoryNode parent, string name, int size) {
		Parent = parent;
		Name = name;
		_size = size;
	}

	public int GetSize() {
		return _size;
	}

	public DirectoryNode? Parent { get; }
	public string Name { get; }

	public List<ITreeNode> Children => throw new InvalidOperationException("Cannot get children of file");
}

public class DirectoryNode : ITreeNode {
	public DirectoryNode(DirectoryNode? parent, string name) {
		Parent = parent;
		Name = name;
		Children = new List<ITreeNode>();
	}

	public int FindSmallestDirectoryAbove(int requiredSpace) {
		int smallest = int.MaxValue;
		foreach (var treeNode in Children) {
			if (treeNode is DirectoryNode dir) {
				var size = dir.FindSmallestDirectoryAbove(requiredSpace);
				if (size > requiredSpace)
					smallest = Math.Min(smallest, size);
			}

			int thisDirSize = GetSize();

			if (thisDirSize > requiredSpace)
				smallest = Math.Min(thisDirSize, smallest);
		}

		return smallest;
	}

	public int SumDirectoriesLessThan(int lessThan) {
		int sum = 0;
		foreach (var treeNode in Children) {
			if (treeNode is DirectoryNode dir) {
				if (dir.GetSize() < lessThan)
					sum += dir.GetSize();

				sum += dir.SumDirectoriesLessThan(lessThan);
			}
		}

		return sum;
	}

	public int GetSize() {
		int sizeSummed = 0;
		foreach (var child in Children) {
			sizeSummed += child.GetSize();
		}

		return sizeSummed;
	}

	public DirectoryNode? Parent { get; }
	public string Name { get; }
	public List<ITreeNode> Children { get; }
}