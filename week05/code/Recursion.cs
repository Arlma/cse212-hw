using System;
using System.Collections.Generic;
using System.Linq;

public static class Recursion
{
    // 1. Sum of squares
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;
        return n * n + SumSquaresRecursive(n - 1);
    }

    // 2. PermutationsChoose
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            char next = letters[i];
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + next);
        }
    }

    // 3. CountWaysToClimb
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? memo = null)
    {
        if (memo == null) memo = new Dictionary<int, decimal>();
        if (s < 0) return 0;
        if (s == 0) return 1;
        if (memo.ContainsKey(s)) return memo[s];

        decimal ways = CountWaysToClimb(s - 1, memo) +
                       CountWaysToClimb(s - 2, memo) +
                       CountWaysToClimb(s - 3, memo);

        memo[s] = ways;
        return ways;
    }

    // 4. WildcardBinary
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        string zero = pattern.Substring(0, index) + "0" + pattern.Substring(index + 1);
        string one = pattern.Substring(0, index) + "1" + pattern.Substring(index + 1);
        WildcardBinary(zero, results);
        WildcardBinary(one, results);
    }

    // 5. SolveMaze
    public static void SolveMaze(List<string> results, Maze maze)
    {
        List<(int, int)> currPath = new List<(int, int)>();
        SolveMazeHelper(results, maze, 0, 0, currPath);
    }

    private static void SolveMazeHelper(List<string> results, Maze maze, int x, int y, List<(int, int)> currPath)
    {
        if (!maze.IsValidMove(currPath, x, y))
            return;

        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            results.Add(PathToString(currPath));
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        SolveMazeHelper(results, maze, x + 1, y, currPath);
        SolveMazeHelper(results, maze, x - 1, y, currPath);
        SolveMazeHelper(results, maze, x, y + 1, currPath);
        SolveMazeHelper(results, maze, x, y - 1, currPath);

        currPath.RemoveAt(currPath.Count - 1);
    }

    private static string PathToString(List<(int, int)> path)
    {
        return "<List>{" + string.Join(", ", path.Select(p => $"({p.Item1}, {p.Item2})")) + "}";
    }
}
