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
    /// <summary>
    ///  Gere la sauvagarde des informations dans les ScriptableObject ou objetAssets dans le dossier Assets\Resources)
    ///  3 objetsAssets : Scene1,Scene2,Scene3 stockent les infos concernant les données relatives à un Level (Nb d'objet à trouvé, Liste avec nom des objets trouvés...)
    ///  Un objet asset : SaveCollectivesDatabases (contient l'ensemble des données
    /// </summary>
    [SerializeField]
    public class SaveDatabaseManager : MonoBehaviour
    {
        // [SerializeField]
        private String serializedSave;
        [SerializeField]
        private static int sceneIDCounter = 0;
        public List<String> sceneNameID; // permet trouver la position dans le tableau en fonction du nom de la scene courante
        public List<SceneCollectiblesState> sceneCollectiblesDatabaseList; // liste de database "Scene" (scritables objects) correspondant à chaque LEVEL 
        [SerializeField]
        public SceneCollectiblesState currentSceneState;                   // servira de reference vers la database correspondante au LEVEL courant
        [SerializeField]
        private CollectivesDatabasesScriptableObject database;             // database "globale".
        [SerializeField]
        public int totalCollectedItem;
        private Scoreboard scoreboard;
        [SerializeField]
        private String currentSceneName;
        [SerializeField]
        private int currentSceneIndex;
        [SerializeField]
        private SaveStructure saveStructure;

        private void Awake()
        {            
            DontDestroy(); // permet de ne conserver qu'une instance de ce script
            scoreboard = GameObject.FindObjectOfType<Scoreboard>(); // cherche et lie script scoreboard
        }

        // Use this for initialization
        void Start()
        {
            LoadGameData("Save.json"); // charge le fichier "Save.json" (pour Windows dans C:\Users\UTILISATEUR\AppData\LocalLow\DefaultCompany\Test emissive)
            NewSceneIsLoaded();        //
        }

        /// <summary>
        ///     rafraichi toute les infos car une nouvelle scene a ete chargée
        /// </summary>
        public void NewSceneIsLoaded()
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            SelectCurrentSceneState(); // detecte la scene courant et choisi la database "LEVEL" correspondant
            UpdateScoreBoard();        // met a jour les infos dans l'UI
        }

        /// <summary>
        ///  detecte la scene courant et choisi la database "LEVEL" correspondant
        /// </summary>
        private void SelectCurrentSceneState()
        {
            currentSceneName = SceneManager.GetActiveScene().name;
            currentSceneIndex = sceneNameID.FindIndex(x => x == currentSceneName);
            if (currentSceneIndex >= 0) // =-1 quand MenuPrincipal
                currentSceneState = Resources.Load<SceneCollectiblesState>(currentSceneName);
            else
                currentSceneState = null;
        }

        /// <summary>
        ///  Ajoute les informations du collectible dans la database "LEVEL"
        /// </summary>
        public void AddCollectible(ItemCollectible collectible)
        {
            SelectCurrentSceneState();
            int nbInScene = currentSceneState.nbCollectedItemInScene;
            nbInScene++;
            currentSceneState.nbCollectedItemInScene = nbInScene;
            currentSceneState.itemsCollectedList.Add(collectible.itemName);
            if (nbInScene >= currentSceneState.totalCollectiblesToCollectInScene)
                Debug.Log("Level Finish");
            database.totalCollected++;
            database.score += collectible.itemValue;    
            SaveGameData("Save.json");
            UpdateScoreBoard();
        }

        /// <summary>
        ///     Retour True si le collectible passé en parametre à déjà été collecté
        /// </summary>
        public bool CheckIfCollected(ItemCollectible collectible)
        {
            SelectCurrentSceneState();
            return currentSceneState.itemsCollectedList.Contains(collectible.itemName);
        }

        /// <summary>
        ///     Met à jour le ScoreBoard
        /// </summary>
        private void UpdateScoreBoard()
        {            
            if (currentSceneState != null )
            {   // = null quand MenuPrincipal
                int nbInScene = database.SceneDataBaseList[currentSceneIndex].nbCollectedItemInScene;
                scoreboard.UpdateNbItemsCollectedInScene(nbInScene);
            }
            scoreboard.UpdateGlobalItemsCollected(database.totalCollected);
            scoreboard.UpdateScore(database.score);
        }

        /// <summary>
        ///     Charge les données dépuis le fichier Save.JSON
        /// </summary>
        public void LoadGameData(String fileName)
        {            
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                saveStructure = JsonUtility.FromJson<SaveStructure>(dataAsJson);
                Debug.Log("données du fichier de sauvegarde chargé");
            }
            else
            {
                Debug.Log("pas de fichier de sauvegarde");
                totalCollectedItem = 0;
                database.totalCollected = 0;
                database.score=0;
            }
        }

        /// <summary>
        ///     Sauve les données dans le fichier Save.JSON
        /// </summary>
        public void SaveGameData(String fileName)
        {
            saveStructure = new SaveStructure();
            saveStructure.totalCollected = database.totalCollected;
            saveStructure.score= database.score;
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            // if (File.Exists(filePath))
            {
                for (int i=0;  i< sceneCollectiblesDatabaseList.Count(); i++)
                {
                    string SceneDataBaseListSerialized = JsonUtility.ToJson(database.SceneDataBaseList[i]);
                    saveStructure.SceneDataBaseListSerialized[i] = SceneDataBaseListSerialized;
                }
                serializedSave = JsonUtility.ToJson(saveStructure);                
                File.WriteAllText(filePath, serializedSave);
                Debug.Log("Sauvegarde fait dans"+ filePath);
            }
        }

        /// <summary>
        ///     permet de ne conserver qu'une instance de ce script
        /// </summary>
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