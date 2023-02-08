using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float speed;
    bool shieldBreak= false;
    public int shieldHealth = 25;
    public int scoreWorth=50;
    public int health = 15;
    public int shotsPerS = 5;
    public float bulletSpeedE= 750.0f;
    public float intervalE = .7f;
    float firedRound;
    public SpriteRenderer spriteRenderer;
    public Sprite noShield;
    private GameObject player;
    public GameObject shot;
    PlayerMove playerScript;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMove>();
        StartCoroutine(fire(intervalE));
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //put projectiles here
    }
    void Update()
    {
        int bombAmount = playerScript.bombCount;
        if(Input.GetKey(KeyCode.Q)&& bombAmount>0)
        {
            Destruct();
            //int bombCounter = FindObjectofType<PlayerMove>().bombCount;
            
        }
        float timer = playerScript.usablTime;
        if(timer<=0)
        {
            Destroy(gameObject);
        }

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
        }

    }
    public void Destruct()
    {
        FindObjectOfType<PlayerMove>().Score(150);
        Destroy(gameObject);
    }
    private IEnumerator fire(float interval)
    {

        yield return new WaitForSeconds(interval);
        firedRound = Time.time;
        GameObject projectileObject = Instantiate(shot, rigidbody2dE.position + Vector2.down * 0.5f, Quaternion.identity);
        EnemyFIre projectile = projectileObject.GetComponent<EnemyFIre>();
        projectile.Launch(bulletSpeedE);
        StartCoroutine(fire(interval));
    }
    
}
