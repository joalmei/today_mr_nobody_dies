using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    private bool Activated;
    private float ActivatedStart;

    public float TimeBeforeDamage;
    public float DamageTime;

    public void Activate()
    {
        Activated = true;
        ActivatedStart = Time.time;

        Renderer LaserRenderer = gameObject.GetComponent<Renderer>();
        //LaserRenderer.material.color = 
    }

	// Use this for initialization
	void Start () {
        Activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Activated && Time.time > ActivatedStart + )
	}
}
