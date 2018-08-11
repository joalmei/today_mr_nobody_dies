using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ActionOnTouch
{
    public Vector3 Direction;
    public float Speed;

    public override void PlayerTouched(PlayerController player)
    {
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
        gameObject.transform.position = gameObject.transform.position + Direction.normalized * Speed * Time.deltaTime;

    }
}
