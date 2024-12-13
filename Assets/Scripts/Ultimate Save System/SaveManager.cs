using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

        // Check if a file exists in save location
        if (Directory.Exists(SAVES_DIR)){
            var files = Directory.GetFiles(SAVES_DIR);
            Debug.Log(SAVES_DIR);
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

            Debug.Log($"Loaded save from {jsonFiles[0]}. Data:"); 
            Debug.Log(currentSave.ToString());
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
        Debug.Log("Saved to " + SAVES_DIR + saveFileName + FILE_EXT);
    }
}
