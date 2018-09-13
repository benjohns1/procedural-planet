using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    public float slowSpeed = 30f;
    public float fastSpeed = 200f;
    public Vector2 mouseSensitivity = new Vector2(100f, 100f);
    public float rollSpeed = 100f;

    private bool runLock = false;

    private Vector3 rotation = Vector3.zero;
    Quaternion originalRotation;

    private void Awake()
    {
        originalRotation = transform.localRotation;
    }

    private void Update()
    {
        // Mouse look and roll rotation
        rotation += GetRotation(mouseSensitivity, rollSpeed);
        transform.localRotation = originalRotation * Quaternion.Euler(-rotation.y, rotation.x, rotation.z);

        // Movement
        if (Input.GetKey(KeyCode.CapsLock))
        {
            runLock = !runLock;
        }
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 movement = moveInput * Time.deltaTime * (runLock || Input.GetKey(KeyCode.LeftShift) ? fastSpeed : slowSpeed);
        transform.Translate(movement);
    }

    private static Vector3 GetRotation(Vector2 mouseSensitivity, float rollSpeed)
    {
        float x = Input.GetAxisRaw("Mouse X") * mouseSensitivity.x;
        float y = Input.GetAxisRaw("Mouse Y") * mouseSensitivity.y;
        float z = ((Input.GetKey(KeyCode.Q) ? 1 : 0) + (Input.GetKey(KeyCode.E) ? -1 : 0)) * rollSpeed;
        return new Vector3(x, y, z) * Time.deltaTime;
    }
}
