using UnityEngine;

public class ProjectilEnemicEspecial : MonoBehaviour
{
    private float _vel = 5f;
    private bool _potCollidir = false;

    void Start()
    {
        Invoke("ActivarCollidir", 0.1f);
    }

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

    private void ActivarCollidir() { _potCollidir = true; }

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