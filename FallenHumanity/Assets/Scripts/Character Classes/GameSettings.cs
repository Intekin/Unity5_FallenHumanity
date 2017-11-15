using UnityEngine;
using System.Collections;
using System.IO;
using System; 
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

public class GameSettings : MonoBehaviour
{
	private static string _playerSpawnPoint = "Player Spawn Point";	//This is the Default name of the game object that the player will spawn at, at the start of the level.

	public static string[] levelNames = new string[3] {"MainMenu", "CharacterGeneration", "HexWorld_01"};
	
	void Awake ()
	{
		DontDestroyOnLoad (this);		
	}
	
	public static string PlayerSpawnPoint {
		get{ return _playerSpawnPoint;}
		set{ _playerSpawnPoint = value; }
	}
}

//	public void SaveCharacterData ()
//	{
//		GameObject pc = GameObject.Find ("pc");
//		
//		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter> ();
//		
//		PlayerPrefs.SetString ("PlayerName", pcClass.Name);
//		
//		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
//			PlayerPrefs.SetInt (((AttributeName)i).ToString () + " - Base Value", pcClass.GetPrimaryAttribute (i).BaseValue);
//			PlayerPrefs.SetInt (((AttributeName)i).ToString () + " - EXP To Level", pcClass.GetPrimaryAttribute (i).ExpToLevel);
//		}
//		
//		for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
//			PlayerPrefs.SetInt (((VitalName)i).ToString () + " - Base Value", pcClass.GetVital (i).BaseValue);
//			PlayerPrefs.SetInt (((VitalName)i).ToString () + " - EXP To Level", pcClass.GetVital (i).ExpToLevel);
//			PlayerPrefs.SetInt (((VitalName)i).ToString () + " - Current Value", pcClass.GetVital (i).CurrentValue);
//		}
//		
//		for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
//			PlayerPrefs.SetInt (((SkillName)i).ToString () + " - Base Value", pcClass.GetSkills (i).BaseValue);
//			PlayerPrefs.SetInt (((SkillName)i).ToString () + " - EXP To Level", pcClass.GetSkills (i).ExpToLevel);
//		}
//	}
//	
//	public void LoadCharacterData ()
//	{
//		GameObject pc = GameObject.Find ("pc");
//		
//		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter> ();
//		
//		pcClass.Name = PlayerPrefs.GetString ("PlayerName", "NoName");
//
//		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
//			pcClass.GetPrimaryAttribute (i).BaseValue = PlayerPrefs.GetInt (((AttributeName)i).ToString () + " - Base Value", 0);
//			pcClass.GetPrimaryAttribute (i).ExpToLevel = PlayerPrefs.GetInt (((AttributeName)i).ToString () + " - EXP To Level", 0);
//		}
//		
//		for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
//			pcClass.GetVital (i).BaseValue = PlayerPrefs.GetInt (((VitalName)i).ToString () + " - Base Value", 0);
//			pcClass.GetVital (i).ExpToLevel = PlayerPrefs.GetInt (((VitalName)i).ToString () + " - EXP To Level", 0);
//			
//			//Make sure this is called so that the adjusted value will be calculated before the call to currentValue.
//			pcClass.GetVital (i).Update ();
//			
//			//gets the stored value for the currentValue for each Vital.
//			pcClass.GetVital (i).CurrentValue = PlayerPrefs.GetInt (((VitalName)i).ToString () + " - Current Value", 1);
//		}
//		
//		for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
//			pcClass.GetSkills (i).BaseValue = PlayerPrefs.GetInt (((SkillName)i).ToString () + " - Base Value", 0);
//			pcClass.GetSkills (i).ExpToLevel = PlayerPrefs.GetInt (((SkillName)i).ToString () + " - EXP To Level", 0);
//		}
//	}
//}

/*
// === This is the info container class ===
[Serializable ()]
public class SaveData : ISerializable
{
 
// === Values ===
// Edit these during gameplay
	public string saveName = "";
	public Vector3 savePosition = new Vector3(0,0,0);
	public int[] saveAttributes;
	public int[] saveSkills;
	public int saveExperiance;
// === /Values ===
 
// The default constructor. Included for when we call it during Save() and Load()
	public SaveData ()
	{
	}
 
// This constructor is called automatically by the parent class, ISerializable
// We get to custom-implement the serialization process here
	public SaveData (SerializationInfo info, StreamingContext ctxt)
	{
// Get the values from info and assign them to the appropriate properties. Make sure to cast each variable.
// Do this for each var defined in the Values section above
		foundGem1 = (bool)info.GetValue ("foundGem1", typeof(bool));
		score = (float)info.GetValue ("score", typeof(float));
 
		levelReached = (int)info.GetValue ("levelReached", typeof(int));
		
		saveName = (string)info.GetValue("PlayerName", "NoName");
		
	}
 
// Required by the ISerializable class to be properly serialized. This is called automatically
	public void GetObjectData (SerializationInfo info, StreamingContext ctxt)
	{
// Repeat this for each var defined in the Values section
		info.AddValue ("foundGem1", (foundGem1));
		info.AddValue ("score", score);
		info.AddValue ("levelReached", levelReached);
	}
}
 
// === This is the class that will be accessed from scripts ===
public class SaveLoad
{
 
	public static string currentFilePath = "SaveData.cjc"; // Edit this for different save files
 
// Call this to write data
	public static void Save () // Overloaded
	{
		Save (currentFilePath);
	}

	public static void Save (string filePath)
	{
		SaveData data = new SaveData ();
 
		Stream stream = File.Open (filePath, FileMode.Create);
		BinaryFormatter bformatter = new BinaryFormatter ();
		bformatter.Binder = new VersionDeserializationBinder ();
		bformatter.Serialize (stream, data);
		stream.Close ();
	}
 
// Call this to load from a file into "data"
	public static void Load ()
	{
		Load (currentFilePath);
	} // Overloaded
	public static void Load (string filePath)
	{
		SaveData data = new SaveData ();
		Stream stream = File.Open (filePath, FileMode.Open);
		BinaryFormatter bformatter = new BinaryFormatter ();
		bformatter.Binder = new VersionDeserializationBinder ();
		data = (SaveData)bformatter.Deserialize (stream);
		stream.Close ();
 
// Now use "data" to access your Values
	}
 
}
 
// === This is required to guarantee a fixed serialization assembly name, which Unity likes to randomize on each compile
// Do not change this
public sealed class VersionDeserializationBinder : SerializationBinder
{
	public override Type BindToType (string assemblyName, string typeName)
	{
		if (!string.IsNullOrEmpty (assemblyName) && !string.IsNullOrEmpty (typeName)) {
			Type typeToDeserialize = null;
 
			assemblyName = Assembly.GetExecutingAssembly ().FullName;
 
// The following line of code returns the type.
			typeToDeserialize = Type.GetType (String.Format ("{0}, {1}", typeName, assemblyName));
 
			return typeToDeserialize;
		}
 
		return null;
	}
}*/
