using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    [Header("Mouse Variables")]
    public SquareMapController Map;
    public LayerMask TileLayerMask;

    [Header("Checking Variables")]
    public DragAndDrop StartDragger;
    public DragAndDrop EndDragger;

    // Update is called once per frame
    void Update()
    {
        if (PathfindingController.FinishedPathfinding && !StartDragger.IsDragging && !EndDragger.IsDragging)
        {
            if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 rawMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 formattedPosition = new Vector3(rawMousePosition.x, rawMousePosition.y, 0);

                TileComponent tile = TileComponent.GetTileAt(formattedPosition);

                if (Input.GetMouseButton(0) && tile == null)
                    Map.GenerateTile(Mathf.RoundToInt(formattedPosition.x), Mathf.RoundToInt(formattedPosition.y));

                else if (Input.GetMouseButton(1) && tile != null)
                    tile.DestroyTile();
            }
        }
    }
}
