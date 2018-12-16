using System;
using UnityEngine;

    public class BB8 : MonoBehaviour
    {
        public Rigidbody Head;
        public Transform headT;
        public float HeadDownScale;
        public float HeadTurnScale;
        public float HeadUpScale;
        [SerializeField] private float m_MovePower = 5; // The force added to the ball to move it.
        [SerializeField] private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
        [SerializeField] private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        [SerializeField] private float m_JumpPower = 2; // The force added to the ball when it jumps.

        private const float k_GroundRayLength = 0.75f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;


        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            // Set the maximum angular velocity.
            m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocity;
        }

        void LateUpdate()
        {
            Head.transform.position = transform.position;
        }

        void FixedUpdate()
        {
            Head.position = transform.position;
            Head.AddTorque(m_Rigidbody.angularVelocity* HeadDownScale);
            HeadUp(Head, m_Rigidbody, HeadUpScale);
            // Point head in direction of movement
            var angle = Vector3.Dot(Head.transform.right, m_Rigidbody.velocity.normalized) * (Mathf.Rad2Deg * Time.fixedDeltaTime)* HeadTurnScale;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);            
            Head.rotation *= q;            
        }
        
        public void Move(Vector3 moveDirection, bool jump)
        {
            // If using torque to rotate the ball...
            if (m_UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x)*m_MovePower);
            }
            else
            {
                // Otherwise add force in the move direction.
                m_Rigidbody.AddForce(moveDirection*m_MovePower);
            }

            // If on the ground and jump is pressed...
            if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
            {
                // ... add force in upwards.
                m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Acceleration);
            }
        }

        void HeadUp(Rigidbody head, Rigidbody body, float scale)
        {
            var target = Vector3.up;
            var current = head.transform.forward;
            // Axis of rotation
            var x = Vector3.Cross(current, target);
            var theta = Mathf.Asin(x.magnitude);
            // Change in angular velocity
            var w = x.normalized * (theta / Time.fixedDeltaTime * scale);
            // Current rotation in world space
            var q = head.rotation * head.inertiaTensorRotation;
            // Transform to local space
            w = Quaternion.Inverse(q) * w;
            // Calculate torque and convert back to world space
            var T = q * Vector3.Scale(head.inertiaTensor, w);
            head.AddTorque(T, ForceMode.Impulse);            
        }
    }