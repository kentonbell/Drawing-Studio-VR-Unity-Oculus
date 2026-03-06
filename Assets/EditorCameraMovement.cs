using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorCameraMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float fastSpeedMultiplier = 2f;
    public float rotationSpeed = 200f;
    public float zoomSpeed = 10f;

    private Vector3 lastMousePosition;

    private bool enabled = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.M))
            enabled = !enabled;


        if (Input.GetKeyDown(KeyCode.N)) SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart

        if (Input.GetKeyDown(KeyCode.Escape)) //exit
        {
            transform.position = new Vector3(40f, 20f, -23f);
            transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
            Application.Quit();
        }


        if (!enabled) return;

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

//using UnityEngine;

//public class EditorCameraMovement : MonoBehaviour
//{
//    public float movementSpeed = 5f;
//    public float rotationSpeed = 200f;
//    public float zoomSpeed = 10f;

//    private Vector3 lastMousePosition;

//    void Update()
//    {
//        //WASD Movement
//        Vector3 movement = new Vector3();
//        if (Input.GetKey(KeyCode.W))
//            movement += transform.forward;
//        if (Input.GetKey(KeyCode.S))
//            movement -= transform.forward;
//        if (Input.GetKey(KeyCode.A))
//            movement -= transform.right;
//        if (Input.GetKey(KeyCode.D))
//            movement += transform.right;
//        if (Input.GetKey(KeyCode.E))
//            movement += transform.up;
//        if (Input.GetKey(KeyCode.Q))
//            movement -= transform.up;

//        if (movement != Vector3.zero)
//        {
//            transform.Translate(movement.normalized * movementSpeed * Time.deltaTime);
//        }

//        //Mouse Rotation (Pan)
//        if (Input.GetMouseButton(2)) //Middle mouse button
//        {
//            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
//            transform.Rotate(Vector3.up, mouseDelta.x * rotationSpeed * Time.deltaTime);
//            transform.Rotate(-transform.right, mouseDelta.y * rotationSpeed * Time.deltaTime);
//        }

//        //Mouse Rotation (Rotate)
//        if (Input.GetMouseButton(1)) //Right mouse button
//        {
//            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
//            transform.Rotate(Vector3.up, mouseDelta.x * rotationSpeed * Time.deltaTime);
//            transform.Rotate(-transform.right, mouseDelta.y * rotationSpeed * Time.deltaTime);
//        }

//        //Zoom
//        float scrollWheelDelta = Input.GetAxis("Mouse ScrollWheel");
//        if (scrollWheelDelta != 0)
//        {
//            transform.Translate(Vector3.forward * scrollWheelDelta * zoomSpeed * Time.deltaTime);
//        }

//        //Update last mouse position for the next frame
//        lastMousePosition = Input.mousePosition;
//    }
//}
