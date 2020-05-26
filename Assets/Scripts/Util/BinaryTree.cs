using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree<T> where T : IComparable<T>
{
    public int Count { get { return AllValues.Count; } }

    HashSet<T> AllValues;
    BinaryTreeNode<T> Root;

    public BinaryTree()
    {
        AllValues = new HashSet<T>();
    }

    public void Add(T _item)
    {
        AllValues.Add(_item);

        if (Root == null)
            Root = new BinaryTreeNode<T>(_item);
        else
        {
            BinaryTreeNode<T> currentNode = Root;

            while(true)
            {
                if (_item.CompareTo(currentNode.Value) < 0)
                {
                    if (currentNode.LeftNode == null)
                    {
                        currentNode.SetLeft(new BinaryTreeNode<T>(_item, currentNode));
                        break;
                    }
                    else
                        currentNode = currentNode.LeftNode;
                }
                else
                {
                    if (currentNode.RightNode == null)
                    {
                        currentNode.SetRight(new BinaryTreeNode<T>(_item, currentNode));
                        break;
                    }
                    else
                        currentNode = currentNode.RightNode;
                }
            }
        }
    }

    public T Pop()
    {
        if (Root == null)
            throw new IndexOutOfRangeException();
        
        BinaryTreeNode<T> currentMin = Root;

        while(true)
        {
            if(currentMin.LeftNode == null)
            {
                Remove(currentMin);
                return currentMin.Value;
            }
            else
            {
                currentMin = currentMin.LeftNode;
            }
        }
    }

    void Remove(BinaryTreeNode<T> _remove)
    {
        BinaryTreeNode<T> replacement;

        if (_remove.LeftNode == null && _remove.RightNode == null)
            replacement = null;
        else if (_remove.LeftNode == null)
            replacement = _remove.RightNode;
        else if (_remove.RightNode == null)
            replacement = _remove.LeftNode;
        else
        {
            BinaryTreeNode<T> closest = GetMax(_remove.LeftNode);
            Remove(closest);
            AllValues.Add(closest.Value);
            closest.SetLeft(closest == _remove.LeftNode ? null : _remove.LeftNode);
            closest.SetRight(_remove.RightNode);

            closest.SetParentOfChildren();

            replacement = closest;
        }

        if (replacement != null)
            replacement.SetParent(_remove.Parent);

        if (_remove.Parent == null)
        {
            Root = replacement;
        }
        else
        {
            if (_remove.Parent.LeftNode == _remove)
                _remove.Parent.SetLeft(replacement);
            else
                _remove.Parent.SetRight(replacement);

            _remove.Parent.SetParentOfChildren();
        }

        AllValues.Remove(_remove.Value);
    }

    public void Remove(T _item)
    {
        if (!AllValues.Contains(_item))
            return;

        BinaryTreeNode<T> currentNode = Root;

        while(true)
        {
            if(currentNode.Value.CompareTo(_item) == 0)
            {
                Remove(currentNode);
                break;
            }

            if (_item.CompareTo(currentNode.Value) < 0)
                currentNode = currentNode.LeftNode;
            else
                currentNode = currentNode.RightNode;
        }
    }

    public bool Contains(T _item) { return AllValues.Contains(_item); }

    BinaryTreeNode<T> GetMin(BinaryTreeNode<T> _start)
    {
        BinaryTreeNode<T> currentNode = _start;

        while (true)
        {
            if (currentNode.LeftNode == null)
                return currentNode;

            currentNode = currentNode.LeftNode;
        }
    }

    BinaryTreeNode<T> GetMax(BinaryTreeNode<T> _start)
    {
        BinaryTreeNode<T> currentNode = _start;

        while (true)
        {
            if (currentNode.RightNode == null)
                return currentNode;

            currentNode = currentNode.RightNode;
        }
    }

    public IEnumerable<T> Values { get { return AllValues; } }
}
