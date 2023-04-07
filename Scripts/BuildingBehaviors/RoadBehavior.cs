using UnityEngine;

public class RoadBehavior : MonoBehaviour
{
    [SerializeField] private GameObject straightRoad;
    [SerializeField] private GameObject straightRoad2;
    [SerializeField] private GameObject turnBendDownRight;
    [SerializeField] private GameObject turnBendUpRight;
    [SerializeField] private GameObject turnBendDownLeft;
    [SerializeField] private GameObject turnBendUpLeft;
    [SerializeField] private GameObject forkRight;
    [SerializeField] private GameObject forkLeft;
    [SerializeField] private GameObject forkUp;
    [SerializeField] private GameObject forkDown;
    [SerializeField] private GameObject crossRoad;

    public int index;

    private void Update()
    {
        SetRoad(PlaceForBuilding.Instance.GetNeighboorRoadObjectPositions(index));
    }

    public void SetRoad(int index)
    {
        switch (index)
        {
            default:
            case 0:
                Hide();
                straightRoad.SetActive(true); 
                break;
            case 1: 
                Hide();
                straightRoad2.SetActive(true);
                break;
            case 2:
                Hide();
                turnBendDownRight.SetActive(true);
                break;
            case 3:
                Hide();
                turnBendUpRight.SetActive(true);
                break;
            case 4:
                Hide();
                turnBendDownLeft.SetActive(true);
                break;
            case 5:
                Hide();
                turnBendUpLeft.SetActive(true);
                break;
            case 6:
                Hide();
                forkUp.SetActive(true);
                break;
            case 7:
                Hide();
                forkDown.SetActive(true);
                break;
            case 8:
                Hide();
                forkLeft.SetActive(true);
                break;
            case 9:
                Hide();
                forkRight.SetActive(true);
                break;
            case 10:
                Hide();
                crossRoad.SetActive(true);
                break;
        }
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    private void Hide()
    {
        straightRoad.SetActive(false);
        straightRoad2.SetActive(false);
        turnBendDownRight.SetActive(false);
        turnBendUpRight.SetActive(false);
        turnBendDownLeft.SetActive(false);
        turnBendUpLeft.SetActive(false);
        forkRight.SetActive(false);
        forkLeft.SetActive(false);
        forkUp.SetActive(false);
        forkDown.SetActive(false);
        crossRoad.SetActive(false);
    }
}
