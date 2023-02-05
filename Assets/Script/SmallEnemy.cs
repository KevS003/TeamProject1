using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    //boundaries/zigzag//drops sword upgrade
    public float speed;
    int lnR = 1;
    public int scoreWorth=50;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    
    // Start is called before the first frame update
    void Awake()
    {
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
        if(Random.value<0.5)
            lnR = -1;
        else
            lnR = 1;

    }
    //HAVE ENEMY MOVE LnR till bottom and then destroy object
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 spot = rigidbody2dE.position;
        spot.y = spot.y + Time.deltaTime * speed * -1;//direction turns it pos or negative   
        spot.x = spot.x + Time.deltaTime * speed * lnR;
        transform.position = new Vector2(spot.x,spot.y);
        if (transform.position.magnitude > 20.0f)
            Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D contact)
    {
        if(contact.collider.tag == "Projectile")
        {
            FindObjectOfType<PlayerMove>().Score(50);
            Destroy(gameObject);
        }
        if(contact.collider.tag == "Player")
        {
            PlayerMove dmg = contact.gameObject.GetComponent<PlayerMove>();
            dmg.HitPlayer(1);
            Destroy(gameObject);
        }
        if(contact.collider.tag == "Border")
        {
            lnR*=-1;
        }

    }
    
}
