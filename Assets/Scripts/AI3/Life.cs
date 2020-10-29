using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [AddComponentMenu("AI3/Mechanics/Life/Base")]
    public class Life : MonoBehaviour
    {
        #region Variables
        [Header("General Life Stats")]
        [SerializeField, Tooltip("Movement speed.")]
        protected float speed;

        [SerializeField, Tooltip("Array of composite behaviours this creature can access.")]
        protected CompositeBehaviour[] behaviourSets;

        [SerializeField, TextArea]
        protected string behavioursExplained;

        [SerializeField, Tooltip("Creature's Flock")]
        protected Flock flock;
        #endregion
        #region Start
        protected void StartLife()
        {
            flock = GetComponentInParent<Flock>(); //connect to flock

            flock.behaviour = behaviourSets[0]; //initial composite bahaviour
        }
        #endregion
        #region Update
        protected void UpdateLife()
        {
            DetermineBehaviour();
        }
        #endregion
        #region Functions
        protected virtual void DetermineBehaviour()
        {
            Debug.Log("this function is empty, no behaviour selected");
        }
        #endregion
    }
}
/* Notes:
 * 
 * each state should have its own composite behaviours
 * 
 * 
 * 
 * 
 */
