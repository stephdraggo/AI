using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace AI
{
    [AddComponentMenu("AI/WayPointAI")]
    public class WayPointAI : MonoBehaviour
    {
        #region Variables
        [Header("Variable References")]
        public GameObject player;
        //public Text healthText;
        public Vector2 targetPos;
        public float speed, onTarget = 1f, sightDistance = 10f;
        public int wayPointId, health, strength;
        public GameObject[] wayPoints;
        #endregion

        void Start()
        {
            //get player object
            player = GameObject.FindGameObjectWithTag("Player");

            //get waypoints 
            wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");

            //get health UI
            //healthText = GetComponentInChildren<Text>();

            wayPointId = Random.Range(0, wayPoints.Length); //random start
            speed = Random.Range(3f, 5f); //random speed
            health = Random.Range(70, 110); //random health
            strength = Random.Range(1, 10); //random strength (attack)

            //set target to first waypoint
            targetPos = wayPoints[wayPointId].transform.position;
        }

        void Update()
        {
            //healthText.text = health + " HP";

            if (Vector2.Distance(transform.position, player.transform.position) < onTarget) //if in range to attack
            {
                Debug.Log("in range");
            }

            if (Vector2.Distance(transform.position, player.transform.position) > sightDistance) //if not close to player
            {
                Patrol();
            }
            else //if close to player
            {
                targetPos = player.transform.position; //target is player position
            }
            MoveAi();
        }
        public void MoveAi()
        {
            //move towards target position
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }


        public void Patrol()
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
    }
}