using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    void Start() {
        GameManager.Instance.OnPlay.AddListener(ActivePlayer);
    }

    void ActivePlayer(){
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other) 
        {
            if (other.transform.tag == "Obstacle")
            {
                gameObject.SetActive(false);
            GameManager.Instance.GameOver();
            }

        }
}
