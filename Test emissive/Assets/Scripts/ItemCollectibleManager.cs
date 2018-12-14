using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestEmissive
{
    public class ItemCollectibleManager : MonoBehaviour
    {
        [SerializeField]
        private ItemCollectible item;
        // [SerializeField]
        // private SceneCollectibleDatabaseManager sceneCollectibleDatabaseManager;
        // [SerializeField]
        // private SceneCollectiblesDatabase database; // pour raccourcir la syntaxe
        [SerializeField]
        private SaveDatabaseManager saveDatabaseManager;
        // Use this for initialization
        void Awake()
        {
            item = this.gameObject.GetComponent<ItemCollectible>();
            // sceneCollectibleDatabaseManager = GameObject.FindObjectOfType<SceneCollectibleDatabaseManager>();
            // database = sceneCollectibleDatabaseManager.database;
        }

        // Use this for initialization
        void Start()
        {     
            /*
            if (database.itemsCollectedDictionary.ContainsKey(item.itemID))
            { 
                if (database.itemsCollectedDictionary[item.itemID])
                    Destroy(gameObject); //we've been collected already; destroy ourself
            }
            else
            {
                database.itemsCollectedDictionary.Add(item.itemID, false);
            }
            */
        }

        // Update is called once per frame
        void Update()
        {

        }

        void ItemCollected()
        {
            item.b_collected = true;
            // sceneCollectibleDatabaseManager.AddCollectible(item);
            // globalCollectibleDatabase.SaveGameData();
            saveDatabaseManager.AddCollectible(item);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ItemCollected();
                // Debug.Log(itemsCollectedDictionary.Values.Count(v => v.Equals(true)) + " item(s) collecté(s)");
                this.gameObject.SetActive(false);
                // Destroy(this.gameObject);               
            }
        }
    }
}