using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [AddComponentMenu("AI3/Mechanics/Life/Predator")]
    public class Predator : Life
    {
        #region Variables
        [Header("Predator Specific Stats")]
        [SerializeField, Tooltip("Current behaviour state of this creature.")]
        private PredatorState state;
        
        #endregion
        #region Start
        void Start()
        {
            StartLife();

            state = PredatorState.Wander; //start by wandering
        }
        #endregion
        #region Update
        void Update()
        {
            //if can see prey
            //state is chase
            //else wander and look for prey

            UpdateLife();
        }
        #endregion
        #region Collision
        private void OnCollisionStay2D(Collision2D collision)
        {
            //if collision is prey
            //state is attack
        }
        #endregion
        #region Functions
        protected override void DetermineBehaviour()
        {
            if (state == PredatorState.Wander)
            {
                flock.behaviour = behaviourSets[0];
            }
            else if (state == PredatorState.Chase)
            {
                flock.behaviour = behaviourSets[1];
            }
            else if (state == PredatorState.Attack)
            {
                flock.behaviour = behaviourSets[2];
            }
            else //catch if no state assigned
            {
                state = PredatorState.Wander;
                flock.behaviour = behaviourSets[0];
            }
        }
        #endregion
    }
    #region State enum
    public enum PredatorState
    {
        Wander, //find
        Chase, //offset pursuit
        Attack,
    }
    #endregion
}