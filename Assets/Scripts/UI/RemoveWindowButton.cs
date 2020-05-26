using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWindowButton : MonoBehaviour
{
    [Header("Window Variables")]
    public GameObject Window;

    public void RemoveWindow() { Destroy(Window); }
}
