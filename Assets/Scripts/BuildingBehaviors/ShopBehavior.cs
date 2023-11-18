using System.Collections;
using UnityEngine;

public class ShopBehavior : MonoBehaviour
{
    private int _earningsPerTime;
    private int _waitTime = 3;
    public int EarningsPerTime
    {
        get { return _earningsPerTime; }
    }

    private void Start()
    {
        if (this.gameObject.CompareTag("Shop1"))
            _earningsPerTime = 1;
        else if (this.gameObject.CompareTag("Shop2"))
            _earningsPerTime = 3;
        else if (this.gameObject.CompareTag("Shop3"))
            _earningsPerTime = 8;

        StartCoroutine(AddMoney());
    }

    private IEnumerator AddMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(_waitTime);
            BuildingBehavior.Instance.AddMoney(_earningsPerTime);
        }
    }
}
