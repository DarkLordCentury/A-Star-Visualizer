using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BinaryTree<int> test = new BinaryTree<int>();
        test.Add(5);
        test.Add(4);
        test.Add(6);
        test.Add(1);

        test.Remove(5);

        Debug.Log(test.Pop() + " : " + test.Count);
        Debug.Log(test.Pop() + " : " + test.Count);
        Debug.Log(test.Pop() + " : " + test.Count);
        //Debug.Log(test.Pop() + " : " + test.Count);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
