using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [AddComponentMenu("AI3/Mechanics/Life/Prey")]
    public class Prey : Life
    {
        #region Variables
        [Header("Prey Specific Stats")]
        [SerializeField, Tooltip("Current behaviour state of this creature.")]
        private PreyState state;
        #endregion
        #region Start
        void Start()
        {
            StartLife();

            state = PreyState.Wander; //start by wandering
        }
        #endregion
        #region Update
        void Update()
        {

        }
        #endregion
        #region Functions
        protected override void DetermineBehaviour()
        {
            if (state == PreyState.Flee)
            {
                flock.behaviour = behaviourSets[0];
            }
            else if (state == PreyState.Wander)
            {
                flock.behaviour = behaviourSets[1];
            }
            else //catch if no state assigned
            {
                state = PreyState.Wander;
                flock.behaviour = behaviourSets[1];
            }
        }
        #endregion
    }
    #region State enum
    public enum PreyState
    {
        Wander, //flock
        Flee, //hide,evade
    }
    #endregion
}