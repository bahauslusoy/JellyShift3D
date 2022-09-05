using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Transform playerHolderTransform;

    public Color[] colors;

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Obstacle"))       //If the gamObject collides with an obstacle
            GetComponent<Renderer>().material.color = colors[0];        //Then the color will be set to the first color of the array (red)
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Obstacle"))       //If the gamObject exits collision with an obstacle
            GetComponent<Renderer>().material.color = colors[1];        //Then the color will be set to the second color of the array (green)
    }
}
