using UnityEngine;

public class Player : MonoBehaviour 
{
	public int Dinero = 0;
	public int IdPlayer = 0;
	
	public Bag[] Bags;
	public int CantBolsAct = 0;
	public string TagBolsas = "";
	
	public enum Estados{EnDescarga, EnConduccion, EnCalibracion, EnTutorial}
	public Estados EstAct = Estados.EnConduccion;
	
	public bool EnConduccion = true;
	public bool EnDescarga = false;
	
	public UnloadController ContrDesc;
	public TutorialController ContrCalib;
	public ControlTutorial ContrTuto;
	
	public Visualization MiVisualizacion;
	
	//------------------------------------------------------------------//
	void Awake () 
	{
		for(int i = 0; i< Bags.Length;i++)
			Bags[i] = null;
		
		MiVisualizacion = GetComponent<Visualization>();
	}
	
	//------------------------------------------------------------------//
	
	public bool AgregarBolsa(Bag b)
	{
		if(CantBolsAct + 1 <= Bags.Length)
		{
			Bags[CantBolsAct] = b;
			CantBolsAct++;
			Dinero += (int)b.Monto;
			b.Desaparecer();
			return true;
		}
		else
			return false;
	}
	
	public void VaciarInv()
	{
		for(int i = 0; i< Bags.Length;i++)
			Bags[i] = null;
		
		CantBolsAct = 0;
	}
	
	public bool ConBolsas()
	{
		for(int i = 0; i< Bags.Length;i++)
		{
			if (Bags[i] != null)
			{
				return true;
			}
		}
		return false;
	}
	
	public void SetContrDesc(UnloadController contr)
	{
		ContrDesc = contr;
	}
	
	public UnloadController GetContr()
	{
		return ContrDesc;
	}
	
	public void CambiarATutorial()
	{
		MiVisualizacion.CambiarATutorial();
		EstAct = Estados.EnCalibracion;
	}
	
	public void CambiarAConduccion()
	{
		MiVisualizacion.CambiarAConduccion();
		EstAct = Estados.EnConduccion;
	}
	
	public void CambiarADescarga()
	{
		MiVisualizacion.CambiarADescarga();
		EstAct = Estados.EnDescarga;
	}
	
	public void SacarBolasa()
	{
		for(int i = 0; i < Bags.Length; i++)
		{
			if(Bags[i] != null)
			{
				Bags[i] = null;
				return;
			}				
		}
	}
}