using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using MaskTransitions;
using UnityEngine.SceneManagement;

public class BossAttackHandler : MonoBehaviour
{
    [SerializeField] private BossAttackHandlerData handlerData;
    [HideInInspector] public UnityEvent OnEncounterActive;
    [HideInInspector] public UnityEvent OnSpecialPerformed;
    [SerializeField] private bool skipSpecial = false;
    private AttackPattern currentAttack;

    [SerializeField]
    private float startDelay = 3f; //for audio, any initialize animations, will be cool to time the music with it

    [Header("Attacking Stats")] 
    [SerializeField] private float percentageForSpecialMove = 0.25f;
    public float encounterTimer { get;  set; }
    private bool encounterComplete = false;
    private bool specialPhaseActive = false;

    public List<AttackPattern> attackPatterns;

    [Header("Upgrades")] 
    [GetFromSelf] private EncounterDistanceDataHandler encounterDistanceData;
    private float encounterDuration = 30f;
    public int ProjectileDamageBuffer { get; set; } = 0;
    public ProjectileSpeedUpgradeEnum ProjectileSpeedState { get; set; } = ProjectileSpeedUpgradeEnum.NORMAL;

    private GameManager gameManager;
    private IEnumerator timerCoroutine;

    [field: SerializeField] public GameObject PlayerObject { get; private set; }

    private void Start()
    {
        InitializeAttackPatterns();
        SetUpAttackListeners();
        InitGamemanager();
        encounterDuration = encounterDistanceData.EncounterDuration;
        
        StartCoroutine(DelayThenStart());
        IEnumerator DelayThenStart()
        {
            yield return new WaitForSeconds(startDelay);
            StartEncounterAttacks();
        }
    }

    private void InitGamemanager()
    {
        gameManager = GameManager.Instance;
        gameManager.BossAttackHandler = this;
        InitPlayer();
        gameManager.OnStartEncounter();//read cards
    }
    private void InitPlayer()
    {
        PlayerObject = FindObjectOfType<PlayerHealth>().gameObject;
        if (!PlayerObject)
        {
            Debug.LogError("No player object in scene");
            return;
        }
        PlayerObject.GetComponent<PlayerHealth>().LoadHealth();
        if (gameManager.willHealThisRound)
        {
            Debug.Log("Healing the player");
            PlayerObject.GetComponent<PlayerHealth>().Heal(gameManager.HEAL_AMOUNT);

        }
    }

    private void InitializeAttackPatterns()
    {
        var potentialAttacks = GetComponentsInChildren<AttackPattern>();
        foreach (var ap in potentialAttacks)
        {
            attackPatterns.Add(ap);
            ap.Initialize(this);
        }
    }

    private void SetUpAttackListeners()
    {
        if (handlerData.possibleAttackPatterns.Count > 0)
        {
            foreach (AttackPattern ap in handlerData.possibleAttackPatterns)
            {
                ap.OnCompleteAttack.AddListener(NextAttack);
            }
        }

        if (handlerData.possibleSpecialMoves.Count > 0)
        {
            foreach (AttackPattern sm in handlerData.possibleSpecialMoves)
            {
                sm.OnCompleteAttack.AddListener(NextAttack);
                sm.OnCompleteSpecial.AddListener(SpecialMoveDone);
            }
        }
    }

    [ProButton]
    public void StartEncounterAttacks()
    {
        StartCoroutine(StartEncounterTimer());
        timerCoroutine = StartEncounterTimer();
    }

    private void NextAttack()
    {
        if (encounterComplete)
        {
            return;
        }

        PerformAttack(handlerData.GetRandomAttack());
    }

    private void NextSpecial()
    {
        if (encounterComplete)
        {
            return;
        }

        Debug.Log("Special !");
        PerformSpecial(handlerData.GetRandomSpecial());
    }

    // ENCOUNTER LOOP:
    private IEnumerator StartEncounterTimer()
    {
        Debug.Log("Starting Timer");
        encounterComplete = false;
        encounterTimer = 0f;
        float intervalForSpecial =
            (encounterDuration * percentageForSpecialMove) - 0.5f; // slight offset so the UI doesnt look off
        int intervalCounter = 0;
        NextAttack();

        while (encounterTimer < encounterDuration)
        {
            encounterTimer += Time.deltaTime + 1f;
            OnEncounterActive.Invoke(); // this is for the encounter UI bar to update
            if (!skipSpecial && !specialPhaseActive && encounterTimer >= intervalForSpecial * (intervalCounter + 1))
            {
                specialPhaseActive = true;
                currentAttack?.StopAttack();
                NextSpecial();
                intervalCounter++;
            }

            if (encounterTimer >= intervalForSpecial * (intervalCounter + 1) + intervalForSpecial)
            {
                specialPhaseActive = false;
            }

            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(SpecialPhaseNotActive); // to stop timer when special phase is active
        }

        // this is for when the encounter is complete:
        EndEncounter();
    }

    private void EndEncounter()
    {
      
        currentAttack.StopAttack();
        encounterComplete = true;
        Debug.Log("Encounter complete");
        FindObjectOfType<GameOverHandler>().EndRound();
    }

    public void StopEncounterTimer()
    {
        StopCoroutine(timerCoroutine);
    }

    public bool SpecialPhaseNotActive()
    {
        return specialPhaseActive == false;
    }

    private void SpecialMoveDone()
    {
        specialPhaseActive = false;
    }

    public void PerformAttack(AttackPattern ap)
    {
        currentAttack = ap;
        currentAttack.StartAttack();
        Debug.Log($"Performing Attack move: {ap.name}");
    }

    public void PerformSpecial(AttackPattern ap)
    {
        currentAttack = ap;
        currentAttack.StartAttack();
        OnSpecialPerformed.Invoke();
        Debug.Log($"Performing special move: {ap.name}");
    }

    public float GetEncounterDuration()
    {
        return encounterDuration;
    }

    public void DebugNukeEncounterTime()
    {
        encounterTimer = float.MaxValue; //lol
    }
}