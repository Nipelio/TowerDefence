using UnityEditor;
using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_StartPosition;
    [SerializeField]
    private Vector3 m_Velocity;
    [SerializeField]
    private Vector3 m_GraviCentrePosition;
    [SerializeField]
    private float m_GraviCentreMass;

    private bool m_BreakPhysics = false;
    
    
    [SerializeField]
    private float GRAVITATIONAL_CONSTANT;
    private const float TOLERANCE = 0.1f;

    private Vector3 computateAcceleration()
    {
        Vector3 direction = (m_GraviCentrePosition - transform.position).normalized;
        float range = (m_GraviCentrePosition - transform.position).magnitude;
        if (range < TOLERANCE)
        {
            return new Vector3(0f, 0f, 0f);
        }
        return direction * (GRAVITATIONAL_CONSTANT * m_GraviCentreMass / (range * range));
    }
    private void Start()
    {
        //Time.timeScale = 1;
        transform.position = m_StartPosition;
    }

    void FixedUpdate()
    {
        float deltaTime = Time.deltaTime; // fixedDeltaTime
        if (m_BreakPhysics)
        {
            transform.Translate(m_Velocity * deltaTime);
            return;
        }
        Vector3 acceleration = computateAcceleration();
        float distance = (m_GraviCentrePosition - transform.position).magnitude;
        if (distance < TOLERANCE)
        {
            m_BreakPhysics = true;
            return;
        }
        transform.Translate(m_Velocity * deltaTime + acceleration * (deltaTime * deltaTime / 2f));
        m_Velocity = m_Velocity + acceleration * Time.deltaTime;
    }
}
