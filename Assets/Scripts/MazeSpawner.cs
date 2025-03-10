using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;

    public GameObject BorderPrefab;

    public int Width = 23;
    public int Height = 15;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(i, j), Quaternion.identity).GetComponent < Cell>();
                c.WallLift.SetActive(maze[i, j].WallLeft);
                c.WallBottom.SetActive(maze[i, j].WallBottom);
            }
        }

        SpawnBorders();
    }

    private void SpawnBorders()
    {
        GameObject borders = Instantiate(BorderPrefab, Vector3.zero, Quaternion.identity);

        Transform horizontalWall = borders.transform.Find("HorizontalWall");
        Transform verticalWall = borders.transform.Find("VerticalWall");

        if (horizontalWall != null)
        {
            horizontalWall.position = new Vector3(Width / 2f, Height, 0);
            horizontalWall.localScale = new Vector3(Width + 1, 1, 1);
        }

        if (verticalWall != null)
        {
            verticalWall.position = new Vector3(Width, Height / 2f, 0);
            verticalWall.localScale = new Vector3(1, Height + 1, 1);
        }
    }



}
