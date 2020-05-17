using UnityEngine;

public class PlanetIndicatorArrow : MonoBehaviour
{
    [Tooltip("Planet's Transform reference")]
    [SerializeField]
    private Transform planet;

    [Tooltip("Pause menu UI group reference")]
    [SerializeField]
    private GameObject indicatorUIGroup;

    [Tooltip("Determines when to hide the indicator arrow")]
    [SerializeField]
    private float hideDistance;

    void Update()
    {
        // Direction Vector between planet and pointer
        Vector3 dir = planet.position - transform.position;
        
        // Get the angle between target and pointer
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(dir.magnitude < hideDistance)
        {
            indicatorUIGroup.SetActive(false);
        }
        else
            indicatorUIGroup.SetActive(true);
    }
}
