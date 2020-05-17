using UnityEngine;
using VektorenFormativ;

public class BlackHole : MonoBehaviour
{
    // Black Hole's sphere
    private Sphere sphere;

    [Tooltip("Determines how fast the blackhole spins")]
    [SerializeField]
    private float rotationSpeed;

    [Tooltip("Player Game Object reference")]
    [SerializeField]
    private GameObject playerGO;

    [Tooltip("Player script reference")]
    [SerializeField]
    private Player playerScript;

    // Determines how fast the player spins while being sucked
    private float suckRotationSpeed;

    // Determines how fast the player is sucked
    private float suckSpeed;

    // Player's sphere
    private Sphere playerSphere;

    private Vector rotation;

    void Start()
    {
        playerSphere = playerGO.GetComponent<MySphereCollider>().Sphere;

        sphere = GetComponent<MySphereCollider>().Sphere;
    }

    void Update()
    {
        Spin();
        Vacuum();
    }

    private void Vacuum()
    {
        // set suck speed based on the distance between player and blackhole( Clamped to prevent division by 0)
        suckSpeed = 75 / Mathf.Clamp(Vector.Magnitude(sphere.center - playerSphere.center),1f,float.PositiveInfinity);

        // set suck rotation speed based on the distance between player and blackhole( Clamped to prevent division by 0)
        suckRotationSpeed = 1000 / Mathf.Clamp(Vector.Magnitude(sphere.center - playerSphere.center),1f, float.PositiveInfinity);

        // If collide with player
        if (Collisions.SphereInSphere(playerSphere, sphere))
        {
            // If player is at center of blackhole
            if (Vector.Magnitude(sphere.center - playerSphere.center) <= 1f)
                Player.hasDied = true;

            playerScript.isInBlackHole = true;
            
            // Spin the player
            playerScript.Rotation += new Vector(0, 0, 1 * suckRotationSpeed * Time.deltaTime);
            playerGO.transform.eulerAngles = Helper.ConvertMyVectorToUnityVector(playerScript.Rotation);

            // Suck the player inside
            playerGO.transform.position = Helper.ConvertMyVectorToUnityVector(Vector.MoveTowards(Helper.ConvertUnityVectorToMyVector(playerGO.transform.position),
                                                                                           Helper.ConvertUnityVectorToMyVector(transform.position), suckSpeed * Time.deltaTime));
        }
        else
            playerScript.isInBlackHole = false;
    }

    private void Spin()
    {
        rotation += new Vector(0, 0, 1 * rotationSpeed * Time.deltaTime);
        transform.eulerAngles = Helper.ConvertMyVectorToUnityVector(rotation);
    }
}
