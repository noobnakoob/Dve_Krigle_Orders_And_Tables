using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour {

	// Use this for initialization
	public static void SaveToFile () {

        Stream stream = File.Open(Application.persistentDataPath + "/stored.data", FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, DataClass.current);
        stream.Close();
	}
}