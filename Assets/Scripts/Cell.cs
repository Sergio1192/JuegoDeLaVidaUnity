using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material DeadMaterial;
    public Material AliveMaterial;

    private bool _isAlive;

    // Start is called before the first frame update
    void Start()
    {
        Dead();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead()
    {
        _isAlive = false;

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = DeadMaterial;
    }

    public void Alive()
    {
        _isAlive = true;

        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = AliveMaterial;
    }

    private void OnMouseDown()
    {
        if (_isAlive)
            Dead();
        else
            Alive();
    }
}
