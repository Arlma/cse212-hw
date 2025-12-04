using System;
using System.Collections.Generic;
using System.Linq;

// --- PLACEHOLDER CLASSES FOR PROBLEM 5 ---

/// <summary>
/// Placeholder class for the Maze required by SolveMaze (Problem 5).
/// Assumes a 10x10 maze with the exit at (9, 9) and no walls by default.
/// Includes necessary state management for visited cells.
/// </summary>
public class Maze
{
    public int Width => 10;
    public int Height => 10;
    // Tracks visited cells to prevent cycles and ensure correct pathfinding.
    private readonly HashSet<ValueTuple<int, int>> visited = new HashSet<ValueTuple<int, int>>();

    public bool IsWall(int x, int y) => false; // Simplifying: assume no walls
    public bool IsEnd(int x, int y) => x == Width - 1 && y == Height - 1; // End is at (9, 9)

    public bool IsVisited(int x, int y) => visited.Contains((x, y));
    public void MarkVisited(int x, int y) => visited.Add((x, y));
    public void UnmarkVisited(int x, int y) => visited.Remove((x, y));
}

/// <summary>
/// Placeholder extension method to format the path list for output.
/// </summary>
public static class PathExtensions
{
    public static string AsString(this List<ValueTuple<int, int>> path)
    {
        return string.Join("->", path.Select(p => $"({p.Item1},{p.Item2})"));
    }
}

// --- RECURSION SOLUTIONS CLASS ---

public static class Recursion
{
    /// <summary>
    /// # Problem 1 #
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2.
    /// Base Case: S(n) = 0 if n <= 0
    /// Recursive Step: S(n) = n^2 + S(n-1)
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base Case: If n is 0 or less, the sum is 0.
        if (n <= 0)
        {
            return 0;
        }

        // Recursive Step: n^2 plus the sum of squares for the rest (n-1).
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// # Problem 2 #
    /// Using recursion, insert permutations of length 'size' from a list of 'letters'.
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base Case: If the current 'word' length equals 'size', we found a complete permutation.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive Step: Iterate through available letters and branch the recursion.
        for (int i = 0; i < letters.Length; i++)
        {
            char nextLetter = letters[i];

            // Generate the string of remaining letters (excluding the current one).
            string remainingLetters = letters.Remove(i, 1);

            // Recurse with the letter added to the word.
            PermutationsChoose(results, remainingLetters, size, word + nextLetter);
        }
    }

    /// <summary>
    /// # Problem 3 #
    /// Count how many ways there are to climb 's' stairs (1, 2, or 3 steps).
    /// Uses memoization (dynamic programming top-down approach).
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize memoization table on the first call
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Check memoization table (if result already computed)
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Base Cases
        if (s < 0)
            return 0; // Cannot have negative stairs
        if (s == 0)
            return 1; // One way to be at 0 stairs (the starting position)

        // The existing explicit base cases for 1, 2, and 3 are slightly redundant 
        // with the recursive formula and s=0 base case, but they are efficient.
        if (s == 1) return 1;
        if (s == 2) return 2; // (1+1, 2)
        if (s == 3) return 4; // (1+1+1, 1+2, 2+1, 3)

        // Recursive Step: Ways to climb 's' is the sum of ways to get to s-1, s-2, and s-3.
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        // Store result in memoization table
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// # Problem 4 #
    /// Using recursion, insert all possible binary strings for a given wildcard pattern.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Find the index of the first wildcard character
        int wildcardIndex = pattern.IndexOf('*');

        // Base Case: If no wildcard is found, the pattern is a complete binary string.
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Recursive Step: Replace the first '*' with '0' and then '1', and recurse.

        // 1. Case: Replace '*' with '0'
        string patternWithZero = pattern.Substring(0, wildcardIndex) + '0' + pattern.Substring(wildcardIndex + 1);
        WildcardBinary(patternWithZero, results);

        // 2. Case: Replace '*' with '1'
        string patternWithOne = pattern.Substring(0, wildcardIndex) + '1' + pattern.Substring(wildcardIndex + 1);
        WildcardBinary(patternWithOne, results);
    }

    /// <summary>
    /// # Problem 5 #
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list (Depth First Search).
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // 1. Path Initialization
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // 2. Check Boundary, Wall, and Visited Status (Pruning)
        if (x < 0 || y < 0 || x >= maze.Width || y >= maze.Height || maze.IsWall(x, y) || maze.IsVisited(x, y))
        {
            return;
        }

        // 3. Mark and Add to Path
        currPath.Add((x, y));
        maze.MarkVisited(x, y);

        // 4. Base Case: Reached the end
        if (maze.IsEnd(x, y))
        {
            // The extension method AsString() is used to format the path for output.
            results.Add(currPath.AsString());
        }
        else
        {
            // 5. Recursive Step: Explore Neighbors (Depth First Search)
            // Try all four directions: Up, Down, Left, Right

            SolveMaze(results, maze, x, y - 1, currPath); // Up
            SolveMaze(results, maze, x, y + 1, currPath); // Down
            SolveMaze(results, maze, x - 1, y, currPath); // Left
            SolveMaze(results, maze, x + 1, y, currPath); // Right
        }

        // 6. Backtrack (Cleanup for the next possible path)
        // Remove the current position from the path list
        currPath.RemoveAt(currPath.Count - 1);
        // Unmark as visited so that this square can be part of another, different path
        maze.UnmarkVisited(x, y);
    }
}