using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System;

namespace TestEmissive
{
    [SerializeField]
    public class ItemCollectible : CollectiblesObjects
    {
        [SerializeField]
        public int itemID = -1;        
        private static int IDCounter = 0;
        // public static Dictionary<int, bool> itemsCollectedDictionary;
        [SerializeField]
        public bool b_collected = false;
        //[SerializeField]
        // private SceneCollectiblesDatabase sceneCollectiblesDatabase;
        // [SerializeField]
        // private GlobalCollectibleDatabase globalCollectibleDatabase;
        [SerializeField]
        public String itemName;
        [SerializeField]
        public int itemValue=10;

        void Reset()
        { 
            itemID = IDCounter;
            IDCounter++;
        }

        void Awake()
        {
            scene = SceneManager.GetActiveScene();
            itemName = scene.name + "_" + itemID.ToString();
        }

    }
}