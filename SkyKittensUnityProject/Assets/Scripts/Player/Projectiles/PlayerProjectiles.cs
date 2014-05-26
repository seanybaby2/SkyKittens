using UnityEngine;
using System.Collections;

public class PlayerProjectiles : MonoBehaviour {
	
	public float lifeSpan = 3.0f;
	private float tempLifeSpan;

	void Start(){


	}
	void Update () {
		
		tempLifeSpan += Time.deltaTime;
		
		if(tempLifeSpan >= lifeSpan)
			Destroy(this.gameObject);

		
	}
	
	void OnCollisionEnter (Collision col)
	{
		Destroy(this.gameObject);
	}

}