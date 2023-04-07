using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money;
    public int citizen;
    public int citizenSlot;
    public float workabilityMultiplier;
    public float satisfaction;
    public float criminality;
    public float morbidity;

    public int gridLineCount;
    public GameObject[] buildingObjects;
    public Transform[] buildingObjectsParent;
    public Vector3[] buildingObjectsPosition;
    public bool[] canBuild;
    public int[] roadIndex;

    public GameData()
    {
        this.money = 200;
        this.citizen = 20;
        this.citizenSlot = 20;
        this.workabilityMultiplier = 1;
        this.satisfaction = 1.2f;
        this.criminality = 0;
        this.morbidity = 0;

        gridLineCount = 15;
        buildingObjects = new GameObject[gridLineCount * gridLineCount];
        buildingObjectsPosition = new Vector3[gridLineCount * gridLineCount];
        buildingObjectsParent = new Transform[gridLineCount * gridLineCount];
        canBuild = new bool[gridLineCount * gridLineCount];
        roadIndex = new int[gridLineCount * gridLineCount];

        for (int i = 0; i < canBuild.Length; i++)
        {
            canBuild[i] = true;
        }
    }
}
