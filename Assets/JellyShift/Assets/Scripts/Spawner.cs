using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles, tokens;
    public float timeBetweenSpawns = 1f, reduceTimeBy = 0.05f, minTimeBetweenSpawns = 0.3f;
    public int tokenFrequency = 3;

    private GameObject tempObstacle = null, tempToken = null;

    void Start()
    {
    }

    public void Spawn()
    {
        if (!FindObjectOfType<GameManager>().gameIsOver)        //If the game is not over yet
        {
            if (timeBetweenSpawns - reduceTimeBy >= minTimeBetweenSpawns)       //If the spanwTime can be reduced
                timeBetweenSpawns -= reduceTimeBy;      //Then reduces it

            Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform.position, Quaternion.identity);        //Spawns a random Obstacle to the position of the Spawner


            if (Random.Range(0, tokenFrequency) == 0)       //If it is time to spawn a Token
                Invoke("SpawnToken", timeBetweenSpawns / 2f);       //Spawns Token

            Invoke("Spawn", timeBetweenSpawns);     //Calls the function 'Spawn' again
        }
    }

    public void SpawnToken()
    {
        if (!FindObjectOfType<GameManager>().gameIsOver)        //If the game is not over yet
        {
            tempToken = Instantiate(tokens[Random.Range(0, tokens.Length)], transform.position, Quaternion.identity);        //Spawns a Token to the position of the Spawner
        }
    }
}
