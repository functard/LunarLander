using Math = System.Math;

namespace VektorenFormativ
{
    public class Matrix
    {
        private readonly float[,] Values = new float[4, 4];

        public Matrix()
        {
        }

        public Matrix(float[,] _values)
        {
            if (_values.GetLength(0) != 4 || _values.GetLength(1) != 4)
            {
                throw new System.Exception();
            }

            Values = _values;
        }

        public static Matrix Translate(Vector _v)
        {
            return new Matrix(new float [,]
                { { 1,0,0,0},
                { 0,1,0,0},
                { 0,0,1,0},
                { _v.X, _v.Y, _v.Z, 1} }
                );
        }

        public static Matrix Rotation(Vector _v)
        {
            return RotateX(_v.X) * RotateY(_v.Y) * RotateZ(_v.Z);
        }

        private static Matrix RotateX(float _x)
        {
            float sinX = (float) Math.Sin((double)_x / 180 * Math.PI);
            sinX = (int)(sinX * 1000000000)/ 1000000000.0f;
            float cosX = (float) Math.Cos((double) _x / 180 * Math.PI);
            cosX = (int)(cosX * 1000000000) / 1000000000.0f;

            return new Matrix(new float[,]
                { { 1,0,0,0},
                { 0,cosX,sinX,0},
                { 0,-sinX,cosX,0},
                { 0, 0, 0, 1} }
                );
        }

        private static Matrix RotateY(float _y)
        {
            float sinY = (float)Math.Sin((double)_y / 180 * Math.PI);
            sinY = (int)(sinY * 1000000000) / 1000000000.0f;
            float cosY = (float)Math.Cos((double)_y / 180 * Math.PI);
            cosY = (int)(cosY * 1000000000) / 1000000000.0f;

            return new Matrix(new float[,]
                { { cosY,0,-sinY,0},
                { 0,1,0,0},
                { sinY,0,cosY,0},
                { 0, 0, 0, 1} }
                );
        }

        private static Matrix RotateZ(float _z)
        {
            float sinZ = (float)Math.Sin((double)_z / 180 * Math.PI);
            sinZ = (int)(sinZ * 1000000000) / 1000000000.0f;
            float cosZ = (float)Math.Cos((double)_z / 180 * Math.PI);
            cosZ = (int)(cosZ * 1000000000) / 1000000000.0f;

            return new Matrix(new float[,]
                { { cosZ,sinZ,0,0},
                { -sinZ,cosZ,0,0},
                { 0,0,1,0},
                { 0, 0, 0, 1} }
                );
        }

        public static Matrix Scale(Vector _v)
        {
            return new Matrix(new float[,]
                { { _v.X,0,0,0},
                { 0,_v.Y,0,0},
                { 0,0,_v.Z,0},
                { 0, 0, 0, 1} }
                );
        }

        public static Matrix TRS(Vector _pos, Vector _rot, Vector _scale)
        {
            return Scale(_scale) * Rotation(_rot) * Translate(_pos);
        }

        public static Matrix operator *(Matrix _m1, Matrix _m2)
        {
            if (_m1.Values.GetLength(0) != _m2.Values.GetLength(0) ||
                _m1.Values.GetLength(1) != _m2.Values.GetLength(1))
            {
                throw new System.Exception();
            }

            Matrix result = new Matrix();

            for (int i = 0; i < _m1.Values.GetLength(0); i++)
            {
                for (int j = 0; j < _m1.Values.GetLength(1); j++)
                {
                    for (int k = 0; k < _m1.Values.GetLength(0); k++)
                    {
                        result.Values[i, j] += _m1.Values[i,k] * _m2.Values[k, j];
                    }
                }
            }

            return result;
        }

        public static Vector operator *(Matrix _m, Vector _v)
        {
            return new Vector(_m.Values[0,0] * _v.X + _m.Values[1, 0] * _v.Y + _m.Values[2, 0] * _v.Z + _m.Values[3, 0],
                                _m.Values[0, 1] * _v.X + _m.Values[1, 1] * _v.Y + _m.Values[2, 1] * _v.Z + _m.Values[3, 1],
                                _m.Values[0, 2] * _v.X + _m.Values[1, 2] * _v.Y + _m.Values[2, 2] * _v.Z + _m.Values[3, 2]);
        }
    }
}
