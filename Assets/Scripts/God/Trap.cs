using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public Color NormalColor;
    public Color FlashColor;

    public AnimationCurve FlashPeriod;
    public float ExplodeTime;
    public float ActivateDistance;
    public float GracePeriod;
    public GameObject ExplosionVFX;

    private bool Activated;
    private float SpawnedTime;
    private bool BombState;
    private float LastSwitch;
    private float ActivatedTime;
    
    private void BombState0()
    {
        bool IsInGrace = Time.time < SpawnedTime + GracePeriod;
        Color CurrentColor = NormalColor;

        if (IsInGrace)
        {
            CurrentColor.a = 0.5f;
        }

        Renderer TrapRenderer = gameObject.GetComponent<Renderer>();
        TrapRenderer.material.SetColor("_Color", CurrentColor);

    }
    private void BombState1()
    {
        bool IsInGrace = Time.time < SpawnedTime + GracePeriod;
        Color CurrentColor = FlashColor;

        if (IsInGrace)
        {
            CurrentColor.a = 0.5f;
        }

        Renderer TrapRenderer = gameObject.GetComponent<Renderer>();
        TrapRenderer.material.SetColor("_Color", CurrentColor);
    }

    // Use this for initialization
    void Start ()
    {
        Activated = false;
        SpawnedTime = Time.time;
        BombState0();
    }
	
	// Update is called once per frame
	void Update ()
    {
        float TrapDistance = (PlayerController.Player1.gameObject.transform.position - gameObject.transform.position).magnitude;

        if (Time.time > SpawnedTime + GracePeriod && !Activated)
        {
            if (TrapDistance < ActivateDistance)
            {
                Activated = true;
                LastSwitch = 0.0f;
                BombState = false;
                ActivatedTime = Time.time;
            }
            else
            {
                BombState0();
            }
        }

        float CurrentPeriod = FlashPeriod.Evaluate((Time.time - ActivatedTime) / ExplodeTime);
        if (Activated && Time.time > LastSwitch + CurrentPeriod)
        {
            if (BombState)
            {
                BombState0();
            }
            else
            {
                BombState1();
            }

            BombState = !BombState;
            LastSwitch = Time.time;
        }

        if (Activated && Time.time > ActivatedTime + ExplodeTime)
        {
            Destroy(gameObject);
            if (ExplosionVFX != null)
                Instantiate(ExplosionVFX, gameObject.transform.position, Quaternion.identity);
        }
	}
}
