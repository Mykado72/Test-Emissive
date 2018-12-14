using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestEmissive
{
[CreateAssetMenu(fileName = "SaveCollectivesDatabases", menuName = "Mes Objets/SaveCollectiblesDatabase")]
    public class CollectivesDatabasesScriptableObject : ScriptableObject
    {
        [SerializeField]
        public SceneCollectiblesState[] SceneDataBaseList;
        [SerializeField]
        public int totalCollected;
        [SerializeField]
        public int score;
    }
}
