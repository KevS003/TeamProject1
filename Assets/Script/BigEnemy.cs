using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class BigEnemy : MonoBehaviour
{
    public float speed;
    public int scoreWorth=50;
    public int health = 100;
    public int shotsPerS = 5;
    public float bulletSpeedE= 1500.0f;
    public float intervalE = .7f;
    float firedRound;
    public SpriteRenderer spriteRenderer;
    public Sprite noShield;
    private GameObject player;
    public GameObject shot;
    PlayerMove playerScript;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    bool lastBombUsed = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(fire(intervalE));
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
            if(bombAmount>-1&& lastBombUsed ==false)
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

            
                //count down health on hits
                if(health>0)
                    health--;
                else
                {     
                    FindObjectOfType<PlayerMove>().Score(300);
                    Destroy(gameObject);
                }
            
                
            
        }
        if(contact.collider.tag == "Player")
        {
            PlayerMove dmg = contact.gameObject.GetComponent<PlayerMove>();
            dmg.HitPlayer(1);
        }

    }
    public void Destruct()
    {
        FindObjectOfType<PlayerMove>().Score(300);
        Destroy(gameObject);
    }
    private IEnumerator fire(float interval)
    {

        yield return new WaitForSeconds(interval);
        firedRound = Time.time;
        GameObject projectileObject = Instantiate(shot, rigidbody2dE.position + Vector2.down * 0.5f, Quaternion.identity);
        EnemyFIre projectile = projectileObject.GetComponent<EnemyFIre>();
        projectile.LaunchB(bulletSpeedE);
        StartCoroutine(fire(interval));
    }
    
}

