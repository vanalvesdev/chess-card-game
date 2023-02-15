using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : MonoBehaviour
{
    public bool isMoving;

    CustomTransform targetPoint;

    Guid guid;


    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isMoving)
        {

            Vector3 rotationNormalizes = NormalizeRotation(transform.rotation.eulerAngles);
            rotationNormalizes = fixNegativeRotation(targetPoint.rotation, rotationNormalizes);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.001f &&
                Vector3.Distance(rotationNormalizes, targetPoint.rotation) < 0.001f &&
                Vector3.Distance(transform.localScale, targetPoint.scale) < 0.001f)
            {
                isMoving = false;
                Guid id = targetPoint.id;
                targetPoint = null;
                gameObject.SendMessage("CompletedMoveCallback", id, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                MoveToTargetPoint();
            }
        }
    }

    

    void MoveToTargetPoint()
    {
        float moveStep = (float)(targetPoint.positionSpeed * Time.deltaTime);
        float rotationStep = (float)(targetPoint.rotationSpeed * Time.deltaTime);
        float scaleStep = (float)(targetPoint.scaleSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position,
                                                targetPoint.position,
                                                moveStep);

        Vector3 fixedRotation = NormalizeRotation(transform.rotation.eulerAngles);

        fixedRotation = fixNegativeRotation(targetPoint.rotation, fixedRotation);

        Vector3 distance = Vector3.MoveTowards(fixedRotation,
                                                NormalizeRotation(targetPoint.rotation),
                                                rotationStep);

        transform.rotation = Quaternion.Euler(distance);

        transform.localScale = Vector3.MoveTowards(transform.localScale,
                                                targetPoint.scale,
                                                scaleStep);
    }

    public static Vector3 fixNegativeRotation(Vector3 pivot, Vector3 rotation)
    {
        if (!Vector3.forward.Equals(pivot.normalized) && rotation.z > 300)
        {
            rotation.z = (360 - rotation.z) * -1;
        }
        return rotation;
    }

    static float roundNear(float number)
    {
        return Mathf.Round(number * 100) / 100;
    }

    public void ToTarget(CustomTransform target)
    {
        targetPoint = target;
        isMoving = true;
    }

    public static Vector3 NormalizeRotation(Vector3 rotation)
    {
        return new Vector3(roundNear(rotation.x) == 0 ? 0 : roundNear(rotation.x),
            roundNear(rotation.y) == 0 ? 0 : roundNear(rotation.y),
            roundNear(rotation.z) == 0 ? 0 : roundNear(rotation.z));
    }
}
