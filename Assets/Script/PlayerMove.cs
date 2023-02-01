using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;


public class PlayerMove : MonoBehaviour
{
    //health system adds health based on score
    Animator animator;
    static public int lvlNum= 1;
    public int maxHealth= 3;//when you lose a life reset to power 1
    public int alreadyMult = 0;
    public float speed = 15f;
    public float bulletSpeed = 5000.0f;
    public float originalBspeed = 5000.0f;
    public float shotsPerS = 20.0f;
    static public float lvlTimer = 30.0f;
    public int score = 0;
    float firedRound;
    int cHealth;
    public int health {get{return cHealth;}}//in case health needs to be read
    public GameObject projectileRapid;
    public GameObject projectileRocket;
    public GameObject projectileGrenade;
    public GameObject projectileLaunch;
    public Text scoreTotal;
    public Text winL;
    public static int level;
    Rigidbody2D rigidbody2d;
    float hori;
    float verti;
    int weaponType = 1;
    float originalP;

    //lazers destroy enemy projectile
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        cHealth = maxHealth;
        originalBspeed = bulletSpeed;
        originalP = speed;
    }

    // Update is called once per frame
    void Update()
    {
        verti = Input.GetAxis("Vertical");
        hori = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(hori, verti);
        if(lvlTimer >= 0.0f)
        {
            lvlTimer-=Time.deltaTime; 
        }
        else
        {
            //send to next level if alive.
        }
        if(Input.GetKeyDown(KeyCode.R))//bomb to q
        {
            if(weaponType<2)
            {
                weaponType++;
                if(weaponType==2)
                    bulletSpeed *= .60f;
                    speed = 7;
                /*else if(weaponType==3)
                    bulletSpeed *= .25f;*/
            }
            else
            {
                speed = originalP;
                weaponType = 1;
                bulletSpeed = originalBspeed;
            }
            
            
        }
        if(Input.GetKeyDown(KeyCode.Q))//Make as an emergency
        {
            bomb();
        }
        if(Input.GetKey(KeyCode.Space))
        {
            gun(weaponType);
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * hori * Time.deltaTime;//seconds to render a frame to keep movement consistent
        position.y = position.y + speed * verti * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
    //RETURN VALUE FROM WEAPON SCRIPT FOR SCORE!!!!!
    void gun(int weaponType)
    {
        
        if(weaponType == 1)
        {
            shotsPerS= 20;
            if(Time.time - firedRound >1/shotsPerS)//POWER UP: extra swords that track enemy
            {
                firedRound = Time.time;
                GameObject projectileObject = Instantiate(projectileRapid, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);//NO ROTATION
                WeaponFire projectile = projectileObject.GetComponent<WeaponFire>();
                projectile.Launch(bulletSpeed);    
            }
        }
        else if(weaponType == 2)//beam
        {
                firedRound = Time.time;
                GameObject projectileObject = Instantiate(projectileRocket, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                WeaponFire projectile = projectileObject.GetComponent<WeaponFire>();
                projectile.Launch(bulletSpeed);
        }
    }
    void bomb()
    {
                firedRound = Time.time;
                GameObject projectileObject = Instantiate(projectileGrenade, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                WeaponFire projectile = projectileObject.GetComponent<WeaponFire>();
                projectile.Launch(bulletSpeed);  
            
    }
}
