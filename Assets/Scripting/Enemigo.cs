using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int puntos = 100;
    public float velocidadMinima = 6f;  

    private bool muerto = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (muerto) return;

        
        if (col.gameObject.CompareTag("Bird") || col.relativeVelocity.magnitude > velocidadMinima)
        {
            Morir();
        }
    }

    void Morir()
    {
        muerto = true;
        GameManager.instance.EnemigoMuerto(puntos);
        Destroy(gameObject);
    }
}