using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    //public GameObject projectileRapid;
    //public GameObject projectileRocket;
    //public GameObject projectileGrenade;
    //public SpriteRenderer ammoType;//gets rendered
    //public Sprite ammoChange;//stores data of shot sprites
    Rigidbody2D proRigidBod;
    int score;
    void Awake()
    {
        
        proRigidBod = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 15.0f)
            Destroy(gameObject);
        
    }

    public void Launch(float force)
    {
        proRigidBod.AddForce( new Vector2 (0.0f,1.0f) * force);//should only shoot up
    }
    void OnCollisionEnter2D(Collision2D impact)
    {
        if(impact.collider.tag == "SmallE")
        {
            SmallEnemy smol = impact.collider.GetComponent<SmallEnemy>();
            if(smol!= null)
            {
                Destroy(gameObject);
                //scoreTrack.score += smol.scoreWorth;//gets value of enemy from enemy script
                //Debug.Log(score.ToString());
                //scoreTracking.Score(score);//brings value back to the player
                Debug.Log("ENEMY HIT");
            }
        }
        else if(impact.collider.tag =="BigE")
        {

        }
        else if(impact.collider.tag == "ToughE")
        {

        }

    }

}
