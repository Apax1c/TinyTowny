using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionBehavior : MonoBehaviour
{
    private float satisfaction;

    private void Start()
    {
        satisfaction = 0.5f;

        StartCoroutine(AddSatisfaction());
    }

    private IEnumerator AddSatisfaction()
    {
        while (true)
        {
            yield return new WaitForSeconds(15);
            BuildingBehavior.Instance.AddSatisfaction(satisfaction);
        }
    }
}
