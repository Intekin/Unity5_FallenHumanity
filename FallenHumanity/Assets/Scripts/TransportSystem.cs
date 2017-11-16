using UnityEngine;
using UnityEngine.SceneManagement;

public class TransportSystem : GameSettings
{

		/// The game object to affect. If none, the trigger work on this game object
		public string DoorName;
		public string levelToLoad;
		public string spawnPoint;
		private int _timer = 0;
		private bool _isActive = false;

		void Update ()
		{
				if (_timer > 0) { //Timer to disable the highlight.
						_timer--;
						if (_timer == 0) {
								_isActive = false;
								Debug.Log (PlayerSpawnPoint);
						}
				}
		}

		public void DoorEnter ()
		{
				_timer = 3;
				_isActive = true;
				Debug.Log ("Enter");
		}

		public void AcivateTransfer ()
		{
				PlayerSpawnPoint = spawnPoint;		//Sets the name of the spawnpoint to the PlayerSpawnPoint
				Debug.Log ("Activate");
                SceneManager.LoadScene(levelToLoad); 	//Loades Level
		}

		void OnGUI ()
		{
				if (_isActive) {
						Debug.Log ("GUI");
						GUI.Label (new Rect ((Screen.width - 100) / 2, (Screen.height - 60) / 2, 100, 30), DoorName);	
				}
		}
}