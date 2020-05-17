using UnityEngine;
using VektorenFormativ;

public class GravitationalPull : MonoBehaviour
{
    [Tooltip("Player script reference")]
    [SerializeField]
    private Player player;

    [Tooltip("Gravity constant")]
    [SerializeField]
    private float gravity;

    // Planets sphere
    public Sphere planet;

    private void Awake()
    {
        player.Center = Vector.Zero;
    }

    void Start()
    {
        planet = new Sphere(Vector.Zero, 2);
    }
    // Update is called once per frame
    void Update()
    {
        if(!player.isInBlackHole)
            MyGravitationalPull();
    }

    private void MyGravitationalPull()
    {
        // Distance between planet and player(clamped to prevent division with 0)
        float distance = Mathf.Clamp(Vector.Magnitude(planet.center - player.Center),1f,float.PositiveInfinity);

        // Set the player position
        player.Center -= Matrix.TRS(Vector.Zero, player.Rotation, player.Scale) * new Vector(0, 1 * (gravity / distance), 0) * Time.deltaTime;

        // Set the global transform.position
        player.transform.position = Helper.ConvertMyVectorToUnityVector(player.Center);
    }
}
