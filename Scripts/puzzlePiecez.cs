using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzlePiecez : MonoBehaviour {

    int count = 3;
    Rigidbody body;
    DisplayLink display;
    GameObject Player;
    public GameObject piece1, piece2, piece3, AssociatedUI, HiddenObject;

    public AudioClip sound, fullUnlockSound, Click;
    public float Volume;
    AudioScript sfx;

    void Start() {
        GameObject sfxObj = GameObject.Find("MusicPlayer");
        sfx = sfxObj.gameObject.GetComponent<AudioScript>();

        Player = GameObject.FindGameObjectWithTag("MainCamera");

        GameObject link = GameObject.FindGameObjectWithTag("DisplayLink");
        display = link.gameObject.GetComponent<DisplayLink>();

        body = gameObject.GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationZ
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezePositionX
            | RigidbodyConstraints.FreezePositionY
            | RigidbodyConstraints.FreezePositionZ;
    }

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "realPiece1") {
            switchPiece(piece1, other);
		} else if (other.gameObject.name == "realPiece2") {
            switchPiece(piece2, other);
		} else if (other.gameObject.name == "realPiece3") {
            switchPiece(piece3, other);
		}
	}

    void switchPiece(GameObject piece, Collider other) {
        pieceFound();
        piece.gameObject.SetActive(true);
        other.gameObject.SetActive(false);
    }

    void pieceFound() {
        count--;

        if (count == 0)
        {
            display.show("Something clicked", 2f);

            sfx.OneShot(Click, 1f);
            sfx.Wait_Play(fullUnlockSound, Volume);

            HiddenObject.gameObject.SetActive(true);
            body.constraints = RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationZ
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezePositionX
            | RigidbodyConstraints.FreezePositionY;
        }
        else {
            sfx.OneShot(sound, Volume);
        }
    }

    bool disable = false;

    public void Update()
    {
        if (disable) return;

        if (count != 3) {
            disable = true;
            AssociatedUI.gameObject.SetActive(false);
        }

        float distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);

        if (2f >= distance)
        {
            if (AssociatedUI.gameObject.activeSelf)
                AssociatedUI.gameObject.SetActive(false);
        }
        else {
                if (!AssociatedUI.gameObject.activeSelf && count == 3)
                AssociatedUI.gameObject.SetActive(true);
        }
    }
}

