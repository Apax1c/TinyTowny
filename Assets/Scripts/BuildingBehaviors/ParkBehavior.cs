using System.Collections;
using UnityEngine;

public class ParkBehavior : MonoBehaviour
{
    private float satisfaction;
    private float morbidity;

    private void Start()
    {
        satisfaction = 0.1f;
        morbidity = 0.05f;

        StartCoroutine(AddSatisfactionAndMorbidity());
    }

    private IEnumerator AddSatisfactionAndMorbidity()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            BuildingBehavior.Instance.AddSatisfaction(satisfaction);
            BuildingBehavior.Instance.SubstractMorbidity(morbidity);
        }
    }
}
