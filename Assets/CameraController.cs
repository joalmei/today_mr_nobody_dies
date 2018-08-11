using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public SceneMgr scene_manager;
    public Transform m_player;
    public Vector3 m_offset;
    public float m_starting_y;
    private float m_time;
    private Camera m_camera;
    public float m_approach_factor;
    private float max_time;

    void Start(){
        m_time = 0;
        m_camera = GetComponent<Camera>();
        m_offset.y = m_starting_y;
        max_time = scene_manager.m_max_time;
    }

	// Update is called once per frame
	void Update () {
        if (m_time < max_time)
        {
            m_time += Time.deltaTime;
            m_camera.orthographicSize -= m_approach_factor * m_time;
        }
        Debug.Log(m_offset);
        m_offset.y = m_starting_y * (1 - m_time / max_time);
        transform.position = m_player.position + m_offset;
	}
}
