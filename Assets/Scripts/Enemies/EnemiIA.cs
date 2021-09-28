using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Enums;
using System;

public class EnemiIA : MonoBehaviour
{
    //GraphMask gridA = GraphMask.FromGraphName("GridA");
    //GraphMask gridB = GraphMask.FromGraphName("GridB");
    //[SerializeField] float speed2 = 2f;

    // Enemi follow player parameters
    [SerializeField] private Transform target;
    [SerializeField] private float speed=200f;
    [SerializeField] private float gravity=50f;
    [SerializeField] private float nextWayPointDistance = 4f ;
    [SerializeField] private Transform enemyGFX;

    // Patrol reference parameters
    [SerializeField] private Transform patrolPoints;
    [SerializeField] private float waitTimePatrol;
    [SerializeField] private float starWaitTime;
     private float nextWayPointPatrol=1.2f;

    // Select the behaviour parameters

    [SerializeField] private float distanceToActiveFindPlayer;

    [SerializeField] public float currentLvlEnemie;
    public SpriteRenderer sprParachute;
    public Animator animParachute;
    LevelManager manager;
    



   

    Path path;
    Path pathPatrol;
    int currentWaypoint=0;
    int currentWaypointPatrol=0;
    

    bool reachedEndOfPath=false;
    bool reachedEndOfPathPatrol=false;


    bool goToPlayer=true;
    bool onLvl=false;

    Seeker seeker;
    Rigidbody rbEnemie;

    EnemieSpawnPositions enemyPointPatrol;
    // EnemieSpawnPositions enSpawnPatrol;


	public bool isStoppedIA { get; set; }
    public float Speed{get=>speed;set=>speed=value;}
    public float NextWayPointDistance{get=>nextWayPointDistance;set=>nextWayPointDistance=value;}
    public float DistanceToActiveFindPlayer{get=>distanceToActiveFindPlayer;set=>distanceToActiveFindPlayer=value;}

