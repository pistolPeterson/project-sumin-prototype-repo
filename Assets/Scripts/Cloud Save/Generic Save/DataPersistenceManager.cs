
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : PersistentSingleton<DataPersistenceManager>
{
   private GameData gameData;
   private List<IDataPersist> dataPersistenceObjects;
   private FileDataHandler dataHandler;
   [Header("File Storage Config")] 
   [SerializeField] private String fileName = "pete.file";
   
   protected override void Awake()
   {
      base.Awake();
   }

   private void Start()
   {
      dataHandler = new FileDataHandler();
      dataPersistenceObjects = FindAllDataPersistenceObjects();
      LoadGame();
   }

   private List<IDataPersist> FindAllDataPersistenceObjects()
   {
      IEnumerable<IDataPersist> dpo = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersist>();
      return new List<IDataPersist>(dpo);
   }

   private void OnApplicationQuit()
   {
      SaveGame();
   }

   public void NewGame()
   {
      gameData = new GameData();
   }

   public void LoadGame()
   {
      //TODO: load any saved data from file 
      //if no data can be loaded, init to a new game
      gameData = dataHandler.Load();
      if (gameData == null)
      {
         Debug.Log("No data was found, initializing default");
         NewGame();
      }

      foreach (IDataPersist dataPersistObj in dataPersistenceObjects )
      {
         dataPersistObj.LoadData(gameData);
      }

      Debug.Log("Loaded Current Health: " + gameData.currentHealth);
   }


   public void SaveGame()
   {
      //TODO - pass the data to other scripts 
      foreach (IDataPersist dataPersistObj in dataPersistenceObjects )
      {
         dataPersistObj.SaveData(ref gameData);
      }
      //save that data to a file using data handler
      dataHandler.Save(gameData);
      Debug.Log("Saved Current Health: " + gameData.currentHealth);
   }
   
}
