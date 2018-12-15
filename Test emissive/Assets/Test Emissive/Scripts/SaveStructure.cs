using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStructure  {
    /// <summary>
    ///  Classe définisant la structure du fichier de sauvegarde JSON
    /// </summary>
    [SerializeField]
    public String[] SceneDataBaseListSerialized = new String[3];
    [SerializeField]
    public int totalCollected;
    [SerializeField]
    public int score;

}
