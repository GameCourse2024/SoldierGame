using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
     [SerializeField] private float xMax;
     [SerializeField] private float yMax;
     [SerializeField] private float xMin;
     [SerializeField] private float yMin;


     private Transform target;  // target will be the player
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Char")?.transform; // target is our player
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        if (target)
        {
        // limits the camera outlook by the limits defined
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), -10f);
        }
    }

}
