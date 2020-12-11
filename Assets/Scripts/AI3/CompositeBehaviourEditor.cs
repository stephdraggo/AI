using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI2
{
    [CustomEditor(typeof(CompositeBehaviour))]
    public class CompositeBehaviourEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //setup
            CompositeBehaviour cb = (CompositeBehaviour)target;

            Rect r = EditorGUILayout.BeginHorizontal();
            r.height = EditorGUIUtility.singleLineHeight;

            //check for behaviours
            if (cb.behaviours == null || cb.behaviours.Length == 0)
            {
                EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
                EditorGUILayout.EndHorizontal();
                r = EditorGUILayout.BeginHorizontal();
                r.height = EditorGUIUtility.singleLineHeight;
            }
            else
            {
                r.x = 30f;
                r.width = EditorGUIUtility.currentViewWidth - 95f;
                EditorGUI.LabelField(r, "Behaviors");
                r.x = EditorGUIUtility.currentViewWidth - 65f;
                r.width = 60f;
                EditorGUI.LabelField(r, "Weights");
                r.y += EditorGUIUtility.singleLineHeight * 1.2f;

                EditorGUI.BeginChangeCheck();
                for (int i = 0; i < cb.behaviours.Length; i++)
                {
                    r.x = 5f;
                    r.width = 20f;
                    EditorGUI.LabelField(r, i.ToString());
                    r.x = 30f;
                    r.width = EditorGUIUtility.currentViewWidth - 95f;
                    cb.behaviours[i].behaviour = (FlockBehaviour)EditorGUI.ObjectField(r, cb.behaviours[i].behaviour, typeof(FlockBehaviour), false);
                    r.x = EditorGUIUtility.currentViewWidth - 65f;
                    r.width = 60f;
                    cb.behaviours[i].weight = EditorGUI.FloatField(r, cb.behaviours[i].weight);
                    r.y += EditorGUIUtility.singleLineHeight * 1.1f;
                }
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(cb);
                }
            }

            EditorGUILayout.EndHorizontal();
            r.x = 5f;
            r.width = EditorGUIUtility.currentViewWidth - 10f;
            r.y += EditorGUIUtility.singleLineHeight * 0.5f;
            if (GUI.Button(r, "Add Behavior"))
            {
                AddBehavior(cb);
                EditorUtility.SetDirty(cb);
            }

            r.y += EditorGUIUtility.singleLineHeight * 1.5f;
            if (cb.behaviours != null && cb.behaviours.Length > 0)
            {
                if (GUI.Button(r, "Remove Behavior"))
                {
                    RemoveBehavior(cb);
                    EditorUtility.SetDirty(cb);
                }
            }


        }

        void AddBehavior(CompositeBehaviour cb)
        {
            int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
            FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount + 1];
            float[] newWeights = new float[oldCount + 1];
            for (int i = 0; i < oldCount; i++)
            {
                newBehaviors[i] = cb.behaviours[i].behaviour;
                newWeights[i] = cb.behaviours[i].weight;
            }
            newWeights[oldCount] = 1f;
            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                cb.behaviours[i].behaviour = newBehaviors[i];
                cb.behaviours[i].weight = newWeights[i];
            }
        }

        void RemoveBehavior(CompositeBehaviour cb)
        {
            int oldCount = cb.behaviours.Length;
            if (oldCount == 1)
            {
                cb.behaviours = null;
                return;
            }
            FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount - 1];
            float[] newWeights = new float[oldCount - 1];
            for (int i = 0; i < oldCount - 1; i++)
            {
                newBehaviors[i] = cb.behaviours[i].behaviour;
                newWeights[i] = cb.behaviours[i].weight;
            }
            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                cb.behaviours[i].behaviour = newBehaviors[i];
                cb.behaviours[i].weight = newWeights[i];
            }

        }
    }
}