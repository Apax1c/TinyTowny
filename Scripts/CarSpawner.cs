using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToInstantiate;
    private float minDelay = 1.0f;
    private float maxDelay = 2.5f;

    private void Start()
    {
        StartCoroutine(InstantiateObjectCoroutine());
    }

    private IEnumerator InstantiateObjectCoroutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            int randomIndex = Random.Range(0, objectsToInstantiate.Length);
            Instantiate(objectsToInstantiate[randomIndex], transform.position, this.gameObject.transform.rotation);
        }
    }
}
