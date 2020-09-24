using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
    [AddComponentMenu("AI/Flock Behaviour/Cohesion")]
    public class CohesionBehaviour : FilteredFlockBehaviour
    {
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0) //if there are no neighbours
            {
                return Vector2.zero; //don't move
            }

            Vector2 cohesiveMove = Vector2.zero; //set default to not moving
            //if(filter==null){return context}else{return filter.Filter(agent,context)}
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

            foreach (Transform item in filteredContext) //for each filtered neighbour
            {
                cohesiveMove += (Vector2)item.position; //add all their positions together
            }
            cohesiveMove /= context.Count; //get average position
            cohesiveMove -= (Vector2)agent.transform.position; //get direction

            return cohesiveMove; //return vector2 movement
        }
    }
}
