using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadData : MonoBehaviour {
   
	/// <summary>
    /// Load data from file
    /// </summary>
	public static void LoadFromFile () {
		
        if (File.Exists(Application.persistentDataPath + "/stored.data"))
        {
            Stream stream = File.Open(Application.persistentDataPath + "/stored.data", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            DataClass.current = (DataClass)binaryFormatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Stream stream = File.Open(Application.persistentDataPath + "/stored.data", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            DataClass.current = new DataClass();
            binaryFormatter.Serialize(stream, DataClass.current);
            stream.Close();
        }
	}
}