using UnityEngine;

namespace Seshihiko.Systems.Automobile
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _frontLeftMesh;
        [SerializeField] private WheelCollider _frontLeftCollider;
        [SerializeField] private Transform _frontRightMesh;
        [SerializeField] private WheelCollider _frontRightCollider;
        [SerializeField] private Transform _rearLeftMesh;
        [SerializeField] private WheelCollider _rearLeftCollider;
        [SerializeField] private Transform _rearRightMesh;
        [SerializeField] private WheelCollider _rearRightCollider;
        [Header("Config")]
        [SerializeField] private int _maxSpeed;
        [SerializeField] private float _motorTorque = 2000;
        [SerializeField] private float _brakeStreng = 350;
        [SerializeField] private float _maxAngle = 30;

        private float _speed;

        public float Speed => _speed;
        public int MaxSpeed => _maxSpeed;
        
        private void RotateWheel(WheelCollider collider, Transform transform)
        {
            collider.GetWorldPose(out var position, out var rotation);
 
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Drive(Vector2 direction)
        {
            var torque = (_motorTorque * direction.y);
            var angle = _maxAngle * direction.x;
            
            _speed = _rigidbody.velocity.magnitude * 3.6f;
            
            if (_speed < _maxSpeed)
            {
                _rearLeftCollider.motorTorque = torque;
                _rearRightCollider.motorTorque = torque;   
            }

            _frontLeftCollider.steerAngle = angle;
            _frontRightCollider.steerAngle = angle;

            RotateWheel(_frontLeftCollider, _frontLeftMesh);
            RotateWheel(_frontRightCollider, _frontRightMesh);
            RotateWheel(_rearLeftCollider, _rearLeftMesh);
            RotateWheel(_rearRightCollider, _rearRightMesh);
        }

        public void Breack(float streng)
        {
            if(streng <= 0.5f)
                return;
            
            _rearLeftCollider.brakeTorque = _brakeStreng * streng;
            _rearRightCollider.brakeTorque = _brakeStreng * streng;
            _frontLeftCollider.brakeTorque = _brakeStreng * streng;
            _frontRightCollider.brakeTorque = _brakeStreng * streng;
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