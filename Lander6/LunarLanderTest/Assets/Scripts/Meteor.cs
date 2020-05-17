using UnityEngine;
using VektorenFormativ;

public class Meteor : MonoBehaviour
{
    // Meteor's sphere
    private Sphere sphere;

    // Player's sphere
    private Sphere playerSphere;

    [Tooltip("Gravity constant")]
    [SerializeField]
    private float gravity;

    private Vector center;

    private Vector scale;

    private Vector rotation;

    // Start is called before the first frame update
    void Start()
    {
        playerSphere = GameObject.FindGameObjectWithTag("Player").GetComponent<MySphereCollider>().Sphere;// playerGO.GetComponent<MySphereCollider>().Sphere;

        center = Helper.ConvertUnityVectorToMyVector(transform.position);

        scale = Helper.ConvertUnityVectorToMyVector(transform.localScale);

        rotation = Helper.ConvertUnityVectorToMyVector(transform.rotation.eulerAngles);

        sphere = GetComponentInChildren<MySphereCollider>().Sphere;
    }

    void Update()
    {
        center = Helper.ConvertUnityVectorToMyVector(transform.position);
        
        MeteorGravity();

       // sphere = new Sphere(center, sphere.radius);

        if (Collisions.SphereInSphere(sphere, playerSphere))
        {
            Player.hasDied = true;
        }
    }
    
    private void MeteorGravity()
    {
        center -= Matrix.TRS(Vector.Zero, rotation, scale) * Vector.Up * Time.deltaTime * gravity;
        transform.position = Helper.ConvertMyVectorToUnityVector(center);
    }
}
