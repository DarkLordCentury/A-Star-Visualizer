using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeNode<T> where T : IComparable<T>
{
    public T Value { get; protected set; }
    public BinaryTreeNode<T> Parent { get; protected set; }
    public BinaryTreeNode<T> LeftNode { get; protected set; }
    public BinaryTreeNode<T> RightNode { get; protected set; }

    public BinaryTreeNode(T _value)
    {
        Value = _value;
    }

    public BinaryTreeNode(T _value, BinaryTreeNode<T> _parent)
    {
        Value = _value;
        Parent = _parent;
    }

    public void SetParent(BinaryTreeNode<T> _parent) { Parent = _parent; }
    public void SetLeft(BinaryTreeNode<T> _left) { LeftNode = _left; }
    public void SetRight(BinaryTreeNode<T> _right) { RightNode = _right; }

    public void SetParentOfChildren()
    {
        if (LeftNode != null)
            LeftNode.SetParent(this);
        if (RightNode != null)
            RightNode.SetParent(this);
    }
}
