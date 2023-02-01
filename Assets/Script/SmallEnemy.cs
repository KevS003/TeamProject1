using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    //boundaries/zigzag//drops sword upgrade
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;
    public float speed;
    int lnR;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    // Start is called before the first frame update
    void Awake()
    {
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
    }
    //HAVE ENEMY MOVE LnR till bottom and then destroy object
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 spot = rigidbody2dE.position;
        spot.y = spot.y + Time.deltaTime * speed * -1;//direction turns it pos or negative   
        spot.x = spot.x + Time.deltaTime * speed * lnR;
    }
   /* void OnCollisionEnter(Colle)
    {

    }*/
    
}
