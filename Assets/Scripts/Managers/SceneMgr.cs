using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public float            m_globalZ = 0;
    public static float     GlobalZ { get { return m_manager.m_globalZ; } }
    public float            m_max_time = 60;
    public static float     MaxTime { get { return m_manager.m_max_time; } }
    public static bool      isGameOver;
    
    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private static SceneMgr m_manager;


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake ()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - SceneMgr : scene manager must be unique!");
        m_manager = this;
        isGameOver = false;
	}
}
