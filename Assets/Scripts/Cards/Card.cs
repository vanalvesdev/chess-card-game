using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Card : MonoBehaviour
{

    [SerializeField]
    protected int life;

    [SerializeField]
    protected int attackPower;

    [SerializeField]
    string cardName;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Sprite cardBack;

    [SerializeField]
    Sprite cardFront;

    [SerializeField]
    bool onHand;

    [SerializeField]
    float speedInHand;

    new SpriteRenderer renderer;

    public MoveCard move;

    MoveCard.CustomTransform targetPoint;

    MoveCard.CustomTransform startPoint;

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public bool IsAlive()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        renderer = GetComponent<SpriteRenderer>();
        move = GetComponent<MoveCard>();
        
        renderer.sprite = cardBack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        if (onHand && !move.isMoving)
        {
            Vector3 euler = MoveCard.NormalizeRotation(transform.rotation.eulerAngles);

            Vector3 rotation = Vector3.zero;

            Vector3 position = new Vector3(transform.position.x, -1.6f, 0);

            Vector3 scale = new Vector3(1.4f, 1.4f, 1f);

            if (startPoint == null)
            {
                startPoint = MoveCard.CustomTransform.builder()
                .position(transform.position)
                .rotation(MoveCard.NormalizeRotation(transform.rotation.eulerAngles))
                .scale(transform.localScale)
                .Build();
            }

            MoveCard.CustomTransform target = MoveCard.CustomTransform.builder()
            .position(position)
            .rotation(rotation)
            .scale(scale)
            .positionSpeed(calculateSpeed(transform.position, position, 0.3f))
            .rotationSpeed(calculateSpeed(MoveCard.fixNegativeRotation(rotation, euler), rotation, 0.3f))
            .scaleSpeed(calculateSpeed(transform.localScale, scale, 0.3f))
            .Build();

            move.ToTarget(target);
        }
    }

    void OnMouseExit()
    {
        if (onHand && !move.isMoving)
        {
            Vector3 euler = MoveCard.NormalizeRotation(transform.rotation.eulerAngles);

            startPoint.positionSpeed = calculateSpeed(transform.position, startPoint.position, 0.3f);
            startPoint.rotationSpeed = calculateSpeed(MoveCard.fixNegativeRotation(startPoint.rotation, euler),
                                        startPoint.rotation, 0.3f);
            startPoint.scaleSpeed = calculateSpeed(transform.localScale, startPoint.scale, 0.3f);

            move.ToTarget(startPoint);
        }
    }

    public void Flip()
    {
        renderer.sprite = cardFront;
    }

    public void IsDrawed()
    {
        onHand = true;
    }

    public void MoveToLeft()
    {
        if (!move.isMoving)
        {
            Vector3 euler = MoveCard.NormalizeRotation(transform.rotation.eulerAngles);

            Vector3 rotation = new Vector4(euler.x, euler.y, euler.z + 7);

            if (rotation.z == roundNear(360)) rotation.z = 0;

            Vector3 position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);

            targetPoint = MoveCard.CustomTransform.builder()
                .position(position)
                .rotation(rotation)
                .scale(transform.localScale)
                .positionSpeed(calculateSpeed(transform.position, position, speedInHand))
                .rotationSpeed(calculateSpeed(MoveCard.fixNegativeRotation(rotation, euler), rotation, speedInHand))
                .scaleSpeed(0)
                .Build();

            move.ToTarget(targetPoint);
        }
    }

    public void MoveToRight()
    {
        if (!move.isMoving)
        {
            Vector4 euler = MoveCard.fixNegativeRotation(Vector3.back, MoveCard.NormalizeRotation(transform.rotation.eulerAngles));

            Vector4 rotation = new Vector4(euler.x, euler.y, roundUp(euler.z) - 7, euler.w);

            Vector3 position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);

            targetPoint = MoveCard.CustomTransform.builder()
                .position(position)
                .rotation(rotation)
                .scale(transform.localScale)
                .positionSpeed(calculateSpeed(transform.position, position, speedInHand))
                .rotationSpeed(calculateSpeed(euler, rotation, speedInHand))
                .scaleSpeed(0)
                .Build();

            move.ToTarget(targetPoint);
        }
    }

    public void CompletedMoveCallback(Guid guid)
    {
        if (targetPoint != null && guid.Equals(targetPoint.id))
        {
            startPoint = MoveCard.CustomTransform.builder().Clone(targetPoint);
        }
    }

    public float calculateSpeed(Vector3 startPoint, Vector3 finishPoint, float time)
    {
        return roundUp(Vector3.Distance(startPoint, finishPoint) / time);
    }

    float roundUp(float number)
    {
        return Mathf.Ceil(number * 100) / 100;
    }

    float roundDown(float number) {
        return Mathf.Floor(number * 100) / 100;
    }

    float roundNear(float number) {
        return Mathf.Round(number * 100) / 100;
    }
}
