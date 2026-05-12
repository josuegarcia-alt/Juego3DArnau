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
        if (objecteTocat.tag == "ProjectilJugador" || objecteTocat.tag == "NauJugador")
        {
            GameObject explosio = Instantiate(_ExplosioPrefab);
            explosio.transform.position = transform.position;

            int puntsEnemic = 200;
            GameObject.Find("TextPunts").GetComponent<TextPuntsJugador>().setPuntsJugador(puntsEnemic);

            Destroy(gameObject);
        }
    }
}
