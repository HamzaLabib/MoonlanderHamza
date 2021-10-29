using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D landingCollider;

    public float speed = 3f;
    public float rotateSpeed = 0.5f;
    public float landingRangeAngle = 5f;
    public float landingRangeSpeed = 0.2f;

    bool isComplete = false;


    private void FixedUpdate()
    {
        GetController();
    }

    void GetController()
    {
        if (!isComplete)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                rb.AddTorque(rotateSpeed, ForceMode2D.Force);
            if (Input.GetKey(KeyCode.RightArrow))
                rb.AddTorque(-rotateSpeed, ForceMode2D.Force);
            if (Input.GetKey(KeyCode.UpArrow))
                rb.AddRelativeForce(new Vector2(0, speed), ForceMode2D.Force);
        }
        if (isComplete && Input.anyKey)
        {
            //gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //isComplete = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isComplete = true;
        
        if (collision.otherCollider == landingCollider)
        {
            if (collision.collider.tag == "LandingPad")
                LandingCheck();
            else
                Explosion();
        }
        else
            Explosion();
    }

    void LandingCheck()
    {
        if (rb.velocity.sqrMagnitude < landingRangeSpeed)
        {
            if (transform.rotation.eulerAngles.z > -landingRangeAngle && transform.rotation.eulerAngles.z < landingRangeAngle)
                Win();
            else
                Explosion();
        }
        else
            Explosion();
    }

    void Explosion()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Win()
    {

    }
}
