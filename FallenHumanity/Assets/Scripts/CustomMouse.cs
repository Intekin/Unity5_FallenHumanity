using UnityEngine;
using System.Collections;

public class CustomMouse : MonoBehaviour {
	
	public Texture2D crosshair;
	public float raycastDistance = 1.5f;
	public RaycastHit hit;
    public static Collider collider1 = new Collider();
    private Ray ray;
    private Vector3 vec;
    private LayerMask layerMask;

    void Update () {
      
        // Find the centre of the Screen
        vec.x = (float)Screen.width / 2;
        vec.y = (float)Screen.height / 2;
        vec.z = 0;
      
        // Create the actual Ray based on the screen vector above

		ray = GetComponent<Camera>().ScreenPointToRay(vec);

        // This returns true if an object is hit by the ray
        if (Physics.Raycast(ray, out hit, raycastDistance)) //remove layerMask if you remove it in line above
        {
            //stores the object hit
            collider1 = hit.collider;
			if(collider1.CompareTag("Container"))
			{
				//Debug.Log("ray collides with the container: " + collider1.name);
				Container con = hit.collider.gameObject.GetComponent<Container>();
				con.ContainerEnter();
				if(Input.GetButtonUp("Activate"))// && con.state == Container.State.Close)
				{
					con.Activate();	
				}
				
			}
			else if(collider1.CompareTag("Item"))
			{
				GameObject go = hit.collider.gameObject;
				Debug.Log("looking at" + go.name);
				if(Input.GetButtonUp("Activate"))
				{
					//PlayerCharacter.Inventory.Add(go);
					Destroy(go);
				}
			}
			else if(collider1.CompareTag("Door"))
			{
				Debug.Log("ray collides with the door");
				TransportSystem trans = hit.collider.gameObject.GetComponent<TransportSystem>();
				trans.DoorEnter();
				if(Input.GetButtonUp("Activate"))
				{
					trans.AcivateTransfer();
				}
			}

        }

		if(Input.GetButtonUp("CharacterScreen"))
		{
			Debug.Log("CharacterScreen");
			Messenger.Broadcast("CharacterScreen");	
		}
		
		if(Input.GetButtonUp("Menu"))
		{
			Debug.Log("Display Menu");
			Messenger.Broadcast("DisplayMenu");
		}
    }



	void OnGUI()
	{
		GUI.DrawTexture(new Rect((Screen.width - crosshair.width)/2, (Screen.height - crosshair.height)/2, crosshair.width, crosshair.height), crosshair);	
	}


    
}
