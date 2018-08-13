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

        AnimationStart = GameMgr.Timer;
	}

    private void Update()
    {
        if (GameMgr.Timer > AnimationStart + AnimationLength)
        {
            Destroy(gameObject);
        }
    }
}
