using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    private float _ortographicSize;
    Vector3 touchStart;

    [SerializeField] private LayerMask uiMask;

    private void Update()
    {
        _ortographicSize = cam.orthographicSize;
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, uiMask))
            {
                return;
            }
            else
            {
                touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, uiMask))
            {
                return;
            }
            else
            {
                Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
                cam.transform.position += direction;
            }
        }
    }

    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y > 0 && _ortographicSize < 10f)
        {
            cam.orthographicSize += 0.1f;
        }
        else if (Input.mouseScrollDelta.y < 0 && _ortographicSize > 0.2f)
        {
            cam.orthographicSize -= 0.1f;
        }

        if(Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.001f);
        }
    }

    void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, 0.001f, 10f);
    }
}
