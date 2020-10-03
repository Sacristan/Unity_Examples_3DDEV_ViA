using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    [SerializeField] string name;
    [SerializeField] float posX, posY, posZ;
    [SerializeField] float rotX, rotY, rotZ;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public Vector3 Position
    {
        get => new Vector3(posX, posY, posZ);
        set
        {
            posX = value.x;
            posY = value.y;
            posZ = value.z;

        }
    }

    public Vector3 Rotation
    {
        get => new Vector3(rotX, rotY, rotZ);
        set
        {
            rotX = value.x;
            rotY = value.y;
            rotZ = value.z;
        }
    }
}


public interface IPlayerDataProcessor
{
    void Save(PlayerData data);
    PlayerData Load();
}

public class PlayerDataProcessorBinaryFormatter : IPlayerDataProcessor
{
    public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "savefile_binaryformatter.dat");

    public void Save(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(SavePath);
        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved file @ " + SavePath);
    }

    public PlayerData Load()
    {
        PlayerData data = null;

        if (File.Exists(SavePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SavePath, FileMode.Open);
            data = (PlayerData)bf.Deserialize(file);
            file.Close();
        }

        return data;
    }
}

public class PlayerDataProcessorBinaryWriter : IPlayerDataProcessor
{
    public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "savefile_binarywriter.dat");

    public void Save(PlayerData data)
    {
        FileStream file = File.Create(SavePath);
        BinaryWriter writer = new BinaryWriter(file, System.Text.Encoding.ASCII);

        writer.Write(data.Name);
        writer.Write(data.Position.x);
        writer.Write(data.Position.y);
        writer.Write(data.Position.z);

        writer.Close();
        file.Close();
        Debug.Log("Saved file @ " + SavePath);

    }

    public PlayerData Load()
    {

        PlayerData data = null;

        if (File.Exists(SavePath))
        {
            FileStream file = File.Create(SavePath);
            BinaryReader reader = new BinaryReader(file, System.Text.Encoding.ASCII);

            data = new PlayerData();
            data.Name = reader.ReadString();
            data.Position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            data.Rotation = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

            reader.Close();
            file.Close();
        }

        return data;
    }
}

public class PlayerDataProcessorJSONSerializer : IPlayerDataProcessor
{
    public static readonly string SavePath = Path.Combine(Application.persistentDataPath, "savefile_json.json");

    public void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SavePath, json);

        Debug.Log("Saved file @ " + SavePath);
    }

    public PlayerData Load()
    {
        PlayerData data = null;

        if (File.Exists(SavePath))
        {
            string json = System.IO.File.ReadAllText(SavePath);
            data = JsonUtility.FromJson<PlayerData>(json);
        }

        return data;
    }
}