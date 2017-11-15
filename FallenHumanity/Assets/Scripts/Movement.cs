using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class Movement : MonoBehaviour {
	public float rotateSpeed = 200f;
	public float forwardMoveSpeed = 2f; 
	public float backwardMoveSpeed = 1.5f;
	public float strafeSpeed = 1.7f;
	
	private Transform _myTransform;
	private CharacterController _characterController;
	private Animator _myAnimatior;
	
	
	public void Awake()
	{
		_myTransform = transform;	
		_characterController = GetComponent<CharacterController>();
		_myAnimatior = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Turn();
		Walk();
		Strafe();

	}
	
	private void Turn()
	{
		if(Mathf.Abs(Input.GetAxis("PlayerRotation")) > 0)
		{
			Debug.Log("Rotate");
			_myTransform.Rotate(0, Input.GetAxis("PlayerRotation")* rotateSpeed * Time.deltaTime, 0);
		}
	}
	
	private void Walk()
	{
		if(Mathf.Abs(Input.GetAxis("PlayerForwardMovement")) > 0)
		{
			Debug.Log("Walk");
			_characterController.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("PlayerForwardMovement") * forwardMoveSpeed);
		}
	}
	
	private void Strafe()
	{
		if(Mathf.Abs(Input.GetAxis("PlayerStrafing")) > 0)
		{
			Debug.Log("strafe");
			_characterController.SimpleMove(_myTransform.TransformDirection(Vector3.right) * Input.GetAxis("PlayerStrafing") * strafeSpeed);
		}
	}
}

