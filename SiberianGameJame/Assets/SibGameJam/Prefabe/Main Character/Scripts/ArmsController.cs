using System;
using UnityEngine;

public class ArmsController : MonoBehaviour
{
    [SerializeField]
    private Arm leftArm;

    [SerializeField]
    private Arm rightArm;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float minimumDistance = 2f;

    private bool _flip;

    public bool Flip => _flip;

    public event Action<bool> Flipped;

    private void Awake()
    {
        leftArm.ArmStateChanged += OnLeftArmStateChanged;
        rightArm.ArmStateChanged += OnRightArmStateChanged;
        
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
            {
                throw new Exception("Camera is not set");
            }
        }
    }

    private void OnLeftArmStateChanged(ArmState state)
    {
        if (rightArm.ArmState != ArmState.Holding) return;

        rightArm.Freeze = false;
        if (state == ArmState.Moving)
        {
            rightArm.Freeze = true;
        }
    }

    private void OnRightArmStateChanged(ArmState state)
    {
        if (leftArm.ArmState != ArmState.Holding) return;

        leftArm.Freeze = false;
        if (state == ArmState.Moving)
        {
            leftArm.Freeze = true;
        }
    }

    private void Update()
    {
        var cp = new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x,
            cam.ScreenToWorldPoint(Input.mousePosition).y);
        
        leftArm.SetCursorPosition(cp);
        rightArm.SetCursorPosition(cp);
        
        CheckFlip(cp);
    }

    private void CheckFlip(Vector2 cp)
    {
        var tUp = new Vector2(transform.up.x, transform.up.y);
        var lp = new Vector2(transform.position.x, transform.position.y);

        var distance = Vector2.Dot(cp - lp, Vector2.Perpendicular(tUp));

        if (!(Mathf.Abs(distance) > minimumDistance)) return;

        if (leftArm.Hand.Grabbed && !leftArm.Hand.Hold || rightArm.Hand.Grabbed && !rightArm.Hand.Hold) return;

        var oldFlip = _flip;
        if (!_flip && distance > 0)
        {
            _flip = true;
        }
        else if (_flip && distance < 0)
        {
            _flip = false;
        }

        if (oldFlip != _flip)
        {
            (leftArm.MouseButton, rightArm.MouseButton) = (rightArm.MouseButton, leftArm.MouseButton);
            (leftArm.Hand.mouseButton, rightArm.Hand.mouseButton) = (rightArm.Hand.mouseButton, leftArm.Hand.mouseButton);
            var xScale = Mathf.Abs(transform.localScale.x);
            var modifier = _flip ? -xScale : xScale;
            var scale = transform.localScale;
            scale.x = modifier;
            transform.localScale = scale;
            Flipped?.Invoke(_flip);
        }
    }
}