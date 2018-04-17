using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBall : MonoBehaviour {

    private Vector3 accel;
    private Vector3 dir;
    private Vector3 old, current = new Vector3(0, 0, 0);
    private int initSpeed;
    private AudioSource hitSound;

    public int speed, maxSpeed;
    public int increment;
    public float min;
    public float max;
    public Rigidbody rb;

    public GameController gc;

    // Use this for initialization
    void Start()
    {
        if (!gc.getStartGameStatus())
        {
            initSpeed = speed;
            gc.setStartGameStatus(true);
            accel = new Vector3(rb.position.x, rb.position.y, startZ() * initSpeed);
            rb.AddForce(accel, ForceMode.Force);
        }
        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("GameStatus: " + gc.getStartGameStatus());
        if ((!gc.getStartGameStatus()) && (!gc.getEndGame()))
        {
            rb.AddForce(new Vector3(rb.position.x, rb.position.y, startZ() * speed));
            gc.setStartGameStatus(true);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Wall")
        {
            current = rb.position;
            collisionDetected(c);
        }
        if (c.gameObject.tag == "Player" || c.gameObject.tag == "Opponent")
        {
            speed = speedIncrease(speed);
            collisionDetected(c);
        }
        hitSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            string goal = other.gameObject.name;
            //Debug.Log(other.gameObject.name);
            if (goal.Equals("PlayerGoal"))
            {
                gc.IncreaseOpponentScore();
                respawn();
            }
            else if (goal.Equals("OpponentGoal"))
            {
                gc.IncreasePlayerScore();
                respawn();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            Debug.Log("Out of Boundary");
            respawn();
        }
    }

    private int startZ()
    {
        int rand;
        do
        {
            rand = Random.Range(-10, 10);
        } while (rand == 0);
        if (rand > 0)
            rand = 1;
        else
            rand = -1;
        return rand;
    }

    private int speedIncrease(int s)
    {
        if (s >= maxSpeed)
        {
            s = 255;
        }
        else
        {
            s = s + increment;
            //Debug.Log("New Speed: " + s);
        }
        return s;
    }

    private void respawn()
    {
        rb.position = new Vector3(0, 0.5f, 0);
        rb.velocity = new Vector3(0, 0, 0);
        gc.setStartGameStatus(false);
        speed = initSpeed;
    }

    private void collisionDetected(Collision c)
    {
        old = current;
        //Debug.Log("Old: "+old.x+", "+ old.y+", "+ old.z);
        //Debug.Log("Current: "+current.x+", "+ current.y+", "+ current.z);
        dir = c.contacts[0].point - rb.transform.position;
        dir = -dir.normalized;
        //Debug.Log("Direction: "+dir.x+", "+dir.y+", "+dir.z);
        current = rb.position;
        if (old == current)
        {
            if (dir.z > 0)
            {
                dir.z += 0.1f;
            }
            else
            {
                dir.z -= 0.1f;
            }
            //Debug.Log("New Current: " + current.x + ", " + current.y + ", " + current.z);
        }
        rb.AddForce(dir * speed, ForceMode.Force);
    }
}
