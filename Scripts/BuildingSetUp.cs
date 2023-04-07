using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuildingSetUp : MonoBehaviour
{
    [SerializeField] private BuildingItem _buildingItem;
    public BuildingItem BuildingItem
    {
        get { return _buildingItem; }
    }

    [SerializeField] private BuildingItemTier2 _buildingItemTier2;
    public BuildingItemTier2 BuildingItemTier2
    {
        get { return _buildingItemTier2; }
    }

    [SerializeField] private BuildingItemTier3 _buildingItemTier3;
    public BuildingItemTier3 BuildingItemTier3
    {
        get { return _buildingItemTier3; }
    }

    public GameObject background;

    [SerializeField] private Image _buildingImage;
    [SerializeField] private TextMeshProUGUI _buildingNameText;
    [SerializeField] private TextMeshProUGUI _buildingCost;

    private void Start()
    {
        SettingUp();
        background.SetActive(false);
    }

    public void SettingUp()
    {
        if(_buildingItem != null)
        {
            _buildingNameText.text = _buildingItem.BuildingName;
            _buildingImage.sprite = _buildingItem.BuildingImage;
            _buildingCost.text = _buildingItem.BuildingCost.ToString();
        }
        else if (_buildingItemTier2 != null)
        {
            _buildingNameText.text = _buildingItemTier2.BuildingName;
            _buildingImage.sprite = _buildingItemTier2.BuildingImage;
            _buildingCost.text = _buildingItemTier2.BuildingCost.ToString();
        }
        else if (_buildingItemTier3 != null)
        {
            _buildingNameText.text = _buildingItemTier3.BuildingName;
            _buildingImage.sprite = _buildingItemTier3.BuildingImage;
            _buildingCost.text = _buildingItemTier3.BuildingCost.ToString();
        }
    }
}
