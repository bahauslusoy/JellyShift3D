using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHolder : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();     //Initializes Rigidbody

        MoveObstacle();     //Makes the obstacle move towards the player
    }

    public void StopObstacle()
    {
        rb.Sleep();     //Makes the obstacle stop
    }

    public void MoveObstacle()
    {
        rb.AddForce(transform.forward * -speed);     //Makes the obstacle move towards the player
    }
}
