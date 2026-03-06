using System.Collections.Generic;
using UnityEngine;

public class TipScript : MonoBehaviour
{
    public Transform tip;
    public TrailRenderer brushStroke; // Assign a TrailRenderer prefab
    private TrailRenderer currentBrushStroke;

    private AnimateHandController aController = null;
    private Stack<TrailRenderer> previousStroke = new Stack<TrailRenderer>();

    private Color32 currentColor = Color.gray;

    private int drawCount = 0;
    private bool drawColor = true;

    public float increaseRate = .1f;

    public float brushSize = 1f;

    

    void Start()
    {
        if (tip != null && tip.parent != null)
        {
            tip.parent.GetComponent<Renderer>().material.color = currentColor;
        }
    }

    private void Update()
    {
        brushStroke.widthMultiplier = brushSize;


        // Grab and start painting
        if (aController != null && drawColor)
        {
            if (aController.GetGripValue() == 1 && aController.GetTriggerValue() == 1)
            {
                currentBrushStroke = Instantiate(brushStroke, tip.position, tip.rotation, tip);
                currentBrushStroke.material.color = currentColor;
                previousStroke.Push(currentBrushStroke);

                drawCount = 1;
                drawColor = false;
            }
        }

        // Trigger released while holding brush (stop painting)
        if (!drawColor)
        {
            if (aController != null && drawCount == 1)
            {
                if (aController.GetGripValue() == 1 && aController.GetTriggerValue() != 1)
                {
                    if (currentBrushStroke != null && currentBrushStroke.transform.parent != null)
                    {
                        currentBrushStroke.transform.parent = null;
                    }
                    drawCount = 0;
                }
            }

            // Trigger pressed again (start a new stroke)
            if (aController != null && drawCount == 0)
            {
                if (aController.GetGripValue() == 1 && aController.GetTriggerValue() == 1)
                {
                    currentBrushStroke = Instantiate(brushStroke, tip.position, tip.rotation, tip);
                    currentBrushStroke.material.color = currentColor;
                    previousStroke.Push(currentBrushStroke);

                    drawCount = 1;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("VRHand")) return;

        AnimateHandController aHandController = other.gameObject.GetComponentInChildren<AnimateHandController>();
        if (aHandController != null)
        {
            aController = aHandController;
            drawColor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("VRHand")) return;

        if (currentBrushStroke != null && currentBrushStroke.transform.parent != null)
        {
            currentBrushStroke.transform.parent = null;
        }

        aController = null;
        drawCount = 0;
    }
    public void SetColor(Color color)
    {
        currentColor = color;

        //if (tip != null && tip.parent != null)
        {
            tip.parent.GetComponent<Renderer>().material.color = currentColor;
        }
    }

    public void Delete()
    {
        if (previousStroke.Count > 0)
        {
            TrailRenderer lastStroke = previousStroke.Pop();
            Destroy(lastStroke.gameObject);
        }
    }

    public void DeleteAll()
    {
        while (previousStroke.Count > 0)
        {
            TrailRenderer lastStroke = previousStroke.Pop();
            Destroy(lastStroke.gameObject);
        }
    }

    public void plus()
    {
        brushSize += increaseRate;
    }

    public void minus()
    {
        if (brushSize <= 0) return;
        brushSize -= increaseRate;
    }

}