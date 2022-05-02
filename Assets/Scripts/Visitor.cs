using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Visitor : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private DesiredOptionPopup desiredOptionPopup;

    private int desiredOption;

    private Workplace target;
    private float serviceDelay;
    private Vector3 despawnPoint;
    private Vector3 destinationPos;

    public event Action<IServiceResult> OnServedEvent;
    public event Action OnStartDespawn;

    public void Setup(Workplace target, float serviceDelay, Vector3 despawnPoint) 
    { 
        this.target = target;
        this.serviceDelay = serviceDelay;
        this.despawnPoint = despawnPoint;

        destinationPos = target.ServicePosition.position;
        agent.SetDestination(destinationPos);
        StartCoroutine(WaitForMoving(StartServiceProcess)); 
    }

    private IEnumerator WaitForMoving(Action callback) 
    {
        while (transform.position.x != destinationPos.x && transform.position.z != destinationPos.z)
        {
            yield return null;
        }
        callback?.Invoke();
    }

    private IEnumerator WaitForServiceDelay(Action callback) 
    {
        float time = 0f;
        while (time <= serviceDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }
        callback?.Invoke();
    }

    private void StartServiceProcess() 
    {
        desiredOption = Random.Range(1, 4);
        desiredOptionPopup.SetOption(desiredOption);
        desiredOptionPopup.SetActive(true);

        StartCoroutine(WaitForServiceDelay(ServiceProcess));
    }

    private void ServiceProcess() 
    {
        desiredOptionPopup.SetActive(false);

        target.ServiceProcess(desiredOption, OnServed);
    }

    private void OnServed(int option) 
    {
        if (desiredOption == option)
        {
            OnServedEvent?.Invoke(new SuccessfulResult());
        }
        else
        {
            OnServedEvent?.Invoke(new FailureResult());
        }

        destinationPos = despawnPoint;
        agent.SetDestination(despawnPoint);

        StartCoroutine(WaitForMoving(() => OnStartDespawn?.Invoke()));
    }
}