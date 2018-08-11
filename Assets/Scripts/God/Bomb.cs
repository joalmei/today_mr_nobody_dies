using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : ActionOnTouch
{
    public override void PlayerTouched(PlayerController player)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;

        if (collider.gameObject.GetComponent<PlayerController>() != null)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
