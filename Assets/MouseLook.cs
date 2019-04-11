﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public enum RotationAxis {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxis axes = RotationAxis.MouseXAndY;

    public float sensitivityHor = 9;
    public float sensitivityVert = 9;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;
    
    // Start is called before the first frame update
    void Start() {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        if (axes == RotationAxis.MouseX) {
            //поворот в горизонтальной плоскости
            transform.Rotate(0, Input.GetAxis("Mouse X")*sensitivityHor, 0);
        }
        else if (axes == RotationAxis.MouseY) {
            //поворот в вертикальной плоскости
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else {
            //комбинированный поворот
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
    }
}
