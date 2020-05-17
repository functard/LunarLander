using UnityEngine;
using VektorenFormativ;
    
//THIS SCRIPTS EXECUTION ORDER IS SET TO -50 SO IT ALWAYS GETS EXECUTED BEFORE MY OTHER SCRIPTS.
public class MySphereCollider : MonoBehaviour
{
    public Sphere Sphere
    {
        get { return sphere; }

        set { sphere = value; }
    }
    
    private Sphere sphere;

    [SerializeField]
    private float offsetX, offsetY;

    private Vector offset;

    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    [SerializeField]
    private float radius;

    private Vector center;

    [SerializeField]
    private bool isStatic;

    private void Start()
    {
        offset = new Vector(offsetX, offsetY);

        center = Helper.ConvertUnityVectorToMyVector(transform.position) + offset;

        sphere = new Sphere(center, radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStatic)
        {
            sphere.center = Helper.ConvertUnityVectorToMyVector(transform.position + Helper.ConvertMyVectorToUnityVector(offset));
        }   
    }

    private void OnDrawGizmos()
    {
        if(sphere != null)
         Gizmos.DrawWireSphere(Helper.ConvertMyVectorToUnityVector(sphere.center), radius);
    }
}
