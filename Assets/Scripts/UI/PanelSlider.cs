using UnityEngine;

[ExecuteInEditMode]
public class PanelSlider : MonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public float    m_rightMin  = 0;
    public float    m_rightMax  = 0;

    public bool     m_debug     = false;

    [ConditionalHide("m_debug", true)]
    public float    m_testRatio;
    

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private RectTransform m_rect;


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Start()
    {
        m_rect = this.gameObject.GetComponent<RectTransform>();
    }

    // ======================================================================================
    public void Update()
    {
        if (m_debug)
        {
            m_testRatio = Mathf.Clamp01(m_testRatio);
            SetSlider(m_testRatio);
        }
    }

    // ======================================================================================
    public void SetSlider (float _ratio)
    {
        m_rect.offsetMax = new Vector2(
            Mathf.Lerp(m_rightMin, m_rightMax, Mathf.Clamp01(_ratio)),
            m_rect.offsetMax.y);
    }
}
