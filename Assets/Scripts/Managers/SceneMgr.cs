using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public enum ePlayer
    {
        Player1,
        Player2
    };

    // -------------------------------- PUBLIC ATTRIBUTES -------------------------------- //
    public float            m_globalZ = 0;
    public float            m_max_time = 60;
    public Transform        m_limitUp;
    public Transform        m_limitDown;
    public Transform        m_limitLeft;
    public Transform        m_limitRight;

    public static float     GlobalZ { get { return m_manager.m_globalZ; } }
    public static float     MaxTime { get { return m_manager.m_max_time; } }

    public static float     MaxY { get { return m_manager.m_limitUp.position.y; } }
    public static float     MinY { get { return m_manager.m_limitDown.position.y; } }
    public static float     MaxX { get { return m_manager.m_limitRight.position.x; } }
    public static float     MinX { get { return m_manager.m_limitLeft.position.x; } }

    public static bool     IsGameOver { get; private set; }

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private static SceneMgr m_manager;


    // ======================================================================================
    // PUBLIC MEMBERS
    // ======================================================================================
    public void Awake ()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - SceneMgr : scene manager must be unique!");
        m_manager = this;
        IsGameOver = false;
	}

    // ======================================================================================
    public static void SetGameOver (ePlayer _player)
    {
        IsGameOver = true;

        GUIMgr.GameOverPanel.SetActive(true);

        switch (_player)
        {
            case ePlayer.Player1:
                GUIMgr.GameOverP1Panel.SetActive(true);
                break;
            case ePlayer.Player2:
                GUIMgr.GameOverP2Panel.SetActive(true);
                break;
        }
    }
}
