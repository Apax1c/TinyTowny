using System.Collections;
using UnityEngine;

public class PoliceBehavior : MonoBehaviour
{
    private float criminality;
    private int tax;

    private void Start()
    {
        criminality = 0.3f;
        tax = -1;

        BuildingBehavior.Instance.SubstractCriminality(criminality);
        StartCoroutine(SubsctractCriminality());
        StartCoroutine(GetTax());
    }

    private IEnumerator SubsctractCriminality()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(BuildingBehavior.Instance.GetCitizenAmount() >= 220)
            {
                BuildingBehavior.Instance.SubstractCriminality(1);
            }
            else
            {
                BuildingBehavior.Instance.SubstractCriminality((float)BuildingBehavior.Instance.GetCitizenAmount() / 660);
            }
        }
    }

    private IEnumerator GetTax()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            BuildingBehavior.Instance.AddMoney(tax);
        }
    }
}