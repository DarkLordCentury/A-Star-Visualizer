using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileComponent : MonoBehaviour
{
    [Header("Visible Variables")]
    [SerializeField] private bool HasStart;
    [SerializeField] private bool HasEnd;

    // Start is called before the first frame update
    void Start()
    {
        HasStart = false;
        HasEnd = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Start")
            HasStart = true;

        if (col.tag == "End")
            HasEnd = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Start")
            HasStart = false;

        if (col.tag == "End")
            HasEnd = false;
    }

    public void DestroyTile()
    {
        if (!HasEnd && !HasStart)
            Destroy(gameObject);
    }

    public List<NodeComponent> GetCrossNeighbours()
    {
        List<NodeComponent> neighbours = new List<NodeComponent>();
        AddNeighbourToList(neighbours, Vector3.right);
        AddNeighbourToList(neighbours, Vector3.left);
        AddNeighbourToList(neighbours, Vector3.up);
        AddNeighbourToList(neighbours, Vector3.down);

        return neighbours;
    }
    public List<NodeComponent> GetDiagonalNeighbours()
    {
        List<NodeComponent> neighbours = new List<NodeComponent>();
        AddNeighbourToList(neighbours, new Vector3(1, 1));
        AddNeighbourToList(neighbours, new Vector3(1, -1));
        AddNeighbourToList(neighbours, new Vector3(-1, 1));
        AddNeighbourToList(neighbours, new Vector3(-1, -1));

        return neighbours;
    }
    public NodeComponent GetNeighbour(Vector3 _direction)
    {
        TileComponent tile = TileComponent.GetTileAt(transform.position + _direction);

        if (tile == null)
            return null;

        return tile.GetComponent<NodeComponent>();
    }
    public void AddNeighbourToList(List<NodeComponent> _neighbours, Vector3 _direction)
    {
        NodeComponent neighbour = GetNeighbour(_direction);

        if (neighbour != null)
            _neighbours.Add(neighbour);
    }

    public bool HasStartObject() { return HasStart; }
    public bool HasEndObject() { return HasEnd; }

    public static TileComponent GetTileAt(Vector3 _position)
    {
        RaycastHit2D mouseRaycast = Physics2D.Raycast(_position, Vector3.forward, 0, 1 << LayerMask.NameToLayer("Tile"));

        if (mouseRaycast.collider == null)
            return null;

        return mouseRaycast.collider.GetComponent<TileComponent>();
    }
}
