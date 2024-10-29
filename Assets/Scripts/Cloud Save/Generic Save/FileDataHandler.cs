using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileDataHandler
{
   private const string GAME_DATA_KEY = "gyatttt";
   private const string FILE_PATH = "PETE-SECRET-DATA.file";
   public void Save(GameData data)
   {
      var settings = new ES3Settings
      {
         path = FILE_PATH
      };
      ES3.Save(GAME_DATA_KEY, data, settings);
   }


   public GameData Load()
   {
      GameData loadedData = null;
      if (ES3.FileExists())
      {
         loadedData = ES3.Load<GameData>(GAME_DATA_KEY);
      }

      return loadedData;
   }
}
