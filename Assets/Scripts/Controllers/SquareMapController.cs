using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMapController : MonoBehaviour
{
    [Header("Square Map Variables")]
    public GameObject SquareTile;
    public int Width;
    public int Height;
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateSquareMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateSquareMap()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    public GameObject GenerateTile(int _x, int _y)
    {
        GameObject tileObject = Instantiate(SquareTile, new Vector3(_x, _y), Quaternion.identity, transform);

        return tileObject;
    }

    public GameObject GenerateTile(float _x, float _y) { return GenerateTile((int)_x, (int)_y); }
}
