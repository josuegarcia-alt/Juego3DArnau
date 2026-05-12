using UnityEngine;

public class NauEnemicEspecial : MonoBehaviour
{
    float _velHoritzontal = 3f;
    float _velVertical = 3f;
    bool _moventHoritzontalment = true;
    float _direccioX;

    public GameObject _ExplosioPrefab;
    public GameObject _ProjectilEnemicEspecialPrefab;

    void Start()
    {
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 centPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDist));
        _direccioX = (transform.position.x >= centPantalla.x) ? 1f : -1f;
        InvokeRepeating("CreaProjectil", 0.5f, 0.5f);
    }

    void Update()
    {
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDist));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDist));

        if (_moventHoritzontalment)
        {
            transform.position = new Vector3(
                transform.position.x + _direccioX * _velHoritzontal * Time.deltaTime,
                transform.position.y,
                0f
            );

            if (transform.position.x >= maxPantalla.x || transform.position.x <= minPantalla.x)
            {
                _moventHoritzontalment = false;
                CancelInvoke("CreaProjectil");
            }
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - _velVertical * Time.deltaTime,
                0f
            );

            if (transform.position.y < minPantalla.y)
                Destroy(gameObject);
        }
    }

    private void CreaProjectil()
    {
        if (GameObject.FindWithTag("NauJugador") != null)
        {
            GameObject projectil = Instantiate(_ProjectilEnemicEspecialPrefab);
            projectil.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider objecteTocat)
    {
        if (objecteTocat.tag == "ProjectilJugador" || objecteTocat.tag == "NauJugador")
        {
            GameObject explosio = Instantiate(_ExplosioPrefab);
            explosio.transform.position = transform.position;

            GameObject tp = GameObject.Find("TextPunts");
            if (tp != null)
                tp.GetComponent<TextPuntsJugador>().setPuntsJugador(500);

            Destroy(gameObject);
        }
    }
}
