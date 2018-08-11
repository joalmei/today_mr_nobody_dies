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
    public float            m_dashMaxSpeed              = 5;
    public float            m_dashDuration              = 0.1f;
    public float            m_dashCoolDownDuration      = 1;

    [Header("Jetpack")]
    public float            m_maxJetPackSpeed           = 5;
    public float            m_jetPackAccUp              = 12;
    public float            m_jetPackAccDown            = 5;
    public float            m_maxJetpackFuel            = 2;

    public static PlayerController Player1 { get; protected set; }

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    // walk
    private float           m_walkSpeed                 = 0;

    // jetpack
    private float           m_jetPackSpeed              = 0;
    private float           m_jetpackFuel               = 0;

    // dash
    private float           m_dashTimer                 = 0;
    private Vector2         m_dashDirection             = Vector2.zero;
    private float           m_dashCooldownTimer         = 0;

    // DEFINES
    private const float     MIN_SPEED_TO_MOVE           = 0.1f;
    private const float     GROUND_Y_VALUE_TO_DELETE    = -2.5f;

    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        Debug.Assert(Player1 == null, this.gameObject.name + " - PlayerController : Player 1 must be UNIQUE!");
        Player1 = this;

        m_dashTimer         = m_dashDuration;
        m_dashDirection     = Vector2.right;

        m_jetpackFuel       = m_maxJetpackFuel;
        m_dashCooldownTimer = m_dashCoolDownDuration;
    }

    // ======================================================================================
    public void Update ()
    {
        // get input
        float   horizontal      = Input.GetAxis("Horizontal");
        float   vertical        = Input.GetAxis("Vertical");
        bool    enableJetPack   = Input.GetButton("Jump");
        bool    doDash          = Input.GetButtonDown("Dash");


        // update position
        UpdateTransform(horizontal, vertical, enableJetPack, doDash);
        UpdateGravity();
	}

    // ======================================================================================
    // PRIVATE MEMBERS
    // ======================================================================================
    private void UpdateTransform(float _inputHorizontal, float _inputVertical, bool _jetpackUp, bool _doDash)
    {
        UpdateWalk(_inputHorizontal);
        UpdateDash(_inputHorizontal, _inputVertical, _doDash);
        UpdateJetpack(_jetpackUp);
    }

    // ======================================================================================
    private void UpdateWalk (float _inputHorizontal)
    {
        // walk
        m_walkSpeed = Mathf.Lerp(m_walkSpeed, m_maxWalkSpeed * _inputHorizontal, Time.deltaTime * m_walkAcc);
        this.transform.position += Vector3.right * Time.deltaTime * m_walkSpeed;
    }

    // ======================================================================================
    private void UpdateJetpack(bool _jetpackUp)
    {
        if (_jetpackUp)
        {
            m_jetpackFuel = Mathf.Max(0, m_jetpackFuel - Time.deltaTime);
        }
        else
        {
            m_jetpackFuel = Mathf.Min(m_maxJetpackFuel, m_jetpackFuel + Time.deltaTime);
        }

        if (_jetpackUp && m_jetpackFuel > 0)
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
    private void UpdateDash(float _inputHorizontal, float _inputVertical, bool _doDash)
    {
        m_dashCooldownTimer -= Time.deltaTime;

        // Init Dash
        if (_doDash && m_dashCooldownTimer < 0)
        {
            m_dashTimer = 0;
            m_dashCooldownTimer = m_dashCoolDownDuration;
        }
        m_dashTimer += Time.deltaTime;

        // Keep memory of dash direction
        if (_inputHorizontal != 0 || _inputVertical != 0)
        {
            m_dashDirection.x = _inputHorizontal;
            m_dashDirection.y = _inputVertical;
        }

        m_dashDirection.Normalize();

        Debug.DrawRay(this.transform.position, new Vector3(m_dashDirection.x, m_dashDirection.y), Color.magenta);

        // Dash if necessary
        if (m_dashTimer < m_dashDuration)
        {
            this.transform.position += new Vector3(m_dashDirection.x, m_dashDirection.y) * Time.deltaTime * m_dashMaxSpeed * m_dashSpeed.Evaluate(m_dashTimer / m_dashDuration);
        }
    }

    // ======================================================================================
    private void UpdateGravity ()
    {
        this.transform.position += Time.deltaTime * Physics.gravity * .6f;

        if (this.transform.position.y < GROUND_Y_VALUE_TO_DELETE)
        {
            this.transform.position = new Vector3(this.transform.position.x, GROUND_Y_VALUE_TO_DELETE, this.transform.position.z);
        }
    }
}
