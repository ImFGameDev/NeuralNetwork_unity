using UnityEngine;

namespace Seshihiko.Systems.Entity.Components
{
    public class Movement
    {
        private CharacterController _characterController;
        private Transform _transform;
        private Vector3 _moveVector;
        private float _speedMovement; 
        private float _angularSpeed;
        private float _gravityForce;

        public Movement(CharacterController controller, Transform transform, float speedMovement, float angularAngularSpeed)
        {
            _characterController = controller;
            _transform = transform;
            _speedMovement = speedMovement;
            _angularSpeed = angularAngularSpeed;
        }
        
        private void Gravity(float delta)
        {
            if (!_characterController.isGrounded)
            {
                _gravityForce -= -Physics.gravity.y * delta;
            }
            else _gravityForce = -1f;
        }
        
        public void Move(Vector2 value, float delta)
        {
            _moveVector = _transform.right * value.x + _transform.forward * value.y;
        
            var direction = _moveVector * _speedMovement;
            direction.y = _gravityForce;
        
            _characterController.Move(direction * delta);
            Gravity(delta);
        }

        public void Look(Vector2 value)
        {
            _transform.Rotate(new Vector3(0, value.x * _angularSpeed, 0));
        }
    }
}