using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    void OnMouseDown()
    {
        /*Debug.Log("Click start");
        Debug.Log(BuildingSystem.GetMouseWorldPosition());
        Debug.Log(transform.position);*/
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
        /*Debug.Log(offset);
        Debug.Log("Click end");*/
    }

    void OnMouseDrag()
    {
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        Debug.Log(pos);
        transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
        Debug.Log(transform.position);
    }
}
