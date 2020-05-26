using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Variables")]
    public float MovementSpeed;
    public float ZoomSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraDelta = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += cameraDelta * Time.deltaTime * MovementSpeed;

        //Uses Scroll Wheel to zoom in and out
        float rawScrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (rawScrollInput != 0)
            Camera.main.orthographicSize += -Mathf.Sign(rawScrollInput) * ZoomSpeed * Time.deltaTime;
    }
}
