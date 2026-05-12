using UnityEngine;
using UnityEngine.SceneManagement;

public class NauJugador : MonoBehaviour
{
    private float _vel;
    public GameObject _ExplosioPrefab;
    public GameManager _gameManager;

    void Start()
    {
        _vel = 8f;
    }

    void Update()
    {
        float direccioInputX = Input.GetAxisRaw("Horizontal");
        float direccioInputY = Input.GetAxisRaw("Vertical");
        Vector3 direccioIndicada = new Vector3(direccioInputX, direccioInputY, 0f).normalized;
        MoureNau(direccioIndicada);
    }

    void MoureNau(Vector3 direccioIndicada)
    {
        Vector3 posNau = transform.position;
        posNau += direccioIndicada * _vel * Time.deltaTime;
        posNau.z = 0f;

        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDist));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDist));

        maxPantalla.x -= 0.6f;
        minPantalla.x += 0.6f;
        maxPantalla.y -= 0.8f;
        minPantalla.y += 0.8f;

        posNau.x = Mathf.Clamp(posNau.x, minPantalla.x, maxPantalla.x);
        posNau.y = Mathf.Clamp(posNau.y, minPantalla.y, maxPantalla.y);

        transform.position = posNau;
    }

    private void OnTriggerEnter(Collider objecteTocat)
    {
        if (objecteTocat.tag == "Enemic" || objecteTocat.tag == "ProjectilEnemic")
        {
            GameObject explosio = Instantiate(_ExplosioPrefab);
            explosio.transform.position = transform.position;

            ValorsGlobals.videsJugador--;

            GameObject textVides = GameObject.Find("LivesText");
            if (textVides != null)
            {
                TextVidesJugador tvj = textVides.GetComponent<TextVidesJugador>();
                if (tvj != null) tvj.ActualitzarVides();
            }

            if (ValorsGlobals.videsJugador <= 0)
            {
                SceneManager.LoadScene("EscenaResultats");
            }
        }
    }
}
