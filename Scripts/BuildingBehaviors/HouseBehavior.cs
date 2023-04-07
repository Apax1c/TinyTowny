using UnityEngine;

public class HouseBehavior : MonoBehaviour
{
    private int citizenSlotsToAdd;
    public int CitizenSlotsToAdd
    {
        get { return citizenSlotsToAdd; }
    }

    public void AddCitizen()
    {
        if (this.gameObject.CompareTag("House1"))
            citizenSlotsToAdd = 20;
        else if (this.gameObject.CompareTag("House2"))
            citizenSlotsToAdd = 30;
        else if (this.gameObject.CompareTag("House3"))
            citizenSlotsToAdd = 50;

        BuildingBehavior.Instance.AddCitizenSlots(citizenSlotsToAdd);
    }
}
