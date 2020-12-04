using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Hide")]
    public class HideBehavior : FilteredFlockBehaviour
    {
        public ContextFilter obstaclesFilter;
        public float hideBehindObstacleDistance = 2f;
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            List<Transform> obstacleContext = (filter == null) ? context : obstaclesFilter.Filter(agent, context);

            if (filteredContext.Count == 0)
            {
                return Vector2.zero;
            }
            //find nearest obstacle to hide behind
            float nearestDistance = float.MaxValue;
            Transform nearestObstacle = null;
            foreach (Transform item in obstacleContext)
            {
                float distance = Vector2.Distance(item.position, agent.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObstacle = item;
                }
            }
            //if no obstacle
            if (nearestObstacle == null)
            {
                return Vector2.zero;
            }
            //find best hiding spot
            Vector2 hidePosition = Vector2.zero;
            foreach (Transform item in filteredContext)
            {
                Vector2 obstacleDirection = nearestObstacle.position - item.position;
                obstacleDirection = obstacleDirection.normalized * hideBehindObstacleDistance;
                hidePosition += ((Vector2)nearestObstacle.position) + obstacleDirection;
            }
            hidePosition /= filteredContext.Count;

            //for debug only
            Debug.DrawRay(hidePosition, Vector2.up * 1f);
            return hidePosition - (Vector2)agent.transform.position;

        }



    }
}