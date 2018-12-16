using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TestEmissive
{
    /// <summary>
    ///  Gere l'affichage du score, nombres d'items...
    /// </summary>
    public class Scoreboard : MonoBehaviour {

        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text nbItemsCollectedInSceneText;
        [SerializeField]
        private Text nbItemsCollectedTotalText;
        [SerializeField]
        public int nbItemsCollectedTotal;
        [SerializeField]
        private bool ShowNbItemsCollectedInScene;

        void Awake()
        {
            DontDestroy(); // permet de ne conserver qu'une instance de ce script
        }

        // Use this for initialization
        void Start()
        {

        }

        public void UpdateScore(int value)
        {
            scoreText.text = "Score : " + value.ToString();
        }

        public void UpdateGlobalItemsCollected(int nb)
        {
            nbItemsCollectedTotalText.text = "Total Item : " + nb;
        }

        public void UpdateNbItemsCollectedInScene(int nb)
        {
            nbItemsCollectedInSceneText.text = "Item found in this level : " + nb;
        }

        public void LevelFinished(string name)
        {            
            nbItemsCollectedInSceneText.text = "LEVEL "+name+" FINISHED !";            
        }

        private void DontDestroy()
        {
            // permet de ne conserver qu'une instance de ce script
            Scoreboard[] objs = GameObject.FindObjectsOfType<Scoreboard>();  // cherche les instances de Scorboard
            if (objs.Length > 1)                // il y a déjà une instance.
            {
                Destroy(this.gameObject);       // on ne continu pas avec cette instance, on la detroy
            }
            DontDestroyOnLoad(this.gameObject); // permet de ne pas destroy la toute première instance.
        }
    }
}
