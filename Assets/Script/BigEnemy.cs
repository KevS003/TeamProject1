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
    AudioSource bigEAudio;
    public AudioClip dmg;
    public AudioClip shoot; 
    public GameObject damage;
    
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
        bigEAudio = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(fire(intervalE));
    }
    
    void Update()
    {


    }
    void OnCollisionEnter2D(Collision2D contact)
    {
        if(contact.collider.tag == "Projectile")
        {
            
            
                //count down health on hits
                if(health>0)
                {
                    health--;
                    bigEAudio.PlayOneShot(dmg);
                    GameObject projectileObject = Instantiate(damage, rigidbody2dE.position + Vector2.up * 0.5f, Quaternion.identity);
                }
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
        bigEAudio.PlayOneShot(shoot);
        firedRound = Time.time;
        GameObject projectileObject = Instantiate(shot, rigidbody2dE.position + Vector2.down * 0.5f, Quaternion.identity);
        EnemyFIre projectile = projectileObject.GetComponent<EnemyFIre>();
        projectile.LaunchB(bulletSpeedE);
        StartCoroutine(fire(interval));
    }
    
}

