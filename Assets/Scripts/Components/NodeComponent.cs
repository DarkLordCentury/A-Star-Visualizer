using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeComponent : MonoBehaviour, IComparable<NodeComponent>
{
    [Header("Visible Variable")]
    [SerializeField] private NodeComponent NodeParent;
    [SerializeField] private float G;
    [SerializeField] private float H;

    public void SetNodeParent(NodeComponent _parent) { NodeParent = _parent; }
    public void SetG(float _value) { G = _value; }
    public void SetH(Vector3 _end) { H = Vector3.Magnitude(_end - transform.position); }

    public float GetG() { return G; }
    public float GetH() { return H; }
    public float GetF() { return GetG() + GetH(); }

    public void ResetNode()
    {
        NodeParent = null;
        G = 0;
        H = 0;
    }

    public int CompareTo(NodeComponent other)
    {
        if (other == null)
            return 1;

        return GetF().CompareTo(other.GetF());
    }

    public NodeComponent GetNodeParent() { return NodeParent; }
}
