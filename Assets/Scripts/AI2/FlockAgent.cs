using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI2
{
    [AddComponentMenu("AI/Flock Agent")]
    [RequireComponent(typeof(CircleCollider2D))]
    public class FlockAgent : MonoBehaviour
    {
        #region Variables
        [Header("Reference Variables")]
        private Flock agentFlock;
        private Collider2D agentCollider;

        #endregion

        #region Properties (get/set)
        //make private values publicly readable
        public Collider2D AgentCollider
        {
            get => agentCollider;
        }
        public Flock AgentFlock
        {
            get => agentFlock;
        }
        #endregion

        #region Start
        void Start()
        {
            agentCollider = GetComponent<Collider2D>(); //connect collider
        }
        #endregion

        #region Functions
        public void Initialise(Flock flock)
        {
            agentFlock = flock;
        }
        public void Move(Vector2 velocity)
        {
            transform.up = velocity; //velocity gives speed and direction?
            transform.position += (Vector3)velocity * Time.deltaTime;
        }
        #endregion
    }
}