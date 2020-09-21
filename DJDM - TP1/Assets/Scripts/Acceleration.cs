using UnityEngine;

public class Acceleration : MonoBehaviour 
{
	public Transform ManoDer;
	public Transform ManoIzq;
	
	public float AlturaMedia = 0;//valor en eje Y que calibra el 0 de cada pedal
	
	public float SensAcel = 1;
	public float SensFren = 1;
	
	public Transform Camion;//lo que va a conducir
	
	//pedales
	public Transform PedalAcel;
	Vector3 PAclPosIni;
	public Transform PedalFren;
	Vector3 PFrnPosIni;
	public float SensivPed = 1;
	
	float DifIzq;
	float DifDer;
	
	float Frenado;
	float Acelerado;
	
	//---------------------------------------------------------//
	void Start () 
	{
		PAclPosIni = PedalAcel.localPosition;
		PFrnPosIni = PedalFren.localPosition;
	}
	
	void Update () 
	{
		DifDer = ManoDer.position.y - AlturaMedia;
		DifIzq = ManoIzq.position.y - AlturaMedia;
		
		//acelerar
		if(DifDer > 0)
		{
            Acelerado = DifDer * SensAcel * UnityEngine.Time.deltaTime;
			
			Camion.position += Acelerado * Camion.forward;
			
			PedalAcel.localPosition = PAclPosIni - PedalAcel.forward * SensivPed * Acelerado;
		}
		else
		{
			//PedalFren.localPosition = PAclPosIni;
		}
		
		//frenar
		if(DifIzq > 0)
		{
            Frenado = DifIzq * SensFren * UnityEngine.Time.deltaTime;
			
			Camion.position -= Frenado * Camion.forward;
			
			PedalFren.localPosition = PFrnPosIni - PedalFren.forward * SensivPed * Frenado;
		}
		else
		{
			//PedalFren.localPosition = PFrnPosIni;
		}
	}
}
