using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
    [AddComponentMenu("AI/Flock Behaviour/Avoidance")]
    public class AvoidanceBehaviour : FilteredFlockBehaviour
    {
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0) //if no neighbours
            {
                return Vector2.zero; //don't move
            }

            Vector2 avoidanceMove = Vector2.zero; //set default to not moving
            int avoidNeighbours = 0; //number of neighbours to avoid
            //if(filter==null){return context}else{return filter.Filter(agent,context)}
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

            foreach (Transform item in filteredContext) //for each filtered neighbour
            {
                if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius) //if the distance is less than the avoidance radius
                {
                    avoidNeighbours++; //add neighbour to avoid list
                    avoidanceMove += (Vector2)(agent.transform.position - item.position); //add neighbours' positions together
                }
                if (avoidNeighbours > 0) //if there are neighbours to avoid
                {
                    avoidanceMove /= avoidNeighbours; //get average position to avoid
                }
            }

            return avoidanceMove; //return vector2 movement
        }
    }
}

