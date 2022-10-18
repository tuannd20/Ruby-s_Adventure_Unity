using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        RubyController ruby = other.GetComponent<RubyController>();

        if (ruby != null) {
            ruby.ChangeHealth(-1);
        }
    }
}
