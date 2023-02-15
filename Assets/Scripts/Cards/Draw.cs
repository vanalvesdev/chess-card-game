using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{

    Card card;
    CustomTransform drawFlipSpot;
    CustomTransform drawShowCaseSpot;
    CustomTransform drawOnHand;
    MoveCard move;

    public Transform previousDrawOnHand;


    //Draw animator transform values
    Vector3[] drawFlipSpotValues = {new Vector3(6.7f, -1.4f, 0),new Vector3(0, 90, 0), new Vector3(1.6f, 1.6f, 1.0f)};
    Vector3[] drawShowCaseSpotValues = { new Vector3(6.0f, 0f, 0), new Vector3(0, 0, 0), new Vector3(1.6f, 1.6f, 1.0f) };
    Vector3[] drawOnHandValues = { new Vector3(0f, -4.3f, 0), new Vector3(0, 0, 0), new Vector3(0.8f, 0.8f, 1.0f) };

    // Start is called before the first frame update
    void Start()
    {
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
        if(previousDrawOnHand != null)
        {
            drawOnHandValues[0] = new Vector3(previousDrawOnHand.position.x + card.onHandPositionDelta, previousDrawOnHand.position.y, previousDrawOnHand.position.z);
            
            Vector4 euler = MoveCard.fixNegativeRotation(Vector3.back, MoveCard.NormalizeRotation(previousDrawOnHand.rotation.eulerAngles));

            Vector4 rotation = new Vector4(euler.x, euler.y, roundUp(euler.z) - card.onHandRotationDelta, euler.w);

            drawOnHandValues[1] = rotation;
        }

        drawFlipSpot = BuildTransform(drawFlipSpotValues[0], drawFlipSpotValues[1], drawFlipSpotValues[2], 0.5f);

        drawShowCaseSpot = BuildTransform(drawFlipSpot.position,
            drawShowCaseSpotValues[0],
            drawFlipSpot.rotation,
            drawShowCaseSpotValues[1], 
            drawFlipSpot.scale,
            drawShowCaseSpotValues[2],
            0.5f);
        
        drawOnHand = BuildTransform(drawShowCaseSpot.position,
            drawOnHandValues[0],
            drawShowCaseSpot.rotation,
            drawOnHandValues[1],
            drawShowCaseSpot.scale,
            drawOnHandValues[2],
            1.0f);
    }

    CustomTransform BuildTransform(Vector3 position, Vector4 rotation, Vector3 scale, float time)
    {

        return CustomTransform.builder()
            .position(position)
            .rotation(rotation)
            .scale(scale)
            .positionSpeed(card.calculateSpeed(transform.position, position, time))
            .rotationSpeed(card.calculateSpeed(transform.rotation.eulerAngles, rotation, time))
            .scaleSpeed(card.calculateSpeed(transform.localScale, scale, time))
            .Build();
    }

    CustomTransform BuildTransform(Vector3 startPosition,
        Vector3 targetPosition,
        Vector4 startRotation,
        Vector4 targetRotation,
        Vector3 startScale,
        Vector3 targetScale, 
        float time)
    {

        return CustomTransform.builder()
            .position(targetPosition)
            .rotation(targetRotation)
            .scale(targetScale)
            .positionSpeed(card.calculateSpeed(startPosition, targetPosition, time))
            .rotationSpeed(card.calculateSpeed(startRotation, targetRotation, time))
            .scaleSpeed(card.calculateSpeed(startScale, targetScale, time))
            .Build();
    }

    float roundUp(float number)
    {
        return Mathf.Ceil(number * 100) / 100;
    }

    void DrawAnimation()
    {
        move.ToTarget(drawFlipSpot);
    }

    public void CompletedMoveCallback(Guid guid)
    {
        if (drawFlipSpot != null && guid.Equals(drawFlipSpot.id))
        {
            card.Flip();
            move.ToTarget(drawShowCaseSpot);
        }

        else if (drawShowCaseSpot != null && guid.Equals(drawShowCaseSpot.id))
        {
            StartCoroutine(MoveToHand());
        }

        else if (drawOnHand != null && guid.Equals(drawOnHand.id))
        {
            card.IsDrawed();
            this.enabled = false;
        }
    }

    IEnumerator MoveToHand()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        move.ToTarget(drawOnHand);
    }
}
