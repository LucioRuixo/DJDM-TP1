using UnityEngine;

public class TextureLoop : MonoBehaviour 
{
	public float Intervalo = 1;
	float Tempo = 0;
	
	public Texture2D[] Imagenes;
	int Contador = 0;

	void Start () 
	{
		if(Imagenes.Length > 0)
			GetComponent<Renderer>().material.mainTexture = Imagenes[0];
	}
	
	void Update () 
	{
        Tempo += UnityEngine.Time.deltaTime;
		
		if(Tempo >= Intervalo)
		{
			Tempo = 0;
			Contador++;
			if(Contador >= Imagenes.Length)
			{
				Contador = 0;
			}
			GetComponent<Renderer>().material.mainTexture = Imagenes[Contador];
		}
	}
}
