using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceForBuilding : MonoBehaviour, IDataPersistence
{
    public static PlaceForBuilding Instance { get; private set; }

    public event EventHandler OnBuildingCreated;
    public event EventHandler OnBuildingUpdated;
    public event EventHandler OnBuildingBestCreated;
    public event EventHandler OnPoliceCreated;

    [SerializeField] private BuildingBehavior buildingBehavior;
    
    public Camera camera;

    private int gridLinesCount = 15; //if change value here, don't forget to change in GameData.cs

    public GameObject gameController;
    public GameObject buildingParentPrefab;
    public GameObject[] buildingParentObject;
    public Transform[] buildingParent;
    public GameObject[] buildingObjects;
    public Vector3[] buidingObjectsPosition;
    public bool[] canBuild;
    public int[] roadIndexes;

    private void Awake()
    {
        Instance = this;

        buildingParentObject = new GameObject[gridLinesCount * gridLinesCount];
        buildingParent = new Transform[gridLinesCount * gridLinesCount];
        buildingObjects = new GameObject[gridLinesCount * gridLinesCount];
        buidingObjectsPosition = new Vector3[gridLinesCount * gridLinesCount];
        canBuild = new bool[gridLinesCount * gridLinesCount];

        roadIndexes = new int[gridLinesCount * gridLinesCount];


        for (int gridLines = 0; gridLines < gridLinesCount; gridLines++)
        {
            for (int gridColoumns = 0; gridColoumns < gridLinesCount; gridColoumns++)
            {
                int index = (gridLines * gridLinesCount + gridColoumns);

                buildingParentObject[index] = Instantiate(buildingParentPrefab, this.gameObject.transform);
                buildingParentObject[index].transform.position = new Vector3(0.5f + gridLines, -0.001f, 0.5f + gridColoumns);
                buildingParent[index] = buildingParentObject[index].transform;
            }
        }
    }

    public void AddBuilding(Transform parent)
    {
        int index = Array.IndexOf(buildingParentObject, parent.parent.gameObject);
        
        if(buildingBehavior.Building != null && buildingBehavior.canBuy)
        {
            if (canBuild[index] && buildingBehavior.objectRefernce.tag.Substring(buildingBehavior.objectRefernce.tag.Length - 1) == "1")
            {
                AddFirstBuilding(parent, index);
                canBuild[index] = false;
            }
            else if (!canBuild[index] && buildingBehavior.objectRefernce.tag.Substring(buildingBehavior.objectRefernce.tag.Length - 1) == "2")
            {
                UpgradeToSecondTier(parent, index);
            }
            else if (!canBuild[index] && buildingBehavior.objectRefernce.tag.Substring(buildingBehavior.objectRefernce.tag.Length - 1) == "3")
            {
                UpgradeToThirdTier(parent, index);
            }
        }
    }

    private void AddFirstBuilding(Transform parent, int index)
    {
        GameObject NewBuilding = Instantiate(buildingBehavior.Building, parent);
        NewBuilding.transform.rotation = Quaternion.Euler(0, 180, 0);
        NewBuilding.transform.localScale = new Vector3(10, 10, 10);

        buildingObjects[index] = buildingBehavior.Building;
        buidingObjectsPosition[index] = parent.transform.position;

        if (buildingObjects[index].TryGetComponent<HouseBehavior>(out HouseBehavior houseBehavior))
        {
            houseBehavior.AddCitizen();
        }
        else if(NewBuilding.TryGetComponent<RoadBehavior>(out RoadBehavior roadBehavior))
        {
            roadIndexes[index] = index;
            roadBehavior.SetIndex(index);
        }
        else if(NewBuilding.CompareTag("Police1"))
        {
            if(SceneManager.GetActiveScene().buildIndex == 2)
            {
                OnPoliceCreated.Invoke(this, EventArgs.Empty);
            }
        }

        OnBuildingCreated.Invoke(this, EventArgs.Empty);
    }

    public void UpgradeToSecondTier(Transform parent, int index)
    {
        if(parent.GetChild(0).gameObject.tag == buildingBehavior.objectRefernce.tag.Substring(0, buildingBehavior.objectRefernce.tag.Length - 1) + "1")
        {
            Destroy(parent.GetChild(0).gameObject);
            AddFirstBuilding(parent, index);

            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                OnBuildingUpdated.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void UpgradeToThirdTier(Transform parent, int index)
    {
        if (parent.GetChild(0).gameObject.tag == buildingBehavior.objectRefernce.tag.Substring(0, buildingBehavior.objectRefernce.tag.Length - 1) + "2")
        {
            int indexOfLeftPlace = index - gridLinesCount;
            int indexOfRightPlace = index + gridLinesCount;

            if (buildingParentObject[indexOfLeftPlace].transform.GetChild(0).childCount != 0 &&
                buildingParentObject[indexOfLeftPlace].transform.GetChild(0).GetChild(0).gameObject.tag == 
                buildingBehavior.objectRefernce.tag.Substring(0, buildingBehavior.objectRefernce.tag.Length - 1) + "2")
            {
                Destroy(parent.GetChild(0).gameObject);
                buildingObjects[indexOfLeftPlace] = null;
                Destroy(buildingParentObject[indexOfLeftPlace].transform.GetChild(0).GetChild(0).gameObject);
                AddBuildingThirdTier(parent, 0);
            }
            else if (buildingParentObject[indexOfRightPlace].transform.GetChild(0).childCount != 0 &&
                buildingParentObject[indexOfRightPlace].transform.GetChild(0).GetChild(0).gameObject.tag == 
                buildingBehavior.objectRefernce.tag.Substring(0, buildingBehavior.objectRefernce.tag.Length - 1) + "2")
            {
                Destroy(parent.GetChild(0).gameObject);
                buildingObjects[indexOfRightPlace] = null;
                Destroy(buildingParentObject[indexOfRightPlace].transform.GetChild(0).GetChild(0).gameObject);
                AddBuildingThirdTier(parent, 1);
            }
        }
    }

    private void AddBuildingThirdTier(Transform parent, int localPositionIndex)
    {
        Vector3 localPosition = new(5, 0, 0);

        GameObject NewBuilding = Instantiate(buildingBehavior.Building, parent);

        int index = Array.IndexOf(buildingParentObject, parent.parent.gameObject);
        buildingObjects[index] = buildingBehavior.Building;

        if (localPositionIndex == 0)
        {
            NewBuilding.transform.localPosition -= localPosition;
            buidingObjectsPosition[index] -= localPosition/10;
        }
            
        else if(localPositionIndex == 1)
        {
            NewBuilding.transform.localPosition += localPosition;
            buidingObjectsPosition[index] += localPosition/10;
        }

        NewBuilding.transform.rotation = Quaternion.Euler(0, 180, 0);
        NewBuilding.transform.localScale = new Vector3(10, 10, 10);

        if (buildingBehavior.Building.TryGetComponent<HouseBehavior>(out HouseBehavior houseBehavior))
        {
            houseBehavior.AddCitizen();
        }

        OnBuildingCreated.Invoke(this, EventArgs.Empty);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            OnBuildingBestCreated.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetNeighboorRoadObjectPositions(int i)
    {
        if ((i + 1) % gridLinesCount != 0 && buildingObjects[i + 1] != null && buildingObjects[i + 1].CompareTag("Road1"))
        {
            if (i % gridLinesCount != 0 && buildingObjects[i - 1] != null && buildingObjects[i - 1].CompareTag("Road1"))
            {
                if (i <= (gridLinesCount * gridLinesCount - gridLinesCount - 1) && buildingObjects[i + gridLinesCount] != null 
                    && buildingObjects[i + gridLinesCount].CompareTag("Road1"))
                {
                    if (i >= gridLinesCount && buildingObjects[i - gridLinesCount] != null && buildingObjects[i - gridLinesCount].CompareTag("Road1"))
                    {
                        return 10;
                    }
                    else
                    {
                        return 7;
                    }
                }
                else
                {
                    if (i >= gridLinesCount && buildingObjects[i - gridLinesCount] != null && buildingObjects[i - gridLinesCount].CompareTag("Road1"))
                    {
                        return 6;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                if (i <= (gridLinesCount * gridLinesCount - gridLinesCount - 1) && buildingObjects[i + gridLinesCount] != null 
                    && buildingObjects[i + gridLinesCount].CompareTag("Road1"))
                {
                    if (i >= gridLinesCount && buildingObjects[i - gridLinesCount] != null && buildingObjects[i - gridLinesCount].CompareTag("Road1"))
                    {
                        return 9;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    if (i >= gridLinesCount && buildingObjects[i - gridLinesCount] != null && buildingObjects[i - gridLinesCount].CompareTag("Road1"))
                    {
                        return 3;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
        else
        {
            if (i % gridLinesCount != 0 && buildingObjects[i - 1] != null && buildingObjects[i - 1].CompareTag("Road1"))
            {
                if (i <= (gridLinesCount * gridLinesCount - gridLinesCount - 1) && buildingObjects[i + gridLinesCount] != null 
                    && buildingObjects[i + gridLinesCount].CompareTag("Road1"))
                {
                    if (i >= gridLinesCount && buildingObjects[i - gridLinesCount] != null && buildingObjects[i - gridLinesCount].CompareTag("Road1"))
                    {
                        return 8;
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    if (i >= gridLinesCount && buildingObjects[i - gridLinesCount] != null && buildingObjects[i - gridLinesCount].CompareTag("Road1"))
                    {
                        return 5;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                if (i == 0 && buildingObjects[i + 1] != null && buildingObjects[i + 1].CompareTag("Road1"))
                {
                    if (buildingObjects[i + gridLinesCount] != null && buildingObjects[i + gridLinesCount].CompareTag("Road1"))
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else if(i == (gridLinesCount * gridLinesCount - gridLinesCount) && buildingObjects[i + 1] != null && buildingObjects[i + 1].CompareTag("Road1"))
                {
                    if(buildingObjects[i - gridLinesCount] != null && buildingObjects[i - gridLinesCount].CompareTag("Road1"))
                    {
                        return 3;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public void LoadData(GameData data)
    {
        data.buildingObjects.CopyTo(this.buildingObjects, 0);
        data.buildingObjectsPosition.CopyTo(this.buidingObjectsPosition, 0);
        canBuild = new bool[gridLinesCount * gridLinesCount];
        data.canBuild.CopyTo(this.canBuild, 0);
        data.roadIndex.CopyTo(this.roadIndexes, 0);

        for (int i = 0; i < buildingObjects.Length; i++)
        {
            if (data.buildingObjects[i] != null)
            {
                GameObject NewBuilding = Instantiate(this.buildingObjects[i], buildingParent[i].GetChild(0));
                NewBuilding.transform.SetPositionAndRotation(this.buidingObjectsPosition[i], Quaternion.Euler(0, 180, 0));
                NewBuilding.transform.localScale = Vector3.one * 10f;

                if (data.buildingObjects[i].CompareTag("Road1"))
                {
                    NewBuilding.GetComponent<RoadBehavior>().SetIndex(roadIndexes[i]);
                }
            }
        }
    }

    public void SaveData(GameData data)
    {
        this.buildingObjects.CopyTo(data.buildingObjects, 0);
        this.buidingObjectsPosition.CopyTo(data.buildingObjectsPosition, 0);
        this.canBuild.CopyTo(data.canBuild, 0);
        this.roadIndexes.CopyTo(data.roadIndex, 0);
    }
}