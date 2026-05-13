using UnityEngine;

public class NauEnemic : MonoBehaviour
{
    float _vel = 3f;
    public GameObject _ExplosioPrefab;

    void Update()
    {
        Vector3 novaPos = transform.position;
        novaPos += Vector3.down * _vel * Time.deltaTime;
        novaPos.z = 0f;
        transform.position = novaPos;

        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDist));
        if (transform.position.y < minPantalla.y)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider objecteTocat)
    {
        // Colpejat per projectil del jugador
        if (objecteTocat.tag == "ProjectilJugador")
        {
            Explotar();
            GameObject tp = GameObject.Find("TextPunts");
            if (tp != null)
                tp.GetComponent<TextPuntsJugador>().setPuntsJugador(200);
            Destroy(gameObject);
        }

        // Xoc directe amb el jugador
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