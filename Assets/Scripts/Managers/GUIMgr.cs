using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMgr : MonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    [Header("Player GUI")]
    public Slider               m_jetpackFuelSlider;

    [Header("God GUI")]
    public PanelSlider          m_bombSlider;
    public PanelSlider          m_turretSlider;
    public PanelSlider          m_trapSlider;

    public static Slider        JetPackFueldSlider  { get { return m_manager.m_jetpackFuelSlider; } }
    public static PanelSlider   BombSlider          { get { return m_manager.m_bombSlider; } }
    public static PanelSlider   TurretSlider        { get { return m_manager.m_turretSlider; } }
    public static PanelSlider   TrapSlider          { get { return m_manager.m_trapSlider; } }

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private static GUIMgr   m_manager;

    // ======================================================================================
    void Start ()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - GUIMgr : GUI Mgr must be unique!");
        m_manager = this;
    }
}
