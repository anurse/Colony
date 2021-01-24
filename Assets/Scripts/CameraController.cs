using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public float moveSpeed;
  public float zoomSpeed;
  public float rotateSpeed;

  public float minZoomDist;
  public float maxZoomDist;

  private Camera cam;
  private float mouseX;
  private float mouseY;

  void Awake()
  {
    cam = Camera.main;
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Zoom();
    Rotate();
  }

  void FocusOnPosition(Vector3 pos)
  {
    transform.position = pos;
  }

  void Zoom()
  {
    float scrollInput = Input.GetAxis("Mouse ScrollWheel");
    float dist = Vector3.Distance(transform.position, cam.transform.position);

    if (dist < minZoomDist && scrollInput > 0.0f)
    {
      return;
    }
    else if (dist > maxZoomDist && scrollInput < 0.0f)
    {
      return;
    }

    cam.transform.position += cam.transform.forward * scrollInput * zoomSpeed;
  }

  void Move()
  {
    float xInput = Input.GetAxis("Horizontal");
    float zInput = Input.GetAxis("Vertical");

    Vector3 dir = transform.forward * zInput + transform.right * xInput;
    transform.position += dir * moveSpeed * Time.deltaTime;
  }

  void Rotate()
  {
    if (Input.GetMouseButton(1))
    {
      mouseX += Input.GetAxis("Mouse X") * rotateSpeed;
      mouseY -= Input.GetAxis("Mouse Y") * rotateSpeed;
      mouseY = Mathf.Clamp(mouseY, -30, 45);
      transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
  }
}
