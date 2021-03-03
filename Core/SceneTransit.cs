using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransit : MonoBehaviour
{
    enum DestinationIdentifier
    {
        A, B, C, D, E
    }

    [SerializeField] int sceneIndexToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    //[SerializeField] Fader fader;

    //bool selectedSceneIsLoaded = false;
    //bool sceneIsLoading = false;

    private void Start()
    {
    }

    //private void Update()
    //{
    //    if (sceneIsLoading && SceneManager.sceneLoaded)
    //    {

    //    }
    //        selectedSceneIsLoaded = true;

    //}

    private IEnumerator SceneTransition()
    {
        if(sceneIndexToLoad < 0)
        {
            Debug.LogError("Scene to load not set.");
            yield break;
        }

        DontDestroyOnLoad(gameObject);

        //Possible scene fade out here
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(1);

        //GameObject player = GameObject.FindWithTag("Player");
        //player.transform.SetParent(null);
        //print(player.transform.parent);
        //DontDestroyOnLoad(player);
        //print("Player not destroyed?");

        yield return SceneManager.LoadSceneAsync(sceneIndexToLoad);
        yield return new WaitForSeconds(1f);

        SceneTransit otherTransit = GetOtherTransit();
        UpdatePlayer(otherTransit);

        //Add possible scene fade in here
        yield return fader.FadeIn(1);


        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" )
        {
            StartCoroutine(SceneTransition());

            //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            //sceneIsLoading = true;
        }
    }

    private void UpdatePlayer(SceneTransit otherTransit)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player.transform.parent != null)
        {
            GameObject parent = player.transform.parent.gameObject;
            parent.transform.position = otherTransit.spawnPoint.position;
            parent.transform.rotation = otherTransit.spawnPoint.rotation;

            Move_Elevator move_Elevator = parent.GetComponent<Move_Elevator>();
            move_Elevator.SetWaypoint(parent.transform.position);
        }
        else
        {
            player.transform.position = otherTransit.spawnPoint.position;
            player.transform.rotation = otherTransit.spawnPoint.rotation;
        }
        
        DontDestroyOnLoad(player);

    }

    private SceneTransit GetOtherTransit()
    {
        foreach (SceneTransit transit in FindObjectsOfType<SceneTransit>())
        {
            if (transit == this) continue;
            if (transit.destination != destination) continue;

            return transit;
        }

        return null;
    }
}