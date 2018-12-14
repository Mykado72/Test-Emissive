using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TestEmissive
{
    [SerializeField]
    public class SaveDatabaseManager : MonoBehaviour
    {
        [SerializeField]
        private String serializedSave;
        [SerializeField]
        private static int sceneIDCounter = 0;
        public List<String> sceneNameID; // permet trouver la position dans le tableau en fonction du nom de la scene courante
        public List<SceneCollectiblesState> sceneCollectiblesDatabaseList;
        [SerializeField]
        public SceneCollectiblesState currentSceneState;
        [SerializeField]
        private CollectivesDatabasesScriptableObject database;
        [SerializeField]
        public int totalCollectedItem;
        private Scoreboard scoreboard;
        [SerializeField]
        private String currentSceneName;
        [SerializeField]
        private int currentSceneIndex;

        private void Awake()
        {
            DontDestroy();
            scoreboard = GameObject.FindObjectOfType<Scoreboard>();
        }

        // Use this for initialization
        void Start()
        {
            LoadGameData("Save.json");
            UpdateScoreBoard();
            NewSceneIsLoaded();
        }

        public void NewSceneIsLoaded()
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            /*
            if (!sceneNameID.Contains(currentSceneName)) // la scene courante est une nouvelle scene
            {
                SceneCollectiblesDatabaseManager sceneCollectiblesDatabaseManager = GameObject.FindObjectOfType<SceneCollectiblesDatabaseManager>();
                // sceneNameID.Add(currentsceneName); // ajoute la scene dans la liste
                // SceneCollectiblesState sceneDatabase = Resources.Load<SceneCollectiblesState>(currentsceneName); 
                // sceneCollectiblesDatabaseList.Add(sceneDatabase);
                // sceneIDCounter++;
            }
            */
            SelectCurrentSceneState();
            UpdateScoreBoard();
        }

        private void SelectCurrentSceneState()
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            currentSceneIndex = sceneNameID.FindIndex(x => x == currentSceneName);
            if (currentSceneIndex >= 0)
                currentSceneState = Resources.Load<SceneCollectiblesState>(currentSceneName);
        }

        public void AddCollectible(ItemCollectible collectible)
        {
            SelectCurrentSceneState();
            int nbInScene = currentSceneState.nbCollectedItemInScene;
            nbInScene++;
            currentSceneState.nbCollectedItemInScene = nbInScene;
            if (nbInScene >= currentSceneState.totalCollectiblesToCollectInScene)
                Debug.Log("Level Finish");
            database.totalCollected++;
            database.score += collectible.itemValue;    
            SaveGameData("Save.json");
            UpdateScoreBoard();
        }

        private void UpdateScoreBoard()
        {            
            if (currentSceneIndex>-1)
            {
                int nbInScene = database.SceneDataBaseList[currentSceneIndex].nbCollectedItemInScene;
                scoreboard.UpdateNbItemsCollectedInScene(nbInScene);
            }
            scoreboard.UpdateGlobalItemsCollected(database.totalCollected);
            scoreboard.UpdateScore(database.score);
        }

        public void LoadGameData(String fileName)
        {            
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                // SceneCollectiblesState itemStateSave = JsonUtility.FromJson<SceneCollectiblesState>(dataAsJson);
                Debug.LogError(dataAsJson);
            }
            else
            {
                Debug.Log("Cannot find file!");
                totalCollectedItem = 0;
                database.totalCollected = 0;
                database.score=0;
            }
        }

        public void SaveGameData(String fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(filePath))
            {
                for (int i=0;  i< sceneCollectiblesDatabaseList.Count(); i++)
                {
                    serializedSave = JsonUtility.ToJson(sceneCollectiblesDatabaseList[i]);
                    Debug.Log(serializedSave);
                }
                // File.WriteAllText(filePath, itemStateSave);
            }
        }

        private void DontDestroy()
        {
            SaveDatabaseManager[] objs = GameObject.FindObjectsOfType<SaveDatabaseManager>();
            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }

}