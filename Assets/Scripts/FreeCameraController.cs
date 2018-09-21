using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("Speed Settings")]
    public float speed = 30f;
    public float fastSpeed = 150f;
    public float rollSpeed = 80f;
    public float fastRollSpeed = 150f;
    public Vector2 mouseSensitivity = new Vector2(100f, 100f);

    [Header("Key Bindings")]
    public string horizontalAxis = "Horizontal";
    public string forwardAxis = "Vertical";
    public string xAxis = "Mouse X";
    public string yAxis = "Mouse Y";
    public KeyCode rollLeft = KeyCode.Q;
    public KeyCode rollRight = KeyCode.E;
    public KeyCode haltKey = KeyCode.Space;
    public KeyCode fastKey = KeyCode.LeftShift;
    public KeyCode fastLockKey = KeyCode.CapsLock;

    private bool fastLock = false;
    private bool halt = false;

    private void Update()
    {
        if (Input.GetKeyDown(haltKey))
        {
            halt = !halt;
        }
        if (halt)
        {
            return;
        }
        if (Input.GetKeyDown(fastLockKey))
        {
            fastLock = !fastLock;
        }
        bool fast = fastLock ^ Input.GetKey(fastKey);


        // Mouse look and roll rotation
        Vector3 rotation = GetRotation(mouseSensitivity, (fast ? fastRollSpeed : rollSpeed), xAxis, yAxis, rollLeft, rollRight);
        transform.rotation *= Quaternion.Euler(-rotation.y, rotation.x, rotation.z);

        // Movement
        Vector3 moveInput = new Vector3(Input.GetAxisRaw(horizontalAxis), 0, Input.GetAxisRaw(forwardAxis));
        Vector3 movement = moveInput * Time.deltaTime * (fast ? fastSpeed : speed);
        transform.Translate(movement);
    }

    private static Vector3 GetRotation(Vector2 mouseSensitivity, float rollSpeed, string xAxis, string yAxis, KeyCode rollLeft, KeyCode rollRight)
    {
        float x = Input.GetAxisRaw(xAxis) * mouseSensitivity.x;
        float y = Input.GetAxisRaw(yAxis) * mouseSensitivity.y;
        float z = ((Input.GetKey(rollLeft) ? 1 : 0) + (Input.GetKey(rollRight) ? -1 : 0)) * rollSpeed;
        return new Vector3(x, y, z) * Time.deltaTime;
    }
}
