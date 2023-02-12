using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
using TMPro;

public class ToughEnemy : MonoBehaviour
{
    public float speed;
    bool shieldBreak= false;
    int bombAmount;
    public int shieldHealth = 25;
    public int scoreWorth=50;
    public int health = 15;
    public int shotsPerS = 5;
    public float bulletSpeedE= 750.0f;
    public float intervalE = .7f;
    //float firedRound;
    public SpriteRenderer spriteRenderer;
    public Sprite noShield;
    public Transform[] targets;
    private GameObject player;
    public GameObject shot;
    PlayerMove playerScript;
    Animator animatorE;
    Rigidbody2D rigidbody2dE;
    public Transform startMarker;
    public Transform endMarker;
    bool lastBombUsed = false;
    private float startTime;

    private float journeyLength;
    public AudioClip dmg;
    public AudioClip shooting;
    AudioSource toughEAudio;
    public GameObject damage;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        startTime = Time.time;
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        toughEAudio = GetComponent<AudioSource>();
        animatorE = GetComponent<Animator>();
        rigidbody2dE = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        if(player!= null)
        {
            player = GameObject.FindWithTag("Player");
            playerScript = player.GetComponent<PlayerMove>();
        }
        StartCoroutine(fire(intervalE));
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float distCovered = (Time.time - startTime) * speed;// Measures time passed to get a pace.//Speed changes how fast coordinates change later
        float fracJourney = distCovered / journeyLength;
        Vector2 position = rigidbody2dE.position;
        /*
        position.x = position.x + speed * 0 * Time.deltaTime;//seconds to render a frame to keep movement consistent
        position.y = position.y + speed * -1 * Time.deltaTime;
        rigidbody2dE.MovePosition(position);*/
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));
        //movement here
    }
    void Update()
    {
        /*
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
            playerScript = player.GetComponent<PlayerMove>();
            
        }
        else
            bombAmount = playerScript.bombCount;
        if(Input.GetKey(KeyCode.Q) && bombAmount>=0 &&lastBombUsed == false)
        {

            Destruct();
            if(bombAmount ==0)
            {
                lastBombUsed = true;
            }

            //int bombCounter = FindObjectofType<PlayerMove>().bombCount;
            
        }*/

    }
    void OnCollisionEnter2D(Collision2D contact)
    {
        if(contact.collider.tag == "Projectile")
        {
            GameObject projectileObject = Instantiate(damage, rigidbody2dE.position + Vector2.up * 0.5f, Quaternion.identity);
            if(shieldBreak == false)
            {
            
                if(shieldHealth>0)
                {
                    shieldHealth--;
                }
                else if(shieldHealth==0)
                {
                    //change sprite to broken shield.
                    spriteRenderer.sprite = noShield; //or use an animation instead
                    //animatorE.SetTrigger("")// whatever u named it on Unity
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
        toughEAudio.PlayOneShot(shooting);
        //firedRound = Time.time;
        GameObject projectileObject = Instantiate(shot, rigidbody2dE.position + Vector2.down * 0.5f, Quaternion.identity);
        EnemyFIre projectile = projectileObject.GetComponent<EnemyFIre>();
        projectile.Launch(bulletSpeedE);
        StartCoroutine(fire(interval));
    }
    
}
