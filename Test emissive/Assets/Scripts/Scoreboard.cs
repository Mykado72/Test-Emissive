using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TestEmissive
{
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
            Scoreboard[] objs = GameObject.FindObjectsOfType<Scoreboard>();

            if (objs.Length > 1)
            {                
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }

        // Use this for initialization
        void Start()
        {
            Initialise();
            SceneManager.sceneLoaded += OnSceneLoaded; // permet de detecter le loading d'une scene
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Debug.Log("OnSceneLoaded: " + scene.name);
            // Debug.Log(mode);
            Initialise();
            databaseManager.NewSceneIsLoaded();
        }

        private void Initialise()
        {
            // AddToScore(0);
            SceneCollectiblesState = GameObject.FindObjectOfType<SceneCollectiblesState>();
            databaseManager = GameObject.FindObjectOfType<SaveDatabaseManager>();
            // database= databaseManager.
            // UpdateScore(globalDatabase.sc)

            if (ShowNbItemsCollectedInScene) // la scene courante est un LEVEL
                UpdateNbItemsCollectedInScene(0); // affiche le nombre Item collecté dans ce LEVEL
            UpdateGlobalItemsCollected(0);
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
    }
}
