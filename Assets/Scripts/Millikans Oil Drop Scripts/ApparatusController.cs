using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApparatusController : MonoBehaviour
{
    public static double voltage;
    public static double distanceBetweenTwoPlates; 
    
    public double v;
    public double d;


    public TextMeshProUGUI velocity;
    public TextMeshProUGUI voltageText;
    public TextMeshProUGUI distance;
    public TextMeshProUGUI mass;

    private Oil drop;
    private Oil selectedDrop;

    public static double GetElectricField()
    {
        return voltage / distanceBetweenTwoPlates;
    }

    // Update is called once per frame
    void Update()
    {
        voltage = v;
        distanceBetweenTwoPlates = d;

        voltageText.text = "Voltage: " + voltage;
        distance.text = "Distance between plates: " + distanceBetweenTwoPlates.ToString();

        if (Input.GetMouseButton(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.collider == null) return;
            if (raycastHit.collider.gameObject.CompareTag("Oil"))
            {
                if (selectedDrop)
                {
                    selectedDrop.Deselect();
                }
                drop = raycastHit.collider.GetComponent<Oil>();
                drop.Select();
                selectedDrop = drop;
                Debug.Log("Hit!" + drop.name);
            }
        }

        FillUI(drop);


    }

    public void FillUI(Oil oil)
    {
       if(oil == null)
        {
            velocity.text = "Select a particle to display velocity";
            mass.text = "Select a particle to display mass";
            return;
        }
        velocity.text = "Velocity: " + oil.velocity.y;
        mass.text = "Mass: " + oil.mass;
    }

    public void ChangeVoltage(float value)
    {
        v = value;
    }
}
