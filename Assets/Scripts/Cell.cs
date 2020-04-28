using System.Linq;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material DeadMaterial;
    public Material AliveMaterial;

    private static float WIDTH;
    private static float HEIGHT;

    public bool IsAlive { get; private set; }
    public Vector2Int Position { get; private set; }

    void Awake()
    {
        WIDTH = this.transform.localScale.x;
        HEIGHT = this.transform.localScale.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        Kill();
    }

    // Functions
    public void Init(int x, int y)
    {
        Position = new Vector2Int(x, y);
        transform.position = new Vector2((x * WIDTH) + (WIDTH / 2), (y * HEIGHT) + (HEIGHT / 2));
    }

    public void Kill()
    {
        IsAlive = false;

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = DeadMaterial;
    }

    public void Born()
    {
        IsAlive = true;

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = AliveMaterial;
    }

    public int GetNumberNeighbours()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, WIDTH);
        var cells = colliders.Select(c => c.GetComponentInParent<Cell>());
        var aliveCells = cells.Where(c => c.IsAlive && c.Position != Position);
        var result = aliveCells.Count();

        return result;
    }

    public int GetIsAlive()
    {
        return IsAlive ? 1 : 0;
    }

    // Events
    private void OnMouseDown()
    {
        if (IsAlive)
            Kill();
        else
            Born();
    }
}
