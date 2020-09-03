using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [AddComponentMenu("AI/Flock Behaviour Base")]
    //abstract means it doesn't run on its own, must be inherited from
    //ScriptableObject means it can run without a game object
    public abstract class FlockBehaviour : ScriptableObject
    {
        #region Variables

        #endregion
        public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
    }
}