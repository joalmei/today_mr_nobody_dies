using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum eStates
    {
        Idle,
        Walking,
        JetpackUp,
        Falling,
        Dashing
    }


    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    [Header("Animation")]
    public Animator         m_animator;
    public SpriteRenderer   m_spriteRenderer;
    public string           m_isRunningBoolParam        = "IsRunning";
    public string           m_isDashingBoolParam        = "IsDashing";
    public string           m_isJetpackUpBoolParam      = "IsJetpackUp";
    public string           m_isFallingBoolParam        = "IsFalling";

    public float            m_minSpeedToWalkAnim        = .2f;

    [Header("Locomotion")]
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

    [Header("Dimensions")]
    public float            m_width                     = .1f;
    public float            m_height                    = .1f;

    // TODO : Create Ground
    [Header("Powers")]
    [Header("Create Ground")]
    public float            m_groundOffset              = 1;

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

    private eStates         m_state;

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
        //bool    createGround    = Input.GetButton("Create Ground");


        // update position
        UpdateTransform(horizontal, vertical, enableJetPack, doDash);
        UpdateGravity();
        UpdateAnimator();
	}


    // ======================================================================================
    public void TakeDamage()
    {
    }

    // ======================================================================================
    // PRIVATE MEMBERS
    // ======================================================================================
    private void UpdateTransform(float _inputHorizontal, float _inputVertical, bool _jetpackUp, bool _doDash)
    {
        UpdateWalk(_inputHorizontal);
        UpdateJetpack(_jetpackUp);
        UpdateDash(_inputHorizontal, _inputVertical, _doDash);
    }

    // ======================================================================================
    private void UpdateWalk (float _inputHorizontal)
    {
        // walk
        m_walkSpeed = Mathf.Lerp(m_walkSpeed, m_maxWalkSpeed * _inputHorizontal, Time.deltaTime * m_walkAcc);
        this.transform.position += Vector3.right * Time.deltaTime * m_walkSpeed;

        m_spriteRenderer.flipX = m_walkSpeed < 0.0f;

        if (Mathf.Abs(m_walkSpeed) > m_minSpeedToWalkAnim)
        {
            m_state = eStates.Walking;
        }
        else
        {
            m_state = eStates.Idle;
        }
    }

    // ======================================================================================
    private void UpdateJetpack(bool _jetpackUp)
    {
        // Fuel
        if (_jetpackUp)
        {
            m_jetpackFuel = Mathf.Max(0, m_jetpackFuel - Time.deltaTime);
        }
        else
        {
            m_jetpackFuel = Mathf.Min(m_maxJetpackFuel, m_jetpackFuel + Time.deltaTime);
        }

        if (m_jetpackFuel < m_maxJetpackFuel)
        {
            m_state = eStates.JetpackUp;
            GUIMgr.JetPackFueldSlider.fillRect.gameObject.SetActive(true);
            GUIMgr.JetPackFueldSlider.value = 1 - m_jetpackFuel / m_maxJetpackFuel;
        }
        else
        {
            GUIMgr.JetPackFueldSlider.fillRect.gameObject.SetActive(false);
        }

        // Translation
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
            this.transform.position = CheckCollision(this.transform.position, this.transform.position + Vector3.up * Time.deltaTime * m_jetPackSpeed);
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
            this.transform.position = CheckCollision(   this.transform.position,
                                                        this.transform.position + new Vector3(m_dashDirection.x, m_dashDirection.y) * Time.deltaTime * m_dashMaxSpeed * m_dashSpeed.Evaluate(m_dashTimer / m_dashDuration));

            m_state = eStates.Dashing;
        }
    }

    // ======================================================================================
    private void UpdateGravity ()
    {
        this.transform.position = CheckCollision(   this.transform.position,
                                                    this.transform.position + Time.deltaTime * Physics.gravity * .6f);

        //if (this.transform.position.y < GROUND_Y_VALUE_TO_DELETE)
        //{
        //    this.transform.position = new Vector3(this.transform.position.x, GROUND_Y_VALUE_TO_DELETE, this.transform.position.z);
        //}
    }

    // ======================================================================================
    private Vector3 CheckCollision (Vector3 _startPos, Vector3 _endPos)
    {
        RaycastHit  hitInfo;
        Vector3     direction = _endPos - _startPos;
        Vector3     finalEndPos = _endPos;

        if (direction.y < 0)
        {
            if (Physics.Raycast(_startPos, direction, out hitInfo, direction.magnitude, ~(1 << this.gameObject.layer)))
            {
                Ground gnd = hitInfo.collider.gameObject.GetComponent<Ground>();

                if (gnd != null)
                {
                    finalEndPos.y = gnd.SurfaceY();
                }
            }
        }

        finalEndPos.x = Mathf.Clamp(finalEndPos.x, SceneMgr.MinX + m_width / 2, SceneMgr.MaxX - m_width / 2);
        finalEndPos.y = Mathf.Clamp(finalEndPos.y, SceneMgr.MinY, SceneMgr.MaxY /*- m_height / 2*/);
        return finalEndPos;
    }

    // ======================================================================================
    private void UpdateAnimator ()
    {
        switch(m_state)
        {
            case eStates.Idle:
                m_animator.SetBool(m_isDashingBoolParam, false);
                m_animator.SetBool(m_isFallingBoolParam, false);
                m_animator.SetBool(m_isJetpackUpBoolParam, false);
                m_animator.SetBool(m_isRunningBoolParam, false);
                break;

            case eStates.Walking:
                m_animator.SetBool(m_isDashingBoolParam, false);
                m_animator.SetBool(m_isFallingBoolParam, false);
                m_animator.SetBool(m_isJetpackUpBoolParam, false);
                m_animator.SetBool(m_isRunningBoolParam, true);
                break;

            case eStates.JetpackUp:
                m_animator.SetBool(m_isDashingBoolParam, false);
                m_animator.SetBool(m_isFallingBoolParam, false);
                m_animator.SetBool(m_isJetpackUpBoolParam, true);
                m_animator.SetBool(m_isRunningBoolParam, false);
                break;

            case eStates.Falling:
                m_animator.SetBool(m_isDashingBoolParam, false);
                m_animator.SetBool(m_isFallingBoolParam, true);
                m_animator.SetBool(m_isJetpackUpBoolParam, false);
                m_animator.SetBool(m_isRunningBoolParam, false);
                break;
                
            case eStates.Dashing:
                m_animator.SetBool(m_isDashingBoolParam, true);
                m_animator.SetBool(m_isFallingBoolParam, false);
                m_animator.SetBool(m_isJetpackUpBoolParam, false);
                m_animator.SetBool(m_isRunningBoolParam, false);
                break;
        }
    }
}
