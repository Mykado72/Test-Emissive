using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;
using System;

namespace TestEmissive
{
    /// <summary>
    ///  Classe permettant d'initialiser les infos concernant le collectible
    ///  Nom, Valeur ajoutée au score...  
    /// </summary>
    [SerializeField]
    public class ItemCollectible : MonoBehaviour
    {
        [SerializeField]
        public int itemID = -1;        
        private static int IDCounter;
        [SerializeField]
        public bool b_collected = false;
        [SerializeField]
        public String itemName;
        [SerializeField]
        public int itemValue=10;
        [SerializeField]
        public Scene scene;

        void Reset()
        { 
            itemID = IDCounter;     
            IDCounter++; // permet d'incrémenter automatiquement la valeur d'ID en cliquant sur "Reset" dans l'Inspector
        }

        void Awake()
        {
            scene = SceneManager.GetActiveScene();
            itemName = scene.name + "_" + itemID.ToString(); // nomme le collectible sous la forme : nomdulevel_ID
        }
    }
}