using UnityEngine;

public class CarController : MonoBehaviour
{
    private float moveSpeed = 1f;
    private float lifetime = 10f;

    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        Destroy(this.gameObject, lifetime);
    }
}
