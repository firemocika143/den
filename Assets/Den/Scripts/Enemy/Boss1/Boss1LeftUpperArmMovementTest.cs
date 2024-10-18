using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1LeftUpperArmMovementTest : MonoBehaviour
{
    [Header("Right Upper Arm")]
    public Rigidbody2D rightUpperArmRigidbody;
    public HingeJoint2D rightUpperArmJoint;  // HingeJoint2D on the upper arm (right)
    public float rightUpperArmSpeed = 50f;   // Speed at which upper arm rotates
    public float rightUpperArmMaxAngle = 60f;   // Maximum raising angle for upper arm (positive for right hand)
    public float rightUpperArmMinAngle = -10f;  // Minimum lowering angle for upper arm (negative for right hand)
    private RigidbodyConstraints2D rightUpperArmOriginalConstrains;

    [Header("Right Lower Arm")]
    public Rigidbody2D rightLowerArmRigidbody;
    public HingeJoint2D rightLowerArmJoint;  // HingeJoint2D on the lower arm (right)
    public float rightLowerArmSpeed = 50f;   // Speed at which lower arm rotates
    public float rightLowerArmMaxAngle = 120f;  // Maximum raising angle for lower arm (positive for right hand)
    public float rightLowerArmMinAngle = 30f;   // Minimum angle for lower arm (when hand is down, positive for right hand)
    private RigidbodyConstraints2D rightLowerArmOriginalConstrains;

    private bool raising = true;  // Control whether arm is raising or lowering
    

    void Start()
    {
        // Set limits for both joints to prevent over-rotation
        JointAngleLimits2D rightUpperArmLimits = new JointAngleLimits2D();
        rightUpperArmLimits.max = rightUpperArmMaxAngle;
        rightUpperArmLimits.min = rightUpperArmMinAngle;
        rightUpperArmJoint.limits = rightUpperArmLimits;
        rightUpperArmJoint.useLimits = true;
        rightUpperArmOriginalConstrains = rightUpperArmRigidbody.constraints;

        JointAngleLimits2D rightLowerArmLimits = new JointAngleLimits2D();
        rightLowerArmLimits.max = rightLowerArmMaxAngle;
        rightLowerArmLimits.min = rightLowerArmMinAngle;
        rightLowerArmJoint.limits = rightLowerArmLimits;
        rightLowerArmJoint.useLimits = true;
        rightLowerArmOriginalConstrains = rightLowerArmRigidbody.constraints;

        // Make sure motor is enabled on both joints
        rightUpperArmJoint.useMotor = true;
        rightLowerArmJoint.useMotor = true;
    }

    void FixedUpdate()
    {
        MoveRightArm();
    }

    void MoveRightArm()
    {
        // Get the current angle of both joints
        float rightUpperArmAngle = rightUpperArmJoint.jointAngle;
        float rightLowerArmAngle = rightLowerArmJoint.jointAngle;

        JointMotor2D rightUpperMotor = rightUpperArmJoint.motor;
        JointMotor2D rightLowerMotor = rightLowerArmJoint.motor;

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
                //rightUpperArmJoint.useMotor = false;
                //rightUpperArmRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (rightLowerArmAngle < rightLowerArmMaxAngle)
            {
                rightLowerMotor.motorSpeed = rightLowerArmSpeed;
            }
            else
            {
                rightLowerMotor.motorSpeed = 0;
                //rightLowerArmJoint.useMotor = false;
                //rightLowerArmRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            // If both arms reach their max angles, switch to lowering
            if (rightUpperArmAngle >= rightUpperArmMaxAngle && rightLowerArmAngle >= rightLowerArmMaxAngle)
            {
                raising = false;
                //rightUpperArmRigidbody.constraints = rightUpperArmOriginalConstrains;
                //rightLowerArmRigidbody.constraints = rightLowerArmOriginalConstrains;
                //rightUpperArmJoint.useMotor = true;
                //rightLowerArmJoint.useMotor = true;
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
                //rightUpperArmJoint.useMotor = false;
                //rightUpperArmRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (rightLowerArmAngle > rightLowerArmMinAngle)
            {
                rightLowerMotor.motorSpeed = -rightLowerArmSpeed;
            }
            else
            {
                rightLowerMotor.motorSpeed = 0;
                //rightLowerArmJoint.useMotor = false;
                //rightLowerArmRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            // If both arms reach their min angles, switch to raising
            if (rightUpperArmAngle <= rightUpperArmMinAngle && rightLowerArmAngle <= rightLowerArmMinAngle)
            {
                raising = true;
                //rightUpperArmRigidbody.constraints = rightUpperArmOriginalConstrains;
                //rightLowerArmRigidbody.constraints = rightLowerArmOriginalConstrains;
                ////rightUpperArmJoint.useMotor = true;
                //rightLowerArmJoint.useMotor = true;
            }
        }

        //// Explicitly stop the motor if the angles exceed the limits (safety check)
        //if (rightUpperArmAngle >= rightUpperArmMaxAngle || rightUpperArmAngle <= rightUpperArmMinAngle)
        //{
        //    rightUpperMotor.motorSpeed = 0;
        //}

        //if (rightLowerArmAngle >= rightLowerArmMaxAngle || rightLowerArmAngle <= rightLowerArmMinAngle)
        //{
        //    rightLowerMotor.motorSpeed = 0;
        //}

        // Apply motor settings back to the joints
        rightUpperArmJoint.motor = rightUpperMotor;
        rightLowerArmJoint.motor = rightLowerMotor;
    }

    //private bool rotateAutomatic(HingeJoint2D hingeJoint, float motorSpeed, float lowerAngle, float upperAngle, bool rotatingToUpper)
    //{
    //    float currentAngle = hingeJoint.jointAngle;

    //    // 判斷是否達到上限角度，改變旋轉方向
    //    if (rotatingToUpper && currentAngle >= upperAngle - tolerance)
    //    {
    //        rotatingToUpper = false;
    //    }
    //    else if (!rotatingToUpper && currentAngle <= lowerAngle + tolerance)
    //    {
    //        rotatingToUpper = true;
    //    }

    //    // 設置馬達的速度方向
    //    JointMotor2D motor = hingeJoint.motor;
    //    motor.motorSpeed = rotatingToUpper ? motorSpeed : -motorSpeed;
    //    hingeJoint.motor = motor;

    //    // 啟用馬達
    //    hingeJoint.useMotor = true;


    //    return rotatingToUpper;
    //}
}
