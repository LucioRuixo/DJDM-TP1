using UnityEngine;

public class Deposit2 : MonoBehaviour 
{
	Player PjActual;
	public string PlayerTag = "Player";
	public bool empty = true;
	public UnloadController Contr1;
	public UnloadController Contr2;
	
	Collider[] PjColl;
	
	//----------------------------------------------//
	void Start () 
	{
		Contr1 = GameObject.Find("ContrDesc1").GetComponent<UnloadController>();
		Contr2 = GameObject.Find("ContrDesc2").GetComponent<UnloadController>();
		
		Physics.IgnoreLayerCollision(8,9,false);
	}
	
	void Update () 
	{
		if(!empty)
		{
			PjActual.transform.position = transform.position;
			PjActual.transform.forward = transform.forward;
		}
	}
	
	//----------------------------------------------//
	public void Soltar()
	{
		PjActual.VaciarInv();
		PjActual.GetComponent<Break>().RestaurarVel();
		PjActual.GetComponent<Respawn>().Respawnear(transform.position,transform.forward);
		
		PjActual.GetComponent<Rigidbody>().useGravity = true;
		for(int i = 0; i < PjColl.Length; i++)
			PjColl[i].enabled = true;
		
		Physics.IgnoreLayerCollision(8,9,false);
		
		PjActual = null;
		empty = true;
	}
	
	public void Entrar(Player pj)
	{
		if(pj.ConBolsas())
		{
			PjActual = pj;
			
			PjColl = PjActual.GetComponentsInChildren<Collider>();
			for(int i = 0; i < PjColl.Length; i++)
				PjColl[i].enabled = false;
			PjActual.GetComponent<Rigidbody>().useGravity = false;
			
			PjActual.transform.position = transform.position;
			PjActual.transform.forward = transform.forward;
			
			empty = false;
			
			Physics.IgnoreLayerCollision(8,9,true);
			
			Entro();
		}
	}
	
	public void Entro()
	{		
		if(PjActual.IdPlayer == 0)
			Contr1.Activar(this);
		else
			Contr2.Activar(this);
	}
}