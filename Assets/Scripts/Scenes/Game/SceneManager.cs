using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour 
{
	bool JuegoFinalizado = false;
	public float TiempoEsperaFin = 25;//tiempo que espera la aplicacion para volver al video introductorio desp de terminada la partida
	float Tempo = 0;
	
	bool JuegoIniciado = false;
	public float TiempoEsperaInicio = 120;//tiempo que espera la aplicacion para volver al video introductorio desp de terminada la partida
	float Tempo2 = 0;
	
	void Update () 
	{
		if(JuegoFinalizado)
		{
            Tempo += UnityEngine.Time.deltaTime;
			if(Tempo > TiempoEsperaFin)
			{
				Tempo = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			}
		}
		
		if(!JuegoIniciado)
		{
            Tempo2 += UnityEngine.Time.deltaTime;
			if(Tempo > TiempoEsperaInicio)
			{
				Tempo2 = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
			}
		}		
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		//reinicia
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
	}
	
	//---------------------------------------------------//
	public void JuegoFinalizar()
	{
		JuegoFinalizado = true;
	}
	
	public void JuegoIniciar()
	{
		JuegoIniciado = true;
	}
}