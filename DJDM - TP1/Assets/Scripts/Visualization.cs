using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// clase encargada de TODA la visualizacion
/// de cada player, todo aquello que corresconda a 
/// cada seccion de la pantalla independientemente
/// </summary>
public class Visualization : MonoBehaviour
{
    public enum Lado { Izq, Der }
    public Lado LadoAct;

    Player Pj;

    [Header("Cameras:")]
    public Camera CamTutorial;
    public Camera CamConduccion;
    public Camera CamDescarga;

    [Header("Inventory:")]
    public Vector2[] FondoPos;
    public Vector2 FondoEsc = Vector2.zero;

    //public Vector2 SlotsEsc = Vector2.zero;
    //public Vector2 SlotPrimPos = Vector2.zero;
    //public Vector2 Separacion = Vector2.zero;

    //public int Fil = 0;
    //public int Col = 0;

    public Texture2D TexturaVacia;//lo que aparece si no hay ninguna bolsa
    public Texture2D TextFondo;

    public float Parpadeo = 0.8f;
    public float TempParp = 0;

    public Image inventory;
    public Sprite[] inventorySprites;

    [Header("Money:")]
    public Text money;

#if UNITY_STANDALONE
    [Header("Steering Wheel:")]
    //EL VOLANTE
    public Vector2[] VolantePos;
    public float VolanteEsc = 0;

    public GUISkin GS_Volante;
#endif

    [Header("Unload Bonus:")]
    public Vector2 BonusPos = Vector2.zero;
    public Vector2 BonusEsc = Vector2.zero;

    public Color32 ColorFondoBolsa;
    public Vector2 ColorFondoPos = Vector2.zero;
    public Vector2 ColorFondoEsc = Vector2.zero;

    public Vector2 ColorFondoFondoPos = Vector2.zero;
    public Vector2 ColorFondoFondoEsc = Vector2.zero;

    public GUISkin GS_FondoBonusColor;
    public GUISkin GS_FondoFondoBonusColor;
    public GUISkin GS_Bonus;

    [Header("Unload Tutorial:")]
    public Vector2 ReadyPos = Vector2.zero;
    public Vector2 ReadyEsc = Vector2.zero;
    public Texture2D[] ImagenesDelTuto;
    public float Intervalo = 0.8f;//tiempo de cada cuanto cambia de imagen
    float TempoIntTuto = 0;
    int EnCurso = -1;
    public Texture2D ImaEnPosicion;
    public Texture2D ImaReady;
    public GUISkin GS_TutoCalib;

    [Header("Player Number:")]
    public Texture2D TextNum1;
    public Texture2D TextNum2;
    public GameObject Techo;

    Rect R;

    //------------------------------------------------------------------//
    void Awake()
    {
        TempoIntTuto = Intervalo;
        Pj = GetComponent<Player>();
    }

    void OnGUI()
    {
        switch (Pj.EstAct)
        {
            case Player.Estados.EnConduccion:
                //inventario
                SetInventory();
                //contador de dinero
                SetMoney();
#if UNITY_STANDALONE
                //el volante
                SetVolante();
#endif
                break;

            case Player.Estados.EnDescarga:
                //inventario
                SetInventory();
                //el bonus
                SetBonus();
                //contador de dinero
                SetMoney();
                break;

            case Player.Estados.EnCalibracion:
                //SetCalibr();
                break;

            case Player.Estados.EnTutorial:
                SetInventory();
                SetTuto();
#if UNITY_STANDALONE
                SetVolante();
#endif
                break;
        }

        GUI.skin = null;
    }

    //--------------------------------------------------------//
    public void CambiarATutorial()
    {
        CamTutorial.enabled = true;
        CamConduccion.enabled = false;
        CamDescarga.enabled = false;
    }

    public void CambiarAConduccion()
    {
        CamTutorial.enabled = false;
        CamConduccion.enabled = true;
        CamDescarga.enabled = false;
    }

    public void CambiarADescarga()
    {
        CamTutorial.enabled = false;
        CamConduccion.enabled = false;
        CamDescarga.enabled = true;
    }

    //---------//

