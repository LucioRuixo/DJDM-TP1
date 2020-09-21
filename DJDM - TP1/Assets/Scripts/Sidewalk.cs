using UnityEngine;

public class Sidewalk : MonoBehaviour 
{
	public string PlayerTag = "Player";
	public float GiroPorSeg = 0;
	public float RestGiro = 0; // valor que se le suma al giro cuando sale para restaurar la estabilidad

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.tag == PlayerTag)
		{
			other.SendMessage("SumaGiro", GiroPorSeg * Time.deltaTime);
		}	
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == PlayerTag)
		{
			other.SendMessage("SumaGiro", RestGiro);
		}	
	}
}
