using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFIre : MonoBehaviour
{
    float angle;
    Rigidbody2D proRigidBod;
    void Awake()
    {
        angle=Random.Range(-10.0f,-1.0f);
        
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
        proRigidBod.AddForce( new Vector2 (0.0f,-1.0f) * force);
    }
    public void LaunchB(float force)
    {
        proRigidBod.AddForce(new Vector2 (0.0f,angle) *force);
    }
    void OnCollisionEnter2D(Collision2D impact)
    {
        if(impact.collider.tag == "Player")
        {
            PlayerMove dmg = impact.gameObject.GetComponent<PlayerMove>();
            dmg.HitPlayer(1);
            Destroy(gameObject);
        }

    }
}
