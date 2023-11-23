using System;
using TMPro;
using UnityEngine;

public class AmmeterController : MonoBehaviour
{
    float batteryVoltage = 1.5f;  // Voltage of the battery in volts
    float resistorResistance = 4.7f;  // Resistance of the resistor in ohms
    float rheostatTotalResistance = 100.0f; // Total resistance of the rheostat in ohms

    [SerializeField] private TextMeshProUGUI reading,uiReading;
    public static float ammeterReading;

    private void Start()
    {
        RheostatController.OnRheostatChange += ChangeCurrentReading;
        ChangeCurrentReading(0.7f);

    }

    private void ChangeCurrentReading(float knobDistance)
    {
        var positionAsPercet = ((knobDistance - (-0.7f)) / (0.7f - (-0.7f))) * 100;
        var rheoStatResistace = rheostatTotalResistance * positionAsPercet / 100;
        var totalResistance = resistorResistance + rheoStatResistace;
        //
        var current = batteryVoltage / totalResistance;
        current =(float)(Math.Round(current, 3) * 1000); 
        reading.text = uiReading.text = current.ToString() + "mA";
        ammeterReading = current;
    }
}

