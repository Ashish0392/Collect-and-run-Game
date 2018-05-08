using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Transform target;
    public bool pursuit = false;
    public float triggerRadius = 2f;
    public float range = 10f;
    public float fov = 60f;
    public float coolDownTime = 10f;
    Vector3 chasePos;
    Vector3 targetPrevPos;
    float dist;
    RaycastHit hit;
    public NavMeshAgent agent;
    
    float countDown;

    

	// Use this for initialization
	void Start () {

        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponentInChildren<Light>().enabled = false;
        chasePos = new Vector3();
        targetPrevPos = new Vector3();
        
        countDown = coolDownTime;

  
      
        
	}
	
	// Update is called once per frame
	void Update () {

        
        //checking if pursuit is already live
        if (pursuit != true)
        {
            //checking if target triggered 
            if (dist < triggerRadius)
            {
                //pursuit live
                pursuit = true;

                //activate this
                Activate(true);
                return;
            }
            return;
        }

        //if target in sight
        if (LineOfSight())
        {
            //if (dist < range)
            chasePos = target.position;
            Chase();
            targetPrevPos = target.position;
        }
        else {
            countDown -= Time.deltaTime;
            if(countDown > 0f)
            {
                Search(targetPrevPos, 3.0f);
            }
            if (countDown < 0f) {
                pursuit = false;
                countDown = coolDownTime;
                Activate(false);
            }

        }

           
	}

    private void FixedUpdate()
    {
        //active distance btw this and target
        dist = Vector3.Distance(this.transform.position, target.position);
    }

    private void Activate(bool state)
    {
        if (state)
        {
            Debug.LogError("Activated");
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.GetComponentInChildren<Light>().enabled = true;
            this.gameObject.GetComponent<AudioSource>().Play();
            //Chase();
        }
        else {
            Debug.LogError("DeActivated");
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponentInChildren<Light>().enabled = false;
            agent.GetComponent<NavMeshAgent>().enabled = false;
        }
            


    }

    private bool LineOfSight() {
        //code below is faulty linecast not working
        //hit = 
            bool check = Physics.Linecast(transform.position, target.position, 2);
        Debug.Log(check);
        //check if target is in  sight and send true or false accordingly
        Debug.Log(Vector3.Angle(target.position - transform.position, transform.forward));
        if (Vector3.Angle(target.position - transform.position, transform.forward) <= fov && check == false) {
            return true;
        }
        return false;

    }

    private void Chase() {

        //Debug.LogError("Chasing");
        if(agent != null)
        agent.SetDestination(chasePos);

    }

    private void Search(Vector3 pos, float range)
    {
        Vector3 offset = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        pos += offset;

        agent.SetDestination(pos);

        //Debug.LogError("searching");



    }
}

