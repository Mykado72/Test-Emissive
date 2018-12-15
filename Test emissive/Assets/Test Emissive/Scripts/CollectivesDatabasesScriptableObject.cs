using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestEmissive
{
    /// <summary>
    ///  Permet de créer le scritableobject /Oject asset depuis le menu Assets\Create\Mes Objets\SaveCollectiblesDatabase
    ///  sorte de Database dont la fonction est de stocker toutes les informations en temps réel
    /// </summary>
    [CreateAssetMenu(fileName = "SaveCollectivesDatabases", menuName = "Mes Objets/SaveCollectiblesDatabase")]
    public class CollectivesDatabasesScriptableObject : ScriptableObject
    {        
        public SceneCollectiblesState[] SceneDataBaseList;
        [SerializeField]
        public String[] SceneDataBaseListSerialized;
        [SerializeField]
        public int totalCollected;
        [SerializeField]
        public int score;
    }
}
