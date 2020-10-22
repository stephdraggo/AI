using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Wander")]
    [AddComponentMenu("AI/Flock Behaviour/Wander")]
    public class WanderBehaviour : FilteredFlockBehaviour
    {
        Path path = null;
        int currentWaypointIndex = 0;
        Vector2 waypointDirection = Vector2.zero;

        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (path == null)
            {
                FindPath(agent, context);
            }


            return FollowPath(agent);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="context"></param>
        private void FindPath(FlockAgent agent, List<Transform> context)
        {
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

            if (filteredContext.Count == 0)
            {
                return;
            }

            int randomPath = Random.Range(0, filteredContext.Count);
            path = filteredContext[randomPath].GetComponentInParent<Path>();
        }
        private Vector2 FollowPath(FlockAgent agent)
        {
            if (path == null)
            {
                return Vector2.zero;
            }

            if (InRadius(agent))
            {
                currentWaypointIndex++;
                if (currentWaypointIndex > path.waypoints.Count)
                {
                    currentWaypointIndex = 0;
                }

                return Vector2.zero;
            }

            return waypointDirection;
        }
        private bool InRadius(FlockAgent agent)
        {
            waypointDirection = (Vector2)(path.waypoints[currentWaypointIndex].position - agent.transform.position);

            if (waypointDirection.magnitude < path.radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}