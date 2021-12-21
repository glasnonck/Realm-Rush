using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void FindPath()
    {
        path.Clear();
        
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            var waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null) { path.Add(waypoint); }
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    
    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
    
    private IEnumerator FollowPath()
    {
        // Move to each waypoint in the path.
        foreach (Waypoint waypoint in path)
        {
            var startPosition = transform.position;
            var endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            
            // Look towards the next waypoint.
            transform.LookAt(endPosition);

            // Move to the next waypoint over time.
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        // Reached the end of the path.
        FinishPath();
    }
}
