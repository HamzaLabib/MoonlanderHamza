using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D landingCollider;
    public SpriteRenderer fireTale;
    public SpriteRenderer explosion;
    public AudioSource rocketSound;

    float speed = 2f;
    public float rotateSpeed = 0.5f;
    public float landingRangeAngle = 5f;
    public float landingRangeSpeed = 0.2f;

    bool isComplete = false;

    //public float maxSpeed = 3f;
    //public float acceleration = 2f;
    //public float decleration = 2f;

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
            {
                rb.AddRelativeForce(new Vector2(0, speed), ForceMode2D.Force);
                fireTale.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
                rocketSound.Play();
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                fireTale.gameObject.SetActive(false);
                rocketSound.Stop();
            }
        }
        if (isComplete && Input.anyKey)
        {
            //gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //isComplete = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }



    }
    /*float SpeedChanging(float currentSpeed)
    {
        if(currentSpeed < maxSpeed)
            currentSpeed = (currentSpeed - acceleration) * Time.deltaTime;
        if(currentSpeed > decleration)
            currentSpeed = (currentSpeed - decleration) * Time.deltaTime;

        return currentSpeed;
    }*/

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
        explosion.gameObject.SetActive(true);
        explosion.transform.position = this.transform.position;
    }

    void Win()
    {
        Debug.Log("Winner");
    }
}
