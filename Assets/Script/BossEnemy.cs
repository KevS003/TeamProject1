using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using TMPro;

public class BossEnemy : MonoBehaviour
{
    public float speed;
    public int scoreWorth=50;
    public int spawnHealth;
    public int health = 1000;
    public float bulletSpeedE= 750.0f;
    public float intervalE = .7f;
    public int bombDmg = 150;
    float angle;
    float direction;
    //float firedRound;
    int phase=0;
    public SpriteRenderer spriteRenderer;
    public Sprite noShield;
    private GameObject player;
    public GameObject shot;
    public TextMeshProUGUI bossHealth;
    PlayerMove playerScript;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    
    AudioSource bigEAudio;
    public AudioClip dmg;
    public AudioClip shoot; 
    
    void Awake()
    {
        spawnHealth = health;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerMove>();
        health = spawnHealth;
        bossHealth.enabled =true;
        StartCoroutine(fire(intervalE, 0));//starts at stage 0
    }
    
    void Update()
    {
        bossHealth.text = "Boss Health:\n" + health.ToString("f0") + "/750";
        int bombAmount = playerScript.bombCount;
        if(Input.GetKeyDown(KeyCode.Q)&& bombAmount>0)
        {
            //blink red
            health-=bombDmg; 
            if(health<0)
            {
                Destruct();
            }     
            //dmg animation with some effects and Sound 
        }
        if(health <= 500 && health >200 )
            {
                phase = 1;
                bulletSpeedE= 150;
                intervalE= .8f;
            }
        else if(health <= 200)
            {
                phase = 2;
                bulletSpeedE = 100;
                intervalE = .6f;
            }

    }
    void OnCollisionEnter2D(Collision2D contact)
    {
        if(contact.collider.tag == "Projectile")
        {
            
        
            //count down health on hits
            if(health>0)
            {
                //bigEAudio.PlayOneShot(dmg);
                health--;
            }
            else
            {     
                Destruct();
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
        if(health<0)
            bossHealth.text = "Boss Health:\n0/750";
        //play death sound and have effect
        FindObjectOfType<PlayerMove>().Score(5000);
        Destroy(gameObject);
    }
    private IEnumerator fire(float interval, int stage)//add stage to health amount
    {
        interval = intervalE;//tweaks fire rate depending on stage
        stage = phase;//shifts weapon type on boss
        angle=Random.Range(-10.0f,10.0f);
        direction=Random.Range(-10.0f,10.0f);
        yield return new WaitForSeconds(interval);
        //bigEAudio.PlayOneShot(shoot);
        //firedRound = Time.time;
        GameObject projectileObject = Instantiate(shot, rigidbody2dE.position + Vector2.down* 0.5f, Quaternion.identity);
        EnemyFIre projectile = projectileObject.GetComponent<EnemyFIre>();
        projectile.LaunchBoss(bulletSpeedE, stage);
        StartCoroutine(fire(interval, stage));
    }
    
}
