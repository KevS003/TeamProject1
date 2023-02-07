using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
using TMPro;


public class PlayerMoveB : MonoBehaviour
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
    public float usablTime;
    static public int score = 0;
    float firedRound;
    int cHealth;
    public bool lvlOver=false;
    static bool restart = false;
    bool controlAct = true;
    public int bombCount = 3;
    public GameObject projectileRapid;
    public GameObject projectileRocket;//Actually lazer now
    public GameObject projectileGrenade;
    //public GameObject projectileLaunch;
    public TextMeshProUGUI scoreTotal;
    public TextMeshProUGUI winL;
    public TextMeshProUGUI lives;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI bombT;
    public static int level = 1;
    Rigidbody2D rigidbody2d;
    float hori;
    float verti;
    int weaponType = 1;
    float originalP;

    //lazers destroy enemy projectile
    // Start is called before the first frame update
    void Start()
    {
        controlAct = true;
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        cHealth = maxHealth;
        originalBspeed = bulletSpeed;
        originalP = speed;
        winL.enabled = false;
        lives.text = "Lives: " + cHealth.ToString();
        if(level == 1)
            scoreTotal.text = score.ToString();
        else
            scoreTotal.enabled = false;
        timer.text = "Timer:\n  " + lvlTimer.ToString();
        lvlOver = false;
        if(restart == true)
        {
            lvlTimer = 30.0f;
            restart=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lvlTimer>=0 && lvlOver == false && level == 1)
        {
            lvlTimer-= Time.deltaTime;
            usablTime=lvlTimer;

        }
        else
        {
            if(level==1)
            {
                lvlOver=true;
                controlAct=false;
                //winLtext and start a coroutine to wait for next level.
            }
        }
        if(controlAct == true)
        {
            verti = Input.GetAxis("Vertical");
            hori = Input.GetAxis("Horizontal");
            Vector2 move = new Vector2(hori, verti);
        
            if(lvlTimer >= 0.0f)
            {
                lvlTimer-=Time.deltaTime; 
                timer.text = "Timer:\n  " + lvlTimer.ToString("f0");
            }
            /*else
            {
                //send to next level if alive.
            }*/
            if(Input.GetKeyDown(KeyCode.R))//bomb to q
            {
                if(weaponType<2)
                {
                    weaponType++;
                    if(weaponType==2)
                        bulletSpeed *= .60f;
                        speed = 7;
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
                if(bombCount>0)
                {

                    Bomb();
                }
                    
            }
            if(Input.GetKey(KeyCode.Space))
            {
                Gun(weaponType);
            }
        }

        if(lvlOver == true)
        {
            //press R to restart current 
            if(lvlTimer>0)
            {
                winL.enabled = true;
                if(Input.GetKeyDown(KeyCode.R))
                {
                    if(level == 1)
                        score = 0;
                    Restart();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    score = 0;
                    restart=true;
                    SceneManager.LoadScene("MenuScreen");
                }
            }
            if(lvlTimer<=0)
            {
                winL.text = "You made it to the village press\nKey: N for next level or ESC for main menu";
                winL.enabled = true;
                if(Input.GetKeyDown(KeyCode.N))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    score = 0;
                    restart=true;
                    SceneManager.LoadScene("MenuScreen");
                }
                //load next level
            }
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
    void Gun(int weaponType)
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
    void Bomb()
    {
        
        bombCount--;
        bombT.text = "= " + bombCount.ToString();
        //SOUND EFFECT AND EXPLOSION ANIMATION
        //DO IT FOR BIG ENEMY
                /*
                firedRound = Time.time;
                GameObject projectileObject = Instantiate(projectileGrenade, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                WeaponFire projectile = projectileObject.GetComponent<WeaponFire>();
                projectile.Launch(bulletSpeed); */ 
            
    }
    public void HitPlayer(int dmgAmount)
    {
        if(lvlOver==false)
        {
            cHealth-=dmgAmount;
            Debug.Log(cHealth.ToString());
            lives.text = "Lives: " + cHealth.ToString();
            if(cHealth == 0)
            {
                controlAct = false;
                lvlOver = true;
                //set sprite to dead sprite here//dead sound effect here
                //LoseCond();
            }
        }
        //play sound effect, make invinsible
    }
    public void Score(int amountPoint)
    {
        score+=amountPoint;
        //update score UI here;
        scoreTotal.text = score.ToString();
        Debug.Log(score.ToString());
    }
    void Restart()
    {
        restart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /*void LoseCond()
    {
            winL.enabled = true;
            if(Input.GetKeyDown(KeyCode.R))
                {
                    Restart();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene("MenuScreen");
                }
    }*/
    //pause menu through function that stops the spawner temporarily and player movement
}
