using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGameController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject messageUI;
    [SerializeField] private Button hideMessageButton;

    [SerializeField] private GameObject messageText1;
    [SerializeField] private GameObject messageText2;
    [SerializeField] private GameObject messageText3;
    [SerializeField] private GameObject messageText4;
    [SerializeField] private GameObject messageText5;
    [SerializeField] private GameObject messageText6;
    [SerializeField] private GameObject messageText7;
    [SerializeField] private GameObject messageText8;
    [SerializeField] private GameObject messageText9;
    [SerializeField] private GameObject messageText10;
    [SerializeField] private GameObject messageText11;

    private bool isBuildingCreatedFirstTime;
    private bool isBuildingUpgradedGirstTime;
    private bool isFirstBestBuildingCreated;
    private bool isWarnedFirstTime;
    private bool isFinalMessageShowed;

    private void Start()
    {
        isBuildingCreatedFirstTime = false;
        isBuildingUpgradedGirstTime = false;
        isFirstBestBuildingCreated = false;
        isWarnedFirstTime = false;
        isFinalMessageShowed = false;

        BuildingBehavior.Instance.SubstractCriminality(1);
        BuildingBehavior.Instance.SubstractMorbidity(1);
        BuildingBehavior.Instance.AddMoney(500);

        HideAllMessages();

        ShowMessage(1);
        PauseGame();

        hideMessageButton.onClick.AddListener(() => 
        {
            HideAllMessages();
            HideMessagesBackground();
            UnpauseGame();
        });

        PlaceForBuilding.Instance.OnBuildingCreated += PlaceForBuilding_OnBuildingCreated;
        PlaceForBuilding.Instance.OnBuildingUpdated += PlaceForBuilding_OnBuildingUpdated;
        PlaceForBuilding.Instance.OnBuildingBestCreated += PlaceForBuilding_onBuildingBestCreated;
        PlaceForBuilding.Instance.OnPoliceCreated += PlaceForBuilding_OnPoliceCreated;
        BuildingBehavior.Instance.OnWarningCalled += BuildingBehavior_OnWarningCalled;
    }

    private void Update()
    {
        BuildingBehavior.Instance.SubstractMorbidity(1);

        if(!isFirstBestBuildingCreated)
        {
            BuildingBehavior.Instance.SubstractCriminality(1);
        }

        if (isWarnedFirstTime && BuildingBehavior.Instance.GetCriminality() < 1)
        {
            StartCoroutine(ShowFinalMessage());
        }
    }

    private void PlaceForBuilding_OnBuildingCreated(object sender, EventArgs e)
    {
        StartCoroutine(FirstBuildingCreated());
    }    

    private void PlaceForBuilding_OnBuildingUpdated(object sender, EventArgs e)
    {
        StartCoroutine(BuildingUpgraded());
    }

    private void PlaceForBuilding_onBuildingBestCreated(object sender, EventArgs e)
    {
        StartCoroutine(FirstBestBuildingCreated());
    } 

    private void PlaceForBuilding_OnPoliceCreated(object sender, EventArgs e)
    {
        BuildingBehavior.Instance.SubstractCriminality(1000);
    }

    private void BuildingBehavior_OnWarningCalled(object sender, EventArgs e)
    {
        StartCoroutine(FirstTimeWarned());
    }

    private IEnumerator FirstBuildingCreated()
    {
        while (!isBuildingCreatedFirstTime)
        {
            yield return new WaitForSeconds(1);
            PauseGame();

            ShowMessage(2);

            hideMessageButton.onClick.RemoveAllListeners();
            hideMessageButton.onClick.AddListener(() =>
            {
                HideAllMessages();
                ShowMessage(3);
                hideMessageButton.onClick.RemoveAllListeners();
                hideMessageButton.onClick.AddListener(() =>
                {
                    HideAllMessages();
                    HideMessagesBackground();
                    UnpauseGame();
                });
            });
            
            isBuildingCreatedFirstTime = true;
        }
    }

    private IEnumerator BuildingUpgraded()
    {
        while (!isBuildingUpgradedGirstTime)
        {
            yield return new WaitForSeconds(1);
            PauseGame();

            ShowMessage(4);

            hideMessageButton.onClick.RemoveAllListeners();
            hideMessageButton.onClick.AddListener(() =>
            {
                HideAllMessages();
                ShowMessage(5);
                hideMessageButton.onClick.RemoveAllListeners();
                hideMessageButton.onClick.AddListener(() =>
                {
                    HideAllMessages();
                    HideMessagesBackground();
                    UnpauseGame();
                });
            });

            isBuildingUpgradedGirstTime = true;
        }
    }


    private IEnumerator FirstBestBuildingCreated()
    {
        while (!isFirstBestBuildingCreated)
        {
            yield return new WaitForSeconds(1);
            PauseGame();

            ShowMessage(6);

            hideMessageButton.onClick.RemoveAllListeners();
            hideMessageButton.onClick.AddListener(() =>
            {
                HideAllMessages();
                ShowMessage(7);
                hideMessageButton.onClick.RemoveAllListeners();
                hideMessageButton.onClick.AddListener(() =>
                {
                    HideAllMessages();
                    HideMessagesBackground();
                    UnpauseGame();
                    ToggleCrimeWarning();
                });
            });

            isFirstBestBuildingCreated = true;
        }
    } 

    public void ToggleCrimeWarning()
    {
        BuildingBehavior.Instance.SubstractCriminality(BuildingBehavior.Instance.GetCriminality() - 1);
    }

    private IEnumerator FirstTimeWarned()
    {
        while (!isWarnedFirstTime)
        {
            yield return new WaitForSeconds(1);
            PauseGame();

            ShowMessage(8);

            hideMessageButton.onClick.RemoveAllListeners();
            hideMessageButton.onClick.AddListener(() =>
            {
                HideAllMessages();
                ShowMessage(9);
                hideMessageButton.onClick.RemoveAllListeners();
                hideMessageButton.onClick.AddListener(() =>
                {
                    HideAllMessages();
                    HideMessagesBackground();
                    UnpauseGame();
                });
            });

            isWarnedFirstTime = true;
        }
    }

    private IEnumerator ShowFinalMessage()
    {
        while (!isFinalMessageShowed)
        {
            yield return new WaitForSeconds(1);

            PauseGame();

            ShowMessage(10);

            hideMessageButton.onClick.RemoveAllListeners();
            hideMessageButton.onClick.AddListener(() =>
            {
                HideAllMessages();
                ShowMessage(11);
                hideMessageButton.onClick.RemoveAllListeners();
                hideMessageButton.onClick.AddListener(() =>
                {
                    HideAllMessages();
                    HideMessagesBackground();
                    UnpauseGame();
                    canvas.GetComponent<SceneController>().FinishTutorial();
                });
            });

            isFinalMessageShowed = true;
        }
    }

    private void ShowMessagesBackground()
    {
        messageUI.SetActive(true);
    }

    private void HideMessagesBackground()
    {
        messageUI.SetActive(false);
    }

    private void HideAllMessages()
    {
        messageText1.SetActive(false);
        messageText2.SetActive(false);
        messageText3.SetActive(false);
        messageText4.SetActive(false);
        messageText5.SetActive(false);
        messageText6.SetActive(false);
        messageText7.SetActive(false);
        messageText8.SetActive(false);
        messageText9.SetActive(false);
        messageText10.SetActive(false);
        messageText11.SetActive(false);
    }

    private void ShowMessage(int index)
    {
        ShowMessagesBackground();

        switch (index)
        {
            case 1: messageText1.SetActive(true);
                break;
            case 2: messageText2.SetActive(true);
                break;
            case 3: messageText3.SetActive(true);
                break;
            case 4: messageText4.SetActive(true);
                break;
            case 5: messageText5.SetActive(true);
                break;
            case 6: messageText6.SetActive(true);
                break;
            case 7: messageText7.SetActive(true);
                break;
            case 8: messageText8.SetActive(true);
                break;
            case 9: messageText9.SetActive(true);
                break;
            case 10: messageText10.SetActive(true);
                break;
            case 11: messageText11.SetActive(true);
                break;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1.0f;
    }
}
