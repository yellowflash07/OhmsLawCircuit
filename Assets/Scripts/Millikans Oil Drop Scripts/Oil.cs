using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{

    public float charge;
    public float dropSpeed;

    private Rigidbody rb;
    private float currTime;
    public bool passedHole;

    public Vector3 velocity = Vector3.zero;
    private Vector3 position = Vector3.zero;

    public Color selectedColor, defaultColor;

    private const double fundamentalMass = 1.6e-15;
    private const double chargeOfElectron = 1.6e-19f;

    public double mass;
    private float initialSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = transform.position;
      //  defaultColor = GetComponent<Renderer>().sharedMaterial.color;
        Deselect();
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime < 1.1f) return;

        if(!passedHole)
        {
            rb.MovePosition(transform.position + (Vector3.down * Time.deltaTime * initialSpeed));
        }
        else 
        {
            MoveParticle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cathode" && !passedHole)
        {
            initialSpeed = 0;
        }
        if (other.tag == "CathodeHole")
        {
            position = transform.position;
            rb.isKinematic = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Blocker"))
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "CathodeHole")
        {
        //    position = transform.position;
        //    rb.isKinematic = false;
            
            passedHole = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "CathodeHole")
        {
            rb.isKinematic = true;
            // transform.name = "PassedDrop" + transform.name;
        }
        if (other.tag == "Cathode")
        {

        }

    }


    internal void Deselect()
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }

    internal void Select()
    {
        GetComponent<Renderer>().material.color = selectedColor;
    }

    public void AssignRandomChargeAndMass()
    {
        int random = Random.Range(0, 101);

        if (random > 70)
        {
            charge = 1;
        }
        else
        {
            charge = -1;
        }

        int randomMass = Random.Range(1, 5);
        mass = fundamentalMass * randomMass;
    }

    private void MoveParticle()
    {
        if (this.transform.position.y > 1.4f || this.transform.position.y < 1.0f) return;
        // Calculate forces
        double electricField = ApparatusController.GetElectricField(); // V/d

        double F_electric = charge * chargeOfElectron * electricField; //qE
        double F_gravity = mass * -9.8f; //mg


        // Calculate net force
        double F_net = F_gravity + F_electric;

        // Calculate acceleration
        Vector3 acceleration = Vector3.up * (float)( F_net / mass);

        // Update velocity using Euler's method
        velocity = acceleration * Time.deltaTime;

        if (Mathf.Abs(velocity.y) < 0.005f) return;
        // Update position
        position += velocity * Time.deltaTime;

        rb.MovePosition(position);
    //    float volume = (4f / 3f) * Mathf.PI * Mathf.Pow(0.5f / 2, 3);
        double shouldcharge = (mass * 9.8 / electricField);
        Debug.Log(shouldcharge, this);
    }


}
