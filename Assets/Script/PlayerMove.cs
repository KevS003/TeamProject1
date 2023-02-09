using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
using TMPro;


public class PlayerMove : MonoBehaviour
{
    //health system adds health based on score
    Animator animator;
    public int maxHealth= 3;//when you lose a life reset to power 1
    public int alreadyMult = 0;
    public float speed = 15f;
    public float bulletSpeed = 5000.0f;
    public float originalBspeed = 5000.0f;
    public float shotsPerS = 20.0f;
    static public float lvlTimer = 30.0f;
    static public bool mainMenuR=false;
    public float usablTime;
    public float timeInvincible = 2.0f;
    float invincibleTimer;
    static public int score = 0;
    float firedRound;
    int cHealth;
    public bool lvlOver=false;
    bool gameOver = false;
    //bool restart = false;
    bool controlAct = true;
    bool invincible;
    static bool nextLevel = false;
    static int bombStore=3;
    public int bombCount;
    private bool bossDed=false;
    public GameObject projectileRapid;
    public GameObject projectileRocket;//Actually lazer now
    public GameObject projectileGrenade;
    private GameObject bossHere;
    GameObject[] destroyAll;
    //public GameObject projectileLaunch;
    public TextMeshProUGUI scoreTotal;
    public TextMeshProUGUI winL;
    public TextMeshProUGUI lives;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI bombT;
    public static int level = 1;
    Rigidbody2D rigidbody2d;
    BossEnemy bossScript;
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
        bombCount = bombStore;
        bombT.text = "= " + bombCount.ToString();
        if(level == 1)
        {
            scoreTotal.text = score.ToString();
            timer.text = "Timer:\n  " + lvlTimer.ToString();
        }
        else
        {
            bossHere = GameObject.FindWithTag("BossE");
            bossScript = bossHere.GetComponent<BossEnemy>();
            scoreTotal.text = score.ToString();
            timer.enabled = false;//could be replaced with boss health
        }
        if(mainMenuR==true)
        {
            level = 1;
            mainMenuR = false;
        }
        lvlOver = false;
        //if(restart == true)
        //{
            lvlTimer = 30.0f;
            usablTime=lvlTimer;
            //restart=false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //TIMER
        if(lvlTimer>0 && cHealth > 0 && nextLevel == false)//if the timer gets to 0 //lvlTimer>=0 && lvlOver == false && level == 1
        {
            lvlTimer-= Time.deltaTime;
            usablTime=lvlTimer;  
            timer.text = "Timer:\n  " + lvlTimer.ToString("f0");     
        }
        else if(nextLevel == false)
        {
            if(cHealth>0)
            {
                lvlOver=true;
                controlAct=false;
                //level++;
            }
        }
        if(nextLevel == true)
        {
            if(bossScript.health <=0 && bossDed == false)
            {
                bossDed = true;
                GameOver();
                
            }
        }
        //CONTROLLER INPUTS
        if(controlAct == true)
        {
            verti = Input.GetAxis("Vertical");
            hori = Input.GetAxis("Horizontal");
            Vector2 move = new Vector2(hori, verti);
            if (invincible)
            {
                invincibleTimer -= Time.deltaTime;
                gameObject.tag = "PlayerInv";

                if (invincibleTimer < 0)
                {
                    invincible = false;
                    gameObject.tag = "Player";
                    
                }

            }
            if(Input.GetKeyDown(KeyCode.R))//bomb to q
            {
                if(weaponType<2)//change sprite here to match weapon being used, match sound depending on weapon being used
                {
                    weaponType++;
                    if(weaponType==2)
                        bulletSpeed *= .60f;
                        speed = 3;
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

        //ending STATES
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
                    //restart=true;
                    level = 1;
                    SceneManager.LoadScene("MenuScreen");
                }
            }
            else if(lvlTimer<=0)
            {
                winL.text = "You made it to the village press\nKey: N for next level or ESC for main menu";
                winL.enabled = true;
                if(Input.GetKeyDown(KeyCode.N))
                {
                    level++;
                    nextLevel = true;
                    bombStore=bombCount;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                if(Input.GetKeyDown(KeyCode.Escape))//THIS IS THE PROBLEM !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    score = 0;
                    mainMenuR = true;
                    SceneManager.LoadScene("MenuScreen");
                }
                //load next level
            }
        }
        if(gameOver == true)
        {
                winL.text = "You saved the Village! ESC for main menu";
                winL.enabled = true;
                if(Input.GetKeyDown(KeyCode.Escape))//THIS IS THE PROBLEM !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                {
                    score = 0;
                    mainMenuR = true;
                    gameOver = false;
                    nextLevel = false;
                    level =1;
                    bombStore = 3;
                    score =0;
                    SceneManager.LoadScene("MenuScreen");
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
    void Gun(int weaponType)//place weapon animations here 
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
            shotsPerS= 60;
            if(Time.time - firedRound >1/shotsPerS)
            {
                firedRound = Time.time;
                GameObject projectileObject = Instantiate(projectileRocket, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                WeaponFire projectile = projectileObject.GetComponent<WeaponFire>();
                projectile.Launch(bulletSpeed);
            }
        }
    }

    //Whipes screen
    void Bomb()
    {
        
        bombCount--;
        bombT.text = "= " + bombCount.ToString();
        destroyAll = GameObject.FindGameObjectsWithTag ("Enemy");
        for(var i = 0 ; i < destroyAll.Length ; i ++)
        {
            Destroy(destroyAll[i]);
        }
        Score(500);
        //SOUND EFFECT AND EXPLOSION ANIMATION
        //DO IT FOR BIG ENEMY
                /*
                firedRound = Time.time;
                GameObject projectileObject = Instantiate(projectileGrenade, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                WeaponFire projectile = projectileObject.GetComponent<WeaponFire>();
                projectile.Launch(bulletSpeed); */ 
            
    }

    //Damages player
    public void HitPlayer(int dmgAmount)
    {
        if(lvlOver==false)
        {
            if(invincible == false)
            {
                cHealth-=dmgAmount;
                Debug.Log(cHealth.ToString());
                lives.text = "Lives: " + cHealth.ToString();
                invincible = true;
                invincibleTimer = timeInvincible;
                //This is where you set an animation to show the player got hit. and add the sound 
                //animator.SetTrigger("Hit");
                //GameObject projectileObject = Instantiate(damage, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                //PlaySound(dmg);
            }
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

    //calcs score
    public void Score(int amountPoint)
    {
        score+=amountPoint;
        //update score UI here;
        scoreTotal.text = score.ToString();
        Debug.Log(score.ToString());
    }

    //restarts game
    void Restart()
    {
        //restart = true;
        bombCount = bombStore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void GameOver()
    {
         controlAct = false;
         gameOver = true;
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