    public void SetLado(Lado lado)
    {
        LadoAct = lado;

        Rect r = new Rect();
        if (GameOptionsManager.instance.gameMode == GameOptionsManager.GameMode.Singleplayer)
            r.Set(0f, 0f, 1f, 1f);
        else
        {
            r.width = CamConduccion.rect.width;
            r.height = CamConduccion.rect.height;
            r.y = CamConduccion.rect.y;

            switch (lado)
            {
                case Lado.Der:
                    r.x = 0.5f;
                    break;


                case Lado.Izq:
                    r.x = 0;
                    break;
            }
        }

        CamTutorial.rect = r;
        CamConduccion.rect = r;
        CamDescarga.rect = r;

        if (LadoAct == Lado.Izq)
        {
            Techo.GetComponent<Renderer>().material.mainTexture = TextNum1;
        }
        else
        {
            Techo.GetComponent<Renderer>().material.mainTexture = TextNum2;
        }
    }

    void SetBonus()
    {
        if (Pj.ContrDesc.PEnMov != null)
        {
            //el fondo
            GUI.skin = GS_FondoFondoBonusColor;

            R.width = ColorFondoFondoEsc.x * Screen.width / 100;
            R.height = ColorFondoFondoEsc.y * Screen.height / 100;
            R.x = ColorFondoFondoPos.x * Screen.width / 100;
            R.y = ColorFondoFondoPos.y * Screen.height / 100;
            if (LadoAct == Lado.Der)
                R.x += (Screen.width) / 2;
            GUI.Box(R, "");


            //el fondo
            GUI.skin = GS_FondoBonusColor;

            R.width = ColorFondoEsc.x * Screen.width / 100;
            R.height = (ColorFondoEsc.y * Screen.height / 100) * (Pj.ContrDesc.Bonus / (int)Pallet.Valores.Valor2);
            R.x = ColorFondoPos.x * Screen.width / 100;
            R.y = (ColorFondoPos.y * Screen.height / 100) - R.height;
            if (LadoAct == Lado.Der)
                R.x += (Screen.width) / 2;
            GUI.Box(R, "");


            //la bolsa
            GUI.skin = GS_Bonus;

            R.width = BonusEsc.x * Screen.width / 100;
            R.height = R.width / 2;
            R.x = BonusPos.x * Screen.width / 100;
            R.y = BonusPos.y * Screen.height / 100;
            if (LadoAct == Lado.Der)
                R.x += (Screen.width) / 2;
            GUI.Box(R, "     $" + Pj.ContrDesc.Bonus.ToString("0"));
        }
    }

    void SetMoney()
    {
        money.text = "$" + Pj.Dinero.ToString();
    }

    void SetTuto()
    {
        if (Pj.ContrTuto.Finalizado)
        {
            GUI.skin = GS_TutoCalib;

            R.width = ReadyEsc.x * Screen.width / 100;
            R.height = ReadyEsc.y * Screen.height / 100;
            R.x = ReadyPos.x * Screen.width / 100;
            R.y = ReadyPos.y * Screen.height / 100;
            if (LadoAct == Lado.Der)
                R.x = (Screen.width) - R.x - R.width;

            GUI.Box(R, "ESPERANDO AL OTRO JUGADOR");
        }
    }

#if UNITY_STANDALONE
    void SetVolante()
    {
        GUI.skin = GS_Volante;

        R.width = VolanteEsc * Screen.width / 100;
        R.height = VolanteEsc * Screen.width / 100;
        R.x = VolantePos[0].x * Screen.width / 100;
        R.y = VolantePos[0].y * Screen.height / 100;

        if (LadoAct == Lado.Der)
            R.x = VolantePos[1].x * Screen.width / 100;
        //R.x = (Screen.width) - ((Screen.width/2) - R.x);

        Vector2 centro;
        centro.x = R.x + R.width / 2;
        centro.y = R.y + R.height / 2;
        float angulo = 100 * Direccion.GetTurn();

        GUIUtility.RotateAroundPivot(angulo, centro);

        GUI.Box(R, "");

        GUIUtility.RotateAroundPivot(angulo * (-1), centro);
    }
#endif

    void SetInventory()
    {
        if (Pj.CantBolsAct < 3)
            inventory.sprite = inventorySprites[Pj.CantBolsAct];
        else
        {
            TempParp += Time.deltaTime;

            if (TempParp >= Parpadeo)
            {
                if (inventory.sprite == inventorySprites[4]) inventory.sprite = inventorySprites[3];
                else inventory.sprite = inventorySprites[4];

                TempParp = 0f;
            }
        }
    }
}