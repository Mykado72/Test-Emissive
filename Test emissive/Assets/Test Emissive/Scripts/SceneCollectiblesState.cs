using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestEmissive
{
    /// <summary>
    ///  Permet de créer le scritableobject /Oject asset depuis le menu Assets\Create\Mes Objets\SceneCollectivesState
    ///  sorte de Database dont la fonction est de stocker toutes les informations concernant une scene/Level particulier(e)
    /// </summary>
    [CreateAssetMenu(fileName = "SceneCollectivesState", menuName = "Mes Objets/SceneCollectivesState")]
    public class SceneCollectiblesState : ScriptableObject
    {
        [SerializeField]
        public string sceneName;
        [SerializeField]
        public Dictionary<int, bool> itemsCollectedDictionary;
        [SerializeField]
        public List<String> itemsCollectedList;
        [SerializeField]
        public int totalCollectiblesToCollectInScene;
        [SerializeField]
        public int nbCollectedItemInScene;
    }
}