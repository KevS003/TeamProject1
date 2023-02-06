using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToughEnemy : MonoBehaviour
{
    public float speed;
    int amountHit = 0;
    bool shieldBreak= false;
    bool brokenOff = false;
    public int shieldHealth = 25;
    public int scoreWorth=50;//hi
    public int health = 15;
    public SpriteRenderer spriteRenderer;
    public Sprite noShield;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //put projectiles here
    }
    void OnCollisionEnter2D(Collision2D contact)
    {
        if(contact.collider.tag == "Projectile")
        {
            if(shieldBreak == false)
            {
            
                if(shieldHealth>0)
                {
                    shieldHealth--;
                }
                else if(shieldHealth==0)
                {
                    //change sprite to broken shield.
                    spriteRenderer.sprite = noShield;
                    shieldBreak = true;
                }
                        
            }
            else
            {
                //count down health on hits
                if(health>0)
                    health--;
                else
                {     
                    FindObjectOfType<PlayerMove>().Score(150);
                    Destroy(gameObject);
                }
            }
                
            
        }
        if(contact.collider.tag == "Player")
        {
            PlayerMove dmg = contact.gameObject.GetComponent<PlayerMove>();
            dmg.HitPlayer(1);
            //Destroy(gameObject);
        }

    }
    
}
