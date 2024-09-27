using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]// this is a header attribute, which can add a header above some fields in the Inspector.
    [SerializeField] private string fileName;

    private GameData gameData;// this is a pointer initialized as pointing on null
    private List<IDataPersistence> dataPersistenceObjects;// collect all needed to be saved data
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; } // public static make it not shown in inspector, the function of {} make it only editable in this script

    private void Awake()
    {
        // when will Awake() called
        if (instance != null)
        {
            Debug.LogError("Found more than 1 Data Persistence Manager");// Why is this the only reason?
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName); // https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
        this.dataPersistenceObjects = findAllDataPersistenceObjects();// shouldn't we use this-> instead of this. ?
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        SaveGame();
    }

    public void LoadGame()
    {
        // TODO - load data saved from a file through the data handler
        this.gameData = dataHandler.Load();

        // if no data can be loaded, initialize a new game
        if (this.gameData == null)
        {
            Debug.Log("No Data was found");
            NewGame();
        }

        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }
        // Debug.Log("loaded current health -- " + gameData.currentHealth);

        // TODO - push all loaded data to all other scripts that need it
    }

    public void SaveGame()
    {
        // TODO - push the data to other scripts so they can update it
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.SaveData(ref gameData);
        }
        // Debug.Log("saved current health -- " + gameData.currentHealth);

        // TODO - save the data to the file using the data handler
        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> findAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();//I guess this traverse all the mono behaviour script classes to find those with IDataPersistence
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
