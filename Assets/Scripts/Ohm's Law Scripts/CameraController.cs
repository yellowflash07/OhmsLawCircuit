using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening.Core;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
     [SerializeField] private CinemachineFreeLook freeLook;
     [SerializeField] private CinemachineCameraOffset camOffset;

    private Vector2 initialCamPos;

    private void Start()
    {
        initialCamPos.x = freeLook.m_XAxis.Value;
        initialCamPos.y = freeLook.m_YAxis.Value;
    }

    public void ResetCamera()
    {
        freeLook.m_XAxis.Value = initialCamPos.x;
        freeLook.m_YAxis.Value = initialCamPos.y;
        camOffset.m_Offset = new Vector3(0, 0, -1);
    }

    void Update()
    {

        var inputScroll = Input.mouseScrollDelta;
      //  Debug.Log((float)inputScroll.x + "," + (float)inputScroll.y);
        if (inputScroll.y > 0)
        {
           DOVirtual.Float(camOffset.m_Offset.z, camOffset.m_Offset.z + 0.1f, 0.1f, OnTween);

        }
        if (inputScroll.y < 0)
        {
            DOVirtual.Float(camOffset.m_Offset.z, camOffset.m_Offset.z - 0.1f, 0.1f, OnTween);
        }

        if(Input.GetMouseButton(2))
        {
            var mousePosition = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"),0);
            Debug.Log(mousePosition.normalized);
            if(mousePosition.normalized.x > 0.2f)
            {
               camOffset.m_Offset.x-=0.05f;
            }
            if (mousePosition.normalized.x < -0.2f)
            {
                camOffset.m_Offset.x += 0.05f;
            }
            if (mousePosition.normalized.y < 0)
            {
                camOffset.m_Offset.y += 0.05f;
            }

            if (mousePosition.normalized.y > 0)
            {
                camOffset.m_Offset.y -= 0.05f;
            }


        }

        if (!Input.GetMouseButton(1))
        {
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisName = "";
            return;
        }

        freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        freeLook.m_XAxis.m_InputAxisName = "Mouse X";
    }

    private void OnTween(float value)
    {
        camOffset.m_Offset.z = value;

    }
}
