/// <summary>
/// Targeting.cs
/// Christoffer Gustafsson
/// 07/08-2013
/// 
/// Used for targeting mobs.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targeting : MonoBehaviour {
	
	public List<Transform> targets;
	public Transform selectedTarget;
	
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		myTransform = transform;
		
		AddAllEnemies();
	
	}
	
	public void AddAllEnemies()
	{
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");	
		
		foreach(GameObject enemy in go)
			AddTarget(enemy.transform);
	}
	
	public void AddTarget(Transform enemy)
	{
		targets.Add(enemy);
	}
	
	private void SortTargetsByDistance()
	{
		targets.Sort(delegate(Transform t1, Transform t2) {
			return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
			});
	}
	
	private void TargetEnemy()
	{
		if(targets.Count == 0)
			AddAllEnemies();
		
		if(targets.Count > 0)
		{
			if(selectedTarget == null)
			{
				SortTargetsByDistance();
				selectedTarget = targets[0];	
			}
			else
			{
				int index = targets.IndexOf(selectedTarget);
				
				if(index < targets.Count - 1)
				{
					index++;		
				}
				else 
				{
					index = 0;	
				}
				DeselectTarget();
				selectedTarget = targets[index];
			}
			
			SelectedTarget();
		}
	}
	
	private void SelectedTarget()
	{
		Transform name = selectedTarget.FindChild("Name");
		
		if(name == null)
		{
			Debug.LogError("No name on " + selectedTarget.name);
			return;
		}
		
		name.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().Name;
		name.GetComponent<MeshRenderer>().enabled = true;
		
		selectedTarget.GetComponent<Mob>().DisplayHealth();
		
		Messenger<bool>.Broadcast("show mob vitalbars", true);
		
	}
	
	private void DeselectTarget()
	{
		selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;
		
		selectedTarget = null;
		Messenger<bool>.Broadcast("show mob vitalbars", false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			TargetEnemy();	
		}
	
	}
}
