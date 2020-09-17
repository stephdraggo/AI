using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [CreateAssetMenu(menuName = "Flock/Behaviour/Homing")]
    [AddComponentMenu("AI/Flock Behaviour/Homing")]
    public class HomingBehaviour : FlockBehaviour
    {
        #region Variables
        [SerializeField] private Vector2 centre = Vector2.zero;
        [SerializeField] private float radius = 15;
        #endregion
        public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector2 centreOffset = centre - (Vector2)agent.transform.position; //get direction to centre
            float t = centreOffset.magnitude / radius; //get distance to centre
            if (t < 5f) //if in range
            {
                return Vector2.zero; //don't affect direction
            }

            return centreOffset; //pull to centre is stronger depending on how far from centre
        }
    }
}