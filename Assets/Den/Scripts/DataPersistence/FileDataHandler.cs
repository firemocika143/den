using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string DataDirPath = "";
    private string DataFileName = "";
    private string FullPath = "";

    public FileDataHandler(string dirPath, string fileName)
    {
        this.DataDirPath = dirPath;
        this.DataFileName = fileName;
        this.FullPath = Path.Combine(DataDirPath, DataFileName);// different from the tutorial, but why not?
    }

    public GameData Load()
    {
        GameData loadedData = null; // why should we set it to null here but never doing so in other places?
        if (File.Exists(FullPath))
        {
            try
            {
                // load the serialized data from the file
                string dataToLoad = "";

                using (FileStream stream = new FileStream(FullPath, FileMode.Open)) // use using method to help us close the connection to the file every time we read or write into a file
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // deserializing
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data" + FullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        try
        {
            // Create the file if it's not already exist
            Directory.CreateDirectory(Path.GetDirectoryName(FullPath));

            // serialize the .cs game data object into .json
            string dataToStore = JsonUtility.ToJson(data, true);

            // write the serialized data to the file
            using (FileStream stream = new FileStream(FullPath, FileMode.Create)) // use using method to help us close the connection to the file every time we read or write into a file
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file" + FullPath + "\n" + e);
        }
    }
}
