using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed;
    [SerializeField] KeyCode     upKey       = KeyCode.W;
    [SerializeField] KeyCode     downKey     = KeyCode.S;
 
    [SerializeField] float zLimit = 20f;


    Rigidbody rb;
    float halfLength;
    public float GetHalfLength()=> halfLength;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation   = RigidbodyInterpolation.Interpolate;
        var col = GetComponent<Collider>();
        halfLength = col != null ? col.bounds.extents.z : 0.5f;
    }

    void FixedUpdate()
    {
        float inputZ = GetPlayerInput();

        float newZ = Mathf.Clamp(
            rb.position.z + inputZ * Time.fixedDeltaTime,
            -zLimit + halfLength,
             zLimit - halfLength);
 
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }

    float GetPlayerInput()
    {
        float dir = 0f;
        if (Input.GetKey(upKey))   dir += 1f;
        if (Input.GetKey(downKey)) dir -= 1f;
        return dir * moveSpeed;
    }
}
