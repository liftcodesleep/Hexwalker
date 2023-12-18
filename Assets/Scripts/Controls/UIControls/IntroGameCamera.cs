using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameCamera : MonoBehaviour
{
    //public float moveSpeed = .001f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position of the camera
        Vector3 currentPosition = transform.position;

        // Calculate the new position by moving to the left
        Vector3 newPosition = new Vector3(currentPosition.x + .2f * Time.deltaTime, currentPosition.y, currentPosition.z);

        // Update the camera position gradually
        transform.position = newPosition;
    }
}
