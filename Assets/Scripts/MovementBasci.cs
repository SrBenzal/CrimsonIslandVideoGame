using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBasci : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 70.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 15.0f;
//		var y = Input.GetKey (KeyCode.Space) * Time.deltaTime * 10.0f;
		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}
}
