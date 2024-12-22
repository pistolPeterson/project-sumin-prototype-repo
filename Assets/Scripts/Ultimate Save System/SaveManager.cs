using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using com.cyborgAssets.inspectorButtonPro;
using Unity.Services.CloudSave;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public SaveFile CurrentSave => currentSave;
    [SerializeField] private SaveFile currentSave; 
    private const string saveFileName = "SuperDuperSecretDataForPete";

    private static string SAVES_DIR => Application.dataPath + "/Saves/";
    private const string FILE_EXT = ".ult";

    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        Instance = this;

        //TryLocalLoadData();
    }

    private void TryLocalLoadData()
    {
        // Check if a file exists in save location
        if (Directory.Exists(SAVES_DIR)){
            var files = Directory.GetFiles(SAVES_DIR);
        
            var jsonFiles = new List<string>();

            // Filter for the actual save files in case other files exist there as well for whatever reason.
            foreach (var f in files){
                if (f.EndsWith(FILE_EXT) && f.Contains(saveFileName)){
                    jsonFiles.Add(f);
                }
            }

            // For now always load the first save file, but multiple save files can co-exist for future upgrade
            // to allow the player to select save file instead.
            // TODO: Add functionality to allow the player to select which save file they want to load instead of doing it manually.
            if (jsonFiles.Count == 0) {
                Debug.Log("No save files found, creating new!");
                currentSave = new SaveFile();
                return;
            }

            // Load from file.
            var bf = new BinaryFormatter();
            var fs = new FileStream(jsonFiles[0], FileMode.Open);

            currentSave = JsonUtility.FromJson<SaveFile>((string)bf.Deserialize(fs));

            fs.Close();

            //   Debug.Log($"Loaded save from {jsonFiles[0]}. Data:"); 
          
        }
    }

    public void CreateNewSave()
    {
        currentSave = new SaveFile();
    }

    public bool HasSave(){
        if (!Directory.Exists(SAVES_DIR)) return false;

        var files = Directory.GetFiles(SAVES_DIR);

        if (files.Length == 0) return false;

        // Filter for the actual save files in case other files exist there as well for whatever reason.
        return files.Any(f => f.EndsWith(FILE_EXT) && f.Contains(saveFileName));
    }

    public void UpdateMapNodeEnums(List<NodeEnum> nodeEnums){
        currentSave.mapNodeEnums = nodeEnums;
        currentSave.nodeCount = nodeEnums.Count;
    }

    public void UpdateCurrentNodeId(int id){
        currentSave.currentNodeId = id;
    }

    
    
  

    public void SaveCurrent(){
        if (!Directory.Exists(SAVES_DIR)){
            Directory.CreateDirectory(SAVES_DIR);
        }

        var jsonSaveText = JsonUtility.ToJson(currentSave);
        var bf = new BinaryFormatter();
        var fs = new FileStream(SAVES_DIR + saveFileName + FILE_EXT, FileMode.OpenOrCreate);
        bf.Serialize(fs, jsonSaveText);
        fs.Close();
       // Debug.Log("Saved to " + SAVES_DIR + saveFileName + FILE_EXT);
    }
    
    
    [ProButton]
    public async void SaveAllDataOnline()
    {
       // var data = new Dictionary<string, object>{{"keyName", "value"}};
       var data = currentSave.ConvertDataToDictionary();
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }

    /*public async void LoadAllDataOnline()
    {
        currentSave.health = await TryLoadHealth();
        currentSave.mapNodeEnums = await TryLoadMapNodeEnums();
        currentSave.nodeCount = await TryLoadNodeCount();
        currentSave.currentNodeId = await TryLoadCurrentNode();
        currentSave.playerCards = await TryLoadPlayerCards();

    }*/
    [ProButton]
    public async void LoadAllDataOnline()
    {
        currentSave.health = await TryLoadData<int>(SaveFile.HEALTH_KEY, 0);
        currentSave.mapNodeEnums = await TryLoadData(SaveFile.MAP_NODE_ENUMS_KEY, new List<NodeEnum>());
        currentSave.nodeCount = await TryLoadData<int>(SaveFile.NODE_COUNT_KEY, 0);
        currentSave.currentNodeId = await TryLoadData<int>(SaveFile.CURRENT_NODE_ID_KEY, 0);
        currentSave.playerCards = await TryLoadData(SaveFile.PLAYER_CARDS_KEY, new List<CardDataBaseSO>());
    }

    private async Task<T> TryLoadData<T>(string key, T defaultValue)
    {
        try
        {
            return await RetrieveSpecificData<T>(key);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve data for key '{key}': {ex}");
            return defaultValue;
        }
    }
  

    /*
    public async Task<int> TryLoadHealth()
    {
        try
        {
            return await RetrieveSpecificData<int>(SaveFile.HEALTH_KEY);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve health: {ex}"); 
            return 0; // Or another appropriate default value
        }
    }
    
    public async Task<List<NodeEnum>> TryLoadMapNodeEnums()
    {
        try
        {
            return await RetrieveSpecificData<List<NodeEnum>>(SaveFile.MAP_NODE_ENUMS_KEY);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve map node enums: {ex}"); 
            return new List<NodeEnum>(); 
        }
    }
    
    public async Task<int> TryLoadNodeCount()
    {
        try
        {
            return await RetrieveSpecificData<int>(SaveFile.NODE_COUNT_KEY);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve Node count: {ex}"); 
            return 0; 
        }
    }
    
    public async Task<int> TryLoadCurrentNode()
    {
        try
        {
            return await RetrieveSpecificData<int>(SaveFile.CURRENT_NODE_ID_KEY);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve current: {ex}"); 
            return 0; 
        }
    }
    
    public async Task<List<CardDataBaseSO>> TryLoadPlayerCards()
    {
        try
        {
            return await RetrieveSpecificData<List<CardDataBaseSO>>(SaveFile.PLAYER_CARDS_KEY);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve player cards: {ex}"); 
            return new List<CardDataBaseSO>(); 
        }
    }*/
    
    private async Task<T> RetrieveSpecificData<T>(string key)
    {
        try
        {
            var results = await CloudSaveService.Instance.Data.Player.LoadAsync(
                new HashSet<string> { key }
            );

            if (results.TryGetValue(key, out var item))
            {
                return item.Value.GetAs<T>();
            }
            else
            {
                Debug.Log($"There is no such key as {key}!");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }
    
    
    
}
