using UnityEngine;
using UnityEngine.InputSystem;

public class Pericos : MonoBehaviour
{
    public InputActionReference posicion; 
    public InputActionReference click;    

    public float fuerza = 10f;          
    public float distanciaMaxima = 3f;  

    private Rigidbody2D rb;
    private Collider2D col;
    private Vector3 posicionInicial;
    private bool arrastrando = false;
    private bool lanzado = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        posicion.action.Enable();
        click.action.Enable();

        click.action.started += AlPresionar;
        click.action.canceled += AlSoltar;
    }

    void OnDisable()
    {
        click.action.started -= AlPresionar;
        click.action.canceled -= AlSoltar;
    }

    void Start()
    {
        rb.bodyType = RigidbodyType2D.Kinematic; 
        posicionInicial = transform.position;
    }

    
    Vector3 PosicionMouse()
    {
        Vector2 pantalla = posicion.action.ReadValue<Vector2>();
        Vector3 mundo = Camera.main.ScreenToWorldPoint(pantalla);
        mundo.z = 0;
        return mundo;
    }

    void AlPresionar(InputAction.CallbackContext ctx)
    {
        if (lanzado) return;

        
        Collider2D tocado = Physics2D.OverlapPoint(PosicionMouse());
        if (tocado == col)
        {
            arrastrando = true;
        }
    }

    void Update()
    {
        if (!arrastrando) return;

        
        Vector3 direccion = PosicionMouse() - posicionInicial;
        if (direccion.magnitude > distanciaMaxima)
        {
            direccion = direccion.normalized * distanciaMaxima;
        }

        transform.position = posicionInicial + direccion;
    }

    void AlSoltar(InputAction.CallbackContext ctx)
    {
        if (!arrastrando) return;

        arrastrando = false;
        lanzado = true;

        
        Vector2 direccionLanzamiento = posicionInicial - transform.position;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(direccionLanzamiento * fuerza, ForceMode2D.Impulse);

        
        GameManager.instance.AveLanzada();

        
    }
}