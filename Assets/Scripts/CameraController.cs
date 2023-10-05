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

    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisName = "";
            return;
        }

        freeLook.m_YAxis.m_InputAxisName = "Mouse Y";
        freeLook.m_XAxis.m_InputAxisName = "Mouse X";
        var inputScroll = Input.mouseScrollDelta;
        Debug.Log((float)inputScroll.x + "," + (float)inputScroll.y);
        if (inputScroll.y > 0)
        {
            for (int i = 0; i < freeLook.m_Orbits.Length - 1; i++)
            {
                var newFloat = DOVirtual.Float(freeLook.m_Orbits[i].m_Radius, freeLook.m_Orbits[i].m_Radius - 1, 0.1f, OnTween);
            }
        }
        if (inputScroll.y < 0)
        {
            for (int i = 0; i < freeLook.m_Orbits.Length - 1; i++)
            {
                var newFloat = DOVirtual.Float(freeLook.m_Orbits[i].m_Radius, freeLook.m_Orbits[i].m_Radius + 1, 0.1f, OnTween);

            }
        }
    }

    private void OnTween(float value)
    {
        for (int i = 0; i < freeLook.m_Orbits.Length - 1; i++)
        {
            freeLook.m_Orbits[i].m_Radius = value;
            // freeLook.m_Orbits[i].m_Radius.;
        }
    }
}
