using UnityEngine;
using UnityEngine.Serialization;

public class JointLimitsCorrector : MonoBehaviour
{
    [SerializeField]
    private ArmsController armsController;

    private HingeJoint2D _joint;
    private JointAngleLimits2D _initialLimits;

    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
        if (_joint != null)
        {
            _initialLimits = _joint.limits;
            armsController.Flipped += OnFlipped;
        }
    }

    private void OnFlipped(bool flip)
    {
        if (!flip)
        {
            _joint.limits = _initialLimits;
        }
        else
        {
            _joint.limits = new JointAngleLimits2D
            {
                min = -_initialLimits.min,
                max = -_initialLimits.max,
            };
        }
    }
}