using UnityEngine;

public class camerafollow : MonoBehaviour {

    Transform target;

    public float smoothSpeed = 10f;

    public Vector3 offset;

    private void Start()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag("Player") as GameObject;
        target = targetObject.transform;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed*Time.deltaTime);
        transform.position = smoothedPosition;

        //transform.LookAt(target);
    }


}
