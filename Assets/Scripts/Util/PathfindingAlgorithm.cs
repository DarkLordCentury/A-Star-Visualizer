using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingAlgorithm
{
    private bool DoDiagonal;
    private BinaryTree<NodeComponent> Open;
    private HashSet<NodeComponent> Closed;
    private NodeComponent Start;
    private NodeComponent End;

    public void StartPathfinding(NodeComponent _startNode, NodeComponent _endNode, bool _doDiagonal)
    {
        Open = new BinaryTree<NodeComponent>();
        Closed = new HashSet<NodeComponent>();

        Start = _startNode;
        End = _endNode;
        DoDiagonal = _doDiagonal;

        Open.Add(Start);
    }

    // 1 => path found!, 0 => next step required, -1 => no path possible
    public int NextStep()
    {
        if (Open.Count == 0)
            return -1;

        NodeComponent currentTile = Open.Pop();

        if (currentTile == null)
            return -1;

        if (currentTile == End)
            return 1;

        List<NodeComponent> neighbours = currentTile.GetComponent<TileComponent>().GetCrossNeighbours();

        if(DoDiagonal)
            neighbours.AddRange(currentTile.GetComponent<TileComponent>().GetDiagonalNeighbours());

        for (int i = 0; i < neighbours.Count; i++)
        {
            NodeComponent currentNeighbour = neighbours[i];

            if (Open.Contains(currentNeighbour))
            {
                float currentF = currentTile.GetG() + currentNeighbour.GetH() + Vector3.Magnitude(currentNeighbour.transform.position - currentTile.transform.position);

                if (currentF < currentNeighbour.GetF())
                {
                    Open.Remove(currentNeighbour);
                    Open.Add(currentNeighbour);
                }
            }
            else if (Closed.Contains(currentNeighbour))
            {
                float currentF = currentTile.GetG() + currentNeighbour.GetH() + Vector3.Magnitude(currentNeighbour.transform.position - currentTile.transform.position);

                if (currentF < currentNeighbour.GetF())
                {
                    Closed.Remove(currentNeighbour);
                    Open.Add(currentNeighbour);
                }
            }
            else
            {
                FormatNode(currentNeighbour, currentTile);
                Open.Add(currentNeighbour);
            }
        }

        ChangeTileToColor(currentTile, Color.blue);
        Closed.Add(currentTile);

        return 0;
    }

    public void DisplayPath()
    {
        NodeComponent currentNode = End;

        while (currentNode != null)
        {
            ChangeTileToColor(currentNode, Color.green);
            currentNode = currentNode.GetNodeParent();
        }
    }

    public List<Vector3> GetPath()
    {
        List<Vector3> path = new List<Vector3>();
        NodeComponent currentNode = End;

        while (currentNode != null)
        {
            path.Add(currentNode.transform.position);
            currentNode = currentNode.GetNodeParent();
        }

        path.Reverse();
        return path;
    }

    void FormatNode(NodeComponent _node, NodeComponent _parent)
    {
        _node.SetNodeParent(_parent);
        _node.SetG(_parent.GetG() + Vector3.Magnitude(_node.transform.position - _parent.transform.position));
        _node.SetH(End.transform.position);
        ChangeTileToColor(_node, Color.yellow);
    }
    void ChangeTileToColor(NodeComponent _node, Color _color)
    {
        SpriteRenderer sprite = _node.GetComponent<SpriteRenderer>();
        sprite.color = _color;
    }

    public void ResetAffectedTiles()
    {
        List<NodeComponent> affectedTiles = new List<NodeComponent>();

        affectedTiles.AddRange(Open.Values);
        affectedTiles.AddRange(Closed);
        affectedTiles.Add(Start);
        affectedTiles.Add(End);

        for(int i = 0; i < affectedTiles.Count; i++)
        {
            NodeComponent affected = affectedTiles[i];

            affected.ResetNode();
            affected.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
    }
}
