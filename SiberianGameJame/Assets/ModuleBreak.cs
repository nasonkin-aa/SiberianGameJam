using System;
using UnityEngine;
using System.Linq;

public class ModuleBreak : MonoBehaviour
{
    public GameObject moduleForDestroy;
    public static int speedLimitForBreak = 15;
    public Rigidbody2D hipsRB;
    private Rigidbody2D _rb;
    private int prevSpeed;
    public event Action moduleBreak;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && prevSpeed > speedLimitForBreak)
            moduleBreak?.Invoke();
    }

    public void ModuleSetActive (bool state)
    {
        var copy = Instantiate(moduleForDestroy);

        copy.transform.position = moduleForDestroy.transform.position;
        copy.transform.rotation = moduleForDestroy.transform.rotation;
        copy.transform.localScale = moduleForDestroy.transform.localScale;

        copy.GetComponentsInChildren<HingeJoint2D>().ToList().ForEach(component => component.enabled = state);
        copy.GetComponentsInChildren<Arm>().ToList().ForEach(component => { component.Disable(); component.enabled = state;});
        copy.GetComponentsInChildren<Balance>().ToList().ForEach(component => component.enabled = state);
        copy.GetComponentsInChildren<JointLimitsCorrector>().ToList().ForEach(component => component.enabled = state);
        copy.GetComponentsInChildren<Grab>().ToList().ForEach(component => component.enabled = state);
        copy.GetComponentsInChildren<FixedJoint2D>().ToList().ForEach(component => component.enabled = state);

        moduleForDestroy.SetActive(state);
    }

    public void Update()
    {
        prevSpeed = (int)hipsRB.velocity.magnitude;
    }
}
