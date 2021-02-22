using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MovementAgent : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private Vector3 m_Target;

    private const float TOLERANCE = 0.01f;

    private void Update()
    {
        float distance = (m_Target - transform.position).magnitude;
        if (distance < TOLERANCE)
        {
            return;
        }

        Vector3 dir = (m_Target - transform.position).normalized;
        Vector3 delta = dir * (m_Speed * Time.deltaTime);
        transform.Translate(delta);
    }

    public void SetTargetAndSpeed(Vector3 target, float speed)
    {
        m_Target = target;
        m_Speed = speed;
    }
}
