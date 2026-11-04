using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject avePrefab;
    public Transform puntoInicio;

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
        enemigosRestantes = GameObject.FindGameObjectsWithTag("Enemy").Length;

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
    }

    void SiguienteTurno()
    {
        if (enemigosRestantes <= 0)
        {
            panelGanaste.SetActive(true);
        }
        else if (tiros > 0)
        {
            Instantiate(avePrefab, puntoInicio.position, Quaternion.identity);
        }
        else
        {
            panelPerdiste.SetActive(true);
        }
    }

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