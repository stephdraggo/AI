using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
    [AddComponentMenu("AI/Flock Behaviour/Composite")]
    public class CompositeBehaviour : FlockBehaviour
    {
        [System.Serializable]
        public struct BehaviourGroup
        {
            public FlockBehaviour behaviour; //the behaviour type
            public float weight; //the weight/effect the behaviour has
        }

        public BehaviourGroup[] behaviours;

        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector2 move = Vector2.zero; //default is 0
            for (int i = 0; i < behaviours.Length; i++) //for every behaviour in the array
            {
                Vector2 partialMove = behaviours[i].behaviour.CalculateMove(agent, context, flock) * behaviours[i].weight; //calculate part of the movement based on the given behaviour and its weight
                if (partialMove != Vector2.zero) //if the partial movement is not zero
                {
                    if (partialMove.sqrMagnitude > behaviours[i].weight * behaviours[i].weight) //if the distance is greater than the given weight
                    {
                        partialMove.Normalize(); //keep direction but make distance 1
                        partialMove *= behaviours[i].weight; //multiply partial movement by the allowed weight again
                    }
                    move += partialMove; //add partial movement to full movement
                }
            }

            return move;
        }
    }
}