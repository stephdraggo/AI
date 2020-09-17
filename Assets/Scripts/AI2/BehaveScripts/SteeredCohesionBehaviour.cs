using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
    [AddComponentMenu("AI/Flock Behaviour/Steered Cohesion")]
    public class SteeredCohesionBehaviour : FlockBehaviour
    {
        #region Variables
        Vector2 currentVelocity;
        [Range(0f, 5f)] public float agentSmoothTime = 0.5f;
        #endregion

        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0) //if there are no neighbours
            {
                return Vector2.zero; //don't move
            }

            Vector2 cohesionMove = Vector2.zero; //set default to not moving
            foreach (Transform item in context) //for each neighbour
            {
                cohesionMove += (Vector2)item.position; //add all their positions together
            }
            cohesionMove /= context.Count; //get average position
            cohesionMove -= (Vector2)agent.transform.position; //get direction

            cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime); //smooths turning

            return cohesionMove; //return vector2 movement
        }
    }
}