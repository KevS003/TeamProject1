using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    
    public GameObject smallEnemy;
  
    public GameObject toughEnemy;
   
    public GameObject bigEnemy;
    [SerializeField]
    private float spawnRateSmall= 2.5f;
    [SerializeField]
    private float spawnRateTough = 5.0f;
    [SerializeField]
    private float spawnRateBig= 10;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn(spawnRateSmall, smallEnemy));
        StartCoroutine(spawn(spawnRateTough, toughEnemy));
        StartCoroutine(spawn(spawnRateBig, bigEnemy));   
    }

   private IEnumerator spawn(float interval, GameObject typeE)
   {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(typeE, new Vector3(-4.965637f,7.02f,2.104684f), Quaternion.identity);
        StartCoroutine(spawn(interval, typeE));
   }
}
