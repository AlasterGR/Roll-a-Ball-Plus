using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    public GameObject playerBall;

    private Vector3 offset;
    private Vector3 offset1 ;
    public float zoomOut;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerBall.transform.position;
        offset1 = new Vector3(1, zoomOut, 1) ;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerBall.transform.position + offset;
    }
}
