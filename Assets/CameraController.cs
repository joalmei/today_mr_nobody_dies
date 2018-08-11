using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform m_player;
    public Vector3 m_offset;
    private float m_time;
    public float m_approach_factor;

    void Start(){
        m_time = 0;
    }

	// Update is called once per frame
	void Update () {
        m_time += Time.deltaTime;
        Debug.Log(m_time);
        m_offset.z += m_approach_factor * m_time;
        transform.position = m_player.position + m_offset;
	}
}
