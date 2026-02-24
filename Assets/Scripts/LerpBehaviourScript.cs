using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBehaviourScript : MonoBehaviour
{

    public Transform target;
    public float moveLerpSpeed = 5f;
    public float rotationLerpSpeed = 10f;

    void Update()
    {
        if (target == null) return;

        // Move
        transform.position = Vector3.Lerp(
            transform.position,
            target.position,
            moveLerpSpeed * Time.deltaTime
        );

        // Face target (Z rotation)
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion targetRot = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRot,
            rotationLerpSpeed * Time.deltaTime
        );
    }
}
