using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{

    Card card;
    MoveCard.CustomTransform drawFirstStep;
    MoveCard.CustomTransform drawSecondStep;
    MoveCard.CustomTransform handPoint;
    MoveCard move;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(7.4f, -2.8f, 0);
        card = GetComponent<Card>();
        move = GetComponent<MoveCard>();
        BuildTransforms();
        DrawAnimation();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void BuildTransforms()
    {

        drawFirstStep = BuildTransform(new Vector3(6.7f, -1.4f, 0),
            new Vector3(0, 90, 0), new Vector3(1.6f, 1.6f, 1.0f), 0.5f);

        drawSecondStep = BuildTransform(drawFirstStep.position,
            new Vector3(6.0f, 0f, 0),
            drawFirstStep.rotation,
            new Vector3(0, 0, 0), 
            drawFirstStep.scale,
            new Vector3(1.6f, 1.6f, 1.0f),
            0.5f);

        handPoint = BuildTransform(drawSecondStep.position,
            new Vector3(0, -4.3f, 0),
            drawSecondStep.rotation,
            new Vector3(0, 0, 0),
            drawSecondStep.scale,
            new Vector3(0.8f, 0.8f, 1.0f),
            1.0f);
    }

    MoveCard.CustomTransform BuildTransform(Vector3 position, Vector4 rotation, Vector3 scale, float time)
    {

        return MoveCard.CustomTransform.builder()
            .position(position)
            .rotation(rotation)
            .scale(scale)
            .positionSpeed(card.calculateSpeed(transform.position, position, time))
            .rotationSpeed(card.calculateSpeed(transform.rotation.eulerAngles, rotation, time))
            .scaleSpeed(card.calculateSpeed(transform.localScale, scale, time))
            .Build();
    }

    MoveCard.CustomTransform BuildTransform(Vector3 startPosition,
        Vector3 targetPosition,
        Vector4 startRotation,
        Vector4 targetRotation,
        Vector3 startScale,
        Vector3 targetScale, 
        float time)
    {

        return MoveCard.CustomTransform.builder()
            .position(targetPosition)
            .rotation(targetRotation)
            .scale(targetScale)
            .positionSpeed(card.calculateSpeed(startPosition, targetPosition, time))
            .rotationSpeed(card.calculateSpeed(startRotation, targetRotation, time))
            .scaleSpeed(card.calculateSpeed(startScale, targetScale, time))
            .Build();
    }

    void DrawAnimation()
    {
        move.ToTarget(drawFirstStep);
    }

    public void CompletedMoveCallback(Guid guid)
    {
        if (drawFirstStep != null && guid.Equals(drawFirstStep.id))
        {
            card.Flip();
            move.ToTarget(drawSecondStep);
        }

        else if (drawSecondStep != null && guid.Equals(drawSecondStep.id))
        {
            StartCoroutine(MoveToHand());
        }

        else if (handPoint != null && guid.Equals(handPoint.id))
        {
            card.IsDrawed();
            this.enabled = false;
        }
    }

    IEnumerator MoveToHand()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        move.ToTarget(handPoint);
    }
}
