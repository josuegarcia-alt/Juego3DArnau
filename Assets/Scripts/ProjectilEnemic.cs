using UnityEngine;

public class ProjectilEnemic : MonoBehaviour
{
    private float _vel;
    private bool _continuaUltimaDireccio;
    private Vector3 _direccioJugador;
    private bool _potCollidir = false;

    void Start()
    {
        _vel = 5f;
        _continuaUltimaDireccio = false;
        _direccioJugador = Vector3.down;
        Invoke("ContinuaUltimaDireccio", 1.5f);
        // Espera 0.1s abans d'activar col·lisions per evitar xoc amb la nau que el crea
        Invoke("ActivarCollidir", 0.1f);
    }

    void Update()
    {
        if (GameObject.FindWithTag("Jugador") != null)
        {
            if (!_continuaUltimaDireccio)
            {
                GameObject nauJugador = GameObject.FindWithTag("Jugador");
                _direccioJugador = (nauJugador.transform.position - transform.position).normalized;
                _direccioJugador.z = 0f;
            }

            Vector3 novaPos = transform.position;
            novaPos += _direccioJugador * _vel * Time.deltaTime;
            novaPos.z = 0f;
            transform.position = novaPos;

            ComprovarDinsPantalla();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ActivarCollidir() { _potCollidir = true; }
    private void ContinuaUltimaDireccio() { _continuaUltimaDireccio = true; }

    private void ComprovarDinsPantalla()
    {
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDist));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDist));
        if (transform.position.y < minPantalla.y || transform.position.x < minPantalla.x ||
            transform.position.y > maxPantalla.y || transform.position.x > maxPantalla.x)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider objecteTocat)
    {
        if (!_potCollidir) return;

        if (objecteTocat.tag == "Jugador")
        {
            NauJugador nau = objecteTocat.GetComponent<NauJugador>();
            if (nau != null) nau.RebreImpacte();
            Destroy(gameObject);
        }
    }
}