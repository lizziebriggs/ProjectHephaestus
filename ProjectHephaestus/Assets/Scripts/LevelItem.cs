using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LevelItem : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private Color _particleColour;
    public int Level => _level;

    public Color ParticleColour => _particleColour;
}
