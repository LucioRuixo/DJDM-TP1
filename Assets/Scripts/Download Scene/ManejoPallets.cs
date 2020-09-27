using System.Collections.Generic;
using UnityEngine;

public class ManejoPallets : MonoBehaviour 
{
	protected List<Pallet> Pallets = new List<Pallet>();
	public UnloadController unloadController;
	protected int Contador = 0;
	
	public virtual bool Recibir(Pallet pallet)
	{
		Pallets.Add(pallet);
		pallet.Pasaje();
		return true;
	}
	
	public bool HasPallets()
	{
		return Pallets.Count > 0;
	}
	
	public virtual void Dar(ManejoPallets receptor)
	{
		//es el encargado de decidir si le da o no la bolsa
	}
}