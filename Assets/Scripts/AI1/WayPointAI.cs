using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AI
{
    [AddComponentMenu("AI/WayPointAI")]
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

            //get player script
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

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
        public void MoveAi()
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime); //move towards target position
        }

        public void CheckHealth()
        {
            CheckDeath();
            if (health < maxHealth / 4 && health < playerStats.health) //if health less than 25% and less than player's health
            {
                behaviour = AiBehaviour.Flee; //behaviour is flight
            }
        }

        public void CheckDeath()
        {
            if (health <= 0) //if no health
            {
                playerStats.killCount++; //increase kill count of player
                Destroy(gameObject); //die
            }
        }

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
        private void NextBehaviour() //code comment this part bc don't understand it yet
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
        private IEnumerator Patrol()
        {
            while (behaviour == AiBehaviour.Patrol) //while behaviour is in patrolling state
            {
                CheckHealth();

                PatrolTarget(); //find target of patrol
                MoveAi(); //move in target direction

                if (Vector2.Distance(transform.position, playerObject.transform.position) <= sightDistance) //if can see player
                {
                    behaviour = AiBehaviour.Chase; //behaviour is chasing
                }

                yield return null; //do this enumeration again
            }
            NextBehaviour();
        }
        private IEnumerator Chase()
        {
            while (behaviour == AiBehaviour.Chase) //while behaviour is in chasing state
            {
                CheckHealth();

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

                yield return null; //do this enumeration again
            }
            NextBehaviour();
        }
        private IEnumerator Attack()
        {
            while (behaviour == AiBehaviour.Attack) //while behaviour is in attack state
            {
                CheckHealth();

                playerStats.health -= strength; //do damage
                health -= playerStats.strength; //take damage

                yield return new WaitForSeconds(1f); //recharge attack

                MoveAi();
                if (Vector2.Distance(transform.position, playerObject.transform.position) > onTarget) //if not in range of player
                {
                    behaviour = AiBehaviour.Chase; //behaviour is chasing
                }
            }
            NextBehaviour();
        }
        private IEnumerator Flee()
        {
            while (behaviour == AiBehaviour.Flee) //while behaviour is in flight state
            {
                CheckDeath();

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
                yield return null;
            }
            NextBehaviour();
        }
        #endregion
    }
}