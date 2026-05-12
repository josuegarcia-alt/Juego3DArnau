using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInici : MonoBehaviour
{
    public void AnarAPantallaJoc()
    {
        ValorsGlobals.videsJugador = 3;
        ValorsGlobals.videsAgafades = 0;
        ValorsGlobals.puntsAconseguits = "Punts: 0";
        SceneManager.LoadScene("EscenaJoc");
    }
}
