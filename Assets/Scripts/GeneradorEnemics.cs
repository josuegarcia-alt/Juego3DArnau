using UnityEngine;

public class GeneradorEnemics : MonoBehaviour
{
    public GameObject _NauEnemicPrefab;
    public GameObject _NauEnemicEspecialPrefab;
    public GameObject _ObjecVidaPrefab;

    void Start()
    {
        IniciGeneraEnemics();
    }

    public void IniciGeneraEnemics()
    {
        InvokeRepeating("CreaEnemic", 2f, 1f);
        InvokeRepeating("CreaEnemicEspecial", 5f, 8f);
        InvokeRepeating("CreaObjecVida", 6f, 7f);
    }

    public void AturaGenerarEnemics()
    {
        CancelInvoke("CreaEnemic");
        CancelInvoke("CreaEnemicEspecial");
        CancelInvoke("CreaObjecVida");
    }

    private void CreaEnemic()
    {
        GameObject nauEnemic = Instantiate(_NauEnemicPrefab);
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, camDist));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, camDist));
        float posX = Random.Range(minPantalla.x, maxPantalla.x);
        nauEnemic.transform.position = new Vector3(posX, maxPantalla.y, 0f);
    }

    private void CreaEnemicEspecial()
    {
        if (_NauEnemicEspecialPrefab == null) return;
        GameObject nauEspecial = Instantiate(_NauEnemicEspecialPrefab);
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, camDist));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, camDist));
        float posX = Random.Range(minPantalla.x, maxPantalla.x);
        nauEspecial.transform.position = new Vector3(posX, maxPantalla.y, 0f);
    }

    private void CreaObjecVida()
    {
        if (_ObjecVidaPrefab == null) return;
        GameObject objecVida = Instantiate(_ObjecVidaPrefab);
        float camDist = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 minPantalla = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, camDist));
        Vector3 maxPantalla = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, camDist));
        float posX = Random.Range(minPantalla.x, maxPantalla.x);
        objecVida.transform.position = new Vector3(posX, maxPantalla.y, 0f);
    }
}
