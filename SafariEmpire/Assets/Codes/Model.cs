using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEditor.Experimental.GraphView;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;


public class Model : MonoBehaviour
{
    private int difficulty; //0: easy, 1:medium, 2:hard
    private int money;
    private int time;
    private int timeSpeed; //0: óra, 1: nap, 2: hét (?)
    private int DaysUntilWin; //1 hónap = 30 nap
    private int popularity;
    private int ticketPrice; //ticket az UML-ben
    private int visitorsWaiting;
    private int visitorCount;
    //public List<Jeep> jeeps;
    //public List<Plant> plants;
    //public List<SecuritySystem> security;
    //public List<Ranger> rangers;
    //public List<Poacher> poachers;
    //public List<AnimalGroup> animalGroups;

    //terepi akadályok:
    public GameObject hillObject;
    public List<Hill> hills;
    public GameObject riverObject;
    public List<River> rivers;
    public GameObject pondObject;
    public List<Pond> ponds;

    //constructor
    void Start()
    {
        //terepi akadályok generálása
        //HEGYEK
        hills = new List<Hill>();
        Hill hill1 = new Hill(2, 2);
        Hill hill2 = new Hill(-2, 1);
        Hill hill3 = new Hill(0, 0);
        hills.Add(hill1);
        hills.Add(hill2);
        hills.Add(hill3);
        foreach (Hill hill in hills)
        {
            hill.obj = Instantiate(hillObject, hill.spawnPosition, Quaternion.identity);
        }
        //FOLYÓK
        rivers = new List<River>();
        River river1 = new River(-3, -3);
        River river2 = new River(-5, 3);
        rivers.Add(river1);
        rivers.Add(river2);
        foreach (River river in rivers)
        {
            river.obj = Instantiate(riverObject, river.spawnPosition, Quaternion.identity);

        }
        //TAVAK
        ponds = new List<Pond>();
        Pond pond1 = new Pond(4, -4);
        ponds.Add(pond1);
        foreach (Pond pond in ponds)
        {
            pond.obj = Instantiate(pondObject, pond.spawnPosition, Quaternion.identity);

        }

        move(hill1, new Vector2(-2,-2));

        //entityk generálása
        //NÖVÉNYEK
        
        //ÁLLATOK

        //ORVVADÁSZOK

        //VADÕRÖK

        //MEGFIGYELÕ RENDSZER

        //JEEPS


        //felülírandók
        this.time = 0;
        this.timeSpeed = 0;
        this.popularity = 1;
        this.ticketPrice = 100;
        this.visitorsWaiting = 0;
        this.visitorCount = 0;
        switch (difficulty)
        {
            case 0:
                DaysUntilWin = 3 * 30;
                money = 1000;
                break;
            case 1:
                DaysUntilWin = 6 * 30;
                money = 2000;
                break;
            case 2:
                DaysUntilWin = 12 * 30;
                money = 3000;
                break;
        }
    }

    //MOZGÁS
    /*
     * moves an entity to a position
     */
    private bool isMoving;
    private Vector2 targetPosition1;
    private SpriteRenderer sr;
    private float fspeed = 1f;
    //hill helyett entity lesz
    public void move(Hill entity, Vector2 p)
    {
        this.sr = entity.obj.GetComponent<SpriteRenderer>();
        this.targetPosition1 = p;
        isMoving = true;
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            this.sr.transform.position = Vector2.MoveTowards(this.sr.transform.position, targetPosition1, fspeed * Time.deltaTime);
            if (Vector2.Distance(this.sr.transform.position, targetPosition1) < 0.01f)
            {
                isMoving = false;
            }
        }
    }


    void Update()
    {

    }


    /*
     * pays for the rangers every month(?)
     */
    public void payCheck()
    {
        //this.money -= this.rangers.count * rangerPrice; //rangerPrice is undefined
    }

    /*
     * kills the oldest animal in a group
     */
    /*
    public AnimalGroup killAnimal(AnimalGroup group)
    {
        int maxage = 0;
        int maxind = -1;
        for(int i = 0; i<group.count; i++)
        {
            if (group[i].age > maxage)
            {
                maxage = group[i].age;
                maxind = i;
            }
        }
        group.RemoveAt(maxind);

        return group;
    }*/

    /*
     * checks if we win or lose
     */
    public void checkWinLose()
    {
        if (money <= 0)
        {
            //lose
        }
    }


    //GETTEREK, SETTEREK
    public void setDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
    }
    public int getDifficulty()
    {
        return this.difficulty;
    }

    public void setMoney(int money)
    {
        this.money = money;
    }
    public int getMoney()
    {
        return this.money;
    }

    public void setTime(int time)
    {
        this.time = time;
    }
    public int getTime()
    {
        return this.time;
    }

    public void setTimeSpeed(int timeSpeed)
    {
        this.timeSpeed = timeSpeed;
    }
    public int getTimeSpeed()
    {
        return this.timeSpeed;
    }

    public void setDaysUntilWin(int daysUntilWin)
    {
        this.DaysUntilWin = daysUntilWin;
    }
    public int getDaysUntilWin()
    {
        return this.DaysUntilWin;
    }

    public void setPopularity(int popularity)
    {
        this.popularity = popularity;
    }
    public int getPopularity()
    {
        return this.popularity;
    }

    public void setTicketPrice(int ticketPrice)
    {
        this.ticketPrice = ticketPrice;
    }
    public int getTicketPrice()
    {
        return this.ticketPrice;
    }

    public void setVisitorsWaiting(int visitorsWaiting)
    {
        this.visitorsWaiting = visitorsWaiting;
    }
    public int getVisitorsWaiting()
    {
        return this.visitorsWaiting;
    }

    public void setVisitorCount(int visitorCount)
    {
        this.visitorCount = visitorCount;
    }
    public int getVisitorCount()
    {
        return this.visitorCount;
    }
}
