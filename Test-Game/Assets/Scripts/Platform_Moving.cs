using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Moving : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed_platform = 2;
    private int waypoint_index = 0;

    void Start()
    {
        transform.position = waypoints[waypoint_index].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(transform.position, waypoints[waypoint_index].transform.position) < 0.1f)
        {
            waypoint_index++;
            if(waypoint_index >= waypoints.Length)
            {
                waypoint_index = 0;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypoint_index].transform.position, Time.deltaTime * speed_platform);
        }
    }
}

// Ok to say this out loud, if the distance between the platform and waypoint hits 0, then we add to the index, but if the index
// is more than the array size we reset the index. Basically we are switching between the two positions. Else, move the platform
// towards the waypoint