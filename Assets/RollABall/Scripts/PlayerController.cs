using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
	public Text countText;
	public Text winText;
	public float speed ;
	public int NumberToWin;

	private int count ;
	private Rigidbody BallRigidBody;
	public Renderer BallRenderer;
	public Material BallMaterial ;
	public Collider BallCollider;
	public Vector3 jumpForce ;
	//private Material material;

	void Start()
	{
		BallRigidBody = GetComponent<Rigidbody>();
		BallMaterial = Resources.Load<Material>("Materials/MaterialRUBBER");
		BallRenderer = this.GetComponent<Renderer>();
		BallCollider.material = Resources.Load<PhysicMaterial>("PhysicMaterials/Rubber");

		count = 0;
		NumberToWin = 12;
		SetCountText();
		winText.text = "";

		jumpForce = new Vector3 (0, 5, 0);
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		BallRigidBody.AddForce (movement * speed);

		// unnatural stop of the playerBall object
		if (Input.GetKeyDown ("c")) { BallRigidBody.Sleep(); }

		// natural effort of the game object to accelerate itself
		if (Input.GetKeyDown ("q")) { speed += 1; }

		// natural effort of the game object to stop itself
		if (Input.GetKeyDown ("e")) { speed -= 1; }

		// natural effort of the game object to stop itself
		if (Input.GetKeyDown ("z")) { BallRigidBody.velocity = new Vector3 (0,BallRigidBody.velocity.y,0) ;  }

		// natural effort of the game object to jump
		if (Input.GetKeyDown (KeyCode.Space)) { BallRigidBody.AddForce(jumpForce, ForceMode.Impulse) ; }
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("Pickup")) 
		{
			other.gameObject.SetActive (false);
			count += 1;
			SetCountText();
			BallRenderer.material = other.GetComponent<Renderer>().material;
			BallCollider.material = other.material;
			BallRigidBody.mass = other.GetComponent<Rigidbody>().mass;
		}
		/*if (other.gameObject.name.Equals ("Pickup BLACK")) 
		{
			BallMaterial = Resources.Load<Material>("Materials/MaterialBLACK");
			BallRenderer.material = BallMaterial;
		}
		if (other.gameObject.name.Equals ("Pickup GOLDEN")) 
		{
			
			BallRenderer.material = other.GetComponent<Renderer>().material;
			//BallRenderer.material = other.renderer.
			//BallCollider.material = Resources.Load<PhysicMaterial> ("PhysicMaterials/Metal");
			BallCollider.material = other.material;
		}*/
	}

	void SetCountText()
	{
		countText.text = "Count :" + count.ToString();
		if (count >= NumberToWin) { winText.text = "You Win !"; } 
	}

}