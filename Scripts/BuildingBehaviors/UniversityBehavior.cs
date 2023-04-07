using System.Collections;
using UnityEngine;

public class UniversityBehavior : MonoBehaviour
{
    private float workabilityMultiplier;
    private int tax;

    private void Start()
    {
        workabilityMultiplier = 0.04f;
        tax = -2;

        StartCoroutine(AddWorkability());
    }

    private IEnumerator AddWorkability()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            BuildingBehavior.Instance.AddWorkabilityMultiplaier(workabilityMultiplier);
            BuildingBehavior.Instance.AddMoney(tax);
        }
    }
}
