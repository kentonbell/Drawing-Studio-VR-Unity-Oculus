using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;

    [Header("Ray Visual & Interaction")]
    public XRInteractorLineVisual lineVisual;
    public XRRayInteractor rayInteractor;
    public LayerMask raycastMask;

    private float _gripValue;
    private float _triggerValue;
    private RaycastHit hit;

    private void Start()
    {
             
    }

    void Update()
    {
        _gripValue = gripInputActionReference.action.ReadValue<float>();
        _triggerValue = triggerInputActionReference.action.ReadValue<float>();

        // Show or hide the line
        lineVisual.enabled = (_gripValue == 1 || _triggerValue == 1);

        // Change ray type
        if (_gripValue == 1)
            rayInteractor.lineType = XRRayInteractor.LineType.BezierCurve;

        if (_triggerValue == 1)
            rayInteractor.lineType = XRRayInteractor.LineType.StraightLine;

        // Raycast on trigger press
        if (_triggerValue == 1)
        {
            HandleRaycast();
        }
    }

    void HandleRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 30f, raycastMask))
        {
            GameObject obj = hit.transform.gameObject;

            if (obj.CompareTag("Apple"))
            {
                Debug.Log("Ray hit an apple: " + obj.name);

                // Optional: change color
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                    renderer.material.color = Color.green;

                // Optional: scale it up slightly
                obj.transform.localScale *= 1.1f;

                // Optional: add logic here to "collect" or destroy the apple
                // Destroy(obj);
            }
        }
    }
}

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    public GameObject testCube;
    public LayerMask raycastMask;
    public InputActionReference triggerActionReference;
    public LineType lineType = LineType.BezierCurves;

    private int count = 0;
    private bool lineVisualsEnabled = true;

    void Start()
    {
        testCube.gameObject.SetActive(false);
    }

    void Update()
    {
        float triggerValue = triggerActionReference.action.ReadValue<float>();

        if (triggerValue > 0.1f)
        {
            if (!lineVisualsEnabled)
            {
                lineVisualsEnabled = true;

                if (lineType == LineType.BezierCurves || lineType == LineType.StraightLines)
                {
                    HandleRaycast();
                }
            }
        }
        else
        {
            lineVisualsEnabled = false;
        }
    }

    void HandleRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 300f, raycastMask) && count == 0)
        {
            if (hit.collider.tag == "Apple")
            {
                count = 1;
                testCube.gameObject.SetActive(true);
                testCube.transform.position = hit.point;
            }
        }

        if (count == 1)
        {
            testCube.gameObject.SetActive(false);
            count = 0;
        }
    }
}

public enum LineType
{
    BezierCurves,
    StraightLines
}*/