using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ActionOnTouch
{
    public Vector3 Direction;
    public float Speed;

    public GameObject Turret;

    public override void PlayerTouched(PlayerController player)
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != Turret)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
            }

            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        
        PlayerController player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage();
            print("Take that!");
        }
        else
        {
            print("Nope");
        }

        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        // Nothing here yet
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + Direction.normalized * Speed * GameMgr.DeltaTime;

    }
}
