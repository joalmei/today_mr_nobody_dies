using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimTest : MonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public bool m_animateWithCurve;
    [ConditionalHide("m_animateWithCurve", true)]
    public AnimationCurve m_curve;
    [ConditionalHide("m_animateWithCurve", true)]
    public float m_duration;

    private float m_time;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    void Start () {

	}

    // ======================================================================================
    private void OnCollisionEnter(Collision collision)
    {
        m_time = 0;
    }

    // ======================================================================================
    void Update () {

        m_time += Time.deltaTime;

        Renderer r = this.gameObject.GetComponent<Renderer>();
        r.material.color = Color.Lerp( Color.white, Color.red, m_curve.Evaluate(m_time));
    }
}
