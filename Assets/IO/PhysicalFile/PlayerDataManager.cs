using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour
{
    enum SaveSystem
    {
        JSONSerializer,
        BinaryFormatter,
        BinaryWriterReader
    }

    static readonly string[] Names = new string[] { "Isaac", "Peter", "Clark", "Aaron", "John", "Jack", "Simon", "Dez", "Tim", "Nick", "Evan", "Josh", "Jordan" };

    PlayerData playerData = new PlayerData();

    [SerializeField] Text nameField;

    [SerializeField] SaveSystem saveSystem = SaveSystem.BinaryFormatter;
    IPlayerDataProcessor saveFileSystem = null;


    private void Start()
    {
        playerData.Name = Names[Random.Range(0, Names.Length)];
        nameField.text = playerData.Name;

        switch (saveSystem)
        {
            case SaveSystem.BinaryFormatter:
                saveFileSystem = new PlayerDataProcessorBinaryFormatter();
                break;
            case SaveSystem.JSONSerializer:
                saveFileSystem = new PlayerDataProcessorJSONSerializer();
                break;
            case SaveSystem.BinaryWriterReader:
                saveFileSystem = new PlayerDataProcessorBinaryWriter();
                break;
        }
    }

    public void Save()
    {
        playerData.Position = transform.position;
        playerData.Rotation = transform.eulerAngles;

        saveFileSystem.Save(playerData);
    }

    public void Load()
    {
        PlayerData loadedData = saveFileSystem.Load();

        if (loadedData != null)
        {
            playerData = loadedData;

            transform.position = playerData.Position;
            transform.rotation = Quaternion.Euler(playerData.Rotation);
            nameField.text = playerData.Name;
        }

    }
}
