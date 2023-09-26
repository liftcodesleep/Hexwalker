using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{

    public Transform target; // The target location to pan to
    public float panSpeed = 5f; // The speed of the panning

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Camera test ");
        StartCoroutine(PanToTarget());

    }

    // Update is called once per frame
    void Update()
    {
       

    }


    IEnumerator PanToTarget()
    {
        Debug.Log("Camera start ");
        yield return new WaitForSeconds(.5f);

        target = Game.map.GetHexGO(Game.players[0].Avatar.Location).transform;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z - 5);

       
        Debug.Log("Camera " + target.name);


        if (target != null)
        {
            // Calculate the direction to the target (only on X and Z axes)
            Vector3 direction = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z ).normalized;

            // Move the camera towards the target
            while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z-3), new Vector3(targetPosition.x, 0, targetPosition.z)) > 3f)
            {
                transform.Translate(direction * panSpeed * Time.deltaTime, Space.World);
                yield return null;
            }
        }
    }
        
}
