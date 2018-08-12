using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public float FirePeriod;
    public float Angle;
    public bool Active = false;

    public float Lifespan;

    public GameObject PrefabLaser;

    private float LastFire = 0.0f;
    private float SpawnedTime = 0.0f;

    public void Activate(float AngleParam)
    {
        print("Turret active");
        Active = true;
        Angle = AngleParam;
        LastFire = Time.time;

        SpawnedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (Time.time > SpawnedTime + Lifespan)
            {
                Destroy(gameObject);
            } else if (Time.time > LastFire + FirePeriod)
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
