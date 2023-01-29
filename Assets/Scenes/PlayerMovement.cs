using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;


public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    public int maxHealth= 10000;
    public float speed = 4.0f;
    int cHealth;
    public int health {get{return cHealth;}}//in case health needs to be read
    public GameObject projectileRapid;
    public GameObject projectileRocket;
    public GameObject projectileGrenade;
    public Text scoreTotal;
    public Text winL;
    public static int level;
    Rigidbody2D rigidbody2d;
    float hori;
    float verti;
    int weaponType = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        cHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        verti = Input.GetAxis("Vertical");
        hori = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(hori, verti);
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(weaponType<3)
            {
                weaponType++;
            }
            else
            {
                weaponType = 1;
            }
            
            
        }
        if(Input.GetKeyDown(KeyCode.Space))
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
    void gun(int weaponType)
    {
        if(weaponType==1)
        {
            GameObject projectileObject = Instantiate(projectileRapid, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);//NO ROTATION

            //animator.SetTrigger("Launch");
            //PlaySound(gun);
        }
        else if(weaponType ==2)
        {
             GameObject projectileObject = Instantiate(projectileRocket, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        }
        else if (weaponType ==3)
        {
             GameObject projectileObject = Instantiate(projectileGrenade, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        }
    }
}
