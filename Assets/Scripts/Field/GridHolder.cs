using System;
using UnityEngine;

namespace Field
{
    public class GridHolder : MonoBehaviour
    {
        [SerializeField]
        private int m_GridWidth;
        [SerializeField]
        private int m_GridHeight;
        [SerializeField]
        private float m_NodeSize;

        private Grid m_Grid;
        private Camera m_Camera;
        private Vector3 m_Corner;

        private void DrawGrid()
        {
            m_Grid = new Grid(m_GridWidth, m_GridHeight);
            m_Camera = Camera.main;
            // Default plane size is 10 by 10
            float width = m_GridWidth * m_NodeSize;
            float height = m_GridHeight * m_NodeSize;
            transform.localScale = new Vector3(width * 0.1f, 1f, height * 0.1f);
            m_Corner = transform.position - (new Vector3(width, 0f, height) * 0.5f);
        }

        private void OnValidate()
        {
            DrawGrid();
        }

        private void Awake()
        {
            DrawGrid();
        }

        private void Update()
        {
            if (m_Grid == null || m_Camera == null)
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
                Vector3 difference = hitPosition - m_Corner;

                int x = (int) (difference.x / m_NodeSize);
                int y = (int) (difference.z / m_NodeSize);
                Debug.Log(x + " " + y);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.35f, 0.65f, 0.65f);
            for (int i = 0; i <= m_GridWidth; ++i)
            {
                float height = m_GridHeight * m_NodeSize;
                Vector3 startLine = m_Corner + new Vector3(i * m_NodeSize, 0f, 0f);
                Gizmos.DrawLine(startLine, startLine + new Vector3(0f, 0f, height));
            }
            for (int i = 0; i < m_GridHeight; ++i)
            {
                float width = m_GridWidth * m_NodeSize;
                Vector3 startLine = m_Corner + new Vector3(0f, 0f, i * m_NodeSize);
                Gizmos.DrawLine(startLine, startLine + new Vector3(width, 0f, 0f));
            }
        }
    }
}