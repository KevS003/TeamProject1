using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://opengameart.org/content/creepy-forest-f
//https://opengameart.org/content/boss-battle-music
//https://opengameart.org/content/tragic-ambient-main-menu
public class SpawnSystem : MonoBehaviour
{
    
    public GameObject smallEnemy;
  
    public GameObject toughEnemy;
    public GameObject bigEnemy;
    private Vector3 spawnL;
    public float coolDown= 7.5f; 
    public PlayerMove lvlCheck;
    public int howManySmall = 5;
    bool lvlOver;
    int amountOfSmall=0;
    [SerializeField]
    private float spawnRateSmall= 2.5f;//small ghost
    [SerializeField]
    private float spawnRateTough = 5.0f;//shields to b popped by rocket
    [SerializeField]
    private float spawnRateBig= 10;//
    // Start is called before the first frame update
    void Start()
    {

        spawnL = this.gameObject.transform.position;
        StartCoroutine(spawn(spawnRateSmall, smallEnemy, 1));//starts function and runs rest of code when yield is returned.
        StartCoroutine(spawn(spawnRateTough, toughEnemy, 2));
        StartCoroutine(spawn(spawnRateBig, bigEnemy, 3));   
    }

   private IEnumerator spawn(float interval, GameObject typeE, int type)
   {
        if(type>=2||amountOfSmall<howManySmall)//mainly to stop infinite wave of small enemies
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(typeE, spawnL, Quaternion.identity);
        }
        else
        {
            yield return new WaitForSeconds(coolDown);
            amountOfSmall=0;//resets counter to zero after cooldown is done
        }
        if(type==1)
        {
            amountOfSmall++;//will count only for small enemy spawn
        }
        //lvlOver = player
        lvlOver = lvlCheck.lvlOver;
        if(lvlOver==true)
        {
            StopAllCoroutines();
            yield break;
        }
        StartCoroutine(spawn(interval, typeE, type));
        //loops all coroutines as they are all called after each returns a yield
   }

   //ex. spawnrateSmall starts then returns a waiting yield.(WHILE WAITING IT BEGINS THE NEXT COROUTINE)spawnRateTough begins and returns yield to wait b4 spawning.
   //when tough returns wait val, the Big spawner begins it's coroutine. When the wait timer is done, the code returns and continues with the spawning process. 
}
