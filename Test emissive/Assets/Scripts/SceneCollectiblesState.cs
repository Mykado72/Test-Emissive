using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestEmissive
{
    [CreateAssetMenu(fileName = "SceneCollectivesState", menuName = "Mes Objets/SceneCollectivesState")]
    public class SceneCollectiblesState : ScriptableObject
    {
        [SerializeField]
        public string sceneName;
        [SerializeField]
        public String itemStateSave;
        [SerializeField]
        public Dictionary<int, bool> itemsCollectedDictionary;
        [SerializeField]
        public List<String> allItemsCollectedList;
        [SerializeField]
        public int totalCollectiblesToCollectInScene;
        [SerializeField]
        public int nbCollectedItemInScene;
    }
}