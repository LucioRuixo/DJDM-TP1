using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;

    public GameObject player2Container;
    public GameObject hardDifficultyObstacles;

    public float TiempoDeJuego = 60;

    public enum EstadoJuego { Tutorial, Jugando, Finalizado }
    public EstadoJuego EstAct = EstadoJuego.Tutorial;

    public PlayerInfo PlayerInfo1 = null;
    public PlayerInfo PlayerInfo2 = null;

    public Player Player1;
    public Player Player2;
    public GameObject player1Inventory;
    public GameObject player2Inventory;

    //mueve los esqueletos para usar siempre los mismos
    public Transform Esqueleto1;
    public Transform Esqueleto2;
    //public Vector3[] PosEsqsCalib;
    public Vector3[] PosEsqsCarrera;

    bool PosSeteada = false;

    bool ConteoRedresivo = true;
    public Rect ConteoPosEsc;
    public float ConteoParaInicion = 3;
    public GUISkin GS_ConteoInicio;

    public Rect TiempoGUI = new Rect();
    public GUISkin GS_TiempoGUI;
    Rect R = new Rect();

    public float TiempEspMuestraPts = 3;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];
    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de calibracion
    public GameObject[] ObjsTurorial;
    //la pista de carreras
    public GameObject[] ObjsCarrera;
    //de las descargas se encarga el controlador de descargas

    IList<int> users;

    //--------------------------------------------------------//
    void Awake()
    {
        Instancia = this;

        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.LocalMultiplayer)
            player2Container.SetActive(true);

        if (GameOptionsManager.instance.difficulty == GameOptionsManager.Difficulty.Hard)
            hardDifficultyObstacles.SetActive(true);
    }

    void Start()
    {
        IniciarTutorial();
    }

    void Update()
    {
        //REINICIAR
        if (Input.GetKey(KeyCode.Mouse1) &&
           Input.GetKey(KeyCode.Keypad0))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        //CIERRA LA APLICACION
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (EstAct)
        {
            case EstadoJuego.Tutorial:

                //SKIP EL TUTORIAL
                if (Input.GetKey(KeyCode.Mouse0) &&
                   Input.GetKey(KeyCode.Keypad0))
                {
                    if (PlayerInfo1 != null && PlayerInfo2 != null)
                    {
                        FinTutorial(0);
                        FinTutorial(1);

                        FinTutorial(0);
                        FinTutorial(1);
                    }
                }

                if (PlayerInfo1.PJ == null)
                {
                    PlayerInfo1 = new PlayerInfo(0, Player1);
                    PlayerInfo1.LadoAct = Visualization.Lado.Izq;
                    SetPosicion(PlayerInfo1);
                }

                if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.LocalMultiplayer)
                {
                    if (PlayerInfo2.PJ == null)
                    {
                        PlayerInfo2 = new PlayerInfo(1, Player2);
                        PlayerInfo2.LadoAct = Visualization.Lado.Der;
                        SetPosicion(PlayerInfo2);
                    }
                }

                //cuando los 2 pj terminaron los tutoriales empiesa la carrera
                if (PlayerInfo1.PJ != null && PlayerInfo2.PJ != null)
                {
                    if (PlayerInfo1.FinTutorial && PlayerInfo2.FinTutorial)
                        EmpezarCarrera();
                }

                break;


            case EstadoJuego.Jugando:

                //SKIP LA CARRERA
                if (Input.GetKey(KeyCode.Mouse1) &&
                   Input.GetKey(KeyCode.Keypad0))
                {
                    TiempoDeJuego = 0;
                }

                if (TiempoDeJuego <= 0)
                {
                    FinalizarCarrera();
                }

                /*
                //para testing
                TiempoTranscurrido += T.GetDT();
                DistanciaRecorrida += (Player1.transform.position - PosCamionesCarrera[0]).magnitude;
                */

                if (ConteoRedresivo)
                {
                    //se asegura de que los vehiculos se queden inmobiles
                    //Player1.rigidbody.velocity = Vector3.zero;
                    //Player2.rigidbody.velocity = Vector3.zero;

                    ConteoParaInicion -= Time.deltaTime;
                    if (ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                    }
                }
                else
                {
                    //baja el tiempo del juego
                    TiempoDeJuego -= Time.deltaTime;
                    if (TiempoDeJuego <= 0)
                    {
                        //termina el juego
                    }
                    /*
                    //otro tamaño
                    if(!SeteadoNuevaFontSize && TiempoDeJuego <= 5)
                    {
                        SeteadoNuevaFontSize = true;
                        GS_TiempoGUI.box.fontSize = TamNuevoFont;
                        GS_TiempoGUI.box.normal.textColor = Color.red;
                    }
                    */
                }

                break;


            case EstadoJuego.Finalizado:

                //nada de trakeo con kinect, solo se muestra el puntaje
                //tambien se puede hacer alguna animacion, es el tiempo previo a la muestra de pts

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Final Score Screen");

                break;
        }
    }

    void OnGUI()
    {
        switch (EstAct)
        {
            case EstadoJuego.Jugando:
                if (ConteoRedresivo)
                {
                    GUI.skin = GS_ConteoInicio;

                    R.x = ConteoPosEsc.x * Screen.width / 100;
                    R.y = ConteoPosEsc.y * Screen.height / 100;
                    R.width = ConteoPosEsc.width * Screen.width / 100;
                    R.height = ConteoPosEsc.height * Screen.height / 100;

                    if (ConteoParaInicion > 1)
                    {
                        GUI.Box(R, ConteoParaInicion.ToString("0"));
                    }
                    else
                    {
                        GUI.Box(R, "GO");
                    }
                }

                GUI.skin = GS_TiempoGUI;
                R.x = TiempoGUI.x * Screen.width / 100;
                R.y = TiempoGUI.y * Screen.height / 100;
                R.width = TiempoGUI.width * Screen.width / 100;
                R.height = TiempoGUI.height * Screen.height / 100;
                GUI.Box(R, TiempoDeJuego.ToString("00"));
                break;
        }

        GUI.skin = null;
    }

    //----------------------------------------------------------//
    public void IniciarTutorial()
    {
        for (int i = 0; i < ObjsTurorial.Length; i++)
        {
            ObjsTurorial[i].SetActive(true);
        }

        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(false);
        }

        Player1.CambiarATutorial();
        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.LocalMultiplayer)
            Player2.CambiarATutorial();
    }

    void EmpezarCarrera()
    {
        Player1.GetComponent<Break>().RestaurarVel();
        Player1.GetComponent<InputController>().active = true;

        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.LocalMultiplayer)
        {
            Player2.GetComponent<Break>().RestaurarVel();
            Player2.GetComponent<InputController>().active = true;
        }
    }

    void FinalizarCarrera()
    {
        EstAct = EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        if (Player1.Dinero > Player2.Dinero)
        {
            //lado que gano
            if (PlayerInfo1.LadoAct == Visualization.Lado.Der)
                GameData.LadoGanadaor = GameData.Lados.Der;
            else
                GameData.LadoGanadaor = GameData.Lados.Izq;

            //puntajes
            GameData.PtsGanador = Player1.Dinero;
            GameData.PtsPerdedor = Player2.Dinero;
        }
        else
        {
            //lado que gano
            if (PlayerInfo2.LadoAct == Visualization.Lado.Der)
                GameData.LadoGanadaor = GameData.Lados.Der;
            else
                GameData.LadoGanadaor = GameData.Lados.Izq;

            //puntajes
            GameData.PtsGanador = Player2.Dinero;
            GameData.PtsPerdedor = Player1.Dinero;
        }

        Player1.GetComponent<Break>().Frenar();
        Player1.ContrDesc.FinDelJuego();

        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.LocalMultiplayer)
        {
            Player2.GetComponent<Break>().Frenar();
            Player2.ContrDesc.FinDelJuego();
        }
    }

    //se encarga de posicionar la camara derecha para el jugador que esta a la derecha y viseversa
    void SetPosicion(PlayerInfo pjInf)
    {
        pjInf.PJ.GetComponent<Visualization>().SetLado(pjInf.LadoAct);
        //en este momento, solo la primera vez, deberia setear la otra camara asi no se superponen
        pjInf.PJ.ContrCalib.IniciarTesteo();
        PosSeteada = true;


        if (pjInf.PJ == Player1)
        {
            if (pjInf.LadoAct == Visualization.Lado.Izq)
                Player2.GetComponent<Visualization>().SetLado(Visualization.Lado.Der);
            else
                Player2.GetComponent<Visualization>().SetLado(Visualization.Lado.Izq);
        }
        else
        {
            if (pjInf.LadoAct == Visualization.Lado.Izq)
                Player1.GetComponent<Visualization>().SetLado(Visualization.Lado.Der);
            else
                Player1.GetComponent<Visualization>().SetLado(Visualization.Lado.Izq);
        }
    }

    void CambiarACarrera()
    {
        for (int i = 0; i < ObjsCarrera.Length; i++)
        {
            ObjsCarrera[i].SetActive(true);
        }

        for (int i = 0; i < ObjsTurorial.Length; i++)
        {
            ObjsTurorial[i].SetActive(false);
        }

        PlayerInfo2.FinTutorial = true;

        //posiciona los camiones dependiendo de que lado de la pantalla esten
        if (PlayerInfo1.LadoAct == Visualization.Lado.Izq)
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[0];
            Player2.gameObject.transform.position = PosCamionesCarrera[1];
        }
        else
        {
            Player1.gameObject.transform.position = PosCamionesCarrera[1];
            Player2.gameObject.transform.position = PosCamionesCarrera[0];
        }

        Player1.transform.forward = Vector3.forward;
        Player1.GetComponent<Break>().Frenar();
        Player1.CambiarAConduccion();

        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.LocalMultiplayer)
        {
            Player2.transform.forward = Vector3.forward;
            Player2.GetComponent<Break>().Frenar();
            Player2.CambiarAConduccion();

            Player2.GetComponent<Break>().RestaurarVel();
            Player2.GetComponent<InputController>().active = false;
            Player2.transform.forward = Vector3.forward;

            player2Inventory.SetActive(true);
        }

        Player1.GetComponent<Break>().RestaurarVel();
        Player1.GetComponent<InputController>().active = false;
        Player1.transform.forward = Vector3.forward;

        player1Inventory.SetActive(true);

        EstAct = EstadoJuego.Jugando;
    }

    public void FinTutorial(int playerID)
    {
        if (playerID == 0)
            PlayerInfo1.FinTutorial = true;
        else if (playerID == 1)
            PlayerInfo2.FinTutorial = true;

        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.Singleplayer)
        {
            if (PlayerInfo1.PJ != null)
            {
                if (PlayerInfo1.FinTutorial)
                    CambiarACarrera();
            }
        }
        else
        {
            if (PlayerInfo1.PJ != null && PlayerInfo2.PJ != null)
            {
                if (PlayerInfo1.FinTutorial && PlayerInfo2.FinTutorial)
                    CambiarACarrera();
            }
        }
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public PlayerInfo(int tipoDeInput, Player pj)
        {
            TipoDeInput = tipoDeInput;
            PJ = pj;
        }

        public bool FinTutorial = false;

        public Visualization.Lado LadoAct;

        public int TipoDeInput = -1;

        public Player PJ;
    }
}