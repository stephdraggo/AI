using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AI1
{
    [AddComponentMenu("AI/Canvas Control")]
    public class CanvasControl : MonoBehaviour
    {
        #region Variables
        public PlayerControl player;
        public Slider healthSlider;
        public GameObject enemyPrefab;
        #endregion

        void Update()
        {
            healthSlider.value = player.health;
        }
        #region Functions
        public void ResetScene()
        {
            SceneManager.LoadScene("AI1Scene");
        }
        public void SpawnNewAI()
        {
            Instantiate<GameObject>(enemyPrefab);
        }
        public void ManualHealth()
        {
            player.health = healthSlider.value;
        }
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
        #endregion
    }
}