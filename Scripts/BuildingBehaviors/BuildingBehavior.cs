using UnityEngine;
using TMPro;
using System.Collections;
using System;
using UnityEngine.UI;

public class BuildingBehavior : MonoBehaviour, IDataPersistence
{
    public static BuildingBehavior Instance { get; private set; }

    [SerializeField] private Camera camera;
    [SerializeField] private GameObject grid;
    [SerializeField] private GameObject winCanvas;

    public event EventHandler OnWarningCalled;

    private int _money;
    private int buildingCost;
    private int _citizen;
    private int _citizenSlots;
    private float workabilityMultiplier;
    private float satisfaction;
    private float criminality;
    private float morbidity;
    private bool isWin;

    private float crimeDelay = 3;
    private float morbidityDelay = 3;
    private float satisfyingDelay = 5;

    [SerializeField] private TextMeshProUGUI citizenText;
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Warnings")]
    [SerializeField] private Image criminalityWarningImage;
    [SerializeField] private Image morbidityWarningImage;
    [SerializeField] private Image satisfyingWarnigImage;

    public bool canBuy;

    [SerializeField] private GameObject _building;
    public GameObject Building
    {
        get { return _building; }
        set { _building = value; }
    }
    public GameObject objectRefernce;
    private BuildingSetUp buildingSetUp;

    private void Awake()
    {
        Instance = this;

        PlaceForBuilding.Instance.OnBuildingCreated += PlaceForBuilding_OnBuildingCreated;
    }

    public void PlaceForBuilding_OnBuildingCreated(object sender, EventArgs e)
    {
        buildingSetUp.background.SetActive(false);
        Building = null;
        _money -= buildingCost;
        buildingSetUp = null;
    }

    private void Start()
    {
        criminalityWarningImage.gameObject.SetActive(false);
        morbidityWarningImage.gameObject.SetActive(false);
        satisfyingWarnigImage.gameObject.SetActive(false);
        winCanvas.SetActive(false);

        isWin = false;

        StartCoroutine(MoneyFromCitizen());
        StartCoroutine(Reproduction());
        StartCoroutine(Criminal());
        StartCoroutine(Epidemic());
    }

    private void Update()
    {
        VariablesControl();

        if (Input.GetMouseButtonDown(0))
        {
            ChooseBuilding();
        }

        moneyText.text = _money.ToString();
        citizenText.text = _citizen.ToString() + "/" + _citizenSlots.ToString();

        ToggleWarnings();

        if(_citizen >= 1000 && !isWin)
        {
            ShowWinMessage();
        }
    }

    private void VariablesControl()
    {
        if (_citizen >= _citizenSlots)
        {
            _citizen = _citizenSlots;
        }

        if (_citizen < 0)
        {
            _citizen = 0;
        }

        if (_money < 0)
        {
            _money = 0;
        }

        if (criminality < -0.25f)
        {
            criminality = -0.25f;
        }

        if (morbidity <= -0.25f)
        {
            morbidity = -0.25f;
        }
    }

    private void ShowWinMessage()
    {
        winCanvas.SetActive(true);
        isWin = true;
    }

