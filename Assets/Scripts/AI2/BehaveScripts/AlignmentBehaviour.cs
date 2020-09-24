using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
    [AddComponentMenu("AI/Flock Behaviour/Alignment")]
    public class AlignmentBehaviour : FilteredFlockBehaviour
    {
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0) //if no neighbours
            {
                return agent.transform.up; //maintain current direction
            }

            Vector2 alignmentMove = Vector2.zero; //default direction is 0
            //if(filter==null){return context}else{return filter.Filter(agent,context)}
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

            foreach (Transform item in filteredContext) //for each filtered neighbour
            {
                alignmentMove += (Vector2)item.transform.up; //get average direction by adding
            }
            alignmentMove /= context.Count; //decrease magnitude

            return alignmentMove; //return vector2 direction
        }
    }
}