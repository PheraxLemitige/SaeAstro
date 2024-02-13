using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class MuseumObject : MonoBehaviour
{
    private Interactable interactable;
    public float waitTime = 5.0f;
    private float timer = 0.0f;
    private bool wasGrab = false;

    public Transform snapTo;
    private Rigidbody body;
    public float snapTime = 5.0f;
    private float snapTimer = 0.0f;
    

    private void Start()
    {
        if (interactable == null)
            interactable = GetComponent<Interactable>();
        if (body == null)
            body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!interactable.attachedToHand)
        {
            if (wasGrab)
            {
                timer += Time.deltaTime;
                //Debug.Log(timer);
                if (timer >= waitTime)
                {
                    wasGrab = false;
                    timer = 0.0f;
                    snapTime = 0.0f;
                }
            }
            else
            {
                snapTimer += Time.deltaTime;

                body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, Time.fixedDeltaTime * 4);
                if (body.useGravity)
                    body.AddForce(-Physics.gravity);

                transform.position = Vector3.Lerp(transform.position, snapTo.position, Time.fixedDeltaTime * snapTimer * 0.3f);
                transform.rotation = Quaternion.Slerp(transform.rotation, snapTo.rotation, Time.fixedDeltaTime * snapTimer * 0.2f);
            }
        }
        else
        {
            wasGrab = true;
            timer = 0.0f;
            snapTime = 0.0f;
        }
    }
}
