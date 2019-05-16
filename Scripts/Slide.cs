using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {

    int screws = 4;
    public GameObject HiddenObject, duplicate, FakeKey, RealKey;

    public void removeScrew() {
        --screws;

        if (screws == 0) makeGrabbable();
    }

    private void makeGrabbable() {

        // FOR THE KNIFE

        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        Destroy(collider);

        HiddenObject.gameObject.GetComponent<SwitchARooLock>().MakeGrabbable(); ;

        if (!duplicate.gameObject.activeSelf) {
            duplicate.gameObject.SetActive(true);
            duplicate.gameObject.transform.parent = null;
            gameObject.SetActive(false);
        }

        // FOR KEY

        RealKey.SetActive(true);
        RealKey.transform.parent = null;
        FakeKey.SetActive(false);
    }
}
