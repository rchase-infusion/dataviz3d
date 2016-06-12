using System;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    public class MoveCamera : MonoBehaviour
    {
        [Tooltip("Speed at which the camera looks left/right")]
        public float HorizontalLookSpeed = 50;
        [Tooltip("Speed at which the camera looks up/down")]
        public float VerticalLookSpeed = 50;

        private void Start()
        { }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                LookLeftRight();
                LookUpDown();
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