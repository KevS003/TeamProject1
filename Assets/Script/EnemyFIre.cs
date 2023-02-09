using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class EnemyFIre : MonoBehaviour
{
    float angle;
    float direction;
    Rigidbody2D proRigidBod;
    void Awake()
    {
        angle=Random.Range(-1.0f,1.0f);
        direction=Random.Range(-1.0f,1.0f);
        
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
        proRigidBod.AddForce(new Vector2 (angle,-1.0f) *force);
    }
    public void LaunchBoss(float force, int stage)
    {
        if(stage == 0 )
        {
            proRigidBod.AddForce( new Vector2 (0.0f,-1.0f) * force);
        }
        else if(stage == 1)
        {
            proRigidBod.AddForce(new Vector2 (angle,-1.0f) *force);
        }
        else if(stage == 2)
        {
            proRigidBod.AddForce(new Vector2(angle,direction)* force);
        }
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
