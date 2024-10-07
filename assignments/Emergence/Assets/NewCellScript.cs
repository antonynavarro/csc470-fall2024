using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCellScript : MonoBehaviour
{
    public Renderer cube;
    public bool alive;
    public bool wasVisited = false;
    public int xIndex = -1;
    public int yIndex = -1;
    public Color aliveColorP1; // Player 1 color (Red)
    public Color aliveColorP2; // Player 2 color (Blue)
    public Color deadColor;
    public int playerOwner = 0; // 0 = no one, 1 = Player 1, 2 = Player 2

    GameBattleManager manager;

    // Start is called before the first frame update
    void Start()
    {
        SetColor();
        GameObject gmObj = GameObject.Find("GameBattleManagerObject");
        manager = gmObj.GetComponent<GameBattleManager>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && IsMouseOver())
        {
            SelectCell();
        }
    }

    private void OnMouseDown()
    {
        SelectCell();
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0)) // mouse hovering
        {
            SelectCell();
        }
    }

    void SelectCell()
    {
        // Check that the game hasn't started and it's the correct player's turn
        if (!manager.gameStarted && !alive)
        {
            if (manager.playerTurn == 1 && xIndex < manager.size / 2 && manager.p1CellCount < manager.maxCellsPerPlayer)
            {
                alive = true;
                playerOwner = 1;
                manager.p1CellCount++;
                SetColor();
                manager.p1CountText.text = "Player 1 Cells: " + manager.p1CellCount;

                if (manager.p1CellCount == manager.maxCellsPerPlayer)
                {
                    manager.playerTurn = 2; // Switch to Player 2 after Player 1 finishes
                }
            }
            else if (manager.playerTurn == 2 && xIndex >= manager.size / 2 && manager.p2CellCount < manager.maxCellsPerPlayer)
            {
                alive = true;
                playerOwner = 2;
                manager.p2CellCount++;
                SetColor();
                manager.p2CountText.text = "Player 2 Cells: " + manager.p2CellCount;
            }
        }
    }

    public void SetColor()
    {
        if (alive)
        {
            float darkeningFactor = 0.7f;
            cube.material.color = playerOwner == 1 ? aliveColorP1 * darkeningFactor : aliveColorP2 * darkeningFactor;
        }
        else
        {
            if (playerOwner == 1)
            {
                cube.material.color = aliveColorP1; // Lighter color when dead for Player 1
            }
            else if (playerOwner == 2)
            {
                cube.material.color = aliveColorP2; // Lighter color when dead for Player 2
            }
            
        }
    }


        bool IsMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == gameObject;
        }

        return false;
    }
}
