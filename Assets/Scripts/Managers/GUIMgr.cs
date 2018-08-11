using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMgr : MonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public Slider           m_jetpackFuelSlider;

    public static Slider    JetPackFueldSlider { get { return m_manager.m_jetpackFuelSlider; } }

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private static GUIMgr   m_manager;

    // ======================================================================================
    void Start ()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - GUIMgr : GUI Mgr must be unique!");
        m_manager = this;
    }
}
