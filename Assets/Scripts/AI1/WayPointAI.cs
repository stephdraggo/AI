using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AI1
{
    [AddComponentMenu("AI/WayPoint AI")]
    public class WayPointAI : MonoBehaviour
    {
        #region Variables
        [Header("Variable References")]
        public GameObject playerObject;
        public PlayerControl playerStats;
        public AiBehaviour behaviour;
        public Vector2 targetPos;
        public float speed, onTarget = 2f, sightDistance = 10f;
        [Min(0)]
        public float health, maxHealth;
        public int wayPointId, strength;
        public GameObject[] wayPoints;
        #endregion
        #region Start
        void Start()
        {
            //get player object
            playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject == null)
            {
                Destroy(gameObject);
                return;
            }

            //get player script
            playerStats = playerObject.GetComponent<PlayerControl>();

            //get waypoints 
            wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");

            //get health UI

            //set stats
            wayPointId = Random.Range(0, wayPoints.Length); //random start
            speed = Random.Range(3f, 5f); //random speed
            health = Random.Range(70, 110); //random health
            maxHealth = health;
            strength = Random.Range(1, 10); //random strength (attack)

            //set target to first waypoint
            targetPos = wayPoints[wayPointId].transform.position;

            //set behaviour
            behaviour = AiBehaviour.Patrol;
            NextBehaviour();
        }
        #endregion
        #region Misc Methods
        /// <summary>
        /// Moves towards target position.
        /// </summary>
        public void MoveAi()
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        /// <summary>
        /// Check health and change behaviour to flight if needed.
        /// </summary>
        public void CheckHealth()
        {
            CheckDeath(); //call check death
            if (health < maxHealth / 4 && health < playerStats.health) //if health less than 25% and less than player's health
            {
                behaviour = AiBehaviour.Flee; //behaviour is flight
            }
        }
        /// <summary>
        /// Check health and kill if needed.
        /// </summary>
        public void CheckDeath()
        {
            if (health <= 0) //if no health
            {
                playerStats.killCount++; //increase kill count of player
                Destroy(gameObject); //die
            }
        }
        /// <summary>
        /// Assign target position according to waypoints.
        /// </summary>
        public void PatrolTarget()
        {
            if (Vector2.Distance(transform.position, wayPoints[wayPointId].transform.position) < onTarget) //if arrived at target waypoint
            {
                if (wayPointId == wayPoints.Length - 1) //if waypoint is last waypoint
                {
                    wayPointId = 0; //reset waypoint
                }
                else //if waypoint is not last waypoint
                {
                    wayPointId++; //move to next waypoint
                }
            }
            targetPos = wayPoints[wayPointId].transform.position; //new target is waypoint
        }
        #endregion
        #region Next State
        /// <summary>
        /// Go to new behaviour method.
        /// </summary>
        private void NextBehaviour()
        {
            string methodName = behaviour.ToString();

            System.Reflection.MethodInfo info =
                GetType().GetMethod(methodName,
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            StartCoroutine((IEnumerator)info.Invoke(this, null));
        }
        #endregion
        #region Behaviour Methods
        /// <summary>
        /// Behaviour when patrolling.
        /// </summary>
        private IEnumerator Patrol()
        {
            while (behaviour == AiBehaviour.Patrol) //while behaviour is in patrolling state
            {
                CheckHealth();

                PatrolTarget(); //find target of patrol
                MoveAi(); //move in target direction

                if (playerObject != null)
                {
                    if (Vector2.Distance(transform.position, playerObject.transform.position) <= sightDistance) //if can see player
                    {
                        behaviour = AiBehaviour.Chase; //behaviour is chasing
                    }
                }
                yield return null; //do this enumeration again
            }
            NextBehaviour();
        }
        /// <summary>
        /// Behaviour when chasing.
        /// </summary>
        private IEnumerator Chase()
        {
            while (behaviour == AiBehaviour.Chase) //while behaviour is in chasing state
            {
                CheckHealth();
                if (playerObject != null)
                {
                    targetPos = playerObject.transform.position; //target is player position
                    MoveAi(); //move towards player

                    if (Vector2.Distance(transform.position, playerObject.transform.position) <= onTarget) //if in range of player
                    {
                        behaviour = AiBehaviour.Attack; //behaviour is attacking
                    }
                    else if (Vector2.Distance(transform.position, playerObject.transform.position) > sightDistance) //if can't see player
                    {
                        behaviour = AiBehaviour.Patrol; //behaviour is patrolling
                    }
                }
                else
                {
                    behaviour = AiBehaviour.Patrol;
                }
                yield return null; //do this enumeration again
            }
            NextBehaviour();
        }
        /// <summary>
        /// Behaviour when attacking.
        /// </summary>
        private IEnumerator Attack()
        {
            while (behaviour == AiBehaviour.Attack) //while behaviour is in attack state
            {
                CheckHealth();

                playerStats.health -= strength; //do damage
                health -= playerStats.strength; //take damage

                yield return new WaitForSeconds(1f); //recharge attack

                MoveAi();
                if (playerObject != null)
                {
                    if (Vector2.Distance(transform.position, playerObject.transform.position) > onTarget) //if not in range of player
                    {
                        behaviour = AiBehaviour.Chase; //behaviour is chasing
                    }
                }
                else
                {
                    behaviour = AiBehaviour.Patrol;
                }
            }
            NextBehaviour();
        }
        /// <summary>
        /// Behaviour when fleeing.
        /// </summary>
        private IEnumerator Flee()
        {
            while (behaviour == AiBehaviour.Flee) //while behaviour is in flight state
            {
                CheckDeath();
                if (playerObject != null)
                {
                    if (Vector2.Distance(transform.position, playerObject.transform.position) <= sightDistance) //if can see player
                    {
                        Vector3 difference = playerObject.transform.position - transform.position;
                        targetPos = transform.position - difference;

                        //change this to a distance from player somehow

                        if (Vector2.Distance(transform.position, playerObject.transform.position) <= onTarget) //if in range of player
                        {
                            health -= playerStats.strength * Time.deltaTime; //take damage
                        }

                        MoveAi(); //move towards target position
                    }
                }
                else
                {
                    behaviour = AiBehaviour.Patrol;
                }
                yield return null;
            }
            NextBehaviour();
        }
        #endregion
    }
}
public enum AiBehaviour
{
    Patrol,
    Chase,
    Attack,
    Flee
}