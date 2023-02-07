using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        FindObjectOfType<PlayerMove>().Score(150);
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

