using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobStorage : MonoBehaviour
{
    [SerializeField] List<ButtonInteraction> _jobButtons;
    [SerializeField] FinishedItemBox _finishedItemBox;
    [SerializeField] FurnaceManager _furnaceManager;

    public List<ButtonInteraction> JobButtons => _jobButtons;
    public FinishedItemBox FinishedItemBox => _finishedItemBox;
    public FurnaceManager FurnaceManager => _furnaceManager;
}
