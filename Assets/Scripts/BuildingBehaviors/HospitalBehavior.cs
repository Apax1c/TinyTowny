using System.Collections;
using UnityEngine;

public class HospitalBehavior : MonoBehaviour
{
    private float morbidity;
    private int tax;

    private void Start()
    {
        morbidity = 0.25f;
        tax = -1;

        BuildingBehavior.Instance.SubstractMorbidity(morbidity);
        StartCoroutine(SubstractMorbidity());
    }

    private IEnumerator SubstractMorbidity()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (BuildingBehavior.Instance.GetCitizenAmount() >= 300)
            {
                BuildingBehavior.Instance.SubstractMorbidity(1);
            }
            else
            {
                BuildingBehavior.Instance.SubstractMorbidity((float)BuildingBehavior.Instance.GetCitizenAmount() / 900);
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
