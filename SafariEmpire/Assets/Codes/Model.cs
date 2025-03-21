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
    private int difficulty; //1: easy, 2:medium, 3:hard
    private int money;
    private int time;
    private int timeSpeed; //0: óra, 1: nap, 2: hét (?)
    private int DaysUntilWin; //1 hónap = 30 nap
    private int popularity;
    private int ticketPrice; //ticket az UML-ben
    private int visitorsWaiting;
    private int visitorCount;

    //entityk
    public GameObject jeepObject;
    //public List<Jeep> jeeps;
    public GameObject plantObject;
    //public List<Plant> plants;
    //public List<SecuritySystem> security;
    public GameObject securiryObject;
    //public List<Ranger> rangers;
    //rangerObject
    //public List<Poacher> poachers;
    //poacherObject
    //public List<AnimalGroup> animalGroups;
    public GameObject animalGroupObject;

    public List<Path> paths;
    public GameObject pathObject;

    //terepi akadályok:
    public GameObject hillObject;
    public List<Hill> hills;
    public GameObject riverObject;
    public List<River> rivers;
    public GameObject pondObject;
    public List<Pond> ponds;

    //view cuccai:
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timeText;

    //constructor
    void Start()
    {
        this.difficulty = PlayerPrefs.GetInt("difficulty");
        this.timeSpeed = 0; //viewból setTimeSpeed-del módosítódik
        this.ticketPrice = 100; //viewból setTicketPrice-szal módosítódik
        this.time = 0;

        //terepi akadályok generálása
        //HEGYEK
        hills = new List<Hill>();
        Hill hill1 = new Hill(new Vector2(2, 2));
        Hill hill2 = new Hill(new Vector2(-2, 1));
        Hill hill3 = new Hill(new Vector2(0, 0));
        hills.Add(hill1);
        hills.Add(hill2);
        hills.Add(hill3);
        foreach (Hill hill in hills)
        {
            hill.obj = Instantiate(hillObject, hill.spawnPosition, Quaternion.identity);
        }
        //FOLYÓK
        rivers = new List<River>();
        River river1 = new River(new Vector2(-3, -3));
        River river2 = new River(new Vector2(-5, 3));
        rivers.Add(river1);
        rivers.Add(river2);
        foreach (River river in rivers)
        {
            river.obj = Instantiate(riverObject, river.spawnPosition, Quaternion.identity);

        }
        //TAVAK
        ponds = new List<Pond>();
        Pond pond1 = new Pond(new Vector2(4, -4));
        ponds.Add(pond1);
        foreach (Pond pond in ponds)
        {
            pond.obj = Instantiate(pondObject, pond.spawnPosition, Quaternion.identity);

        }

        //entityk generálása
        //NÖVÉNYEK

        //ÁLLATOK

        //ORVVADÁSZOK

        //VADÕRÖK

        //MEGFIGYELÕ RENDSZER

        //JEEPS
        paths = new List<Path>();


        //felülírandók
        this.popularity = 1;
        this.visitorsWaiting = 0;
        this.visitorCount = 0;
        switch (difficulty)
        {
            case 1:
                DaysUntilWin = 3 * 30;
                money = 1000;
                break;
            case 2:
                DaysUntilWin = 6 * 30;
                money = 2000;
                break;
            case 3:
                DaysUntilWin = 12 * 30;
                money = 3000;
                break;
        }

        //metódus tesztelések
        buy("pond", new Vector2(0,0));
        move(hill1, new Vector2(-2, -2));
        
        
        
        updateView();
    }

    //MOZGÁS
    /*
     * moves an entity to a position
     */
    private bool isMoving;
    private Vector2 targetPosition1;
    private SpriteRenderer sr;
    private float fspeed = 1f;
    public void move(Entity entity, Vector2 p)
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

    //VÁSÁRLÁS
    public bool canBuy(string obj)
    {
        //még nincs megcsinálva, hogy ne lehessen egymásra helyezni itemeket
        int moneyNeeded = -1;
        switch (obj)
        {
            case "water": moneyNeeded = 100; break;
            case "grass": moneyNeeded = 100; break;
            case "bush": moneyNeeded = 100; break;
            case "tree": moneyNeeded = 100; break;
            case "cheetah": moneyNeeded = 100; break;
            case "crocodile": moneyNeeded = 100; break;
            case "gazelle": moneyNeeded = 100; break;
            case "hipo": moneyNeeded = 100; break;
            case "jeep": moneyNeeded = 100; break;
            case "path": moneyNeeded = 100; break;
            case "camera": moneyNeeded = 100; break;
            case "charger": moneyNeeded = 100; break;
            case "drone": moneyNeeded = 100; break;
            case "airballon": moneyNeeded = 100; break;
            
        }
        return moneyNeeded <= this.money;
    }
    public void buy(string obj, Vector2 position)
    {
        if (canBuy(obj))
        {
            if (obj == "path")
            {
                float pathSize = 1.0f;
                position.x = Mathf.Round(position.x / pathSize) * pathSize;
                position.y = Mathf.Round(position.y / pathSize) * pathSize;
            }

            switch (obj)
            {
                //max mennyiséget kell írni bele(?)
                case "water":
                    Pond pond = new Pond(position);
                    ponds.Add(pond);
                    pond.obj = Instantiate(pondObject, pond.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "grass":
                    /* Plant plant = new Plant(position, "grass");
                     * plants.add(Plant);
                     * plant.obj = Instantiate(plantObject, plant.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "bush":
                    /* Plant plant = new Plant(position, "bush");
                     * plants.add(Plant);
                     * plant.obj = Instantiate(plantObject, plant.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "tree":
                    /* Plant plant = new Plant(position, "tree");
                     * plants.add(plant);
                     * plant.obj = Instantiate(plantObject, plant.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "cheetah":
                    /*
                     * AnimalGroup animal = new AnimalGroup(position, "cheetah");
                     * animalGroups.add(animal);
                     * animal.obj = Instantiate(animalGroupObject, animal.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "crocodile":
                    /*
                     * AnimalGroup animal = new AnimalGroup(position, "crocodile");
                     * animalGroups.add(animal);
                     * animal.obj = Instantiate(animalGroupObject, animal.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "gazelle":
                    /*
                     * AnimalGroup animal = new AnimalGroup(position, "gazelle");
                     * animalGroups.add(animal);
                     * animal.obj = Instantiate(animalGroupObject, animal.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "hipo":
                    /*
                     * AnimalGroup animal = new AnimalGroup(position, "hipo");
                     * animalGroups.add(animal);
                     * animal.obj = Instantiate(animalGroupObject, animal.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "jeep":
                    /* Jeep jeep = new Jeep(position);
                     * jeeps.add(jeep);
                     * jeep.obj = Instantiate(jeepObject, jeep.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "path":
                    Path path = new Path(position);
                    paths.Add(path);
                    path.obj = Instantiate(pathObject, path.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "camera":
                    /* SecuritySystem securityItem = new SecuritySystem(position, "camera");
                     * security.add(securityItem);
                     * securityItem.obj = Instantiate(securityObject, securityItem.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    //ellenõrizni kell még, hogy van-e hozzá már töltõ
                    break;
                case "charger":
                    /* SecuritySystem securityItem = new SecuritySystem(position, "charger");
                     * security.add(securityItem);
                     * securityItem.obj = Instantiate(securityObject, securityItem.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "drone":
                    /* SecuritySystem securityItem = new SecuritySystem(position, "drone");
                     * security.add(securityItem);
                     * securityItem.obj = Instantiate(securityObject, securityItem.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
                case "airballon":
                    /* SecuritySystem securityItem = new SecuritySystem(position, "airballon");
                     * security.add(securityItem);
                     * securityItem.obj = Instantiate(securityObject, securityItem.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    break;
            }
        }
        updateView();
    }

    //VIEW FRISSÍTÉSE
    public void updateView()
    {
        moneyText.text = "Pénz: " + this.money;
        timeText.text = "0. hét, 0. nap, 00:00";

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
     *rossz mappában van az animalgroup
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
    }

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
        updateView();
    }
    public int getMoney()
    {
        return this.money;
    }

    public void setTime(int time)
    {
        this.time = time;
        updateView();
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
