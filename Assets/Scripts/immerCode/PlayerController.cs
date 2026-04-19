using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Time.deltaTime * moveSpeed * Vector3.forward);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Time.deltaTime * moveSpeed *Vector3.back);
        }
    }
}
