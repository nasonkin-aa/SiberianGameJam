using System;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 100f;

    [SerializeField]
    private float torqueForce = 100f;

    [SerializeField]
    private float maxTorqueForce = 100f;

    [SerializeField]
    private int mouseButton;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private ArmPart upperArm;

    [SerializeField]
    private ArmPart lowerArm;

    [SerializeField]
    private Rigidbody2D constraint;

    [SerializeField]
    private Grab handGrab;

    [SerializeField]
    private ArmsController armsController;

    public Grab Hand => handGrab;

    private ArmState _armState;

    public int MouseButton
    {
        get => mouseButton;
        set => mouseButton = value;
    }

    public ArmState ArmState
    {
        get => _armState;
        private set
        {
            if (_armState == value) return;
            _armState = value;

            switch (_armState)
            {
                case ArmState.Moving:
                    upperArm.Body.totalTorque = 0;
                    break;
                case ArmState.Idle:
                    Freeze = false;
                    break;
            }

            upperArm.Balancer.Active = _armState == ArmState.Idle;
            lowerArm.Balancer.Active = _armState == ArmState.Idle;

            ArmStateChanged?.Invoke(_armState);
        }
    }

    public bool Flip { get; set; }
    public bool Freeze { get; set; }
    public event Action<ArmState> ArmStateChanged;

    private float _upperArmLength;
    private float _lowerArmLength;

    private float _upperArmTargetAngle;
    private float _lowerArmTargetAngle;

    private Vector2 _lastCursorPosition;


    private void Start()
    {
        armsController.Flipped += Fliper;
        _upperArmLength = Vector3.Distance(upperArm.Transform.position, lowerArm.Transform.position);
        _lowerArmLength = Vector3.Distance(lowerArm.Transform.position, constraint.transform.position);
    }

    private void Update()
    {
        if (handGrab.Grabbed)
        {
            ArmState = ArmState.Holding;
        }
        else if (Input.GetMouseButton(mouseButton))
        {
            ArmState = ArmState.Moving;
        }
        else
        {
            ArmState = ArmState.Idle;
        }
    }

    private void FixedUpdate()
    {
        if (ArmState == ArmState.Idle) return;


        if (!Freeze)
        {
            CalculateAngles(_lastCursorPosition);
        }

        MoveJoints();
    }

    public void SetCursorPosition(Vector2 cursorPosition)
    {
        if (Freeze) return;
        _lastCursorPosition = cursorPosition;
    }

    private void MoveJoints()
    {
        // if (ArmState == ArmState.Holding)
        // {
        //     var currentAngle = NormalizeAngle(upperArm.Body.rotation - body.rotation);
        //     var angleDifference = NormalizeAngle(body.rotation + _upperArmTargetAngle - currentAngle);
        //     var torque = Mathf.Clamp(angleDifference * torqueForce, -maxTorqueForce, maxTorqueForce);
        //
        //     // Применяем вращающий момент к upperArm
        //     upperArm.Body.AddTorque(torque, ForceMode2D.Force);
        // }
        // else
        // {
        //     
        // }

        // upperArm.Body.MoveRotation(Mathf.LerpAngle(upperArm.Body.rotation, body.rotation + _upperArmTargetAngle,
        //     moveSpeed * Time.fixedDeltaTime));
        
        upperArm.Body.MoveRotation(body.rotation + _upperArmTargetAngle);
        lowerArm.Body.MoveRotation(upperArm.Body.rotation + _lowerArmTargetAngle);

        // lowerArm.Body.MoveRotation(Mathf.LerpAngle(lowerArm.Body.rotation, upperArm.Body.rotation + _lowerArmTargetAngle,
        //     moveSpeed * Time.fixedDeltaTime));
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            angle -= 360;
        else if (angle <= -180)
            angle += 360;
        return angle;
    }

    // Inverse Kinematic
    private void CalculateAngles(Vector2 targetPosition)
    {
        var upperArmPos = new Vector2(upperArm.Transform.position.x, upperArm.Transform.position.y);

        var length2 = Mathf.Max(Vector2.Distance(upperArmPos, targetPosition), 0.01f);

        // Угол между Joint0 и Target
        Vector2 diff = targetPosition - upperArmPos;
        diff = Flip ? -diff : diff;
        var atan = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        // Достижима ли цель?
        // Если нет, то мы растягиваемся как можно дальше
        if (_upperArmLength + _lowerArmLength < length2)
        {
            _upperArmTargetAngle = atan;
            _lowerArmTargetAngle = 0f;
        }
        else
        {
            var cosAngle0 =
                ((length2 * length2) + (_upperArmLength * _upperArmLength) - (_lowerArmLength * _lowerArmLength)) /
                (2 * length2 * _upperArmLength);
            cosAngle0 = Mathf.Clamp(cosAngle0, -1.0f, 1.0f); // Зажимаем значение между -1 и 1
            var angle0 = Mathf.Acos(cosAngle0) * Mathf.Rad2Deg;

            var cosAngle1 =
                ((_lowerArmLength * _lowerArmLength) + (_upperArmLength * _upperArmLength) - (length2 * length2)) /
                (2 * _lowerArmLength * _upperArmLength);
            cosAngle1 = Mathf.Clamp(cosAngle1, -1.0f, 1.0f); // Зажимаем значение между -1 и 1
            var angle1 = Mathf.Acos(cosAngle1) * Mathf.Rad2Deg;

            _upperArmTargetAngle = atan - angle0;
            _lowerArmTargetAngle = 180f - angle1;
            
            if (Flip)
            {
                _upperArmTargetAngle = 2 * atan - _upperArmTargetAngle;
                _lowerArmTargetAngle = -_lowerArmTargetAngle;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (upperArm != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(upperArm.Body.position, 0.1f);
        }

        if (lowerArm != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(lowerArm.Body.position, 0.1f);
        }
    }

    private void Fliper (bool flip)
    {
        Flip = flip;
    }

    public void Disable()
    {
        armsController.Flipped -= Fliper;
    }
}

[Serializable]
public class ArmPart
{
    [field: SerializeField]
    public Rigidbody2D Body { get; private set; }

    [field: SerializeField]
    public Balance Balancer { get; private set; }

    public Transform Transform => Body.transform;
}

public enum ArmState
{
    Idle,
    Moving,
    Holding
}