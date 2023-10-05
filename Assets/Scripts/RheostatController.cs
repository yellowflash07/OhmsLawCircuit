using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RheostatController : MonoBehaviour
{
    private GameObject knob;

    public static Action<float> OnRheostatChange;

    private void Awake()
    {
        OnRheostatChange?.Invoke(0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.collider == null) return;
            if (raycastHit.collider.gameObject.CompareTag("Rheostat"))
            {
                knob = raycastHit.collider.gameObject;
                Debug.Log("Hit!");
            }
        }
        else
        {
            knob = null;
        }

        if (!knob) return;

        knob.transform.localPosition =
            new Vector3(knob.transform.localPosition.x,
                       knob.transform.localPosition.y,
                       Mathf.Clamp(knob.transform.localPosition.z + (-Input.GetAxis("Mouse X") * 0.1f), -0.7f, 0.7f));
        OnRheostatChange?.Invoke(knob.transform.localPosition.z);
    }
}
