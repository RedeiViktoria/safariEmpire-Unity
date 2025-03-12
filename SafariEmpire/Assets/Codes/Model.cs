using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEditor.Experimental.GraphView;
using TMPro;


public class Model : MonoBehaviour
{
    private int difficulty; //0: easy, 1:medium, 2:hard
    private int money;
    private int time;
    private int speed; //0: óra, 1: nap, 2: hét (?)
    public int DaysUntilWin; //1 hónap = 30 nap
    public int popularity;
    public int ticketPrice; //ticket az UML-ben
    public int visitorsWaiting;
    public int visitorCount;
    //public List<Jeep> jeeps;
    //public List<Plant> plants;
    //public List<SecuritySystem> security;
    //public List<Ranger> rangers;
    //public List<Poacher> poachers;
    //public List<AnimalGroup> animalGroups;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject obj; // A mozgó objektum
    public Vector3 targetPosition = new Vector2(20, 20); // Célpozíció
    public float fspeed; // Sebesség (speed)

    void Start()
    {

        /*GameObject obj = GameObject.Find("Square");
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        sr.color = Color.blue;*/
        Debug.Log(PlayerPrefs.GetInt("difficulty"));
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        obj.transform.position = Vector2.MoveTowards(obj.transform.position, new Vector2(10, 10), fspeed * Time.deltaTime);
    }

    //constructor
    public Model()
    {
        //felülírandók
        this.time = 0;
        this.speed = 0;
        this.popularity = 1;
        this.ticketPrice = 100;
        this.visitorsWaiting = 0;
        this.visitorCount = 0;
        switch (difficulty)
        {
            case 0:
                DaysUntilWin = 3*30;
                money = 1000;
                break;
            case 1:
                DaysUntilWin = 6*30;
                money = 2000;
                break;
            case 2:
                DaysUntilWin = 12*30;
                money = 3000;
                break;
        }
        //LISTEK GENERÁLÁSA
        //meg kell csinálni hogy ne legyenek objektumok egymáson
        System.Random rnd = new System.Random();

        //növények
        int plantCount = rnd.Next(5, 16);
        //plants = new List<Plant>();
        for(int i = 0; i<plantCount; i++)
        {
            //Plant plant = new Plant(rnd.Next(0,1000), rnd.Next(0,1000));//koordináták megadva
            //plants.Add(plant);
        }

        //vizek
        int waterCount = rnd.Next(1, 5);
        for(int i = 0; i<waterCount; i++)
        {
            if (i % 2 == 0)
            {
                //Pond pond = new Pond(rnd.Next(0, 1000), rnd.Next(0, 1000));//koordináták megadva
            } else
            {
                //River river = new River(rnd.Next(0, 1000), rnd.Next(0, 1000));//koordináták megadva
            }
        }

        //hegyek
        int hillCount = rnd.Next(1, 5);
        for(int i = 0; i<hillCount; i++)
        {
            //Hill hill = new Hill(rnd.Next(0, 1000), rnd.Next(0, 1000));//koordináták megadva
        }
    }

    /*
     * moves an entity to a position
     */
    /*
    public void move(Entity entity, Position p)
    {

    }*/

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
}
