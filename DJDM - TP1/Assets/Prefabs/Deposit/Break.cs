using UnityEngine;

public class Break : MonoBehaviour 
{
	public float VelEntrada = 0;
	public string DepositTag = "Deposito";
	
	InputController KInput;
	
	float DagMax = 15f;
	float DagIni = 1f;
	int Contador = 0;
	int CantMensajes = 10;
	float TiempFrenado = 0.5f;
	float Tempo = 0f;
	
	Vector3 Destino;
	
	public bool Frenando = false;
	bool ReduciendoVel = false;
	
	//-----------------------------------------------------//
	void Start () 
	{
		//RestaurarVel();
		Frenar();
	}
	
	void Update () 
	{
	
	}
	
	void FixedUpdate ()
	{
		if(Frenando)
		{
			Tempo += Time.fixedDeltaTime;
			if(Tempo >= (TiempFrenado / CantMensajes) * Contador)
			{
				Contador++;
				//gameObject.SendMessage("SetDragZ", (float) (DagMax / CantMensajes) * Contador);
			}
			if(Tempo >= TiempFrenado)
			{
				//termino de frenar, que haga lo que quiera
			}
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.tag == DepositTag)
		{
			Deposit2 deposit = other.GetComponent<Deposit2>();
			if(deposit.empty)
			{
				if (GetComponent<Player>().ConBolsas())
				{
					deposit.Entrar(GetComponent<Player>());
					Destino = other.transform.position;
					transform.forward = Destino - transform.position;
					Frenar();
				}				
			}
		}
	}
	
	//-----------------------------------------------------------//
	public void Frenar()
	{
		//if (gameObject.name == "Truck 2" && GameOptionsManager.instance.gameMode != GameOptionsManager.GameMode.LocalMultiplayer)
		//	return;

		//Debug.Log(gameObject.name + "frena");
		GetComponent<InputController>().enabled = false;
		gameObject.SendMessage("SetAcceleration", 0f);
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		
		Frenando = true;
		
		//gameObject.SendMessage("SetDragZ", 25f);
		Tempo = 0;
		Contador = 0;
	}
	
	public void RestaurarVel()
	{
		//Debug.Log(gameObject.name + "restaura la velociad");
		GetComponent<InputController>().enabled = true;
		gameObject.SendMessage("SetAcceleration", 1f);
		Frenando = false;
		Tempo = 0;
		Contador = 0;
		//gameObject.SendMessage("SetDragZ", 1f);
	}
}