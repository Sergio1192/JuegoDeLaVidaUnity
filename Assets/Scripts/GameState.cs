using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject CellPrefab;

    private const int NUMBER_COLUMNS = 100;
    private const int NUMBER_ROWS = 100;

    private Dictionary<Vector2, Cell> _cells = new Dictionary<Vector2, Cell>();
    
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGrid()
    {
        for (int y = 0; y < NUMBER_ROWS; y++)
        {
            for (int x = 0; x < NUMBER_COLUMNS; x++)
            {
                var position = new Vector2(x, y);
                var cell = Instantiate(CellPrefab, position, Quaternion.identity, transform);

                _cells.Add(position, cell.GetComponent<Cell>());
            }
        }
    }


}
