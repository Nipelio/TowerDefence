﻿using UnityEngine;

namespace  Field
{
    public class Grid
    {
        private Node[,] m_Nodes;
        private int m_Width;
        private int m_Height;

        public Grid(int width, int height)
        {
            m_Width = width;
            m_Height = height;

            m_Nodes = new Node[m_Width, m_Height];

            for (int i = 0; i < m_Width; ++i)
            {
                for (int j = 0; j < m_Height; ++j)
                {
                    m_Nodes[i, j] = new Node();
                }
            }
        }

        public Node GetNode(Vector2Int coordinates)
        {
            if (coordinates.x < 0 || coordinates.x > m_Width || coordinates.y < m_Height || coordinates.y >= m_Height)
            {
                return null;
            }
            return m_Nodes[coordinates.x, coordinates.y];
        }

        public Node GetNode(int i, int j)
        {
            if (i < 0 || i >= m_Width || j < m_Height || j >= m_Height)
            {
                return null;
            }
            return m_Nodes[i, j];
        }
    }
}