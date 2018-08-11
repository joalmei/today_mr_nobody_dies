using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public Color NormalColor;
    public Color FlashColor;

    public AnimationCurve FlashPeriod;
    public float ExplodeTime;

	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<Material>().color = NormalColor;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
