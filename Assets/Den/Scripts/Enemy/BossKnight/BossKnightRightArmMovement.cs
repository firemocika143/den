using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1LeftUpperArmMovementTest : MonoBehaviour
{
    [Header("Right Upper Arm")]
    public Rigidbody2D rightUpperArmRigidbody;
    public HingeJoint2D rightUpperArmJoint;  // HingeJoint2D on the upper arm (right)
    public float rightUpperArmMaxAngle = 90f;   // Maximum raising angle for upper arm (positive for right hand)
    public float rightUpperArmMinAngle = -15f;  // Minimum lowering angle for upper arm (negative for right hand)
    public float rightUpperArmSpeed = 50f;   // Speed at which upper arm rotates

    [Header("Right Lower Arm")]
    public Rigidbody2D rightLowerArmRigidbody;
    public HingeJoint2D rightLowerArmJoint;  // HingeJoint2D on the lower arm (right)
    public float rightLowerArmMaxAngle = 60f;  // Maximum raising angle for lower arm (positive for right hand)
    public float rightLowerArmMinAngle = 0f;   // Minimum angle for lower arm (when hand is down, positive for right hand)
    public float rightLowerArmSpeed = 50f;   // Speed at which lower arm rotates

    [Header("Left Upper Arm")]
    public Rigidbody2D leftUpperArmRigidbody;
    public HingeJoint2D leftUpperArmJoint;  // HingeJoint2D on the upper arm (right)
    public float leftUpperArmMaxAngle = 70f;   // Maximum raising angle for upper arm (positive for right hand)
    public float leftUpperArmMinAngle = -15f;  // Minimum lowering angle for upper arm (negative for right hand)

    private float leftUpperArmSpeed;   // Speed at which upper arm rotates

    [Header("Left Lower Arm")]
    public Rigidbody2D leftLowerArmRigidbody;
    public HingeJoint2D leftLowerArmJoint;  // HingeJoint2D on the lower arm (right)
    public float leftLowerArmMaxAngle = 40f;  // Maximum raising angle for lower arm (positive for right hand)
    public float leftLowerArmMinAngle = 0f;   // Minimum angle for lower arm (when hand is down, positive for right hand)

    //private float leftLowerArmSpeed;   // Speed at which lower arm rotates

    [Header("Sword")]
    public Rigidbody2D swordRigidbody;
    public HingeJoint2D swordJoint;
    public float swordMaxAngle = 30f;
    public float swordMinAngle = 0f;
    public float swordSpeed = 50f;

    private bool raising = true;  // Control whether arm is raising or lowering

    private bool reocvery = false;
    private float recoveryTime = 1.5f;

    void Start()
    {
        //leftUpperArmSpeed = (leftUpperArmMaxAngle - leftUpperArmMinAngle) / ((rightUpperArmMaxAngle - rightUpperArmMinAngle) / rightUpperArmSpeed);
        //leftLowerArmSpeed = (leftLowerArmMaxAngle - leftLowerArmMinAngle) / ((rightLowerArmMaxAngle - rightLowerArmMinAngle) / rightLowerArmSpeed);
        
        // Set limits for both joints to prevent over-rotation
        //JointAngleLimits2D rightUpperArmLimits = new JointAngleLimits2D();
        //rightUpperArmLimits.max = rightUpperArmMaxAngle;
        //rightUpperArmLimits.min = rightUpperArmMinAngle;
        //rightUpperArmJoint.limits = rightUpperArmLimits;
        //rightUpperArmJoint.useLimits = true;

        //JointAngleLimits2D rightLowerArmLimits = new JointAngleLimits2D();
        //rightLowerArmLimits.max = rightLowerArmMaxAngle;
        //rightLowerArmLimits.min = rightLowerArmMinAngle;
        //rightLowerArmJoint.limits = rightLowerArmLimits;
        //rightLowerArmJoint.useLimits = true;

        JointAngleLimits2D leftUpperArmLimits = new JointAngleLimits2D();
        leftUpperArmLimits.max = leftUpperArmMaxAngle;
        leftUpperArmLimits.min = leftUpperArmMinAngle;
        leftUpperArmJoint.limits = leftUpperArmLimits;
        leftUpperArmJoint.useLimits = true;

        JointAngleLimits2D leftLowerArmLimits = new JointAngleLimits2D();
        leftLowerArmLimits.max = leftLowerArmMaxAngle;
        leftLowerArmLimits.min = leftLowerArmMinAngle;
        leftLowerArmJoint.limits = leftLowerArmLimits;
        leftLowerArmJoint.useLimits = true;


        JointAngleLimits2D swordLimits = new JointAngleLimits2D();
        swordLimits.max = swordMaxAngle;
        swordLimits.min = swordMinAngle;
        swordJoint.limits = swordLimits;
        swordJoint.useLimits = true;

        rightUpperArmRigidbody.angularDrag = 0.01f;
        rightLowerArmRigidbody.angularDrag = 0.01f;
        leftUpperArmRigidbody.angularDrag = 0.01f;
        leftLowerArmRigidbody.angularDrag = 0.01f;
        swordRigidbody.angularDrag = 0.01f;

        // Make sure motor is enabled on both joints
        rightUpperArmJoint.useMotor = true;
        rightLowerArmJoint.useMotor = true;
        //leftUpperArmJoint.useMotor = true;
        //leftLowerArmJoint.useMotor = true;
        swordJoint.useMotor = true;
    }

    void Update()
    {
        if (!reocvery)
        {
            MoveRightArm();
        }
    }

    void MoveRightArm()
    {
        // Get the current angle of both joints
        float rightUpperArmAngle = rightUpperArmJoint.jointAngle;
        float rightLowerArmAngle = rightLowerArmJoint.jointAngle;
        //float leftUpperArmAngle = leftUpperArmJoint.jointAngle;
        //float leftLowerArmAngle = leftLowerArmJoint.jointAngle;
        float swordAngle = swordJoint.jointAngle;
        bool needRecovery = false;

        JointMotor2D rightUpperMotor = rightUpperArmJoint.motor;
        JointMotor2D rightLowerMotor = rightLowerArmJoint.motor;
        //JointMotor2D leftUpperMotor = leftUpperArmJoint.motor;
        //JointMotor2D leftLowerMotor = leftLowerArmJoint.motor;
        JointMotor2D swordMotor = swordJoint.motor;

        if (raising)
        {
            // Move both arms upwards (positive angles for right hand)
            if (rightUpperArmAngle < rightUpperArmMaxAngle)  // Check positive angle for raising the right arm
            {
                rightUpperMotor.motorSpeed = rightUpperArmSpeed;  // Positive speed for clockwise rotation (right hand)
            }
            else
            {
                rightUpperMotor.motorSpeed = 0;  // Stop when max angle reached
            }

            if (rightLowerArmAngle < rightLowerArmMaxAngle)
            {
                rightLowerMotor.motorSpeed = rightLowerArmSpeed;
            }
            else
            {
                rightLowerMotor.motorSpeed = 0;
            }

            //if (leftUpperArmAngle < leftUpperArmMaxAngle)  // Check positive angle for raising the right arm
            //{
            //    leftUpperMotor.motorSpeed = leftUpperArmSpeed;  // Positive speed for clockwise rotation (right hand)
            //}
            //else
            //{
            //    leftUpperMotor.motorSpeed = 0;  // Stop when max angle reached
            //}

            //if (leftLowerArmAngle < leftLowerArmMaxAngle)
            //{
            //    leftLowerMotor.motorSpeed = leftLowerArmSpeed;
            //}
            //else
            //{
            //    leftLowerMotor.motorSpeed = 0;
            //}

            if (swordAngle < swordMaxAngle)
            {
                swordMotor.motorSpeed = swordSpeed;
            }
            else
            {
                swordMotor.motorSpeed = 0;
            }

            // If both arms reach their max angles, switch to lowering
            //if (rightUpperArmAngle >= rightUpperArmMaxAngle && rightLowerArmAngle >= rightLowerArmMaxAngle && leftUpperArmAngle >= leftUpperArmMaxAngle && leftLowerArmAngle >= leftLowerArmMaxAngle && swordAngle >= swordMaxAngle)
            //{
            //    raising = false;
            //}
            if (rightUpperArmAngle >= rightUpperArmMaxAngle && rightLowerArmAngle >= rightLowerArmMaxAngle && swordAngle >= swordMaxAngle)
            {
                raising = false;
            }
        }
        else
        {
            // Move both arms downwards (negative angles for right hand)
            if (rightUpperArmAngle > rightUpperArmMinAngle)  // Lowering back to negative angle
            {
                rightUpperMotor.motorSpeed = -rightUpperArmSpeed;  // Negative speed for counterclockwise rotation (right hand)
            }
            else
            {
                rightUpperMotor.motorSpeed = 0;
            }

            if (rightLowerArmAngle > rightLowerArmMinAngle)
            {
                rightLowerMotor.motorSpeed = -rightLowerArmSpeed;
            }
            else
            {
                rightLowerMotor.motorSpeed = 0;
            }

            //if (leftUpperArmAngle > leftUpperArmMinAngle)  // Lowering back to negative angle
            //{
            //    leftUpperMotor.motorSpeed = -leftUpperArmSpeed;  // Negative speed for counterclockwise rotation (right hand)
            //}
            //else
            //{
            //    leftUpperMotor.motorSpeed = 0;
            //}

            //if (leftLowerArmAngle > leftLowerArmMinAngle)
            //{
            //    leftLowerMotor.motorSpeed = -leftLowerArmSpeed;
            //}
            //else
            //{
            //    leftLowerMotor.motorSpeed = 0;
            //}

            if (swordAngle > swordMinAngle)
            {
                swordMotor.motorSpeed = -swordSpeed;
            }
            else
            {
                swordMotor.motorSpeed = 0;
            }

            // If both arms reach their min angles, switch to raising
            //if (rightUpperArmAngle <= rightUpperArmMinAngle && rightLowerArmAngle <= rightLowerArmMinAngle && leftUpperArmAngle <= leftUpperArmMinAngle && leftLowerArmAngle <= leftLowerArmMinAngle && swordAngle <= swordMinAngle)
            //{
            //    raising = true;
            //    needRecovery = true;
            //}
            if (rightUpperArmAngle <= rightUpperArmMinAngle && rightLowerArmAngle <= rightLowerArmMinAngle && swordAngle <= swordMinAngle)
            {
                raising = true;
                needRecovery = true;
            }
        }

        // Apply motor settings back to the joints
        rightUpperArmJoint.motor = rightUpperMotor;
        rightLowerArmJoint.motor = rightLowerMotor;
        //leftUpperArmJoint.motor = leftUpperMotor;
        //leftLowerArmJoint.motor = leftLowerMotor;
        swordJoint.motor = swordMotor;

        if (needRecovery)
        {
            StartCoroutine(recoveryTimeCount());
        }
    }

    private IEnumerator recoveryTimeCount()
    {
        reocvery = true;
        //JointAngleLimits2D rightUpperArmLimits = new JointAngleLimits2D();
        //rightUpperArmLimits.max = rightUpperArmMinAngle;
        //rightUpperArmLimits.min = rightUpperArmMinAngle;
        //rightUpperArmJoint.limits = rightUpperArmLimits;
        //rightUpperArmJoint.useLimits = true;

        //JointAngleLimits2D rightLowerArmLimits = new JointAngleLimits2D();
        //rightLowerArmLimits.max = rightLowerArmMinAngle;
        //rightLowerArmLimits.min = rightLowerArmMinAngle;
        //rightLowerArmJoint.limits = rightLowerArmLimits;
        //rightLowerArmJoint.useLimits = true;

        //JointAngleLimits2D swordLimits = new JointAngleLimits2D();
        //swordLimits.max = swordMaxAngle;
        //swordLimits.min = swordMinAngle;
        //swordJoint.limits = swordLimits;
        //swordJoint.useLimits = true;

        swordRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        rightUpperArmRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        rightLowerArmRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        rightUpperArmRigidbody.angularDrag = 100000000f;
        rightLowerArmRigidbody.angularDrag = 100000000f;
        leftUpperArmRigidbody.angularDrag = 100000000f;
        leftLowerArmRigidbody.angularDrag = 100000000f;
        swordRigidbody.angularDrag = 100000000f;

        yield return new WaitForSeconds(recoveryTime);

        reocvery = false;
        rightUpperArmJoint.useLimits = false;
        rightLowerArmJoint.useLimits = false;
        swordJoint.useLimits = false;

        swordRigidbody.constraints = RigidbodyConstraints2D.None;
        rightUpperArmRigidbody.constraints = RigidbodyConstraints2D.None;
        rightLowerArmRigidbody.constraints = RigidbodyConstraints2D.None;

        rightUpperArmRigidbody.angularDrag = 0.01f;
        rightLowerArmRigidbody.angularDrag = 0.01f;
        leftUpperArmRigidbody.angularDrag = 0.01f;
        leftLowerArmRigidbody.angularDrag = 0.01f;
        swordRigidbody.angularDrag = 0.01f;
    }
}
