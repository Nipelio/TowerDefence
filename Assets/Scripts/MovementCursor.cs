using System;
using UnityEngine;

namespace Field
{
    public class MovementCursor : MonoBehaviour
    {
        [SerializeField]
        private int m_GridWidth; //WE NEED TO GET IT FROM GRIDHOLDER!!!
        [SerializeField]
        private int m_GridHeight;//WE NEED TO GET IT FROM GRIDHOLDER!!!
        [SerializeField]
        private float m_NodeSize;//WE NEED TO GET IT FROM GRIDHOLDER!!!
        [SerializeField]
        private MovementAgent m_MovementAgent;
        [SerializeField]
        private GameObject m_Cursor;
        [SerializeField] 
        private bool SimplifyMovement;
        
        private Camera m_Camera;
        private Vector3 m_Corner;
        private void Awake()
        {
            m_Camera = Camera.main;
            // Default plane size is 10 by 10
            float width = m_GridWidth * m_NodeSize;
            float height = m_GridHeight * m_NodeSize;
            transform.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);
            m_Corner = transform.position - (new Vector3(width, 0f, height) * 0.5f);
        }

        private void Update()
        {
            if (m_Camera == null || m_MovementAgent == null || m_Cursor == null)
            {
                return;
            }

            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform != transform)
                {
                    return;
                }
                Vector3 hitPosition = hit.point;
                Vector3 hittedNodeCentre = computateCentreOfNode(hitPosition);
                if (SimplifyMovement)
                {
                    m_Cursor.transform.position = hitPosition;
                }
                else
                {
                    m_Cursor.transform.position = hittedNodeCentre;
                }
                m_Cursor.SetActive(true);
                if (Input.GetMouseButtonDown(1))
                {
                    if (SimplifyMovement)
                    {
                        m_MovementAgent.SetTargetAndSpeed(hitPosition, 1f);
                    }
                    else 
                    {
                        m_MovementAgent.SetTargetAndSpeed(hittedNodeCentre, 1f);
                    }
                }
            }
            else
            {
                m_Cursor.SetActive(false);
            }
        }

        private Vector3 computateCentreOfNode(Vector3 hitPosition)
        {
            Vector3 difference = hitPosition - m_Corner;
            int x = (int) (difference.x / m_NodeSize);
            int z = (int) (difference.z / m_NodeSize);
            Vector3 nodeCentre = m_Corner + new Vector3((x + 0.5f)* m_NodeSize, 0f, (z + 0.5f)* m_NodeSize);
            return nodeCentre;
        }
    }
}
