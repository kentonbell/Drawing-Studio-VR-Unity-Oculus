using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class BirdController : MonoBehaviour
{
    public LayerMask raycastMask;
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;
    public Transform hitMarkerCube;
    public XRInteractorLineVisual lineVisual;
    public XRRayInteractor rayInteractor;

    private float _gripValue;
    private float _triggerValue;
    private RaycastHit hit;
    private int interactionState = 0;

    void Start()
    {
        hitMarkerCube.gameObject.SetActive(false);
    }

    void Update()
    {
        _gripValue = gripInputActionReference.action.ReadValue<float>();
        _triggerValue = triggerInputActionReference.action.ReadValue<float>();

        // Enable line only when pressing something
        lineVisual.enabled = (_gripValue == 1 || _triggerValue == 1);

        // Switch ray type
        rayInteractor.lineType = (_gripValue == 1) ? XRRayInteractor.LineType.BezierCurve :
        (_triggerValue == 1) ? XRRayInteractor.LineType.StraightLine :
        rayInteractor.lineType;

        if (_triggerValue == 1)
            HandleRaycast();
    }

    void HandleRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 30f, raycastMask) && interactionState == 0)
        {
            GameObject target = hit.transform.gameObject;
            if (target != null && target.CompareTag("Apple"))
            {
                interactionState = 1;
                hitMarkerCube.position = hit.point;
                hitMarkerCube.gameObject.SetActive(true);

                AppleScript deformer = target.GetComponent<AppleScript>();
                if (deformer != null)
                {
                    deformer.DeformAtPoint(hit.point);
                }
            }
        }
        else
        {
            interactionState = 0;
            hitMarkerCube.gameObject.SetActive(false);
        }
    }
}