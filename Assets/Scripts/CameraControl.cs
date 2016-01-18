/* Copyright (C) 2015 George Erfesoglou*/
using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    Vector3 MyMousePos = new Vector3(Screen.width/2, Screen.height/2, 0);
    public float MouseSens = 1.0f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    public Vector3 LastPos = new Vector3();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //transform.Translate(Vector3.ClampMagnitude((Vector3.right * -Input.GetAxis("Mouse X") * MouseSens), 10f));
        //transform.Translate(Vector3.ClampMagnitude( transform.up * -Input.GetAxis("Mouse Y") * MouseSens, 10f), Space.World);
        lockedRotationX();
        lockedRotationY();
        //transform.Rotate(Vector3.up * - Input.GetAxis("Mouse X") * MouseSens);
    }

    void lockedRotationX()
    {
        rotationX += Input.GetAxis("Mouse Y") * MouseSens;
        rotationX = Mathf.Clamp(rotationX, -13, 13);

        transform.localEulerAngles = new Vector3(-rotationX, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    void lockedRotationY()
    {
        rotationY += Input.GetAxis("Mouse X") * MouseSens;
        rotationY = Mathf.Clamp(rotationY, -13, 13);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationY, transform.localEulerAngles.z);
    }
}
