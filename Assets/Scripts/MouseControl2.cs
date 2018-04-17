using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl2 : MonoBehaviour {

	//public Rigidbody rb;
	public Transform plr;
	public int speed;
	//public float tilt;
	public Boundary bd = new Boundary();

	//private Vector3 move;
	//private float pastPosition;
	private float currentPosition;

	private Ray ray;
	private RaycastHit hit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray,out hit))
		{
			if (hit.collider)
			{
				currentPosition = hit.point.x;

				//move = new Vector3(currentPosition, 0.0f, 0.0f);

				plr.position = new Vector3(Mathf.Clamp(currentPosition, bd.xMin, bd.xMax),plr.position.y,plr.position.z);
				//rb.rotation = Quaternion.Euler(0.0f, rb.velocity.x * -tilt, 0.0f);
				//Debug.Log ("Current mouse: "+currentPosition);
				//Debug.Log ("Hit Point: "+hit.point.x);
				//Debug.Log("Move: " + move.x + ", " + move.y + ", " + move.z);
				//Debug.Log("Positon: " + rb.position.x + ", " + rb.position.y + ", " + rb.position.z);

			}
		}
	}
}
