using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileDataHandler
{
   private const string GAME_DATA_KEY = "gyatttt";
   private const string FILE_PATH = "PETE-SECRET-DATA.file";
   public void Save(GameData data)
   {
     
      ES3.Save(GAME_DATA_KEY, data, GetCustomSettings());
   }


   public GameData Load()
   {
      GameData loadedData = null;
      if (ES3.FileExists(FILE_PATH))
      {
         loadedData = ES3.Load<GameData>(GAME_DATA_KEY, FILE_PATH);
      }

      if (loadedData == null)
         Debug.Log("GameData is Null when attempting to laod from fileDtatHandler");
      return loadedData;
   }


   private ES3Settings GetCustomSettings()
   {
      var settings = new ES3Settings
      {
         path = FILE_PATH
      };
      return settings;
   }
}
