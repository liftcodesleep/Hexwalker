using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{

    public Transform target; // The target location to pan to
    public float panSpeed = 5f; // The speed of the panning
    public static CameraMovment mainCamera;

    void Start()
    {
        
        //StartCoroutine(IntroPan());
        mainCamera = GetComponent<CameraMovment>();

    }


    void Update()
    {
       

    }


    public void MoveCamera(Transform target)
    {
        StartCoroutine(mainCamera.PanToTarget( target));
    }

    IEnumerator PanToTarget(Transform target)
    {



        //Debug.Log("Camera Move Paning to target");
        if(target == null)
        {
            throw new System.Exception("Camera move Null target pan");
        }
        

        //target = Game.map.GetHexGO(Game.players[0].Avatar.Location).transform;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z - 8);

       
        //Debug.Log("Camera " + target.name);


        
        // Calculate the direction to the target (only on X and Z axes)
        Vector3 direction = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z ).normalized;

        // Move the camera towards the target
        while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z-0), new Vector3(targetPosition.x, 0, targetPosition.z)) > .1f)
        {
            direction = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z).normalized;
            transform.Translate(direction * panSpeed * Time.deltaTime, Space.World);
            yield return null;
        }
        
    }

    IEnumerator IntroPan()
    {
        //Debug.Log("Camera start ");
        yield return new WaitForSeconds(1.5f);

        target = Game.map.GetHexGO(Game.players[1].Avatar.Location).transform;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z - 5);


        //Debug.Log("Camera " + target.name);


        if (target != null)
        {
            // Calculate the direction to the target (only on X and Z axes)
            Vector3 direction = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z).normalized;

            // Move the camera towards the target
            while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z - 3), new Vector3(targetPosition.x, 0, targetPosition.z)) > 3f)
            {
                transform.Translate(direction * panSpeed * Time.deltaTime, Space.World);
                yield return null;
            }
        }

        if(target.GetChild( target.childCount -1 ).Find("HelperText"))
        {
            target.GetChild(target.childCount - 1).Find("HelperText").gameObject.SetActive(true);
        }
        else
        {
            throw new System.Exception("Camera Move Did not Avatar text");
        }
        
        //target.GetChild(1).Find("HelperText").rotation = Quaternion.identity;
        yield return new WaitForSeconds(1f);
        if (target.GetChild(target.childCount - 1).Find("HelperText"))
        {
            target.GetChild(target.childCount - 1).Find("HelperText").gameObject.SetActive(false);
        }

        target = Game.map.GetHexGO(Game.players[0].Avatar.Location).transform;

        targetPosition = new Vector3(target.position.x, target.position.y, target.position.z - 5);


        //Debug.Log("Camera " + target.name);


        if (target != null)
        {
            // Calculate the direction to the target (only on X and Z axes)
            Vector3 direction = new Vector3(targetPosition.x - transform.position.x, 0, targetPosition.z - transform.position.z).normalized;

            // Move the camera towards the target
            while (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z - 3), new Vector3(targetPosition.x, 0, targetPosition.z)) > 3f)
            {
                transform.Translate(direction * panSpeed * Time.deltaTime, Space.World);
                yield return null;
            }
        }
    }
}


