using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_2 : MonoBehaviour {

    public Camera cam;
    public Transform target;
    public float speedX = 360f;
    public float speedY = 240f;
    public float limitY = 40f;
    public float minDistance = 1.5f;
    private float _maxDistance;
    private Vector3 _localPosition;
    private float _currentYRotation;

    private Vector3 _position {
        get { return transform.position; }
        set { transform.position = value; }
    }



    void Start () {
		
	}
	
	
	void Update () {
		
	}
}
