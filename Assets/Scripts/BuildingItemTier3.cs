using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/BuildingTier3", fileName = "New Building Tier 3")]
public class BuildingItemTier3 : ScriptableObject
{
    [SerializeField] private string _buildingName;
    public string BuildingName
    {
        get { return _buildingName; }
    }

    [SerializeField] private GameObject _buildingModel;
    public GameObject BuildingModel
    {
        get { return _buildingModel; }
    }

    [SerializeField] private GameObject _buildingUpgradedModel;
    public GameObject BuildingUpgradedModel
    {
        get { return _buildingUpgradedModel; }
    }

    [SerializeField] private Sprite _buildingImage;
    public Sprite BuildingImage
    {
        get { return _buildingImage; }
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