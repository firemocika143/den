using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1LeftUpperArmMovementTest : MonoBehaviour
{
    [Header("Right Upper Arm")]
    public Rigidbody2D rightUpperArmRigidbody;
    public HingeJoint2D rightUpperArmJoint;  // HingeJoint2D on the upper arm (right)
    public float rightUpperArmSpeed = 50f;   // Speed at which upper arm rotates
    public float rightUpperArmMaxAngle = 90f;   // Maximum raising angle for upper arm (positive for right hand)
    public float rightUpperArmMinAngle = -10f;  // Minimum lowering angle for upper arm (negative for right hand)

    [Header("Right Lower Arm")]
    public Rigidbody2D rightLowerArmRigidbody;
    public HingeJoint2D rightLowerArmJoint;  // HingeJoint2D on the lower arm (right)
    public float rightLowerArmSpeed = 50f;   // Speed at which lower arm rotates
    public float rightLowerArmMaxAngle = 60f;  // Maximum raising angle for lower arm (positive for right hand)
    public float rightLowerArmMinAngle = 0f;   // Minimum angle for lower arm (when hand is down, positive for right hand)

    [Header("Sword")]
    public Rigidbody2D swordRigidbody;
    public HingeJoint2D swordJoint;
    public float swordSpeed = 50f;
    public float swordMaxAngle = 30f;
    public float swordMinAngle = 0f;

    private bool raising = true;  // Control whether arm is raising or lowering

    private bool reocvery = false;
    private float recoveryTime = 1.5f;

    void Start()
    {
        // Set limits for both joints to prevent over-rotation
        JointAngleLimits2D rightUpperArmLimits = new JointAngleLimits2D();
        rightUpperArmLimits.max = rightUpperArmMaxAngle;
        rightUpperArmLimits.min = rightUpperArmMinAngle;
        rightUpperArmJoint.limits = rightUpperArmLimits;
        rightUpperArmJoint.useLimits = true;

        JointAngleLimits2D rightLowerArmLimits = new JointAngleLimits2D();
        rightLowerArmLimits.max = rightLowerArmMaxAngle;
        rightLowerArmLimits.min = rightLowerArmMinAngle;
        rightLowerArmJoint.limits = rightLowerArmLimits;
        rightLowerArmJoint.useLimits = true;

        JointAngleLimits2D swordLimits = new JointAngleLimits2D();
        swordLimits.max = swordMaxAngle;
        swordLimits.min = swordMinAngle;
        swordJoint.limits = swordLimits;
        swordJoint.useLimits = true;

        // Make sure motor is enabled on both joints
        rightUpperArmJoint.useMotor = true;
        rightLowerArmJoint.useMotor = true;
        swordJoint.useMotor = true;
    }

    void Update()
    {
        if (!reocvery)
        {
            MoveRightArm();
        }
        else
        {
            
        }
    }

    void MoveRightArm()
    {
        // Get the current angle of both joints
        float rightUpperArmAngle = rightUpperArmJoint.jointAngle;
        float rightLowerArmAngle = rightLowerArmJoint.jointAngle;
        float swordAngle = swordJoint.jointAngle;
        bool needRecovery = false;

        JointMotor2D rightUpperMotor = rightUpperArmJoint.motor;
        JointMotor2D rightLowerMotor = rightLowerArmJoint.motor;
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

            if (swordAngle < swordMaxAngle)
            {
                swordMotor.motorSpeed = swordSpeed;
            }
            else
            {
                swordMotor.motorSpeed = 0;
            }

            // If both arms reach their max angles, switch to lowering
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

            if (swordAngle > swordMinAngle)
            {
                swordMotor.motorSpeed = -swordSpeed;
            }
            else
            {
                swordMotor.motorSpeed = 0;
            }

            // If both arms reach their min angles, switch to raising
            if (rightUpperArmAngle <= rightUpperArmMinAngle && rightLowerArmAngle <= rightLowerArmMinAngle && swordAngle <= swordMinAngle)
            {
                raising = true;
                needRecovery = true;
            }
        }

        // Apply motor settings back to the joints
        rightUpperArmJoint.motor = rightUpperMotor;
        rightLowerArmJoint.motor = rightLowerMotor;
        swordJoint.motor = swordMotor;

        if (needRecovery)
        {
            StartCoroutine(recoveryTimeCount());
        }
    }

    private IEnumerator recoveryTimeCount()
    {
        reocvery = true;
        //rightUpperArmJoint.useMotor = false;
        //rightLowerArmJoint.useMotor = false;
        //swordJoint.useMotor = false;
        rightUpperArmRigidbody.angularDrag = 10000f;
        rightLowerArmRigidbody.angularDrag = 10000f;
        swordRigidbody.angularDrag = 10000f;

        yield return new WaitForSeconds(recoveryTime);

        reocvery = false;
        //rightUpperArmJoint.useMotor = true;
        //rightLowerArmJoint.useMotor = true;
        //swordJoint.useMotor = true;
        rightUpperArmRigidbody.angularDrag = 0.05f;
        rightLowerArmRigidbody.angularDrag = 0.05f;
        swordRigidbody.angularDrag = 0.05f;
    }
}
