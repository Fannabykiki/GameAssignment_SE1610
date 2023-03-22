using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistencemanager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private FileDataHandler dataHandler;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceList;
    public static DataPersistencemanager instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more thand one Data Manager in the scene");
        }
       
    }
    public void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        instance = this;
        this.dataPersistenceList = FindAllDataPersistenceList();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceList()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No data to load");
            NewGame();
        }
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.LoadData(gameData);
        }
       
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistence in dataPersistenceList)
        {
            dataPersistence.SaveData(gameData);
        }
       
        dataHandler.Save(gameData);
        Debug.Log("Save game successfully");
    }
}
