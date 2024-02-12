using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;

public class MuseumObject : MonoBehaviour
{
    private Interactable interactable;
    public float waitTime = 5.0f;
    private float timer = 0.0f;
    private bool wasGrab = false;

    private void Start()
    {
        if (interactable == null)
            interactable = GetComponent<Interactable>();
    }

    private void FixedUpdate()
    {
        if (!interactable.attachedToHand)
        {
            if (wasGrab)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= waitTime)
                {
                    wasGrab = false;
                    timer = 0.0f;
                    GetComponent<LockToPoint>().enabled = true;
                }
            }
        }
        else
        {
            wasGrab = true;
            timer = 0.0f;
            GetComponent<LockToPoint>().enabled = false;
        }
    }
}
