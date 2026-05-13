using UnityEngine;

public class ObjecVida : MonoBehaviour
{
    float _vel = 2.5f;

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
        if (objecteTocat.tag == "Jugador")
        {
            ValorsGlobals.videsJugador++;
            ValorsGlobals.videsAgafades++;

            GameObject textVides = GameObject.Find("LivesText");
            if (textVides != null)
            {
                TextVidesJugador tvj = textVides.GetComponent<TextVidesJugador>();
                if (tvj != null) tvj.ActualitzarVides();
            }

            Destroy(gameObject);
        }
    }
}
