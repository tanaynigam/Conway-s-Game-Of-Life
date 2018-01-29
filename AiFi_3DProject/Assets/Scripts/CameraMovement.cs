using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float speed = 6.0F;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Vector3 moveDirection = Vector3.zero;
    GameObject Camera;

    // Use this for initialization
    void Start () {
        Camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
        Camera.transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
        Camera.transform.Translate(Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
