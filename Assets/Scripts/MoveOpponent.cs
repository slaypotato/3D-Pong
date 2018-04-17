using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOpponent : MonoBehaviour {

    public Transform ball;
    public Rigidbody rb;
	public Transform trf;
    public int speed;
    public Boundary bd = new Boundary();
    public GameController gc;
    private Vector3 move;
    private Vector3 init;

	// Use this for initialization
	void Start () {
        init = rb.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (PlayerPrefs.GetInt ("Diff") == 2) {
			moveFaster ();
		} else {
			moveNormal ();
		}
	}

	void moveNormal(){
		if (ball.position.z > 1&&gc.getStartGameStatus())
		{
			move = new Vector3(ball.position.x, 0.0f, 0.0f);
			rb.velocity = move * speed;
			rb.position = new Vector3(Mathf.Clamp(rb.position.x, bd.xMin, bd.xMax), rb.position.y, rb.position.z);
		}
		else
		{
			if (!gc.getStartGameStatus())
			{
				rb.position = init;
			}
		}
	}

	void moveFaster(){
		if (ball.position.z > 1 && gc.getStartGameStatus ()) {
			trf.position = new Vector3 (Mathf.Clamp (ball.position.x, bd.xMin, bd.xMax), trf.position.y, trf.position.z);
		}
	}
}
