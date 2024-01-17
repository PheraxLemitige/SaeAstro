using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristMenu : MonoBehaviour {
    private GameObject hand;
    public Vector3 firstRange;
    public Vector3 lastRange;
    // Start is called before the first frame update
    void Start() {
        if (hand == null)
            hand = this.gameObject.transform.parent.gameObject;

        //firstRange = new Vector3(320, 0, 0);
        //lastRange = new Vector3(500, 80, 0);

    }

    // Update is called once per frame
    void Update() {
        if (hand.transform.rotation.eulerAngles.x > firstRange.x && hand.transform.rotation.eulerAngles.y > firstRange.y && hand.transform.rotation.eulerAngles.z > firstRange.z)
            if (hand.transform.rotation.eulerAngles.x < lastRange.x && hand.transform.rotation.eulerAngles.y < lastRange.y && hand.transform.rotation.eulerAngles.z < lastRange.z)
                visible(true);
            else
                visible(false);
        else
            visible(false);
        //Debug.Log(hand.transform.rotation.eulerAngles);
    }

    void visible(bool visible) {
        foreach (Transform child in this.transform)
            child.gameObject.SetActive(visible);
    }
}
