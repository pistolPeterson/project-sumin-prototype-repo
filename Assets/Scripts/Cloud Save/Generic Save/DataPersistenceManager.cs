
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : PersistentSingleton<DataPersistenceManager>
{
   private GameData gameData;
   private List<IDataPersist> dataPersistenceObjects;
   private FileDataHandler dataHandler;
   [Header("Debug: Will ignore data persistance")]
   public bool ignoreSave = false; 
   protected override void Awake()
   {
      base.Awake();
      dataHandler = new FileDataHandler();
   }
   
   public void NewGame()
   {
      gameData = new GameData();
   }

   public void LoadGame()
   {
      gameData = dataHandler.Load();
      if (gameData == null)
      {
         Debug.Log("No data was found...");
         return;
      }

      if (ignoreSave)
      {
         Debug.Log("IGNORING SAVE DATA");
         return;
      }

   foreach (IDataPersist dataPersistObj in dataPersistenceObjects )
      {
         dataPersistObj.LoadData(gameData);
      }

      Debug.Log("Loaded Game Data");
   }


   public void SaveGame()
   {

      if (gameData == null)
      {
         Debug.LogWarning("No data was found. A new game must be started before data can be saved");
      }
      foreach (IDataPersist dataPersistObj in dataPersistenceObjects )
      {
         dataPersistObj.SaveData(ref gameData);
      }
      //save that data to a file using data handler
      dataHandler.Save(gameData);
      Debug.Log("Saving Game Data");
   }

   public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
      dataPersistenceObjects = FindAllDataPersistenceObjects();
      LoadGame();
   }

   public void OnSceneUnloaded(Scene scene)
   {
      SaveGame();
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

   private void OnEnable()
   {
      SceneManager.sceneLoaded += OnSceneLoaded;
      SceneManager.sceneUnloaded += OnSceneUnloaded;
   }

   private void OnDisable()
   {
      SceneManager.sceneLoaded -= OnSceneLoaded;
      SceneManager.sceneUnloaded -= OnSceneUnloaded;
   }
}
