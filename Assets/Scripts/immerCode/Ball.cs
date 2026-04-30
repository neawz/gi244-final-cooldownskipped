using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SettingsManagement;

public class Ball : MonoBehaviour
{

    [Header("Speed")]
    public float initialSpeed;

    
    public int score = 1;
    public  bool player1;
    public  bool player2;
    public bool hasScored = false;

    Rigidbody rb ;
    float currentSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Launch();
    }

    void Launch()
    {
        currentSpeed = initialSpeed;
 
        // สุ่มทิศในแกน XZ ± 30°
        float angle  = Random.Range(-30f, 30f) * Mathf.Deg2Rad;
        float dirX   = Random.value > 0.5f ? 1f : -1f;
        Vector3 dir  = new Vector3(dirX * Mathf.Cos(angle), 0f, Mathf.Sin(angle)).normalized;
 
        rb.linearVelocity = dir * currentSpeed;
    }
    IEnumerator LaunchCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Launch();
        
    }
    /*public void LaunchTheBall()
    {
        StartCoroutine(LaunchCoroutine());
    }*/
    public void ResetBall()
    {
        rb.linearVelocity    =  Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        hasScored = false;
        StartCoroutine(LaunchCoroutine());
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Player2")) return;

        var player = collision.gameObject.GetComponent<PlayerController>();
        float halfZ     = player ? player.GetHalfLength() : 0.5f;
        float hitOffset = Mathf.Clamp(
            (transform.position.z - collision.transform.position.z) / halfZ, -1f, 1f);
 
        float bounceAngle = hitOffset * 50f * Mathf.Deg2Rad;
        float dirX        = rb.linearVelocity.x > 0 ? -1f : 1f;
        Vector3 newDir    = new Vector3(dirX * Mathf.Cos(bounceAngle), 0f, Mathf.Sin(bounceAngle)).normalized;

        rb.linearVelocity = newDir * currentSpeed;


        if(collision.gameObject.CompareTag("Player"))
        {
            player1 = true;
            player2 = false;
        }
        if(collision.gameObject.CompareTag("Player2"))
        {
            player1 = false;
            player2 = true;
        }
    }
}
