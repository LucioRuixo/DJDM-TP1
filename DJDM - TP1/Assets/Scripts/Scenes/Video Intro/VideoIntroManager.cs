using UnityEngine;

public class VideoIntroManager : MonoBehaviour 
{
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void Update () 
	{
		//PARA JUGAR
		if(Input.GetKeyDown(KeyCode.KeypadEnter) || 
		   Input.GetKeyDown(KeyCode.Return) ||
		   Input.GetKeyDown(KeyCode.Mouse0))
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);//el juego
		}
		
		//REINICIAR
		if(Input.GetKeyDown(KeyCode.Mouse1) ||
		   Input.GetKeyDown(KeyCode.Keypad0))
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
		
		//CIERRA LA APLICACION
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		//CALIBRACION DEL KINECT
		if(Input.GetKeyDown(KeyCode.Backspace))
		{
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
		}
	}
}