using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExit : MonoBehaviour
{


    public float movementSpeed = 7f;
    public float fastSpeedMultiplier = 4f;
    public float rotationSpeed = 23f;
    public float zoomSpeed = 100f;

    private Vector3 lastMousePosition;

    public bool tapPforCamera = true;
    private bool enabled = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        //if (Input.GetKeyDown(KeyCode.P) && tapPforCamera)
        //{
        //    enabled = !enabled;
        //}

        if (tapPforCamera)
            EditorCamera();




        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
            Exit();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            New();
        }
    }













    private void Exit()
    {
        
            Debug.Log("Bye!!");
            Application.Quit();
        
    }

    private void New()
    {
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }





    private void EditorCamera()
    {
        //if (!enabled) return;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? movementSpeed * fastSpeedMultiplier : movementSpeed;

        // WASD + EQ Movement
        Vector3 movement = new Vector3();
        if (Input.GetKey(KeyCode.W))
            movement += transform.forward;
        if (Input.GetKey(KeyCode.S))
            movement -= transform.forward;
        if (Input.GetKey(KeyCode.A))
            movement -= transform.right;
        if (Input.GetKey(KeyCode.D))
            movement += transform.right;
        if (Input.GetKey(KeyCode.E))
            movement += transform.up;
        if (Input.GetKey(KeyCode.Q))
            movement -= transform.up;

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.X)) //global up
            movement += Vector3.up;

        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.Z)) //global down
            movement -= Vector3.up;

        if (movement != Vector3.zero)
        {
            transform.position += movement.normalized * currentSpeed * Time.deltaTime;
        }

        // Mouse Rotation (Right Mouse Button - Free Look)
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            float pitch = -mouseDelta.y * rotationSpeed * Time.deltaTime;
            float yaw = mouseDelta.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, yaw, Space.World);
            transform.Rotate(Vector3.right, pitch, Space.Self);
        }

        // Zoom
        float scrollWheelDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelDelta != 0)
        {
            transform.Translate(Vector3.forward * scrollWheelDelta * zoomSpeed * Time.deltaTime);
        }

        lastMousePosition = Input.mousePosition;
    }


}
