using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform m_player;
    public Vector3  m_offset;
    public float    m_starting_y;
    public float    m_approach_factor;
    public float    m_max_size;
    public float    m_min_size;
    public float    valid_margin;

    private float   m_aspect;
    private float   m_time;
    private Camera  m_camera;
    private float   max_time;
    private Vector2 min_pos;
    private Vector2 max_pos;

    bool isValidDeploymentPosition(Vector2 v)
    {
        if ((min_pos.x < v.x && v.x < max_pos.x)
         && (min_pos.y < v.y && v.y < max_pos.y)
         && ((v.x < min_pos.x + valid_margin * m_aspect)
          || (v.x > max_pos.x - valid_margin * m_aspect)
          || (v.y > max_pos.y - valid_margin))) 
        {
            return true;
        }
        return false;
    }

    void Start(){
        m_time = 0;
        m_camera = GetComponent<Camera>();
        m_offset.y = m_starting_y;
        max_time = SceneMgr.MaxTime;
        m_aspect = m_camera.aspect;
    }

	// Update is called once per frame
	void LateUpdate () {
        if (SceneMgr.IsGameOver || GameMgr.IsPaused)
        {
            return;
        }

        if (m_time < max_time)
        {
            m_time += GameMgr.DeltaTime;
            m_camera.orthographicSize = m_max_size - (m_max_size - m_min_size) / max_time * m_time;
        }
        else
        {
            SceneMgr.SetGameOver(2);
        }
        m_offset.y = m_starting_y * (1 - m_time / max_time);
        transform.position = m_player.position + m_offset;

        float camVertExtent = m_camera.orthographicSize;
        float camHorzExtent = m_camera.aspect * camVertExtent;

        transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, SceneMgr.MinX + camHorzExtent, SceneMgr.MaxX - camHorzExtent),
                Mathf.Clamp(transform.position.y, SceneMgr.MinY + camVertExtent, SceneMgr.MaxY - camVertExtent),
                transform.position.z
            );

        float x_min = transform.position.x - m_aspect * m_camera.orthographicSize;
        float x_max = transform.position.x + m_aspect * m_camera.orthographicSize;
        float y_min = transform.position.y - m_camera.orthographicSize;
        float y_max = transform.position.y + m_camera.orthographicSize;
        min_pos = new Vector2(x_min, y_min);
        max_pos = new Vector2(x_max, y_max);
    }
}
