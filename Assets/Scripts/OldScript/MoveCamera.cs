﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float speed = 4;
    public LayerMask maskObstakls;
    

    private Vector3 _position;

    private void Start()
    {
        
        _position = target.InverseTransformPoint(transform.position);
    }

    private void Update()
    {
        var oldRotation = target.rotation;
        target.rotation = Quaternion.Euler(0, oldRotation.eulerAngles.y, 0);
        var currentPosition = target.InverseTransformPoint(_position);
        target.rotation = oldRotation;

        transform.position = Vector3.Lerp(transform.position, currentPosition, speed * Time.deltaTime);
        var currentRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, speed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(target.position, transform.position - target.position, out hit, Vector3.Distance(transform.position, target.position), maskObstakls))
        {
            transform.position = hit.point;
            transform.LookAt(target);
        }
    }
   
}