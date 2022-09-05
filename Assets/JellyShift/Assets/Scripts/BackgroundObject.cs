using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour {

    private Vector3 nextPos;
    private float movementSpeed, invokeTime;
    private bool upwardMovement = false;

	void Start () {
        movementSpeed = Random.Range(0.05f, 0.8f);
        invokeTime = Random.Range(2f, 10f);
        if(Random.Range(0, 2) == 0)
            upwardMovement = true;

        Invoke("ChangeDirection", invokeTime);
	}
	
	void Update () {
        nextPos = transform.position;
        if (upwardMovement)
            nextPos.y += movementSpeed * Time.deltaTime;
        else
            nextPos.y -= movementSpeed * Time.deltaTime;
        transform.position = nextPos;
	}

    private void ChangeDirection()
    {
        movementSpeed = Random.Range(0.05f, 0.8f);
        invokeTime = Random.Range(2f, 10f);
        upwardMovement = !upwardMovement;
        Invoke("ChangeDirection", invokeTime);
    }
}
