using System.Collections;
using UnityEngine;
using VektorenFormativ;

public class MeteorInstantiator : MonoBehaviour
{
    [Tooltip("Meteor prefab to instantiate")]
    [SerializeField]
    private GameObject meteorPrefab;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(InstantiateMeteorRandomly(3f)); 
    }

    IEnumerator InstantiateMeteorRandomly(float _delayInSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            Vector randomPos = new Vector(Random.Range(-25, 26), transform.position.y, 0);
            GameObject meteorInstance =  Instantiate(meteorPrefab, Helper.ConvertMyVectorToUnityVector(randomPos), Quaternion.identity);
            Destroy(meteorInstance, 17f);
        }
    }
}
