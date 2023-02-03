using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    //public GameObject projectileRapid;
    //public GameObject projectileRocket;
    //public GameObject projectileGrenade;
    //public SpriteRenderer ammoType;//gets rendered
    //public Sprite ammoChange;//stores data of shot sprite
    Rigidbody2D proRigidBod;
    // Start is called before the first frame update
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
    /*
    public void AmmoType(int gun)
    {
        if(gun==1)
        {
            ammoChange = projectileRapid.GetComponent<Sprite>();
        }
        else if(gun ==2)
        {
            ammoChange = projectileRocket.GetComponent<Sprite>();
        }
        else if(gun==3)
        {
            ammoChange = projectileGrenade.GetComponent<Sprite>();
        }
        ammoType.sprite= ammoChange;
    }*/

    public void Launch(float force)
    {
        proRigidBod.AddForce( new Vector2 (0.0f,1.0f) * force);//should only shoot up
    }
}
