using System.Collections;
using UnityEngine;

public class SchoolBehavior : MonoBehaviour
{
    private float workabilityMultiplier;
    private float satisfaction;
    private int tax;

    private void Start()
    {
        workabilityMultiplier = 0.02f;
        satisfaction = 0.02f;
        tax = -2;

        StartCoroutine(AddWorkabilityAndSatisfaction());
    }

    private IEnumerator AddWorkabilityAndSatisfaction()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            BuildingBehavior.Instance.AddWorkabilityMultiplaier(workabilityMultiplier);
            BuildingBehavior.Instance.AddSatisfaction(satisfaction);
            BuildingBehavior.Instance.AddMoney(tax);
        }
    }
}
