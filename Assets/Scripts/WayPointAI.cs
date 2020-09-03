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
            //move towards target position
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
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
            //if arrived at target waypoint
            if (Vector2.Distance(transform.position, wayPoints[wayPointId].transform.position) < onTarget)
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
            targetPos = wayPoints[wayPointId].transform.position; //target is waypoint
        }
        #endregion
        #region Next State
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
        private IEnumerator Patrol()
        {
            while (behaviour == AiBehaviour.Patrol)
            {
                PatrolTarget();
                MoveAi();

                if (Vector2.Distance(transform.position, playerObject.transform.position) <= sightDistance) //if can see player
                {
                    behaviour = AiBehaviour.Chase; //behaviour is chasing
                }

                yield return null;
            }
            CheckHealth();
            NextBehaviour();
        }
        private IEnumerator Chase()
        {
            while (behaviour == AiBehaviour.Chase)
            {
                targetPos = playerObject.transform.position; //target is player position
                MoveAi();

                if (Vector2.Distance(transform.position, playerObject.transform.position) <= onTarget) //if in range of player
                {
                    behaviour = AiBehaviour.Attack; //behaviour is attacking
                }
                else if (Vector2.Distance(transform.position, playerObject.transform.position) > sightDistance) //if can't see player
                {
                    behaviour = AiBehaviour.Patrol; //behaviour is patrolling
                }

                yield return null;
            }
            CheckHealth();
            NextBehaviour();
        }
        private IEnumerator Attack()
        {
            while (behaviour == AiBehaviour.Attack)
            {
                playerStats.health -= strength; //do damage
                health -= playerStats.strength; //take damage

                yield return new WaitForSeconds(1f); //recharge attack

                MoveAi();
                if (Vector2.Distance(transform.position, playerObject.transform.position) > onTarget) //if not in range of player
                {
                    behaviour = AiBehaviour.Chase; //behaviour is chasing
                }
            }
            CheckHealth();
            NextBehaviour();
        }
        private IEnumerator Flee()
        {
            while (behaviour == AiBehaviour.Flee)
            {
                CheckDeath();

                if (Vector2.Distance(transform.position, playerObject.transform.position) <= sightDistance) //if can see player
                {
                    targetPos = -playerObject.transform.position; //target is negative player position
                    //change this to a distance from player somehow

                    if (Vector2.Distance(transform.position, playerObject.transform.position) <= onTarget) //if in range of player
                    {
                        health -= playerStats.strength * Time.deltaTime; //take damage
                    }

                    MoveAi();
                }
                yield return null;
            }
            NextBehaviour();
        }
        #endregion
    }
}