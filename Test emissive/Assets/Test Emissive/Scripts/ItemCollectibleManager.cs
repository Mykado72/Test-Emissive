using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestEmissive
{
    /// <summary>
    ///  Classe permettant de lancer les fonctions correspondantes aux elevenements de l'ItemCollectible lié
    ///  Fonctions : OnTriggerEnter -> Enregistrement de l'item dans les databases.
    /// </summary>
    public class ItemCollectibleManager : MonoBehaviour
    {
        [SerializeField]
        private ItemCollectible item;
        [SerializeField]
        private SaveDatabaseManager saveDatabaseManager;
        // Use this for initialization
        void Awake()
        {
            item = this.gameObject.GetComponent<ItemCollectible>();
        }

        private void Start()
        {
            if (saveDatabaseManager == null)                  // si le script n'a pas été lié dans l'inspector
                saveDatabaseManager = GameObject.FindObjectOfType<SaveDatabaseManager>();
            if (saveDatabaseManager.CheckIfCollected(item))  // verifie si l'item existe dans la liste des items déjà ramassé
                DesactiveItem();                            // désastive le. 
        }

        void ItemCollected()
        {
            item.b_collected = true;
            saveDatabaseManager.AddCollectible(item);  // ajoute l'item dans les scritables assets (scene et global).
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ItemCollected();
                DesactiveItem();
            }
        }

        private void DesactiveItem()
        {
            this.gameObject.SetActive(false);
            // Destroy(this.gameObject); 
        }
    }
}