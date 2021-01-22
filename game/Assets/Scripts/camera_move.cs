using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    public GameObject target; 
    public float moveSpeed;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
