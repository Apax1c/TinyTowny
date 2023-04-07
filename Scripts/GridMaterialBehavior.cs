using UnityEngine;

public class GridMaterialBehavior : MonoBehaviour
{
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonUp(0))
        {
            PlaceForBuilding.Instance.AddBuilding(this.gameObject.transform);
        }
    }
}
