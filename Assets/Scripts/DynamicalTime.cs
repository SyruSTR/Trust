using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicalTime : MonoBehaviour {

    public float speedRotate;
    public GameObject Pivot;


    private void FixedUpdate()
    {
        Pivot.transform.Rotate(Vector3.right * speedRotate * Time.deltaTime);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
