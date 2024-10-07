using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cellPrefab;
    CellScript[,] grid;
    float timer = 0f;
    public float tickRate = 0.5f; // Speed of simulation
    public int size = 10;

    // Start is called before the first frame update
    void Start()
    {
        grid = new CellScript[size, size];
        float spacing = 1.1f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, y * spacing);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                grid[x, y] = cell.GetComponent<CellScript>();
                grid[x, y].alive = (Random.value > 0.5f); // Randomly set cell alive or dead
                grid[x, y].xIndex = x;
                grid[x, y].yIndex = y;
                grid[x, y].SetColor(); // Update the initial color
            }
        }
    }

    public int countNeighbors(int x, int y)
    {
        int count = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int newX = x + dx;
                int newY = y + dy;

                if (newX >= 0 && newX < size && newY >= 0 && newY < size)
                {
                    if (grid[newX, newY].alive)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    void UpdateGrid()
    {
        bool[,] newStates = new bool[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                int neighbors = countNeighbors(x, y);
                bool alive = grid[x, y].alive;

                if (alive && (neighbors < 2 || neighbors > 3))
                {
                    newStates[x, y] = false; // Underpopulation or Overpopulation
                }
                else if (!alive && neighbors == 3)
                {
                    newStates[x, y] = true; // Reproduction
                }
                else
                {
                    newStates[x, y] = alive; // Stays the same
                }
            }
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                grid[x, y].alive = newStates[x, y];
                grid[x, y].SetColor(); // Update the visual representation
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickRate)
        {
            UpdateGrid();
            timer = 0f;
        }
    }
}

