using UnityEngine;

public class TextVidesJugador : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _videsText;

    void Start()
    {
        _videsText = GetComponent<TMPro.TextMeshProUGUI>();
        ValorsGlobals.videsJugador = 3;
        ValorsGlobals.videsAgafades = 0;
        ActualitzarText();
    }

    public void ActualitzarVides()
    {
        ActualitzarText();
    }

    private void ActualitzarText()
    {
        _videsText.text = "Vides: " + ValorsGlobals.videsJugador;
    }
}
