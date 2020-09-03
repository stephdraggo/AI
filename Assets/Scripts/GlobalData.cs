using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [AddComponentMenu("AI/Global Enums")]
    public class GlobalData : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }
    }
}

public enum AiBehaviour
{
    Patrol,
    Chase,
    Attack,
    Flee
}