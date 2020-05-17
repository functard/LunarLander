using Math = System.Math;

namespace VektorenFormativ
{
    public struct Vector
    {
        public float X;
        public float Y;
        public float Z;

        public Vector(float _x = 0f, float _y = 0f, float _z = 0f)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }

        public static Vector Up {get { return new Vector(0f, 1f, 0f); } }

        public static Vector Zero { get { return new Vector(0f, 0f, 0f); } }


        public static Vector operator +(Vector _a, Vector _b)
        {
            return new Vector(_a.X + _b.X, _a.Y + _b.Y, _a.Z + _b.Z);
        }

        public static Vector operator -(Vector _a, Vector _b)
        {
            return _a + (-_b);
        }

        public static Vector operator -(Vector _a)
        {
            return _a * -1;
        }
        public static Vector operator -(Vector _a, float _b)
        {
            return new Vector(_a.X - _b, _a.Y - _b, _a.Z - _b);
        }

        public static Vector operator *(Vector _a, float _b)
        {
            return new Vector(_a.X * _b, _a.Y * _b, _a.Z * _b);
        }

        public static Vector operator /(Vector _a, float _b)
        {
            return _a * (1 / _b);
        }

        public override bool Equals(object _obj)
        {
            Vector v = (Vector)_obj;
            return X == v.X && Y == v.Y && Z == v.Z;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static float Magnitude(Vector _v)
        {
            return (float)Math.Sqrt(SqrMagnitude(_v));
        }

        public static float SqrMagnitude(Vector _v)
        {
            return _v.X * _v.X + _v.Y * _v.Y + _v.Z * _v.Z;
        }

        public static Vector Cross(Vector _v1, Vector _v2)
        {
            return new Vector(_v1.Y * _v2.Z - _v1.Z * _v2.Y,
                            _v1.Z * _v2.X - _v1.X * _v2.Z,
                            _v1.X * _v2.Y - _v1.Y * _v2.X);
        }

        public static float Dot(Vector _v1, Vector _v2)
        {
            return _v1.X * _v2.X + _v1.Y * _v2.Y + _v1.Z * _v2.Z;
        }

        public static Vector Normalize(Vector _v)
        {
            return _v / Magnitude(_v);
        }

        public static Vector MoveTowards(Vector current, Vector target, float maxDistanceDelta)
        {
            Vector a = target - current;
            float magnitude = Magnitude(a);
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }
    }
}
