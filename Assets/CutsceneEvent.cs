using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private string gameSceneName;

    private string evelynCurrentAnimation;
    private string jamesCurrentAnimation;

    // Animations
    const string EVELYN_IDLE = "Idle Evelyn";
    const string EVELYN_RUN = "Evelyn Run";
    const string JAMES_IDLE = "Idle James";
    const string JAMES_RUN = "Run James";



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

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);

    }

    IEnumerator MoveActor(GameObject actor, GameObject pausePoint)
    {
        while (actor.transform.position.x < pausePoint.transform.position.x)
        {
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, pausePoint.transform.position, travelSpeed * Time.deltaTime);

            if (actor.name == "EvelynActor")
            {
                if (Mathf.Abs(actor.transform.position.x - pausePoint.transform.position.x) > 0.1f)
                {
                    StartCoroutine(ChangeAnimation(evelynActor, evelynCurrentAnimation, EVELYN_RUN));
                }
                else
                {
                    StartCoroutine(ChangeAnimation(evelynActor, evelynCurrentAnimation, EVELYN_IDLE));

                }
            }

            if (actor.name == "JamesActor")
            {
                if (Mathf.Abs(actor.transform.position.x - pausePoint.transform.position.x) > 0.1f)
                {
                    StartCoroutine(ChangeAnimation(jamesActor, jamesCurrentAnimation, JAMES_RUN));
                }
                else
                {
                    StartCoroutine(ChangeAnimation(jamesActor, jamesCurrentAnimation, JAMES_IDLE));
                }
            }

            yield return null;

        }
    }

    IEnumerator ChangeAnimation(GameObject actor, string currentAnimation, string newAnimation)
    {
        if (currentAnimation != newAnimation)
        {
            actor.GetComponent<Animator>().Play(newAnimation);

            currentAnimation = newAnimation;
        }
        yield return null;


    }

}
