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
        LastFire = GameMgr.Timer;
        SpawnTime = GameMgr.Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if(GameMgr.Timer > SpawnTime + Lifespan)
            {
                Destroy(gameObject);
            }
            else
            {
                if (GameMgr.Timer > LastFire + FirePeriod)
                {
                    GameObject Laser = Instantiate(
                        PrefabLaser,
                        gameObject.transform.position,
                        Quaternion.Euler(0.0f, 0.0f, -Angle)
                    ) as GameObject;

                    Laser.GetComponent<Laser>().Activate();

                    LastFire = GameMgr.Timer;
                }
            }
        }
    }
}
