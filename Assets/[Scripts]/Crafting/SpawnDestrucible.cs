using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestrucible : MonoBehaviour
{
    public List<GameObject> destructibles = new List<GameObject>();
    public float respawnTime;

    private bool run = true;

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(respawnTime);
        
        int rand = Random.Range(0, destructibles.Count);

        GameObject obj = Instantiate(destructibles[rand], gameObject.transform.position, Quaternion.identity);
        obj.transform.parent = gameObject.transform;
        run = true;
    }

    private void Start()
    {
        int rand = Random.Range(0, destructibles.Count);
        GameObject obj = Instantiate(destructibles[rand], gameObject.transform.position, Quaternion.identity);
        obj.transform.parent = gameObject.transform;
    }

    private void Update()
    {
        if (GetComponentInChildren<Destructible>() == null && run)
        {
            StartCoroutine(Spawn());
            run = false;
        }
    }
}
