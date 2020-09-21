using UnityEngine;

public class ControlTutorial : MonoBehaviour 
{
	public Player Pj;
	public float TiempTuto = 15;
	public float Tempo = 0;
	
	public bool Finalizado = false;
	bool Iniciado = false;
	
	GameManager GM;
	
	//------------------------------------------------------------------//
	void Start () 
	{
		GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
		
		Pj.ContrTuto = this;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	//------------------------------------------------------------------//
	public void Iniciar()
	{
		Pj.GetComponent<Break>().RestaurarVel();
		Iniciado = true;
	}
	
	public void Finalizar()
	{
		Finalizado = true;
		GM.FinTutorial(Pj.IdPlayer);
		Pj.GetComponent<Break>().Frenar();
		Pj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Pj.VaciarInv();
	}
}
