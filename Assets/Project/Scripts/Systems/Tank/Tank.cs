using System;
using UnityEngine;

namespace Project.Scripts.Systems.Tank
{
    [Serializable]
    public class Tank
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _frontLeftMesh;
        [SerializeField] private WheelCollider _frontLeftCollider;
        [SerializeField] private Transform _rearLeftMesh;
        [SerializeField] private WheelCollider _rearLeftCollider;
        [SerializeField] private Transform _frontRightMesh;
        [SerializeField] private WheelCollider _frontRightCollider;
        [SerializeField] private Transform _rearRightMesh;
        [SerializeField] private WheelCollider _rearRightCollider;
        [Header("Config")]
        [SerializeField] private int _maxSpeed;
        [SerializeField] private float _motorTorque = 2000;

        private float _speed;
        
        public float Speed => _speed;
        public int MaxSpeed => _maxSpeed;
        
        private void RotateWheel(WheelCollider collider, Transform transform)
        {
            collider.GetWorldPose(out var position, out var rotation);
 
            transform.rotation = rotation;
            transform.position = position;
        }

        private void Drive(float speed, WheelCollider rear, WheelCollider front)
        {
            var torque = (_motorTorque * speed);

            _speed = _rigidbody.velocity.magnitude * 3.6f;
            
            if (_speed < _maxSpeed)
            {
                _rearLeftCollider.motorTorque = torque;
                _frontLeftCollider.motorTorque = torque;   
            }
        }

        public void DriveLeft(float speed)
        {
            Drive(speed, _rearLeftCollider, _frontLeftCollider);
            
            RotateWheel(_rearLeftCollider, _rearLeftMesh);
            RotateWheel(_frontLeftCollider, _frontLeftMesh);
        }

        public void DriveRight(float speed)
        {
            Drive(speed, _rearLeftCollider, _frontLeftCollider);
            
            RotateWheel(_frontRightCollider, _frontRightMesh);
            RotateWheel(_rearRightCollider, _rearRightMesh);
        }
        
        public void EnableMovement()
        {
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
        }

        public void DisableMovement()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;
        }
    }
}