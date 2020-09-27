using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{
	Rect Rect = new Rect();
	
	public float StartAnimationTime = 2.5f;
	float Tempo = 0;
	
	int IndexGanador = 0;

	public GameObject singleplayerUI;
	public GameObject multiplayerUI;

	public Image background;
	public Sprite singleplayerBackground;
	public Sprite multiplayerBackground;

	public Text finalMoney;

	public Text winnerMoney;
	public Text loserMoney;
	
	public Vector2 GanadorPos;
	public Vector2 GanadorEsc;
	public Texture2D[] Ganadores;
	public GUISkin GS_Ganador;
	
	public GameObject Fondo;
	
	public float TiempEspReiniciar = 10;
	
	public float TiempParpadeo = 0.7f;
	float TempoParpadeo = 0;
	bool PrimerImaParp = true;
	
	public bool ActivadoAnims = false;
	
	Visualization Visualization = new Visualization();
	
	void Start () 
	{
		if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.Singleplayer)
			SetSingleplayerUI();
		else
			SetMultiplayerUI();
	}
	
	void Update () 
	{
#if UNITY_STANDALONE
		//CIERRA LA APLICACION
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
#endif

        TiempEspReiniciar -= Time.deltaTime;
		if(TiempEspReiniciar <= 0 )
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		
		if(ActivadoAnims)
		{
            TempoParpadeo += Time.deltaTime;
			
			if(TempoParpadeo >= TiempParpadeo)
			{
				TempoParpadeo = 0;
				
				if(PrimerImaParp)
					PrimerImaParp = false;
				else
				{
					TempoParpadeo += 0.1f;
					PrimerImaParp = true;
				}
			}
		}
		
		if(!ActivadoAnims)
		{
            Tempo += Time.deltaTime;
			if(Tempo >= StartAnimationTime)
			{
				Tempo = 0;
				ActivadoAnims = true;
			}
		}
	}
	
	void OnGUI()
	{
		if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.Singleplayer)
			return;

		if(ActivadoAnims)
			SetCartelGanador();
		
		GUI.skin = null;
	}
	
	//---------------------------------//
	void SetSingleplayerUI()
	{
		background.sprite = singleplayerBackground;

		singleplayerUI.SetActive(true);
		finalMoney.text = "$" + GameData.PtsGanador.ToString();
	}

	void SetMultiplayerUI()
	{
		background.sprite = multiplayerBackground;

		multiplayerUI.SetActive(true);
		winnerMoney.text = "$" + GameData.PtsGanador.ToString();
		loserMoney.text = "$" + GameData.PtsPerdedor.ToString();

		SetGanador();
	}

	void SetGanador()
	{
		switch(GameData.LadoGanadaor)
		{
		case GameData.Lados.Der:
			
			GS_Ganador.box.normal.background = Ganadores[1];
			
			break;
			
		case GameData.Lados.Izq:
			
			GS_Ganador.box.normal.background = Ganadores[0];
			
			break;
		}
	}
	
	void SetCartelGanador()
	{
		GUI.skin = GS_Ganador;
		
		Rect.width = GanadorEsc.x * Screen.width/100;
		Rect.height = GanadorEsc.y * Screen.height/100;
		Rect.x = GanadorPos.x * Screen.width/100;
		Rect.y = GanadorPos.y * Screen.height/100;
		
		//if(PrimerImaParp)//para que parpadee
		GUI.Box(Rect, "");
	}
	
	public void DesaparecerGUI()
	{
		ActivadoAnims = false;
		Tempo = -100;
	}
}