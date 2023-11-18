using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Building", fileName = "New Building")]
public class BuildingItem : ScriptableObject
{
    [SerializeField] private string _buildingName;
    public string BuildingName
    {
        get { return _buildingName; }
    }

    [SerializeField] private Sprite _buildingImage;
    public Sprite BuildingImage
    {
        get { return _buildingImage; }
    }

    [SerializeField] private GameObject _buildingModel;
    public GameObject BuildingModel
    {
        get { return _buildingModel; }
    }

    [SerializeField] private int _buildingCost;
    public int BuildingCost
    {
        get { return _buildingCost; }
    }

    [SerializeField] private int _ñitizenSlot;
    public int CitizenSlot
    {
        get { return _ñitizenSlot; }
    }
}