    private void ChooseBuilding()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.tag.Substring(hit.transform.tag.Length - 1) == "1")
            {
                NewBuilding(hit);
            }
            else if (hit.transform.tag.Substring(hit.transform.tag.Length - 1) == "2")
            {
                UpgradedBuildingSecondTier(hit);
            }
            else if (hit.transform.tag.Substring(hit.transform.tag.Length - 1) == "3")
            {
                UpgradedBuildingThirdTier(hit);
            }
        }
    }

    public void NewBuilding(RaycastHit hit)
    {
        if (_building == hit.collider.GetComponent<BuildingSetUp>().BuildingItem.BuildingModel)
        {
            buildingSetUp.background.SetActive(false);
            buildingSetUp = null;
            _building = null;
            canBuy = false;
            objectRefernce = null;

            hit.collider.GetComponent<BuildingSetUp>().background.SetActive(false);
        }
        else
        {
            if(buildingSetUp != null)
                buildingSetUp.background.SetActive(false);
            buildingSetUp = hit.collider.GetComponent<BuildingSetUp>();
            objectRefernce = hit.collider.gameObject;
            buildingCost = buildingSetUp.BuildingItem.BuildingCost;
            _building = buildingSetUp.BuildingItem.BuildingModel;

            hit.collider.GetComponent<BuildingSetUp>().background.SetActive(true);

            if (buildingCost <= _money)
            {
                canBuy = true;
            }
            else
            {
                canBuy = false;
            }
        }
    }

    public void UpgradedBuildingSecondTier(RaycastHit hit)
    {
        if (_building == hit.collider.GetComponent<BuildingSetUp>().BuildingItemTier2.BuildingModel)
        {
            buildingSetUp.background.SetActive(false);
            buildingSetUp = null;
            objectRefernce = null;
            _building = null;
            canBuy = false;

            hit.collider.GetComponent<BuildingSetUp>().background.SetActive(false);
        }
        else
        {
            if (buildingSetUp != null)
                buildingSetUp.background.SetActive(false);
            buildingSetUp = hit.collider.GetComponent<BuildingSetUp>();
            objectRefernce = hit.collider.gameObject;
            buildingCost = buildingSetUp.BuildingItemTier2.BuildingCost;
            _building = buildingSetUp.BuildingItemTier2.BuildingModel;

            hit.collider.GetComponent<BuildingSetUp>().background.SetActive(true);

            if (buildingCost <= _money)
            {
                canBuy = true;
            }
            else
            {
                canBuy = false;
            }
        }
    }

    public void UpgradedBuildingThirdTier(RaycastHit hit)
    {
        if (_building == hit.collider.GetComponent<BuildingSetUp>().BuildingItemTier3.BuildingModel)
        {
            buildingSetUp.background.SetActive(false);
            buildingSetUp = null;
            objectRefernce = null;
            _building = null;
            canBuy = false;
            hit.collider.GetComponent<BuildingSetUp>().background.SetActive(false);
        }
        else
        {
            if (buildingSetUp != null)
                buildingSetUp.background.SetActive(false);
            buildingSetUp = hit.collider.GetComponent<BuildingSetUp>();
            objectRefernce = hit.collider.gameObject;
            buildingCost = buildingSetUp.BuildingItemTier3.BuildingCost;
            _building = buildingSetUp.BuildingItemTier3.BuildingModel;
            hit.collider.GetComponent<BuildingSetUp>().background.SetActive(true);

            if (buildingCost <= _money)
            {
                canBuy = true;
            }
            else
            {
                canBuy = false;
            }
        }
    }

    private void ToggleWarnings()
    {
        if (criminality >= 1)
        {
            crimeDelay -= Time.deltaTime;
            if (crimeDelay <= 0)
            {
                criminalityWarningImage.gameObject.SetActive(true);
                OnWarningCalled.Invoke(this, EventArgs.Empty);
                crimeDelay = 5;
            }
        }
        else
        {
            crimeDelay = 5;
            criminalityWarningImage.gameObject.SetActive(false);
        }

        if (morbidity >= 1)
        {
            morbidityDelay -= Time.deltaTime;
            if (morbidityDelay <= 0)
            {
                morbidityWarningImage.gameObject.SetActive(true);
                OnWarningCalled.Invoke(this, EventArgs.Empty);
                morbidityDelay = 5;
            }
        }
        else
        {
            morbidityDelay = 5;
            morbidityWarningImage.gameObject.SetActive(false);
        }

        if (satisfaction < 1)
        {
            satisfyingDelay-= Time.deltaTime;
            if (satisfyingDelay <= 0)
            {
                satisfyingWarnigImage.gameObject.SetActive(true);
                OnWarningCalled.Invoke(this, EventArgs.Empty);
                satisfyingDelay = 10;
            }
        }
        else
        {
            satisfyingDelay = 5;
            satisfyingWarnigImage.gameObject.SetActive(false);
        }
    }

    public int GetCitizenAmount()
    {
        return _citizen;
    }

    public void AddCitizenSlots(int slotsToAdd)
    {
        _citizenSlots += slotsToAdd;
    }

    public void AddMoney(int value)
    {
        _money += value;
    }

    // Adds money, depending on the number if inhabitants, for a fixed time
    private IEnumerator MoneyFromCitizen()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            int moneyToDelay = (int)(_citizen * workabilityMultiplier / _citizenSlots);
            _money += moneyToDelay;
        }
    }

    // Adds new citizen per fixed time
    private IEnumerator Reproduction()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            int maxAdd = (int)(((_citizenSlots / _citizen) + 1) * satisfaction);
            _citizen += UnityEngine.Random.Range(1, maxAdd + 1);
        }
    }

    private IEnumerator Criminal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (criminality >= 1)
            {
                _citizen -= (int)criminality;
                _money -= (int)(criminality);
                satisfaction -= criminality/50;
            }
            else if(criminality < 0)
            {
                satisfaction -= criminality/10;
            }

            criminality += (float)_citizen / 750;
        }
    }

    private IEnumerator Epidemic()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (morbidity >= 1)
            {
                _citizen -= (int)morbidity;
                satisfaction -= morbidity/50;
            }

            morbidity += (float)_citizen / 1050;
        }
    }

    public void AddWorkabilityMultiplaier(float value)
    {
        workabilityMultiplier += value;
    }

    public void AddSatisfaction(float value)
    {
        satisfaction += value;
    }

    public void SubstractCriminality(float value)
    {
        criminality -= value;
    }

    public float GetCriminality()
    {
        return criminality;
    }

    public void SubstractMorbidity(float value)
    {
        morbidity -= value;
    }

    public void LoadData(GameData data)
    {
        this._money = data.money;
        this._citizen = data.citizen;
        this._citizenSlots = data.citizenSlot;
        this.workabilityMultiplier = data.workabilityMultiplier;
        this.satisfaction = data.satisfaction;
        this.criminality = data.criminality;
        this.morbidity = data.morbidity;
    }

    public void SaveData(GameData data)
    {
        data.money = this._money;
        data.citizen = this._citizen;
        data.citizenSlot = this._citizenSlots;
        data.workabilityMultiplier = this.workabilityMultiplier;
        data.satisfaction = this.satisfaction;
        data.criminality = this.criminality;
        data.morbidity = this.morbidity;
    }
}