using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBattleManager : MonoBehaviour
{
    public GameObject cellPrefab;
    NewCellScript[,] grid;
    public float tickRate = 0.5f; // Speed 
    public int size = 20; // Size of the grid (20x20)
    public int playerTurn = 1; // 1 for Player 1, 2 for Player 2
    public int p1CellCount = 0; // Player 1 total cells
    public int p2CellCount = 0; // Player 2 total cells
    public int maxCellsPerPlayer = 100;
    public TextMeshProUGUI p1CountText;
    public TextMeshProUGUI p2CountText;
    public TextMeshProUGUI gameOverText;
    public GameObject gameOverButton;
    public GameObject startButton;
    //public TextMeshProUGUI accelerateButton;
    public bool gameStarted = false;
    public bool accelerated = false;
    float timer = 0f;

    public int maxStepsWithoutChange = 15;
    int stepsWithoutChange = 0;
    int previousP1Count, previousP2Count;

    // Start is called before the first frame update
    void Start()
    {
        grid = new NewCellScript[size, size];
        float spacing = 1.1f;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, y * spacing);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                grid[x, y] = cell.GetComponent<NewCellScript>();
                grid[x, y].xIndex = x;
                grid[x, y].yIndex = y;

                if (x == size / 2)
                {
                    GameObject cube = grid[x, y].transform.Find("Cube").gameObject; 
                    Renderer renderer = cube.GetComponent<Renderer>();
                    renderer.material.color = Color.black;
                    Debug.Log("hvboeivbo");
                    Debug.Log($"Renderer color: {renderer.material.color}");
                }
                else
                {
                    grid[x, y].SetColor(); 
                }
            }
        }

        p1CountText.text = "Player 1 Cells: 0";
        p2CountText.text = "Player 2 Cells: 0";
        gameOverText.enabled = false;
        gameOverButton.SetActive(false); ;
       
       
    }


    public void StartGame()
    {
        if (p1CellCount < maxCellsPerPlayer || p2CellCount < maxCellsPerPlayer)
        {
            Debug.Log("Players need to place all cells before starting.");
            return;
        }

        gameStarted = true;
        ClearBlackCells();
        Debug.Log("Game started.");
    }

    void ClearBlackCells()
    {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    GameObject cube = grid[x, y].transform.Find("Cube").gameObject;
                    Renderer renderer = cube.GetComponent<Renderer>();

                    // Check if the cell's color is black
                    if (renderer.material.color == Color.black)
                    {
                        renderer.material.color = Color.white;
                        Debug.Log($"Cell at ({x}, {y}) changed from black to white.");
                    }
                }
            }
    }


    void UpdateGrid()
    {
        bool[,] newStates = new bool[size, size];
        int[,] newOwners = new int[size, size]; //track player ownership

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                int neighbors = countNeighbors(x, y);
                bool alive = grid[x, y].alive;

                if (alive && (neighbors < 2 || neighbors > 3))
                {
                    newStates[x, y] = false; // Underpopulation or Overpopulation
                    newOwners[x, y] = grid[x, y].playerOwner; 
                }
                else if (!alive && neighbors == 3)
                {
                    newStates[x, y] = true; // Reproduction

                    // Determine which player should own this new cell
                    int player1Neighbors = countPlayerNeighbors(x, y, 1);
                    int player2Neighbors = countPlayerNeighbors(x, y, 2);
                    newOwners[x, y] = player1Neighbors > player2Neighbors ? 1 : 2;

                    // Handle counter updates based on color
                    HandleCounters(x, y, newOwners[x, y]);
                }
                else
                {
                    newStates[x, y] = alive; // Stays the same
                    newOwners[x, y] = grid[x, y].playerOwner; // Keep the same owner
                }
            }
        }

        // Apply the new states and owners, then update counts
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                grid[x, y].alive = newStates[x, y];
                grid[x, y].playerOwner = newOwners[x, y];
                grid[x, y].SetColor(); 
            }
        }

       
        p1CountText.text = "Player 1 Cells: " + p1CellCount;
        p2CountText.text = "Player 2 Cells: " + p2CellCount;

        // Check for win conditions
        // (Win condition logic remains unchanged)
    }

   
    void HandleCounters(int x, int y, int newOwner)
    {
        var cell = grid[x, y];

        // If the cell was white (unvisited), increase the counter for the new owner
        if (!cell.wasVisited)
        {
            if (newOwner == 1)
            {
                p1CellCount++;
            }
            else if (newOwner == 2)
            {
                p2CellCount++;
            }
            cell.wasVisited = true; 
        }
        else
        {
            // If the cell was already visited and is switching owners
            if (cell.playerOwner != newOwner)
            {
               
                if (cell.playerOwner == 1 && newOwner == 2)
                {
                    p1CellCount--; 
                    p2CellCount++; 
                }
                else if (cell.playerOwner == 2 && newOwner == 1)
                {
                    p2CellCount--; 
                    p1CellCount++; 
                }
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
    int countPlayerNeighbors(int x, int y, int player)
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
                    if (grid[newX, newY].alive && grid[newX, newY].playerOwner == player)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    void CheckForWinCondition()
    {
        if (p1CellCount == previousP1Count && p2CellCount == previousP2Count)
        {
            stepsWithoutChange++;
        }
        else
        {
            stepsWithoutChange = 0; // Reset if there is a change
        }

       
        previousP1Count = p1CellCount;
        previousP2Count = p2CellCount;

        // If both counts have remained the same for the max steps, end the game
        if (stepsWithoutChange >= maxStepsWithoutChange)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameStarted = false; 
        gameOverText.enabled = true; 
        gameOverButton.SetActive(true);
        if (p1CellCount > p2CellCount)
        {
            gameOverText.text = "Player 1 Wins!";
        }
        else if (p2CellCount > p1CellCount)
        {
            gameOverText.text = "Player 2 Wins!";
        }
        else
        {
            gameOverText.text = "It's a Tie!";
        }
    }

    void Update()
    {

        if (p1CellCount == 100 && p2CellCount == 100 && !gameStarted)
        {
            startButton.SetActive(true);
        }
        // starting the game
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
            Debug.Log("Game Started!");
            startButton.SetActive(false);
        }

        if (!gameStarted) return;

        timer += Time.deltaTime;

        if (timer >= tickRate)
        {
            UpdateGrid();
            CheckForWinCondition();
            timer = 0f;
        }
    }
}

