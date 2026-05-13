using UnityEngine;

public class NauEnemicEspecial : MonoBehaviour
{
    float _velHoritzontal = 4f;
    float _velVertical = 2f;
    float _direccioX;

    public GameObject _ExplosioPrefab;
    public GameObject _ProjectilEnemicEspecialPrefab;

    void Start()
    {
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 centPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDist));
        _direccioX = (transform.position.x >= centPantalla.x) ? -1f : 1f;
        InvokeRepeating("CreaProjectil", 0.5f, 1.5f);
    }

    void Update()
    {
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDist));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDist));

        // Mou horitzontalment i verticalmet alhora (zigzag real)
        float nouX = transform.position.x + _direccioX * _velHoritzontal * Time.deltaTime;
        float nouY = transform.position.y - _velVertical * Time.deltaTime;

        // Rebota quan toca les vores
        if (nouX >= maxPantalla.x)
        {
            nouX = maxPantalla.x;
            _direccioX = -1f;
        }
        else if (nouX <= minPantalla.x)
        {
            nouX = minPantalla.x;
            _direccioX = 1f;
        }

        transform.position = new Vector3(nouX, nouY, 0f);

        if (transform.position.y < minPantalla.y)
            Destroy(gameObject);
    }

    private void CreaProjectil()
    {
        if (_ProjectilEnemicEspecialPrefab == null) return;
        if (GameObject.FindWithTag("Jugador") == null) return;

        GameObject projectil = Instantiate(_ProjectilEnemicEspecialPrefab);
        projectil.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider objecteTocat)
    {
        if (objecteTocat.tag == "ProjectilJugador")
        {
            Explotar();
            GameObject tp = GameObject.Find("TextPunts");
            if (tp != null)
                tp.GetComponent<TextPuntsJugador>().setPuntsJugador(500);
            Destroy(gameObject);
        }

        if (objecteTocat.tag == "Jugador")
        {
            NauJugador nau = objecteTocat.GetComponent<NauJugador>();
            if (nau != null) nau.RebreImpacte();
            Explotar();
            Destroy(gameObject);
        }
    }

    private void Explotar()
    {
        if (_ExplosioPrefab != null)
        {
            GameObject explosio = Instantiate(_ExplosioPrefab);
            explosio.transform.position = transform.position;
        }
    }
}