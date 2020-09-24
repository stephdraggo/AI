using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    public abstract class ContextFilter : ScriptableObject
    {
        /// <summary>
        /// Filters the list of neighbours according to given parameters.
        /// </summary>
        /// <param name="agent">Agent to have its neighbours filtered.</param>
        /// <param name="original">Unfiltered list of neighbours.</param>
        /// <returns></returns>
        public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
    }
}