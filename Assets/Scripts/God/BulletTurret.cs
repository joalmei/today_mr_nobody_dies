using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    public float FirePeriod;
    public Vector3 Direction;
    public bool Active = false;

    public GameObject PrefabBullet;

    private float LastFire = 0.0f;

    public void Activate(Vector3 DirectionParam)
    {
        Active = true;
        Direction = DirectionParam;
        LastFire = GameMgr.Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (Active)
        {
            if (GameMgr.Timer > LastFire + FirePeriod)
            {
                GameObject Bullet = Instantiate(
                    PrefabBullet,
                    gameObject.transform.position,
                    Quaternion.identity
                ) as GameObject;

                Bullet.GetComponent<Bullet>().Direction = Direction;
                Bullet.GetComponent<Bullet>().Turret = gameObject;

                LastFire = GameMgr.Timer;
            }
        }
    }
}
