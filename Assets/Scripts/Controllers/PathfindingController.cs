using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    #region Global Variables
    public static bool FinishedPathfinding { get; protected set; }
    #endregion

    [Header("Pathfinding Variables")]
    public bool DoDiagonal;
    public float TimeBetweenFrames;
    public int StepsPerFrame;

    [Header("Visible Variables")]
    [SerializeField] private StartAndEndController StartAndEnd;
    [SerializeField] private bool IsPathfinding;
    [SerializeField] private float FrameTimer;
    [SerializeField] private PathfindingAlgorithm Pathfinding;
    

    // Start is called before the first frame update
    void Start()
    {
        StartAndEnd = GetComponent<StartAndEndController>();
        Pathfinding = new PathfindingAlgorithm();
        IsPathfinding = false;
        FinishedPathfinding = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPathfinding && FrameTimer < 0)
        {
            FrameTimer = TimeBetweenFrames;

            for(int i = 0; i < StepsPerFrame; i++)
            {
                int result = Pathfinding.NextStep();

                if(result == -1)
                {
                    IsPathfinding = false;
                    Debug.Log("Unpog");
                    break;
                }
                if(result == 1)
                {
                    IsPathfinding = false;
                    Pathfinding.DisplayPath();
                    SetPathOfStart();
                    break;
                }
            }
        }

        FrameTimer -= Time.deltaTime;
    }

    public void StartPathfinding()
    {
        Debug.Log("LETS GO!!");
        Pathfinding.StartPathfinding(StartAndEnd.GetStartNode(), StartAndEnd.GetEndNode(), DoDiagonal);
        IsPathfinding = true;
        FinishedPathfinding = false;
    }

    public void SetPathOfStart()
    {
        List<Vector3> path = Pathfinding.GetPath();

        MoveComponent startMove = StartAndEnd.StartObject.GetComponent<MoveComponent>();

        startMove.SetPath(path);
    }

    public void ResetPathfinding()
    {
        StartAndEnd.ResetStartPosition();
        Pathfinding.ResetAffectedTiles();
        IsPathfinding = false;
        FinishedPathfinding = true;
    }

    public void SetDoDiagonal(bool _doDiagonal) { DoDiagonal = _doDiagonal; }
}
