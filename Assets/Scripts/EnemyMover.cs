using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f,5f)] private float speed = 1f;

    private Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnStart();
        StartCoroutine(moveWaypoint());
    }

    // Update is called once per frame

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    void Update()
    {
        
    }

    void ReturnStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator moveWaypoint()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            
            transform.LookAt(endPosition);
            
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition,endPosition,travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
    }

    void FindPath()
    {
        path.Clear();
        GameObject waypoints = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in waypoints.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            
            if (child != null)
            {
                path.Add(waypoint);
            }
        }
    }
}
