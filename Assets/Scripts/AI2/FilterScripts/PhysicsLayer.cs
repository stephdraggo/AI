using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Filter/Obstacle Layer")]
    public class PhysicsLayer : ContextFilter
    {
        public LayerMask mask; //LayerMask uses binary logic
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            List<Transform> filtered = new List<Transform>();
            foreach (Transform item in original)
            {
                //binary logic instead of boolean logic
                //00001010 << 3 = 01010000 
                // << moves a binary number to the left by a given amount
                // ~ inverts a binary number (~00001000 = 11110111)
                // &(and), |(or), ^(xor), single instead of double

                //(1 << item.gameObject.layer) gets the current game object's layer index as a binary value
                if (mask == (mask | (1 << item.gameObject.layer)))
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}