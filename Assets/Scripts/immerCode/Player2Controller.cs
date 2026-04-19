using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Time.deltaTime * moveSpeed * Vector3.forward);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Time.deltaTime * moveSpeed *Vector3.back);
        }
    }
}
