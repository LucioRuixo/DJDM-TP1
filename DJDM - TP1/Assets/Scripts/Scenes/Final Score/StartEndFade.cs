using UnityEngine;

public class StartEndFade : MonoBehaviour
{
    public float Duracion = 2;
    public float Vel = 2;
    float TiempInicial;

    ScoreManager Mng;

    Color aux;

    bool MngAvisado = false;

    void Start()
    {
        //renderer.material = IniFin;
        Mng = (ScoreManager)GameObject.FindObjectOfType(typeof(ScoreManager));
        TiempInicial = Mng.TiempEspReiniciar;

        aux = GetComponent<Renderer>().material.color;
        aux.a = 0;
        GetComponent<Renderer>().material.color = aux;
    }

    void Update()
    {
        if (Mng.TiempEspReiniciar > TiempInicial - Duracion)//aparicion
        {
            aux = GetComponent<Renderer>().material.color;
            aux.a += UnityEngine.Time.deltaTime / Duracion;
            GetComponent<Renderer>().material.color = aux;
        }
        else if (Mng.TiempEspReiniciar < Duracion)//desaparicion
        {
            aux = GetComponent<Renderer>().material.color;
            aux.a -= UnityEngine.Time.deltaTime / Duracion;
            GetComponent<Renderer>().material.color = aux;

            if (!MngAvisado)
            {
                MngAvisado = true;
                Mng.DesaparecerGUI();
            }
        }
    }
}
