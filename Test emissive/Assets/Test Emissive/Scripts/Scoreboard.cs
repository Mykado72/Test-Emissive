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
        [SerializeField]
        private SceneCollectiblesState SceneCollectiblesState;
        [SerializeField]
        private SaveDatabaseManager databaseManager;
        [SerializeField]
        private CollectivesDatabasesScriptableObject database;

        void Awake()
        {
            DontDestroy(); // permet de ne conserver qu'une instance de ce script
        }

        // Use this for initialization
        void Start()
        {
            // Initialise();  // initialise ou charge les données et les rafraichis dans l'UI 
            SceneManager.sceneLoaded += OnSceneLoaded; // permet de detecter le loading d'une scene
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Initialise(); // initialise ou charge les données et les rafraichis dans l'UI 
            databaseManager.NewSceneIsLoaded();
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
            // permet de ne conserver qu'une instance de ce script
            nbItemsCollectedInSceneText.text = "Item found in this level : " + nb;
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
