using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour {


    public float speed = 10.0F;
     
    public float rotationSpeed = 100.0F;
    Rigidbody self;
    Quaternion targetRotation;
    public int pickUps = 0;
    int count;

    // Use this for initialization
    void Start () {
        self = this.GetComponent<Rigidbody>();
        count = pickUps;
	}
	
	// Update is called once per frame
	void Update () {
        
        //convert input from 2 axis to vector3
        Vector3 input = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));

        if (input != Vector3.zero) {
             targetRotation = Quaternion.LookRotation(input);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Vector3 fwd = transform.TransformDirection(new Vector3(1,0,0));
            Vector3 targetVelocity = fwd * speed;

            if (transform.rotation == targetRotation) {
                self.AddForce(targetVelocity, ForceMode.Force);
            }
            
        }
       


       
       
       
        

       

    }

    private void FixedUpdate()
    {
        if(pickUps > count)
        Debug.Log("crystal = " + pickUps);
        count = pickUps;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
            SceneManager.LoadScene(0);

    }









}
