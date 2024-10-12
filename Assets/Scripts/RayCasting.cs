using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    [SerializeField] GameObject obj;
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();
    }

    private void SetTarget()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("clicked");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("casted");
                obj.transform.position = hit.point;
            }
        }
    }
}
