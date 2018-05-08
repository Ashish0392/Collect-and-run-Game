using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public Transform locator;
    public float repeatTime = 4.0f;
    float timer = 0.0f;
    Transform temp;

	// Use this for initialization
	void Start () {

        //repeated call to spawnlocator
        InvokeRepeating("SpawnLocator", 0.0f, repeatTime);

		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
	}

    // to spawn location beacon
    void SpawnLocator() {
        if(temp != null)
        GameObject.Destroy(temp.gameObject);

        if (locator != null)
        {
            //Debug.Log("particle");
           
           temp = Instantiate(locator, this.transform.position, Quaternion.identity);

          
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            //add 1 crystal to player Inventory
            Debug.Log("SCORE");
            other.transform.GetComponent<playercontroller>().pickUps += 1;
            if (temp != null)
                GameObject.Destroy(temp.gameObject);

            GameObject.Destroy(this.gameObject);
        }
    }


}
