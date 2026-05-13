using UnityEngine;

public class GeneradorProjectilEnemic : MonoBehaviour
{
    public GameObject _ProjectilEnemicPrefab;
    private float _intervalDispar = 2f;

    void Start()
    {
        // Petit delay aleatori perque no tots disparin alhora
        float delay = Random.Range(0.5f, 2f);
        InvokeRepeating("Disparar", delay, _intervalDispar);
    }

    private void Disparar()
    {
        if (_ProjectilEnemicPrefab == null) return;
        if (GameObject.FindWithTag("Jugador") == null) return;

        GameObject projectil = Instantiate(_ProjectilEnemicPrefab);
        projectil.transform.position = transform.position;
    }
}