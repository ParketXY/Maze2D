using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int x, y;
    public bool WallLeft = true, WallBottom = true;
    public bool WallRight = true, WallTop = true; // Добавлены верхняя и правая стены
    public bool Visited = false;
}

public class MazeGenerator : MonoBehaviour
{
    public int Width = 23;
    public int Height = 15;
    private MazeGeneratorCell[,] maze;

    private void Start()
    {
        GenerateMaze();
    }

    public MazeGeneratorCell[,] GenerateMaze()
    {
        maze = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                maze[x, y] = new MazeGeneratorCell { x = x, y = y };

                // Устанавливаем внешние стены
                if (x == Width - 1) maze[x, y].WallRight = true;
                if (y == Height - 1) maze[x, y].WallTop = true;
            }
        }

        RemoveWalls(maze, UnityEngine.Random.Range(0, Width), UnityEngine.Random.Range(0, Height));
        return maze;
    }

    private void RemoveWalls(MazeGeneratorCell[,] maze, int startX, int startY)
    {
        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        MazeGeneratorCell current = maze[startX, startY];
        current.Visited = true;
        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Pop();
            List<MazeGeneratorCell> neighbors = new List<MazeGeneratorCell>();

            int x = current.x;
            int y = current.y;

            if (x > 0 && !maze[x - 1, y].Visited) neighbors.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) neighbors.Add(maze[x, y - 1]);
            if (x < Width - 1 && !maze[x + 1, y].Visited) neighbors.Add(maze[x + 1, y]);
            if (y < Height - 1 && !maze[x, y + 1].Visited) neighbors.Add(maze[x, y + 1]);

            if (neighbors.Count > 0)
            {
                MazeGeneratorCell chosen = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
                RemoveWall(current, chosen);
                chosen.Visited = true;
                stack.Push(current);
                stack.Push(chosen);
            }
        }
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.x == b.x)
        {
            if (a.y > b.y)
            {
                if (a.y > 0) a.WallBottom = false;
                if (b.y < Height - 1) b.WallTop = false;
            }
            else
            {
                if (b.y > 0) b.WallBottom = false;
                if (a.y < Height - 1) a.WallTop = false;
            }
        }
        else
        {
            if (a.x > b.x)
            {
                if (a.x > 0) a.WallLeft = false;
                if (b.x < Width - 1) b.WallRight = false;
            }
            else
            {
                if (b.x > 0) b.WallLeft = false;
                if (a.x < Width - 1) a.WallRight = false;
            }
        }
    }

}
