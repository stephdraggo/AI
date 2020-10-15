using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Offset Pursuit")]
    [AddComponentMenu("AI/Flock Behaviour/Offset Pursuit")]
    public class PursuitBehaviour : FilteredFlockBehaviour
    {
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

            Vector2 move = Vector2.zero; //default is no movement

            if (filteredContext.Count == 0) //if there's no prey close by
            {
                return move; //no movement
            }

            foreach (Transform item in filteredContext) //for each filtered neighbour
            {
                float distance = Vector2.Distance(item.position, agent.transform.position); //get distance

                /* Get the distance from each neighbour at a fraction of the neighbour radius
                 * Invert the fraction so that the closer neighbours have a higher value
                 * Divide the value by how many neighbours there are
                 * That's the weight of this neighbour's direction on the current agent */
                float weight = (1 - distance / flock.neighbourRadius) / filteredContext.Count;

                Vector2 direction = (item.position - agent.transform.position) * weight; //get direction and multiply by weight

                move += direction; //add weighted direction to movement
            }

            return move;
        }
    }
}