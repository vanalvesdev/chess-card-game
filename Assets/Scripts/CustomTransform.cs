using System;
using UnityEngine;

public class CustomTransform
{
    public Vector3 position { get; set; }
    public Vector4 rotation { get; set; }
    public Vector3 scale { get; set; }

    public float positionSpeed { get; set; }

    public float rotationSpeed { get; set; }

    public float scaleSpeed { get; set; }

    public Guid id { get; }

    private CustomTransform(
        Vector3 _position,
        float _positionSpeed,
        Vector4 _rotation,
        float _rotationSpeed,
        Vector3 _scale,
        float _scaleSpeed
    )
    {
        id = Guid.NewGuid();
        position = _position;
        rotation = _rotation;
        scale = _scale;
        positionSpeed = _positionSpeed;
        rotationSpeed = _rotationSpeed;
        scaleSpeed = _scaleSpeed;
    }

    public static Builder builder()
    {
        return new Builder();
    }

    public class Builder
    {
        private Vector3 _position;
        private Vector4 _rotation;
        private Vector3 _scale;
        private float _positionSpeed = 0;
        private float _rotationSpeed = 0;
        private float _scaleSpeed = 0;

        public Builder position(Vector3 position)
        {
            _position = position;
            return this;
        }

        public Builder rotation(Vector4 rotation)
        {
            _rotation = rotation;
            return this;
        }

        public Builder scale(Vector3 scale)
        {
            _scale = scale;
            return this;
        }

        public Builder positionSpeed(float positionSpeed)
        {
            _positionSpeed = positionSpeed;
            return this;
        }

        public Builder rotationSpeed(float rotationSpeed)
        {
            _rotationSpeed = rotationSpeed;
            return this;
        }

        public Builder scaleSpeed(float scaleSpeed)
        {
            _scaleSpeed = scaleSpeed;
            return this;
        }

        public CustomTransform Build()
        {
            return new CustomTransform(_position, _positionSpeed, _rotation, _rotationSpeed, _scale, _scaleSpeed);
        }

        public CustomTransform Clone(CustomTransform original)
        {
            return new CustomTransform(
                original.position,
                original.positionSpeed,
                original.rotation,
                original.rotationSpeed,
                original.scale,
                original.scaleSpeed);
        }
    }
}
