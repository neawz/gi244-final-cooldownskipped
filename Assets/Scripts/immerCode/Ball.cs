using NUnit.Framework;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb ;
    public float startSpeed;
    void Start()
    {
       Launch();
    }

    private void Launch()
    {
        int x = Random.Range(0,2);
        int z = Random.Range(0,2);
        if (x == 0)
        {
            x = -1;
        }
        if (z == 0)
        {
            z = -1;
        }

        rb.linearVelocity = new Vector3(x*startSpeed, 0, z*startSpeed);
    }
}
