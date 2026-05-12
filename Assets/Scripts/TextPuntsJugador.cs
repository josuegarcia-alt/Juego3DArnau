using UnityEngine;

public class TextPuntsJugador : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _puntsJugadorText;
    private int _puntsJugadorInt;

    void Start()
    {
        _puntsJugadorText = GetComponent<TMPro.TextMeshProUGUI>();
        _puntsJugadorInt = 0;
    }

    public void setPuntsJugador(int nousPunts)
    {
        _puntsJugadorInt += nousPunts;
        _puntsJugadorText.text = "Punts: " + _puntsJugadorInt;
        ValorsGlobals.puntsAconseguits = _puntsJugadorText.text;
    }

    public int getPuntsJugador()
    {
        return _puntsJugadorInt;
    }

    public void InicialitzarPunts()
    {
        _puntsJugadorInt = 0;
        _puntsJugadorText.text = "Punts: 0";
        ValorsGlobals.puntsAconseguits = "Punts: 0";
    }
}
