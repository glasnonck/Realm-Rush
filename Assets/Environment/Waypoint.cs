using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get => isPlaceable; }
    
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool wasPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !wasPlaced;   // Only block the tile if a tower was placed.
        }
    }
}
