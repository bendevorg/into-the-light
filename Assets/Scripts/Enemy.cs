using UnityEngine;
using System.Collections;

public class Enemy : LivingEntity {

	Renderer rend;
	public Material black;
	public Material white;

	bool morto = false;

	protected override void Start () {
		base.Start();
		rend = GetComponent<Renderer>();
        // rend.material = material1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			morto = true;
		}
	}
	void FixedUpdate() {
		if (morto)
		{
			rend.material.Lerp(rend.material, white, 0.1f);
		}
	}
}
