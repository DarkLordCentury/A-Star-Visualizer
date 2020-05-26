using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [Header("Movement Variables")]
    public float Speed;

    [Header("Visible Variables")]
    [SerializeField] private List<Vector3> Path;
    [SerializeField] private Vector3 CurrentTarget;

    void Start()
    {
        CurrentTarget = transform.position;
        Path = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTarget == transform.position)
        {
            if (Path.Count != 0)
            {
                CurrentTarget = Path[0];
                Path.RemoveAt(0);
            }
            else
                CurrentTarget = transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, CurrentTarget, Speed * Time.deltaTime);
    }

    public void SetPath(List<Vector3> _path) { Path = _path; }
}
