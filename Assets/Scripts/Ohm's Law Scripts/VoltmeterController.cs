using System;
using UnityEngine;
using TMPro;

public class VoltmeterController : MonoBehaviour
{
    float batteryVoltage =1.5f;  // Voltage of the battery in volts
    float resistorResistance = 4.7f;  // Resistance of the resistor in ohms
    float rheostatTotalResistance = 100.0f; // Total resistance of the rheostat in ohms

    [SerializeField] private TextMeshProUGUI reading, uiReading;
    public static float voltMeterReading;

    private void Start()
    {
        RheostatController.OnRheostatChange += ChangeVoltageReading;
        ChangeVoltageReading(0.7f);
    }

    private void ChangeVoltageReading(float knobDistance)
    {
        var positionAsPercet = ((knobDistance - (-0.7f)) / (0.7f - (-0.7f))) * 100;
        var rheoStatResistace = rheostatTotalResistance * positionAsPercet / 100;
        var totalResistance = resistorResistance  + rheoStatResistace;

        //V = E - IR
        //Voltage across the rheostat (V) = I * Rheostat resistance = 0.061 A * 20 ohms = 1.22V
        var current = batteryVoltage / totalResistance;
        var voltageAcrossRheostat = (current * rheoStatResistace);
        voltMeterReading  = (float)(Math.Round(voltageAcrossRheostat, 2));
        reading.text = uiReading.text = voltMeterReading.ToString() + "V";

        Debug.Log($"knob at {positionAsPercet}%, resistance is {totalResistance}, voltage is {voltMeterReading}");

    }


}

