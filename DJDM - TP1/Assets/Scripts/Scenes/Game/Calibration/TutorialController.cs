using UnityEngine;

public class TutorialController : MonoBehaviour
{
	public Player Pj;

	public float TiempEspCalib = 3;
	float Tempo2 = 0;
	
	public enum Estados{Tutorial, Finalizado}
	public Estados EstAct = Estados.Tutorial;
	
	public ManejoPallets Partida;
	public ManejoPallets Llegada;
	public Pallet P;
    public ManejoPallets palletsMover;
	
	GameManager GM;
	
	//----------------------------------------------------//
	void Start () 
	{
        palletsMover.enabled = false;
        Pj.ContrCalib = this;
		
		GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
		
		P.CintaReceptora = Llegada.gameObject;
		Partida.Recibir(P);
		
		SetActivComp(false);
	}
	
	void Update ()
	{
		if(EstAct == Estados.Tutorial)
		{
			if(Tempo2 < TiempEspCalib)
			{
				Tempo2 += Time.deltaTime;
				if(Tempo2 > TiempEspCalib)
				{
					 SetActivComp(true);
				}
			}
		}
	}
	
	public void IniciarTesteo()
	{
		EstAct = Estados.Tutorial;
        palletsMover.enabled = true;
    }
	
	public void FinTutorial()
	{
		EstAct = Estados.Finalizado;
        palletsMover.enabled = false;
        GM.FinTutorial(Pj.IdPlayer);
	}
	
	void SetActivComp(bool estado)
	{
		if(Partida.GetComponent<Renderer>() != null)
			Partida.GetComponent<Renderer>().enabled = estado;
		Partida.GetComponent<Collider>().enabled = estado;
		if(Llegada.GetComponent<Renderer>() != null)
			Llegada.GetComponent<Renderer>().enabled = estado;
		Llegada.GetComponent<Collider>().enabled = estado;
		P.GetComponent<Renderer>().enabled = estado;
	}
}