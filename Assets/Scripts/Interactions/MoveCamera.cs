using System;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class MoveCamera : MonoBehaviour
    {
        [Tooltip("Speed at which the camera looks left/right.")]
        public float HorizontalLookSpeed = 50;
        [Tooltip("Speed at which the camera looks up/down.")]
        public float VerticalLookSpeed = 50;
        [Tooltip("Speed at which the camera strafes left/right.")]
        public float StrafeSpeed = 10;
        [Tooltip("Speed at which the camera moves forward/backward.")]
        public float MoveSpeed = 10;

        private void Start()
        { }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                LookLeftRight();
                LookUpDown();
            }

            MoveForwardBackward();
            MoveLeftRight();
        }

        private void MoveForwardBackward()
        {
            var moveInput = Input.GetAxis("Vertical");
            if (Math.Abs(moveInput) > 0.1)
            {
                transform.position += transform.forward * moveInput * MoveSpeed * Time.deltaTime;
            }
        }

        private void MoveLeftRight()
        {
            var strafeInput = Input.GetAxis("Horizontal");
            if (Math.Abs(strafeInput) > 0.1)
            {
                transform.position += transform.right * strafeInput * StrafeSpeed * Time.deltaTime;
            }
        }

        private void LookLeftRight()
        {
            var horizontalMouseInput = Input.GetAxis("Mouse X");
            if (Math.Abs(horizontalMouseInput) > 0.1)
                transform.Rotate(Vector3.up * Mathf.Sign(horizontalMouseInput) * HorizontalLookSpeed * Time.deltaTime);
        }

        private void LookUpDown()
        {
            var verticalMouseInput = Input.GetAxis("Mouse Y");
            if (Math.Abs(verticalMouseInput) > 0.1)
                transform.Rotate(Vector3.right * -Mathf.Sign(verticalMouseInput) * HorizontalLookSpeed * Time.deltaTime);
        }
    }
}