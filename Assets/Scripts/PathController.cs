using System.Collections.Generic;
using UnityEngine;
public class PathController : MonoBehaviour
{
    [SerializeField]
    public PathManager pathManager;

    List<Waypoint> thePath;
    Waypoint target;

    public float MoveSpeed;
    public float RotateSpeed;

    public Animator animator;
    bool isWalking;


    // Start is called before the firsn t frame update
    void Start()
    {
        isWalking = false;
        animator.SetBool("isWalking", isWalking);
        
        thePath = pathManager.GetPath();
        if (thePath != null && thePath.Count > 0)
        {
            // set starting target to the first waypoint
            target = thePath[0];
        }
    }
    void rotateTowardsTarget()
    {
        float stepSize = RotateSpeed * Time.deltaTime;

        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f); 
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void moveForward()
    {
        float stepSize = Time.deltaTime * MoveSpeed;
        float distanceToTarget = Vector3.Distance(transform.position, target.pos); 
        if (distanceToTarget < stepSize)
        {
            // we will overshoot the target,
            // so we should do something smarter here
            return;
        }
        // take a step forward
        Vector3 moveDir = Vector3.forward;
        transform.Translate(moveDir * stepSize);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            // toggle if any key is pressed
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
        }
        
        if (isWalking)
        {
            rotateTowardsTarget();
            moveForward();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // switch to next target
        target = pathManager.GetNextTarget();
    }
}
