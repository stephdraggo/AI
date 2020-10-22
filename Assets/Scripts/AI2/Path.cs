using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [AddComponentMenu("AI/Path")]
    public class Path : MonoBehaviour
    {
        #region Variables
        public List<Transform> waypoints;
        public float radius;

        [SerializeField] private Vector3 gizmoSize = Vector3.zero;

        #endregion
        #region Editor Functions
        private void OnDrawGizmos()
        {
            if (waypoints == null || waypoints.Count == 0)
            {
                return;
            }

            for (int i = 0; i < waypoints.Count; i++)
            {
                Transform waypoint = waypoints[i];

                if (waypoint == null) //optional safety
                {
                    continue; //return but for only this loop (do next iteration)
                }
                Gizmos.color = Color.cyan; //gizmos be cyan now
                Gizmos.DrawCube(waypoint.position, gizmoSize);

                if (i + 1 < waypoints.Count && waypoints[i + 1] != null) //if there is a next waypoint
                {
                    Gizmos.color = Color.white; //lines will be white
                    Gizmos.DrawLine(waypoint.position, waypoints[i + 1].position); //connect the cubes
                }
            }
        }
        #endregion
    }
}