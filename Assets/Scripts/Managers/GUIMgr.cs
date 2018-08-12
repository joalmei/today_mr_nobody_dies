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

    public GameObject[]         m_lives;

    public GameObject           m_gameOver;
    public GameObject           m_player1Wins;
    public GameObject           m_player2Wins;

    public static Slider        JetPackFueldSlider  { get { return m_manager.m_jetpackFuelSlider; } }
    public static PanelSlider   BombSlider          { get { return m_manager.m_bombSlider; } }
    public static PanelSlider   TurretSlider        { get { return m_manager.m_turretSlider; } }
    public static PanelSlider   TrapSlider          { get { return m_manager.m_trapSlider; } }

    public static GameObject GameOverPanel          { get { return m_manager.m_gameOver; } }
    public static GameObject GameOverP1Panel        { get { return m_manager.m_player1Wins; } }
    public static GameObject GameOverP2Panel        { get { return m_manager.m_player2Wins; } }

    // -------------------------------- PRIVATE ATTRIBUTES ------------------------------- //
    private static GUIMgr       m_manager;

    // ======================================================================================
    void Start ()
    {
        Debug.Assert(m_manager == null, this.gameObject.name + " - GUIMgr : GUI Mgr must be unique!");
        m_manager = this;

        Debug.Assert(m_lives != null, this.gameObject.name + " - GUIMgr : Missing Lives");
    }

    // ======================================================================================
    public static  void SetLives(int _nbLives)
    {
        for (int i = 0; i < m_manager.m_lives.Length; i++)
        {
            m_manager.m_lives[i].SetActive(i < (_nbLives - 1));
        }
    }
}
