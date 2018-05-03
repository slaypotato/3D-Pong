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
	private Vector3 current;
	private Vector3 destiny;

	// Use this for initialization
	void Start () {
        init = trf.position;
	}
	
	// Update is called once per frame
	void Update () {
		moveFaster ();
        if (!gc.getStartGameStatus())
        {
            trf.position = init;
        }
	}
	
	void moveFaster(){
		if (ball.position.z > 1 && gc.getStartGameStatus ()) {
			current = trf.position;
			destiny = ball.position;
            //Debug.Log(trf.position.x);
            if (Mathf.Abs(destiny.x - current.x) <= speed) {
				trf.position = new Vector3 (Mathf.Clamp (ball.position.x, bd.xMin, bd.xMax), trf.position.y, trf.position.z);
			} else {
				if (destiny.x > current.x){
					trf.position = new Vector3(Mathf.Clamp (trf.position.x+speed,bd.xMin,bd.xMax), trf.position.y, trf.position.z);
				} else {
					trf.position = new Vector3(Mathf.Clamp (trf.position.x-speed,bd.xMin,bd.xMax), trf.position.y, trf.position.z);				
				}
                
			}
            //Debug.Log(trf.position.x);
        } 
	}
}
