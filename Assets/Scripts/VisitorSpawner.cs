using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour, IWorkdayMember
{
    [SerializeField] private Visitor visitorPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float respawnDelay;

    private Visitor currentVisitor;
    private List<Workplace> workplaces;
    private float serviceDelay;
    private Coroutine waitingForRespawn;

    public event Action<IServiceResult> OnVisitorServedEvent;

    public void Setup(List<Workplace> workplaces, float serviceDelay) 
    {
        this.workplaces = workplaces;
        this.serviceDelay = serviceDelay;
    }

    private void Spawn() 
    {
        if (!currentVisitor)
        {
            currentVisitor = Instantiate(visitorPrefab, spawnPoint.position, Quaternion.identity);
            currentVisitor.OnServedEvent += OnVisitorServed;
            currentVisitor.OnStartDespawn += Despawn;
        }
        else
        {
            currentVisitor.transform.position = spawnPoint.position;
            currentVisitor.gameObject.SetActive(true);
        }

        //It is assumed that the visitor chooses a random workplace, but within the task he chooses only the nail bar
        currentVisitor.Setup(workplaces[UnityEngine.Random.Range(0,0)], serviceDelay, spawnPoint.position);
    }

    private void OnVisitorServed(IServiceResult result)
    {
        OnVisitorServedEvent?.Invoke(result);
    }

    private IEnumerator WaitForSpawn(Action callback) 
    {
        float time = 0f;
        while (time < respawnDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }
        callback?.Invoke();
        waitingForRespawn = null;
    }

    private void Despawn() 
    {
        currentVisitor.gameObject.SetActive(false);
        waitingForRespawn = StartCoroutine(WaitForSpawn(Spawn));
    }

    public void OnDayStart()
    {
        waitingForRespawn = StartCoroutine(WaitForSpawn(Spawn));
    }

    public void OnDayEnd()
    {
        if (waitingForRespawn != null)
        {
            StopCoroutine(waitingForRespawn);
            waitingForRespawn = null;
        }
        else
        {
            currentVisitor.gameObject.SetActive(false);
        }
    }
}