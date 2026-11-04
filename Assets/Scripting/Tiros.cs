using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject avePrefab;
    public GameObject avePrefab2;
    public GameObject avePrefab3;
    public Transform puntoInicio;
    public Transform puntoInicio2;
    public Transform puntoInicio3;

    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoTiros;
    public GameObject panelGanaste;
    public GameObject panelPerdiste;

    public int tiros = 3;

    private int puntos = 0;
    private int enemigosRestantes;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        enemigosRestantes = GameObject.FindGameObjectsWithTag("GalloConTenis").Length;

        panelGanaste.SetActive(false);
        panelPerdiste.SetActive(false);

        ActualizarUI();
        Instantiate(avePrefab, puntoInicio.position, Quaternion.identity);
    }

    public void AveLanzada()
    {
        tiros--;
        ActualizarUI();
        Invoke("SiguienteTurno", 3f);
    }

    public void EnemigoMuerto(int puntosGanados)
    {
        puntos += puntosGanados;
        enemigosRestantes--;
        ActualizarUI();
        Debug.Log("Enemigos restantes: " + enemigosRestantes);
        
        if (enemigosRestantes <= 0 && !terminado)
        {
            terminado = true;
            CancelInvoke("SiguienteTurno");
            panelGanaste.SetActive(true);
        }
    }
    void SiguienteTurno()
    {
        if (terminado) return;
        if (tiros > 0)
        {
            Instantiate(avePrefab, puntoInicio.position, Quaternion.identity);
        }
        else
        {
            terminado = true;
            panelPerdiste.SetActive(true);
        }
    }

    private bool terminado = false;

    void ActualizarUI()
    {
        textoPuntos.text = "Puntos: " + puntos;
        textoTiros.text = "Tiros: " + tiros;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}