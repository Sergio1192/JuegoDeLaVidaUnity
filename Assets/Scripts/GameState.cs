using System;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameObject CellPrefab;

    private const int NUMBER_COLUMNS = 50;
    private const int NUMBER_ROWS = 50;
    private const float SLEEP_TIME = 0.1f;

    private bool _isPause;

    private Dictionary<Vector2Int, Cell> _cells = new Dictionary<Vector2Int, Cell>();
    
    // Start is called before the first frame update
    void Start()
    {
        _isPause = true;

        CreateGrid();
        Action();
    }

    void CreateGrid()
    {
        for (int y = 0; y < NUMBER_ROWS; y++)
        {
            for (int x = 0; x < NUMBER_COLUMNS; x++)
            {
                var cellGameObject = Instantiate(CellPrefab, transform);

                Cell cell = cellGameObject.GetComponent<Cell>();
                cell.Init(x, y);

                _cells.Add(cell.Position, cell);
            }
        }
    }

    private void Action()
    {
        if (!_isPause)
        {
            Dictionary<Vector2Int, bool> newStage = new Dictionary<Vector2Int, bool>();
            foreach (Cell cell in _cells.Values)
            {
                //int numberNeighbours = cell.GetNumberNeighbours();
                int numberNeighbours = GetNumberNeighbours(cell.Position);
                if (cell.IsAlive)
                {
                    if (Rules.Kill(numberNeighbours))
                    {
                        newStage.Add(cell.Position, false);
                    }
                }
                else
                {
                    if (Rules.Born(numberNeighbours))
                    {
                        newStage.Add(cell.Position, true);
                    }
                }
            }

            foreach (var position in newStage.Keys)
            {
                Cell cell = _cells[position];

                if (newStage[position])
                {
                    cell.Born();
                }
                else
                {
                    cell.Kill();
                }
            }
        }

        Invoke(nameof(Action), SLEEP_TIME);
    }

    public int GetNumberNeighbours(Vector2Int position)
    {
        int x = position.x;
        int y = position.y;

        var abajoIzquierda =  new Vector2Int(Mod(x - 1, NUMBER_COLUMNS), Mod(y - 1, NUMBER_ROWS));
        var abajo =           new Vector2Int(Mod(x    , NUMBER_COLUMNS), Mod(y - 1, NUMBER_ROWS));
        var abajoDerecha =    new Vector2Int(Mod(x + 1, NUMBER_COLUMNS), Mod(y - 1, NUMBER_ROWS));
        var izquierda =       new Vector2Int(Mod(x - 1, NUMBER_COLUMNS), Mod(y    , NUMBER_ROWS));
        var derecha =         new Vector2Int(Mod(x + 1, NUMBER_COLUMNS), Mod(y    , NUMBER_ROWS));
        var arribaIzquierda = new Vector2Int(Mod(x - 1, NUMBER_COLUMNS), Mod(y + 1, NUMBER_ROWS));
        var arriba =          new Vector2Int(Mod(x    , NUMBER_COLUMNS), Mod(y + 1, NUMBER_ROWS));
        var arribaDerecha =   new Vector2Int(Mod(x + 1, NUMBER_COLUMNS), Mod(y + 1, NUMBER_ROWS));

        int numberNeighbours = _cells[abajoIzquierda].GetIsAlive() +
                               _cells[abajo].GetIsAlive() +
                               _cells[abajoDerecha].GetIsAlive() +
                               _cells[izquierda].GetIsAlive() +
                               _cells[derecha].GetIsAlive() +
                               _cells[arribaIzquierda].GetIsAlive() +
                               _cells[arriba].GetIsAlive() +
                               _cells[arribaDerecha].GetIsAlive();

        return numberNeighbours;
    }

    private int Mod(int a, int n)
    {
        int r = a % n;

        return r < 0 ? a + n : r;
    }

    // Events
    private void OnGUI()
    {
        if (Event.current.isKey)
        {
            _isPause = !_isPause;
        }
    }
}
