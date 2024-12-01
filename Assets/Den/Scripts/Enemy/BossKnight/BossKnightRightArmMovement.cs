using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightRightArmMovement : MonoBehaviour
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
    public float rightLowerArmMinAngle = 30f;   // Minimum angle for lower arm (when hand is down, positive for right hand)
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
    private float recoveryTime = 0.1f;
    //private float raisingStunTime = 0.5f;

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
        float swordAngle = swordJoint.jointAngle;

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

            if (rightUpperArmAngle >= rightUpperArmMaxAngle && rightLowerArmAngle >= rightLowerArmMaxAngle && swordAngle >= swordMaxAngle)
            {
                raising = false;
                StartCoroutine(recoveryTimeCount());
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
                rightUpperArmRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }

            if (rightLowerArmAngle > rightLowerArmMinAngle)
            {
                rightLowerMotor.motorSpeed = -rightLowerArmSpeed;
            }
            else
            {
                rightLowerMotor.motorSpeed = 0;
                if (rightUpperMotor.motorSpeed == 0)
                {
                    rightLowerArmRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }

            if (swordAngle > swordMinAngle)
            {
                swordMotor.motorSpeed = -swordSpeed;
            }
            else
            {
                swordMotor.motorSpeed = 0;
                if (rightUpperMotor.motorSpeed == 0 && rightLowerMotor.motorSpeed == 0)
                {
                    swordRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }

            // If both arms and the sword reach their min angles, switch to raising
            if (rightUpperArmAngle <= rightUpperArmMinAngle && rightLowerArmAngle <= rightLowerArmMinAngle && swordAngle <= swordMinAngle)
            {
                raising = true;
                freezeMoving();
            }
        }

        // Apply motor settings back to the joints
        rightUpperArmJoint.motor = rightUpperMotor;
        rightLowerArmJoint.motor = rightLowerMotor;
        swordJoint.motor = swordMotor;

    }

    private IEnumerator recoveryTimeCount()
    {
        reocvery = true;
        freezeMoving();

        yield return new WaitForSeconds(recoveryTime);

        startMoving();
        reocvery = false;
    }

    public void freezeMoving()
    {
        swordRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        rightUpperArmRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        rightLowerArmRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void startMoving()
    {
        swordRigidbody.constraints = RigidbodyConstraints2D.None;
        rightUpperArmRigidbody.constraints = RigidbodyConstraints2D.None;
        rightLowerArmRigidbody.constraints = RigidbodyConstraints2D.None;
    }

    public bool isRaising() 
    {  
        return raising; 
    }
}
