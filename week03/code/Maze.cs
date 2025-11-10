using System;
using System.Collections.Generic;

/// <summary>
/// Maze class that tracks player position and movement through a maze.
/// The maze is represented as a dictionary where keys are (x, y) coordinates
/// and values are bool arrays: [up, right, down, left]
/// </summary>
public class Maze
{
    private Dictionary<(int x, int y), bool[]> _map;
    private int _currentX;
    private int _currentY;

    /// <summary>
    /// Initialize a new maze with the given map.
    /// Map keys are (x, y) coordinates and values are bool arrays [up, right, down, left]
    /// </summary>
    public Maze(Dictionary<(int x, int y), bool[]> map)
    {
        _map = map ?? new Dictionary<(int x, int y), bool[]>();
        _currentX = 1;
        _currentY = 1;
    }

    /// <summary>
    /// Get the current status of the maze (player location)
    /// </summary>
    public string GetStatus()
    {
        return $"Current location (x={_currentX}, y={_currentY})";
    }

    /// <summary>
    /// Attempt to move up (decrease Y)
    /// </summary>
    public void MoveUp()
    {
        if (!CanMove(0)) // up is index 0
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentY--;
    }

    /// <summary>
    /// Attempt to move right (increase X)
    /// </summary>
    public void MoveRight()
    {
        if (!CanMove(1)) // right is index 1
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentX++;
    }

    /// <summary>
    /// Attempt to move down (increase Y)
    /// </summary>
    public void MoveDown()
    {
        if (!CanMove(2)) // down is index 2
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentY++;
    }

    /// <summary>
    /// Attempt to move left (decrease X)
    /// </summary>
    public void MoveLeft()
    {
        if (!CanMove(3)) // left is index 3
        {
            throw new InvalidOperationException("Can't go that way!");
        }
        _currentX--;
    }

    /// <summary>
    /// Check if a move in the given direction is allowed
    /// direction: 0=up, 1=right, 2=down, 3=left
    /// </summary>
    private bool CanMove(int direction)
    {
        var key = (_currentX, _currentY);

        // Check if current position exists in map
        if (!_map.ContainsKey(key))
            return false;

        var cell = _map[key];

        // Check if move is allowed from current cell
        if (!cell[direction])
            return false;

        // Calculate destination
        int nextX = _currentX;
        int nextY = _currentY;

        if (direction == 0) nextY--; // up
        else if (direction == 1) nextX++; // right
        else if (direction == 2) nextY++; // down
        else if (direction == 3) nextX--; // left

        // Check if destination exists in map
        return _map.ContainsKey((nextX, nextY));
    }
}
