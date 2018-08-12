using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public float FirePeriod;
    public float Angle;
    public bool Active = false;

    public GameObject PrefabLaser;

    private float LastFire = 0.0f;

    public void Activate(float AngleParam)
    {
        Active = true;
        Angle = AngleParam;
        LastFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (Time.time > LastFire + FirePeriod)
            {
                GameObject Laser = Instantiate(
                    PrefabLaser,
                    gameObject.transform.position,
                    Quaternion.Euler(0.0f, 0.0f, Angle)
                ) as GameObject;
                
                LastFire = Time.time;
            }
        }
    }
}