    void Start()
    {
        seeker=GetComponent<Seeker>();
        rbEnemie= GetComponent<Rigidbody>();
        enemyPointPatrol=GetComponentInParent<EnemieSpawnPositions>();
        //manager=GameObject.Find("GameManager").GetComponent<LevelManager>();
        manager = LevelManager.Instance;
        sprParachute =GetComponentInChildren<SpriteRenderer>();
        sprParachute.enabled=false;
        animParachute=GetComponentInChildren<Animator>();
       

        // float rx = Random.Range(0f, Screen.width);
        // float ry = Random.Range(0f, Screen.height);
        // Vector3 randomScreenPos = new Vector3(rx, ry, 0f);
        // Vector3 inWorldPos = Camera.main.ScreenToWorldPoint(randomScreenPos);

        // waitTimePatrol=starWaitTime;
        // patrolPoints.position=(Vector2)(inWorldPos);


        InvokeRepeating("UpdatePath",0f,0.2f);
        seeker.StartPath(rbEnemie.position,rbEnemie.position+target.position*10f,OnPathComplete);

        InvokeRepeating("UpdatePatrol", 0f, 1.5f);
        seeker.StartPath(rbEnemie.position, rbEnemie.position + patrolPoints.position, PathToPatrolComplete);
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && target != null && goToPlayer){
            seeker.StartPath(rbEnemie.position,target.position,OnPathComplete);
        }
    }   

    void OnPathComplete(Path p)
    {

        if(p.error){
            return;
        }
        else
        {
            path=p;
            currentWaypoint=0;
        }
    }
    void PathToPatrolComplete(Path pP){

        if(pP.error){
            return;
        }
        else{
            pathPatrol =pP;   
            currentWaypointPatrol=0;
        }

    }

    void UpdatePatrol(){

        if (seeker.IsDone() && patrolPoints != null && goToPlayer == false)
        {
            Vector3 myNewVector = new Vector3();
            myNewVector = UpdateRandomVector();
            

            seeker.StartPath(rbEnemie.position, myNewVector, PathToPatrolComplete);
        }
    }
    
    void FixedUpdate()
    {
        // float checkDistancePatrol = Vector2.Distance(rbEnemie.position,patrolPoints.position);
        float checkDistancePlayer = Vector2.Distance(rbEnemie.position,target.position);

        //Active the find target behaviour
        if(checkDistancePlayer < distanceToActiveFindPlayer){

            goToPlayer=true;
             if(path==null){
            return;
            }

             if(currentWaypoint>=path.vectorPath.Count){
            reachedEndOfPath=true;
            return;
            
            }
            else
            {
            reachedEndOfPath=false;
            }      
            float distanceToPlayer = Vector3.Distance(rbEnemie.position,path.vectorPath[currentWaypoint]);  

            addForceEnemie();

            if(distanceToPlayer<nextWayPointDistance){
            currentWaypoint++;  
            }

        }

            //Active patrol beahiour
        else
        {
            goToPlayer = false;

            if (pathPatrol == null)
            {
                return;
            }
            if (currentWaypointPatrol >= pathPatrol.vectorPath.Count)
            {
                reachedEndOfPathPatrol = true;
                return;
            }
            else
            {
                reachedEndOfPathPatrol = false;
            }


            float distanceToPatrolPoint = Vector2.Distance(rbEnemie.position, pathPatrol.vectorPath[currentWaypointPatrol]);

            patrolEnemie();

            if (distanceToPatrolPoint < nextWayPointPatrol)
            {
                currentWaypointPatrol++;
            }
        }
    }

    void addForceEnemie(){
        
        Vector3 direction = (path.vectorPath[currentWaypoint]-rbEnemie.position).normalized;




        Vector3 force=direction*speed*Time.deltaTime;
        checkLvlEnmie();

        if(isStoppedIA==false && onLvl)
        {        
            rbEnemie.AddForce(force);
        }
        if(isStoppedIA==true){
           
            
            rbEnemie.velocity=(-transform.up*Time.deltaTime*gravity).normalized;
        }  
    }
   
    void patrolEnemie ()
    {
        Vector3 directionToPointPatrol = (pathPatrol.vectorPath[currentWaypointPatrol] - rbEnemie.position).normalized;

        Vector3 forcePatrol = directionToPointPatrol*speed*Time.deltaTime;
        checkLvlEnmie();

       if(isStoppedIA==false && onLvl)
        {
            rbEnemie.AddForce(forcePatrol);
        }
        if(isStoppedIA==true){
        
            rbEnemie.velocity=(-transform.up*Time.deltaTime*gravity).normalized;
        }
    }

    [SerializeField] public float minX;
    [SerializeField] public float maxX;
    [SerializeField] public float minY;
    [SerializeField] public float maxY;
    private Vector3 UpdateRandomVector()
    {
        // float rx = UnityEngine.Random.Range(0f, Screen.width);
        // //print("rx" + rx);
        // float ry = UnityEngine.Random.Range(0f, Screen.height);
        // //print("ry" + ry);

        // //print($"Object: {gameObject.name}, <{rx},{ry}>");

        // Vector3 randomScreenPos = new Vector3(rx, ry, 0f);
        // Vector3 inWorldPos = Camera.main.ScreenToWorldPoint(randomScreenPos);
        float xMinposRB=transform.position.x-2.5f;
        // print("Este es x rb" + xMinposRB);
        float xMaxposRB=transform.position.x+2.5f;
        float yMinposRB=transform.position.y-2.5f;
        float yMaxposRB=transform.position.y+2.5f;

        // if(xMinposRB>=minX){
        //     xMinposRB=minX;
        // }
        // if(xMaxposRB>=maxX){
        //     xMaxposRB=maxX;
        // }
        // if(yMinposRB>=minY){
        //     yMinposRB=minY;
        // }
        // if(yMaxposRB>=maxY){
        //     yMaxposRB=maxY;
        // }


        // return inWorldPos;
        float xValue=UnityEngine.Random.Range(xMinposRB,xMaxposRB);
        if(xValue<minX ){
            xValue=minX;
            
        }
        if(xValue>maxX){
            xValue=maxX;
        }
        float yValue=UnityEngine.Random.Range(yMinposRB,yMaxposRB);
        if(yValue<minY ){
            yValue=minY;
            
        }
        if(yValue>maxY){
            yValue=maxY;
        }

        Vector3 rndPoint = new Vector3(xValue,yValue,0f);
        // mod Carlos Rojas
        // float disPointToPlayer = Vector2.Distance(rbEnemie.position,rndPoint);
        // if(disPointToPlayer<=4f){
        //     float newSpeed = speed*0.5f;
        //     speed=newSpeed;
            
        // }
        



        return rndPoint;


        // return  enemyPointPatrol.rndPoint;
    }

    public void checkLvlEnmie(){
        if(manager.CurrentLevel==currentLvlEnemie){        

            //print("Somos del mismo nivel");    
            onLvl=true;
        }
        else{
            onLvl=false;
        }
         

    }


}
