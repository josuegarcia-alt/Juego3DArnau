using UnityEngine;

public class ProjectilJugador : MonoBehaviour
{
    float _vel = 10f;

    void Update()
    {
        Vector3 novaPos = transform.position;
        novaPos += Vector3.up * _vel * Time.deltaTime;
        novaPos.z = 0f;
        transform.position = novaPos;

        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, camDist));
        if (transform.position.y > maxPantalla.y)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider objecteTocat)
    {
        if (objecteTocat.tag == "Enemic")
            Destroy(gameObject);
    }
}
