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
    public class SceneCollectiblesDatabaseManager : MonoBehaviour
    {
        [SerializeField]
        private String itemStateSave;
        [SerializeField]
        public SceneCollectiblesState database;
        // [SerializeField]
        // public SaveCollectivesDatabases globalDatabase;
        [SerializeField]
        private String filePath; 

        private void Awake()
        {            
            database.itemsCollectedDictionary = new Dictionary<int, bool>();
            database.sceneName = SceneManager.GetActiveScene().name;
        }  
        // Use this for initialization
        void Start()
        {
            database.totalCollectiblesToCollectInScene = database.itemsCollectedDictionary.Keys.Count;
            Debug.Log(database.totalCollectiblesToCollectInScene + " collectible(s) dans " + database.sceneName);
        }

        // Update is called once per frame
        void Update()
        {

            // foreach (var value in itemsCollectedDictionary.Values)
            /*
            foreach(KeyValuePair<int, bool> p in itemsCollectedDictionary) // loop through both
            {
                Debug.Log(p.Key + " " + p.Value);
                
            }
            */


        }
        
    }
}