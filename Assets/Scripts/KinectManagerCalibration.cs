using UnityEngine;

public class KinectManagerCalibration : MonoBehaviour 
{
	public GameObject[] ParaAct;

	void Start ()
	{
		for(int i = 0; i < ParaAct.Length; i++)
		{
			ParaAct[i].SetActive(false);
		}
	}
	
	void Update () 
	{
		//DISTINTAS CAMARAS
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			for(int i = 0; i < ParaAct.Length; i++)
			{
				ParaAct[i].SetActive(false);
			}
			
			if(ParaAct.Length >= 1)
				ParaAct[0].SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			for(int i = 0; i < ParaAct.Length; i++)
			{
				ParaAct[i].SetActive(false);
			}
			
			if(ParaAct.Length >= 2)
				ParaAct[1].SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Keypad3))
		{
			for(int i = 0; i < ParaAct.Length; i++)
			{
				ParaAct[i].SetActive(false);
			}
			
			if(ParaAct.Length >= 3)
				ParaAct[2].SetActive(true);
		}
		
		//SALE AL VIDEO DE INTRO
		if(Input.GetKeyDown(KeyCode.Return) ||
		   Input.GetKeyDown(KeyCode.Backspace) ||
		   Input.GetKeyDown(KeyCode.KeypadEnter) ||
		   Input.GetKeyDown(KeyCode.Mouse0))
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}

		//SALIR
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		//REINICIAR
		if(Input.GetKeyDown(KeyCode.Mouse1) ||
		   Input.GetKeyDown(KeyCode.Keypad0))
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevel);
		}
	}
}
