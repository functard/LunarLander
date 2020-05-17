using UnityEngine;
using VektorenFormativ;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public static bool hasDied;

    [HideInInspector]
    public static bool hasWon;

    [HideInInspector]
    public bool isInBlackHole;

    [Tooltip("Planet Game Object's reference")]
    [SerializeField]
    private GameObject planetGO;

    // Planet's sphere
    private Sphere planetSphere;

    [Tooltip("Determines how fast the player moves")]
    [SerializeField]
    private float movementSpeed = 1.0f;

    [Tooltip("Determines how fast the player rotates")]
    [SerializeField]
    private float rotationSpeed = 1.0f;

    [SerializeField]
    private float radius;

    public Vector Center
    {
        get { return center; }
        set { center = value; }
    }

    private Vector center;

    public Vector Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    private Vector rotation;

    public Vector Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    private Vector scale;

    private MySphereCollider collider;

    void Start()
    {
        hasWon = false;

        hasDied = false;
         
        isInBlackHole = false;

        planetSphere = planetGO.GetComponentInChildren<MySphereCollider>().Sphere;

        collider = GetComponent<MySphereCollider>();

        scale = Helper.ConvertUnityVectorToMyVector(transform.localScale);

        rotation =  Helper.ConvertUnityVectorToMyVector(transform.rotation.eulerAngles);

        center = Helper.ConvertUnityVectorToMyVector(transform.position);
    }

    void Update()
    {
        scale = Helper.ConvertUnityVectorToMyVector(transform.localScale);

        rotation = Helper.ConvertUnityVectorToMyVector(transform.eulerAngles);

        center = Helper.ConvertUnityVectorToMyVector(transform.position);

        //forwardVector = Helper.ConvertUnityVectorToMyVector(transform.forward);

        transform.eulerAngles = Helper.ConvertMyVectorToUnityVector(rotation);

        transform.localScale = Helper.ConvertMyVectorToUnityVector(scale);


        Movement();

        if (Collisions.SphereInSphere(planetSphere, collider.Sphere))
        {
            hasWon = true;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if(Collisions.PointInSphere(Helper.ConvertUnityVectorToMyVector(transform.GetChild(i).position), planetSphere))
            {
                hasDied = true;
            }
        }
    }

    void Movement()
    {   
        center -= Matrix.TRS(Vector.Zero, rotation, scale) * Vector.Up * Time.deltaTime * movementSpeed * Input.GetAxis("Movement");
        transform.position = Helper.ConvertMyVectorToUnityVector(center);
        
        float horizontalInput = -Input.GetAxis("Horizontal");

        rotation += new Vector(0, 0, horizontalInput * rotationSpeed * Time.deltaTime);
        transform.eulerAngles = Helper.ConvertMyVectorToUnityVector(rotation);
    }

    private void IsDeathAnimCompleted()
    {
        GameManager.deathAnimCompleted = true;
    }
}
