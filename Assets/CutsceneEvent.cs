using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEvent : MonoBehaviour
{
    private DialogController dialogController;
    [SerializeField] private float initialWaitTime;

    [SerializeField] private Dialog[] dialogEvents;

    [SerializeField] private GameObject evelynActor;
    [SerializeField] private GameObject jamesActor;

    [SerializeField] private GameObject evelynActorPausePoint;
    [SerializeField] private GameObject jamesActorPausePoint;

    [SerializeField] private GameObject actorEndPoint;

    [SerializeField] private float travelSpeed;




    void Start()
    {
        dialogController = gameObject.GetComponentInChildren<DialogController>();

        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(initialWaitTime);

        StartCoroutine(MoveActor(evelynActor, evelynActorPausePoint));
        StartCoroutine(MoveActor(jamesActor, jamesActorPausePoint));


        yield return new WaitForSeconds(0.5f);




        foreach (Dialog dialog in dialogEvents)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(dialogController.StartDialog(dialog));
            yield return new WaitForSeconds((1f / 15f) * dialog.dialog.Length + 0.2f);

        }

        StartCoroutine(MoveActor(jamesActor, actorEndPoint));

    }

    IEnumerator MoveActor(GameObject actor, GameObject pausePoint) {
        while (actor.transform.position.x < pausePoint.transform.position.x)
        {
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, pausePoint.transform.position, travelSpeed * Time.deltaTime);
            yield return null;

        }
    }

}
