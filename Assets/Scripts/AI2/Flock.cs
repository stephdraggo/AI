using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [AddComponentMenu("AI/Flock")]
    public class Flock : MonoBehaviour
    {
        #region Variables
        [Header("Reference Variables")]
        public FlockAgent agentPrefab;
        List<FlockAgent> agents = new List<FlockAgent>(); //list of agents in this flock
        public FlockBehaviour behaviour;

        [Range(10, 500)] public int startingCount = 250; //many birbs
        const float AgentDensity = 0.08f; //const values don't change

        [Range(1f, 100f), Tooltip("Multiplier for speed.")] public float driveFactor = 10f;

        [Range(1f, 100f)] public float maxSpeed = 5f;

        [Range(1f, 10f)] public float neighbourRadius = 1.5f;

        [Range(0f, 10f)] public float avoidanceRadius = 0.5f;

        private float squareMaxSpeed, squareNeighbourRadius, squareAvoidanceRadius; //store square roots
        #endregion

        #region Get/Set
        public float SquareAvoidanceRadius
        {
            get => squareAvoidanceRadius;
        }
        #endregion

        #region Start
        void Start()
        {
            squareMaxSpeed = maxSpeed * maxSpeed;
            squareNeighbourRadius = neighbourRadius * neighbourRadius;
            squareAvoidanceRadius = avoidanceRadius * avoidanceRadius;

            for (int i = 0; i < startingCount; i++) //fill the sky with birbs
            {
                FlockAgent newAgent = Instantiate( //birb making instructions:
                    agentPrefab, //premade birb
                    Random.insideUnitCircle * startingCount * AgentDensity, //random position inside a circle
                    Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), //random rotation
                    transform
                    );
                newAgent.name = "Agent " + i; //set birb's name
                Renderer agentColor = newAgent.GetComponent<SpriteRenderer>(); //connect colour
                agentColor.material.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0f, 1f); //give random colour
                newAgent.Initialise(this); //make birb according to these directions
                agents.Add(newAgent); //add the birb to the flock
            }
        }
        #endregion
        #region Update
        void Update()
        {
            foreach (FlockAgent agent in agents) //for each FlockAgent script in agents list
            {
                List<Transform> context = GetNearbyObjects(agent); //get context list of neighbours for this agent
                Vector2 move = behaviour.CalculateMove(agent, context, this); //calculates the movement of the agent based on the agent, its neighbours, and the flock
                move *= driveFactor; //increase speed
                if (move.sqrMagnitude > squareMaxSpeed) //if speed is greater than max speed
                {
                    move = move.normalized * maxSpeed; //keep the direction but set speed to max speed
                }
                agent.Move(move); //move agent according to calculations
            }
        }
        #endregion

        /// <summary>Finds the transform of colliders within the neighbour range of the given flock agent.</summary>
        /// <param name="agent"></param>
        /// <returns>List of transforms of neighbouring flock agents.</returns>
        List<Transform> GetNearbyObjects(FlockAgent agent)
        {
            List<Transform> context = new List<Transform>(); //new empty list
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius); //fill list with everything agent's neighbour radius collides with
            foreach (Collider2D collider in contextColliders) //for each collider in the contextColliders array
            {
                if (collider != agent.AgentCollider) //if the collider is not the agent's collider
                {
                    context.Add(collider.transform); //add the collider to the context list
                }
            }

            return context;
        }
    }
}