using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilManager : MonoBehaviour
{
    private enum AnvilState { Idle, Anvilling, Anvilled }
    private AnvilState _anvilState;

    [SerializeField] private GameObject _gauges;
    [SerializeField] private Transform _anvilPosition;
    [SerializeField] private StrikerTimerController _strikerTimerController;
    [SerializeField] private ParticleSystem[] particles;
    public ParticleSystem[] Particles => particles;

    private GameObject _anvillingObject;
    private GameObject _anvilledObject;

    private void Start()
    {
        _gauges.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(_anvilState)
        {
            case AnvilState.Idle:
                //do nothing
                break;
            case AnvilState.Anvilling:
                Anvilling();
                break;
            case AnvilState.Anvilled:
                Anvilled();
                break;
            default:
                break;
        }

        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<MalleableMaterial>()) // if it is a malleable material
        {
            _anvilledObject = other.GetComponent<MalleableMaterial>().Hammered; // get the hammered object
            _anvillingObject = other.gameObject;// get the object to be hammered
            if (other.GetComponent<DistanceGrabbable>()) // check for grabbable component
            {
                if (!_gauges.activeSelf)
                {
                    _gauges.SetActive(true);
                    _strikerTimerController.currentState = StrikerTimerController.SliderState.Up;
                }                

                MalleableMaterial malleableMaterial = _anvillingObject.GetComponent<MalleableMaterial>();
                malleableMaterial.AnvilManager = this;

                _strikerTimerController.speed = malleableMaterial.Speed;

                var thresholdValues = malleableMaterial.ThresholdValues;

                foreach (var thresholdValue in thresholdValues)
                {
                    if (!_strikerTimerController.thresholdValues.Contains(thresholdValue)) _strikerTimerController.thresholdValues.Add(thresholdValue);
                }

                _anvilState = AnvilState.Anvilling; // change to anvilling state
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DistanceGrabbable>() && other.gameObject.layer != 11) // check objects are grabbable
        {
            _strikerTimerController.thresholdValues.Clear();
            _gauges.SetActive(false);
            _anvilState = AnvilState.Idle; // change to idle state
        }
    }

    private void Anvilling()
    {
        if (_anvillingObject) // if anvil object isn't null
        {
            var anvillingRigidbody = _anvillingObject.GetComponent<Rigidbody>(); // get the rigidbody
            var malleable = _anvillingObject.GetComponent<MalleableMaterial>(); // get the malleable component
            if (!anvillingRigidbody.isKinematic) // when the object is not being held
            {
                malleable.isMalleable = true; // allow it to be hammered
                //malleable.BoxCollider.isTrigger = true;
                _anvilledObject.transform.up = _anvilPosition.up; // set up rotation
                _anvillingObject.transform.position = _anvilPosition.position;// hold position on the anvil

                anvillingRigidbody.constraints = RigidbodyConstraints.FreezeRotation; // freeze rotation
            }
            else
            {
                anvillingRigidbody.constraints = RigidbodyConstraints.None; // unfreeze rotation
                malleable.isMalleable = false; // stop allowing it to be hammered
                //malleable.BoxCollider.isTrigger = false;
            }
            if (malleable.HitCounter >= malleable.HitCount) // once the hit counter has reached max
            {
                Destroy(_anvillingObject); // destroy the hammered object
                GameObject hammeredObject = Instantiate(malleable.FinalHit()); // create the new object
                _anvilledObject = hammeredObject;
                hammeredObject.transform.position = _anvilPosition.position; // set the position
                _anvilState = AnvilState.Anvilled; // change the state
            }
        }
    }

    private void Anvilled()
    {
        if (_anvilledObject) // if anvilled object exists
        {
            var anvilledRigidbody = _anvilledObject.GetComponent<Rigidbody>(); // get rigidbody
            if (!anvilledRigidbody.isKinematic) // if it isn't grabbed
            {
                _anvilledObject.transform.up = _anvilPosition.up; // set the up orientation
                _anvilledObject.transform.position = _anvilPosition.position; // hold the position on the anvil

                anvilledRigidbody.constraints = RigidbodyConstraints.FreezeRotation; // freeze rotation
            }
            else
            {
                anvilledRigidbody.constraints = RigidbodyConstraints.None; // unfreeze rotation
            }
        }
    }
}
