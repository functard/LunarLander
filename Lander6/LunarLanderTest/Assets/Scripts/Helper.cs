using UnityEngine;
using VektorenFormativ;

public class Helper : MonoBehaviour
{
    public static Vector3 ConvertMyVectorToUnityVector(Vector _vector)
    {
        return new Vector3(_vector.X, _vector.Y, _vector.Z);
    }

    public static Vector ConvertUnityVectorToMyVector(Vector3 _vector3)
    {
        return new Vector(_vector3.x, _vector3.y, _vector3.z);
    }
}


