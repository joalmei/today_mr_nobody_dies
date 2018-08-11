using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    [Header("Walk")]
    public float            m_maxWalkSpeed              = 7;
    public float            m_walkAcc                   = 1;

    [Header("Dash")]
    public AnimationCurve   m_dashSpeed                 = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
    public float            m_dashDuration              = 0.1f;

    [Header("Jetpack")]
    public float            m_maxJetPackSpeed = 5;
    public float            m_jetPackAccUp              = 12;
    public float            m_jetPackAccDown            = 5;
    
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private float           m_walkSpeed                 = 0;
    private float           m_jetPackSpeed              = 0;

    private const float     MIN_SPEED_TO_MOVE           = 0.1f;
    private const float     GROUND_Y_VALUE_TO_DELETE    = -2.35f;
    

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Update ()
    {
        float   horizontal      = Input.GetAxis("Horizontal");
        bool    enableJetPack   = Input.GetButton("Jump");
        bool    doDash          = Input.GetButtonDown("Dash");

        UpdateTransform(horizontal, enableJetPack, doDash);
        UpdateGravity(enableJetPack);
	}

    // ======================================================================================
    // PRIVATE MEMBERS
    // ======================================================================================
    private void UpdateTransform(float _inputHorizontal, bool _inputJetpack, bool _doDash)
    {
        m_walkSpeed = Mathf.Lerp(m_walkSpeed, m_maxWalkSpeed * _inputHorizontal, Time.deltaTime * m_walkAcc);

        this.transform.position += Vector3.right * Time.deltaTime * m_walkSpeed;

        if (_doDash)
        {
            this.transform.position += Vector3.right * Time.deltaTime * m_walkSpeed * 7;
        }

        if (_inputJetpack)
        {
            m_jetPackSpeed = Mathf.Lerp(m_jetPackSpeed, m_maxJetPackSpeed, Time.deltaTime * m_jetPackAccUp);
        }
        else
        {
            if (m_jetPackSpeed > MIN_SPEED_TO_MOVE)
            {
                m_jetPackSpeed = Mathf.Lerp(m_jetPackSpeed, 0, Time.deltaTime * m_jetPackAccDown);
            }
            else
            {
                m_jetPackSpeed = 0;
            }
        }

        if (m_jetPackSpeed > 0)
        {
            this.transform.position += Vector3.up * Time.deltaTime * m_jetPackSpeed;
        }
    }

    // ======================================================================================
    private void UpdateGravity (bool _enableJetPack)
    {
        //if (!_enableJetPack)
        {
            this.transform.position += Time.deltaTime * Physics.gravity * .7f;

            if (this.transform.position.y < GROUND_Y_VALUE_TO_DELETE)
            {
                this.transform.position = new Vector3(this.transform.position.x, GROUND_Y_VALUE_TO_DELETE, this.transform.position.z);
            }
        }
    }
}
