using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ActionOnTouch
{
    public float ExplosionRadius = 1.0f;
    public GameObject PrefabExplosion;

    public override void PlayerTouched(PlayerController player)
    {
        Destroy(gameObject);
    }
    
    public void OnCollisionEnter(Collision collision)
    {
        print("Collided!");
        Collider[] ExplosionColliders = Physics.OverlapSphere(
            gameObject.transform.position,
            ExplosionRadius*0.16f, // Magic, don't touch.
            LayerMask.GetMask("Player")
        );

        GameObject ExplosionInstance = Instantiate(
            PrefabExplosion,
            gameObject.transform.position,
            Quaternion.identity
        ) as GameObject;

        ExplosionInstance.transform.localScale = Vector3.one * ExplosionRadius;

        foreach (Collider collider in ExplosionColliders)
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
                print("WithPlayer");
            }
        }

        Destroy(gameObject);

    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
