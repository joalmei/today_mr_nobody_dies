using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public float FirePeriod;
    public float Angle;
    public bool Active = false;

    public GameObject PrefabLaser;
    public float Lifespan;

    private float LastFire = 0.0f;
    private float SpawnTime;

    public void Activate(float AngleParam)
    {
        print("Turret active");
        Active = true;
        Angle = AngleParam;
        LastFire = Time.time;
        SpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if(Time.time > SpawnTime + Lifespan)
            {
                Destroy(gameObject);
            }
            else
            {
                if (Time.time > LastFire + FirePeriod)
                {
                    GameObject Laser = Instantiate(
                        PrefabLaser,
                        gameObject.transform.position,
                        Quaternion.Euler(0.0f, 0.0f, -Angle)
                    ) as GameObject;

                    Laser.GetComponent<Laser>().Activate();

                    LastFire = Time.time;
                }
            }
        }
    }
}
