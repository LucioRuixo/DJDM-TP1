using UnityEngine;

public class AutoAcceleration : MonoBehaviour
{
	public float AcelPorSeg = 0;
	float Velocidad = 0;
	public float VelMax = 0;
	CollisionSpeedReducer Obstaculo = null;
	
	bool Avil = true;
	public float TiempRecColl = 0;
	float Tempo = 0;
	
	void Update ()
	{
		/*
		if(Velocidad < VelMax)
		{
			Velocidad += AcelPorSeg * Time.deltaTime;
		}
		*/
		
		//Debug.Log("Velocidad: "+rigidbody.velocity.magnitude);
		
		if(Avil)
		{
            Tempo += UnityEngine.Time.deltaTime;
			if(Tempo > TiempRecColl)
			{
				Tempo = 0;
				Avil = false;
			}
		}
	}
	
	void FixedUpdate () 
	{
		/*
		//this.rigidbody.MovePosition(this.transform.position + this.transform.forward * Velocidad);
		if(rigidbody.velocity.magnitude < VelMax)
			rigidbody.velocity += transform.forward * AcelPorSeg * Time.deltaTime;
			*/
		
		
		/*
		if(Velocidad < VelMax)
		{
			Velocidad += AcelPorSeg * Time.fixedDeltaTime;
		}
		
		rigidbody.MovePosition(this.transform.position + this.transform.forward * Velocidad);
		*/
		
		if(Velocidad < VelMax)
		{
            Velocidad += AcelPorSeg * UnityEngine.Time.fixedDeltaTime;
		}
		
		GetComponent<Rigidbody>().AddForce(this.transform.forward * Velocidad);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(!Avil)
		{
			Obstaculo = collision.transform.GetComponent<CollisionSpeedReducer>();
			if(Obstaculo != null)
			{
				
				//Velocidad -= Obstaculo.ReduccionVel;
				
				//if(Velocidad < 0)
					//Velocidad = 0;
					
				GetComponent<Rigidbody>().velocity /= 2;
			}
			Obstaculo = null;
		}
	}
	
	public void Chocar(CollisionSpeedReducer obst)
	{
		GetComponent<Rigidbody>().velocity /= 2;
	}
}
