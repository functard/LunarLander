using UnityEngine;

namespace VektorenFormativ
{
    public class Cuboid
    {
        public readonly Vector[] m_Vertices = new Vector[8];

        public Vector m_Center
        {
            get
            {
                return new Vector(m_Vertices[0].X + (m_Vertices[1].X * 0.5f),
                                 (m_Vertices[0].Y + (m_Vertices[3]).Y * 0.5f)
                                 , 0);
            }
        }

        public float m_Width
        {
            get { return (Mathf.Abs(m_Vertices[0].X - m_Vertices[1].X)); }
        }

        public float m_Height
        {
            get
            {
                return (Mathf.Abs(m_Vertices[0].Y - m_Vertices[3].Y));
            }
        }

        /// <summary>
        /// Im Uhrzeigersinn von unten nach oben angeben
        /// </summary>
        /// <param name="_vertices"></param>
        public Cuboid(Vector[] _vertices)
        {
            if (_vertices.Length != 8)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            m_Vertices = _vertices;
        }
    }
}
