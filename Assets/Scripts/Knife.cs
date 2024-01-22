using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    [SerializeField] private float speed; // speed of throwing
    [SerializeField] private Vector2 isRight;   // is right

    private Rigidbody2D myRigid;    // accessing the object

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();  // gets the rigid property
    }

    void Update(){}
    // Update is called once per frame
    void FixedUpdate()
    {
        myRigid.velocity = isRight * speed; // throw by the side and speed 
    }

    public void GetDirection(Vector2 direction)
    {
        this.isRight = direction;   // direction of the player
    }    
}
