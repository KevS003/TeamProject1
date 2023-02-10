using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
using TMPro;

public class SmallEnemy : MonoBehaviour
{
    //boundaries/zigzag//drops sword upgrade
    public float speed;
    int lnR = 1;
    int amountHit = 0;
    public int scoreWorth=50;//hi
    bool lastBombUsed = false;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    private GameObject player;
    PlayerMove playerScript;
    AudioSource smallEAudio;
    AudioClip dmg;
    public GameObject damage;
    
    // Start is called before the first frame update
    void Awake()
    {
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
        smallEAudio = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        
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

   void Update()
    {
        /*
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if(Input.GetKey(KeyCode.Q) && player!= null)
        {
            playerScript = player.GetComponent<PlayerMove>();
            int bombAmount = playerScript.bombCount;
            if(bombAmount>0&& lastBombUsed ==false)
            {
                Destruct();
                if(bombAmount ==0)
                {
                    lastBombUsed = true;
                }
            }
            //int bombCounter = FindObjectofType<PlayerMove>().bombCount;
            
        }*/
    }

    void OnCollisionEnter2D(Collision2D contact)
    {
        if(contact.collider.tag == "Projectile")
        {
            if( amountHit==0)
            {
                FindObjectOfType<PlayerMove>().Score(50);
                amountHit++;
            }
            GameObject projectileObject = Instantiate(damage, rigidbody2dE.position + Vector2.up * 0.5f, Quaternion.identity);
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
    public void Destruct()
    {
        smallEAudio.PlayOneShot(dmg);
        FindObjectOfType<PlayerMove>().Score(50);
        Destroy(gameObject);
    

    }
    
}
