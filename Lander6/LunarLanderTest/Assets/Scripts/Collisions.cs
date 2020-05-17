using UnityEngine;

namespace VektorenFormativ
{
    public static class Collisions
    {

        public static bool PointInSphere(Vector _point, Sphere _sphere)
        {
            return Vector.SqrMagnitude(_point - _sphere.center) <= _sphere.radius * _sphere.radius;
        }

        public static bool SphereInSphere(Sphere _sphere1, Sphere _sphere2)
        {
            return Vector.SqrMagnitude(_sphere1.center - _sphere2.center) <= (_sphere1.radius + _sphere2.radius) * (_sphere1.radius + _sphere2.radius);
        }

        private static Vector GetMinMax(Cuboid _cuboid, Vector _axis)
        {
            float min = float.PositiveInfinity;
            float max = float.NegativeInfinity;

            for (int i = 0; i < 8; ++i)
            {
                float proj = Vector.Dot(_cuboid.m_Vertices[i], _axis);

                if (proj < min) min = proj;
                if (proj > max) max = proj;
            }

            return new Vector(min, max, 0.0f);
        }

        private static Vector GetMinMax(Sphere _sphere, Vector _axis)
        {
            // calculate the projection of the center
            float proj = Vector.Dot(_sphere.center, _axis);

            float min = proj - _sphere.radius;
            float max = proj + _sphere.radius;

            return new Vector(min, max, 0.0f);
        }

        public static bool CuboidInCuboid(Cuboid _cuboid1, Cuboid _cuboid2)
        {
            Vector[] axises = new Vector[15];

            // get the three edges of the first cuboid
            Vector aU = _cuboid1.m_Vertices[1] - _cuboid1.m_Vertices[0];
            Vector aV = _cuboid1.m_Vertices[3] - _cuboid1.m_Vertices[0];
            Vector aW = new Vector(0, 0, 0);

            // get the three edges of the second cuboid
            Vector bU = _cuboid2.m_Vertices[1] - _cuboid2.m_Vertices[0];
            Vector bV = _cuboid2.m_Vertices[3] - _cuboid2.m_Vertices[0];
            Vector bW = new Vector(0, 0, 0); ;

            // Cuboid down/up
            axises[0] = Vector.Cross(aU, aV);
            axises[1] = Vector.Cross(bU, bV);

            // Cuboid front/back
            axises[2] = Vector.Cross(aU, aW);
            axises[3] = Vector.Cross(bU, bW);

            // Cuboid left/right
            axises[4] = Vector.Cross(aV, aW);
            axises[5] = Vector.Cross(bV, bW);

            // interaxis
            axises[6] = Vector.Cross(aU, bU);
            axises[7] = Vector.Cross(aU, bV);
            axises[8] = Vector.Cross(aU, bW);
            axises[9] = Vector.Cross(aV, bU);
            axises[10] = Vector.Cross(aV, bV);
            axises[11] = Vector.Cross(aV, bW);
            axises[12] = Vector.Cross(aW, bU);
            axises[13] = Vector.Cross(aW, bV);
            axises[14] = Vector.Cross(aW, bW);

            // iterate over them
            for (int i = 0; i < axises.Length; ++i)
            {
                Vector axis = axises[i];

                // ignore all axis which have no length
                if (Vector.SqrMagnitude(axis) == 0.0f) continue;

                axis = Vector.Normalize(axis);

                // get the min max of the first cuboid
                Vector minMax1 = GetMinMax(_cuboid1, axis);
                float min1 = minMax1.X;
                float max1 = minMax1.Y;

                // get the min max of the second cuboid
                Vector minMax2 = GetMinMax(_cuboid2, axis);
                float min2 = minMax2.X;
                float max2 = minMax2.Y;

                // check if an intersection on the axis happened
                bool intersection = (min1 >= min2 && min1 <= max2)
                                    || (min2 >= min1 && min2 <= max1);

                // if no intersection on the given axis happend
                if (!intersection)
                {
                    // we found a dividing plane
                    return false;
                }
            }

            // intersections on all axis found, there is no dividing plane
            return true;
        }

        public static bool PointInCuboid(Vector _point, Cuboid _cuboid)
        {
            // create a cuboid at _point, of which all edges length are 0
            Cuboid pointCuboid = new Cuboid(new Vector[8] { _point, _point, _point, _point, _point, _point, _point, _point });

            // use the cuboid with cuboid check
            return CuboidInCuboid(pointCuboid, _cuboid);
        }

        public static Vector CenterOfCuboid(Cuboid _cuboid)
        {
            return new Vector(_cuboid.m_Vertices[0].X + (_cuboid.m_Vertices[1].X * 0.5f), (_cuboid.m_Vertices[0].Y + (_cuboid.m_Vertices[3]).Y * 0.5f), 0);
        }

        public static bool CuboidInSphere(Cuboid _cuboid, Sphere _sphere)
        {
            Vector circleDistance = new Vector
                (Mathf.Abs(_cuboid.m_Center.X - _sphere.center.X),
                (Mathf.Abs(_cuboid.m_Center.Y - _sphere.center.Y)),
                0);

            if (circleDistance.X > (_cuboid.m_Width / 2 + _sphere.radius))
            { return false; }
            if (circleDistance.Y > (_cuboid.m_Height / 2 + _sphere.radius))
            { return false; }
            if (circleDistance.X <= (_cuboid.m_Width / 2))
            { return true; }
            if (circleDistance.Y <= (_cuboid.m_Height / 2))
            { return true; }

            float cornerdistance = (circleDistance.X - _cuboid.m_Width / 2) * (circleDistance.X - _cuboid.m_Width / 2) +
                                    (circleDistance.Y - _cuboid.m_Height / 2) * (circleDistance.Y - _cuboid.m_Height / 2);

            return (cornerdistance <= (_sphere.radius) * (_sphere.radius));
        }
    }
}