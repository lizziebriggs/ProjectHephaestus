using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    [SerializeField] private AudioSource _hammerAudio;

    private bool _justHit = false;
    private float _waitTimer = 1;

    private void OnTriggerEnter(Collider collision)
    {
        if (!_justHit && collision.gameObject.GetComponent<MalleableMaterial>())
        {
            var malleable = collision.gameObject.GetComponent<MalleableMaterial>();
            if (malleable.isMalleable)
            {
                _justHit = true;
                malleable.UpdateValue();
                StartCoroutine(Reset());
            }
        }
    }

    IEnumerator Reset()
    {
        _hammerAudio.Play();
        yield return new WaitForSeconds(_waitTimer);
        _justHit = false;
    }
}
