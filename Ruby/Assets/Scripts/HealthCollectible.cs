using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D  other){
        RubyController rubyPlayer = other.GetComponent<RubyController>();

        if (rubyPlayer != null) {
            if (rubyPlayer.health < rubyPlayer.maxHealth) {
                rubyPlayer.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
