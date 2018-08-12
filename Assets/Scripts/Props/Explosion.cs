using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    Animator m_Animator;
    public float AnimationLength = 0.35f;

    private float AnimationStart;

	// Use this for initialization
	void Start () {
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.Play("Explosion1");

        AnimationStart = Time.time;
	}

    private void Update()
    {
        if (Time.time > AnimationStart + AnimationLength)
        {
            Destroy(gameObject);
        }
    }
}
