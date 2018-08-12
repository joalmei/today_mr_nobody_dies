using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    private bool Activated = false;
    private float ActivatedStart;

    private bool DamagedPlayer = false;

    public float TimeBeforeDamage;
    public float DamageTime;

    public GameObject DamageSFX;

    private bool Transition1 = false;
    private bool Transition2 = false;

    public void Activate()
    {
        Activated = true;
        ActivatedStart = Time.time;

        Renderer LaserRenderer = gameObject.GetComponent<Renderer>();
        Color CurrentColor = LaserRenderer.material.GetColor("_Color");
        CurrentColor.a = 0.5f;
        LaserRenderer.material.SetColor("_Color", CurrentColor);
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Activated)
        {
            if (Time.time < ActivatedStart + TimeBeforeDamage)
            {
                // Do nothing
                if (!Transition1)
                {
                    Transition1 = true;

                    gameObject.GetComponent<AudioSource>().Play();
                }
            } else if (Time.time < ActivatedStart + TimeBeforeDamage + DamageTime)
            {
                if (!Transition2)
                {
                    Transition2 = true;

                    DamageSFX.GetComponent<AudioSource>().Play();
                }
                // Make it opaque
                Renderer LaserRenderer = gameObject.GetComponent<Renderer>();
                Color CurrentColor = LaserRenderer.material.GetColor("_Color");
                CurrentColor.a = 1.0f;
                LaserRenderer.material.SetColor("_Color", CurrentColor);

                // Do damage
                Vector3 LaserOrigin = gameObject.transform.position;
                float LaserAngle = Mathf.Deg2Rad*gameObject.transform.rotation.eulerAngles.z;
                Vector3 LaserDirection = new Vector3(Mathf.Cos(LaserAngle), Mathf.Sin(LaserAngle), 0);
                Ray LaserRay = new Ray(LaserOrigin, LaserDirection);
                RaycastHit[] Hits = Physics.RaycastAll(LaserRay);

                if (!DamagedPlayer)
                {
                    foreach (RaycastHit hit in Hits)
                    {
                        PlayerController player = hit.collider.gameObject.GetComponent<PlayerController>();
                        if (player)
                        {
                            DamagedPlayer = true;
                            player.TakeDamage();
                        }
                    }
                }
            }
            else
            {
                // Disappear
                Destroy(gameObject);
            }
        }
	}
}
