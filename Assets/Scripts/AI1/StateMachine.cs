using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [AddComponentMenu("AI/State Machine")]
    public class StateMachine : MonoBehaviour
    {

        public State state;

        void Start()
        {
            NextState();
        }

        #region Next State
        private void NextState()
        {
            string methodName = state.ToString() + "State";

            System.Reflection.MethodInfo info =
                GetType().GetMethod(methodName,
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            StartCoroutine((IEnumerator)info.Invoke(this, null));
        }
        #endregion

        //state methods replacing update
        #region State Methods
        private IEnumerator CrawlState()
        {
            float startTime = Time.time;
            Debug.Log("Crawl: Enter");
            while (state == State.Crawl)
            {
                yield return null; //pause for one frame
            }
            Debug.Log("Crawl: Exit");
            Debug.Log("Crawling lasted " + (Time.time - startTime) + " seconds.");
            NextState();
        }
        private IEnumerator WalkState()
        {
            Debug.Log("Walk: Enter");
            while (state == State.Walk)
            {
                Debug.Log("AAAHHHHHHHH");
                yield return new WaitForSeconds(2f);
            }
            Debug.Log("Walk: Exit");
            NextState();
        }
        private IEnumerator RunState()
        {
            Debug.Log("Run: Enter");
                while (state == State.Run)
                {
                    yield return null;
                }
            Debug.Log("Run: Exit");
            NextState();
        }
        #endregion

        
    }
    public enum State
    {
        Crawl,
        Walk,
        Run
    }
}


