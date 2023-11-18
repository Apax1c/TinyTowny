using UnityEngine;

public class OfficeBehavior : MonoBehaviour
{
    private float workabilityMultiplier;

    private void Start()
    {
        if (this.gameObject.CompareTag("Office1"))
            workabilityMultiplier = 0.1f;
        else if (this.gameObject.CompareTag("Office2"))
            workabilityMultiplier = 0.1f;
        else if (this.gameObject.CompareTag("Office3"))
            workabilityMultiplier = 0.2f;

        BuildingBehavior.Instance.AddWorkabilityMultiplaier(workabilityMultiplier);
    }
}
