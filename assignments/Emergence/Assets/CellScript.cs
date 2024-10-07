using UnityEngine;

public class CellScript : MonoBehaviour
{
    public Renderer cube;

    public bool alive;
    public int xIndex = -1;
    public int yIndex = -1;
    public Color aliveColor;
    public Color deadColor;

    GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        //SetColor();
        GameObject gmObj = GameObject.Find("GameManagerObject");
        manager = gmObj.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        alive = !alive;
        SetColor();
        Debug.Log($"Cell at ({xIndex}, {yIndex}) new alive state: {alive}");

        //int voisinCount = GameManager.CountNeibors;
    }

    public void SetColor()
    {
        if (alive)
        {
            cube.material.color = aliveColor;
        }
        else
        {
            cube.material.color = deadColor;
        }

    }
}
