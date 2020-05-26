using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAndEndController : MonoBehaviour
{
    [Header("Start Variables")]
    public GameObject StartPrototype;
    public DragAndDrop StartDragger;

    [Header("End Variables")]
    public GameObject EndPrototype;
    public DragAndDrop EndDragger;

    [Header("Visible Variables")]
    public Vector3 StartInitialPosition;
    public GameObject StartObject;
    public GameObject EndObject;

    // Start is called before the first frame update
    void Start()
    {
        StartDragger.SetGenerateObjectAction(GenerateStartObject);
        EndDragger.SetGenerateObjectAction(GenerateEndObject);
    }

    void GenerateStartObject(Vector3 _position)
    {
        Vector3 raisedPosition = new Vector3(_position.x, _position.y, 1);

        RaycastHit2D mouseRaycast = Physics2D.Raycast(raisedPosition, Vector3.forward, 2);

        if (mouseRaycast.collider != null && mouseRaycast.collider.tag == "Tile")
        {
            TileComponent tile = mouseRaycast.collider.GetComponent<TileComponent>();

            if (!tile.HasStartObject() && !tile.HasEndObject())
            {
                if (StartObject != null)
                    Destroy(StartObject);

                StartObject = Instantiate(StartPrototype);
                StartObject.transform.position = new Vector3(Mathf.RoundToInt(_position.x), Mathf.RoundToInt(_position.y), 0);
                StartInitialPosition = StartObject.transform.position;
            }
        }
    }

    void GenerateEndObject(Vector3 _position)
    {
        Vector3 raisedPosition = new Vector3(_position.x, _position.y, 1);

        RaycastHit2D mouseRaycast = Physics2D.Raycast(raisedPosition, Vector3.forward, 2);

        if (mouseRaycast.collider != null && mouseRaycast.collider.tag == "Tile")
        {
            TileComponent tile = mouseRaycast.collider.GetComponent<TileComponent>();

            if (!tile.HasStartObject() && !tile.HasEndObject())
            {
                if (EndObject != null)
                    Destroy(EndObject);

                EndObject = Instantiate(EndPrototype);
                EndObject.transform.position = new Vector3(Mathf.RoundToInt(_position.x), Mathf.RoundToInt(_position.y), 0);
            }
        }
    }

    NodeComponent GetNodeComponentOfBoundary(GameObject _boundary)
    {
        TileComponent tile = TileComponent.GetTileAt(_boundary.transform.position);

        return tile.GetComponent<NodeComponent>();
    }

    public NodeComponent GetStartNode() { return GetNodeComponentOfBoundary(StartObject); }
    public NodeComponent GetEndNode() { return GetNodeComponentOfBoundary(EndObject); }

    public void ResetStartPosition()
    {
        GenerateStartObject(StartInitialPosition);
    }
}
