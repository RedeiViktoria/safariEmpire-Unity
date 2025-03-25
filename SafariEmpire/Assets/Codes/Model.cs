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
using Codes.animal;
using System.Threading;

public class Model : MonoBehaviour
{
    private int difficulty; //1: easy, 2:medium, 3:hard
    private int money;
    //time
    private int hour;
    private int day;
    private int week;
    private int timeSpeed; //0: óra, 1: nap, 2: hét (?)

    private int DaysUntilWin; //1 hónap = 30 nap
    private int popularity;
    private int ticketPrice; //ticket az UML-ben
    private int visitorsWaiting;
    private int visitorCount;

    //entityk
    public GameObject jeepObject;
    //public List<Jeep> jeeps;
    //public List<SecuritySystem> security;
    public GameObject securityObject;
    //public List<Ranger> rangers;
    //rangerObject
    //public List<Poacher> poachers;
    //poacherObject

    //állatok
    public List<AnimalGroup> animalGroups;
    public GameObject cheetahObject;
    public GameObject hippoObject;
    public GameObject gazelleObject;
    public GameObject crocodileObject;

    //növények
    public GameObject treeObject;
    public GameObject grassObject;
    public GameObject bushObject;
    public List<Plant> plants;

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
        this.paths = new List<Path>();
        //idõ telés:
        StartCoroutine(TimerCoroutine());


        this.difficulty = PlayerPrefs.GetInt("difficulty");
        this.timeSpeed = 1; //viewból setTimeSpeed-del módosítódik
        this.ticketPrice = 100; //viewból setTicketPrice-szal módosítódik
        this.hour = 0;
        this.day = 0;
        this.week = 0;

        //terepi akadályok generálása
        //HEGYEK
        hills = new List<Hill>();
        Hill hill1 = new Hill(new Vector2(5, 1));
        Hill hill2 = new Hill(new Vector2(-3, 3));
        Hill hill3 = new Hill(new Vector2(-7, -7));
        hills.Add(hill1);
        hills.Add(hill2);
        hills.Add(hill3);
        foreach (Hill hill in hills)
        {
            hill.obj = Instantiate(hillObject, hill.spawnPosition, Quaternion.identity);
        }
        //FOLYÓK
        rivers = new List<River>();
        River river1 = new River(new Vector2(-4, -2));
        River river2 = new River(new Vector2(-10, 6));
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
        plants = new List<Plant>();
        Bush plant1 = new Bush(new Vector2(6, 8));
        Tree plant2 = new Tree(new Vector2(5, 5));
        Grass plant3 = new Grass(new Vector2(7, 3));
        Grass plant4 = new Grass(new Vector2(-4, -6));
        plants.Add(plant1);
        plants.Add(plant2);
        plants.Add(plant3);
        plants.Add(plant4);
        foreach (Plant plant in plants)
        {
            if (plant.GetType() == typeof(Grass))
            {
                plant.obj = Instantiate(grassObject, plant.spawnPosition, Quaternion.identity);
            } else if (plant.GetType() == typeof(Tree))
            {
                plant.obj = Instantiate(treeObject, plant.spawnPosition, Quaternion.identity);
            } else
            {
                plant.obj = Instantiate(bushObject, plant.spawnPosition, Quaternion.identity);
            }

        }

        //ÁLLATOK
        animalGroups = new List<AnimalGroup>();
        AnimalGroup animal1 = new AnimalGroup(new Vector2(0, 0), "cheetah");
        AnimalGroup animal2 = new AnimalGroup(new Vector2(0, 0), "crocodile");
        AnimalGroup animal3 = new AnimalGroup(new Vector2(0, 0), "hippo");
        AnimalGroup animal4 = new AnimalGroup(new Vector2(0, 0), "gazelle");
        animalGroups.Add(animal1);
        animalGroups.Add(animal2);
        animalGroups.Add(animal3);
        animalGroups.Add(animal4);
        foreach (AnimalGroup animal in animalGroups)
        {
            Debug.Log(animal.animals[0].GetType());
            if(animal.animals[0].GetType() == typeof(Crocodile))
            {
                animal.obj = Instantiate(crocodileObject, animal.spawnPosition, Quaternion.identity);
            } else if (animal.animals[0].GetType() == typeof(Gazella))
            {
                animal.obj = Instantiate(gazelleObject, animal.spawnPosition, Quaternion.identity);
            } else if (animal.animals[0].GetType() == typeof(Hippo))
            {
                animal.obj = Instantiate(hippoObject, animal.spawnPosition, Quaternion.identity);
            } else
            {
                animal.obj = Instantiate(cheetahObject, animal.spawnPosition, Quaternion.identity);
            }

        }

        //ORVVADÁSZOK

        //VADÕRÖK

        //MEGFIGYELÕ RENDSZER

        //JEEPS


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

        Debug.Log(difficulty);

        //metódus tesztelések
        buy("pond", new Vector2(0, 0));
        //move(hill1, new Vector2(-2, -2));



        updateView();
    }
    //TIME SYSTEM
    public void updateTime()
    {
        if (this.hour >= 24)
        {
            this.hour = 0;
            this.day++;
        }
        if (this.day >= 7)
        {
            this.day = 0;
            this.week++;
        }
        updateView();
    }
    IEnumerator TimerCoroutine()
    {
        while (true) // Végtelen ciklus
        {
            switch (this.timeSpeed)
            {
                case 1: this.hour++; break;
                case 2: this.day++; break;
                case 3: this.week++; break;
            }
            updateTime();
            yield return new WaitForSeconds(1f); // 1 másodperces várakozás
        }
    }

    //MOZGÁS
    /*
     * moves an entity to a position
     */
    private float fspeed = 1f;
    public void move(AnimalGroup entity, Vector2 p)
    {
        //this.targetPosition1 = p;
        entity.obj.transform.position = Vector2.MoveTowards(entity.obj.transform.position, p, fspeed * Time.deltaTime);
        if (Vector2.Distance(entity.obj.transform.position, p) < 0.01f)
        {
            entity.targetPosition = new Vector2(UnityEngine.Random.Range(-14, 15), UnityEngine.Random.Range(-14, 15));
        }
    }
    private void FixedUpdate()
    {
        tempMove();
    }
    //ideiglenes mozgás
    public void tempMove()
    {
        foreach (AnimalGroup group in this.animalGroups)
        {
            move(group, group.targetPosition);
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
                     Grass grass = new Grass(position);
                     plants.Add(grass);
                     grass.obj = Instantiate(grassObject, grass.spawnPosition, Quaternion.identity);
                     this.money -= 100;
                    break;
                case "bush":
                    Bush bush = new Bush(position);
                    plants.Add(bush);
                    bush.obj = Instantiate(bushObject, bush.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "tree":
                    Tree tree = new Tree(position);
                    plants.Add(tree);
                    tree.obj = Instantiate(treeObject, tree.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "cheetah":
                     AnimalGroup cheetah = new AnimalGroup(position, "cheetah");
                     animalGroups.Add(cheetah);
                     cheetah.obj = Instantiate(cheetahObject, cheetah.spawnPosition, Quaternion.identity);
                     this.money -= 100;
                    break;
                case "crocodile":
                     AnimalGroup crocodile = new AnimalGroup(position, "crocodile");
                     animalGroups.Add(crocodile);
                    crocodile.obj = Instantiate(crocodileObject, crocodile.spawnPosition, Quaternion.identity);
                     this.money -= 100;
                    break;
                case "gazelle":
                     AnimalGroup gazelle = new AnimalGroup(position, "gazelle");
                     animalGroups.Add(gazelle);
                    gazelle.obj = Instantiate(gazelleObject, gazelle.spawnPosition, Quaternion.identity);
                     this.money -= 100;
                    break;
                case "hippo":
                     AnimalGroup hippo = new AnimalGroup(position, "hippo");
                     animalGroups.Add(hippo);
                     hippo.obj = Instantiate(hippoObject, hippo.spawnPosition, Quaternion.identity);
                     this.money -= 100;
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
                     ConnectPaths(path);
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
        timeText.text = this.week + ". hét, " + this.day + ". nap, " + this.hour + ". óra";

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
        for(int i = 0; i<group.getAnimals().count; i++)
        {
            if (group.getAnimals()[i].age > maxage)
            {
                maxage = group.getAnimals()[i].age;
                maxind = i;
            }
        }
        group.getAnimals().RemoveAt(maxind);

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

    //összefüggõ utak
    private void ConnectPaths(Path newPath)
    {
        bool connected = false;

        // Iterate through existing paths to find neighboring paths
        foreach (Path path in paths)
        {
            if (path != newPath && path.IsAdjacent(newPath))
            {
                // If the new path is adjacent to an existing path, connect them
                newPath.neighbors.Add(path);
                path.neighbors.Add(newPath);
                connected = true;
            }
        }

        // If no connection is made, you might want to create intermediate paths
        if (!connected)
        {
            CreateIntermediatePaths(newPath);
        }
    }

    private void CreateIntermediatePaths(Path newPath)
    {
        // Find the nearest existing path to connect to
        Path nearestPath = FindNearestPath(newPath);
        if (nearestPath != null)
        {
            // Determine direction to create intermediate paths
            Vector2 direction = (nearestPath.spawnPosition - newPath.spawnPosition).normalized;

            // Calculate the distance between the two paths
            float distance = Vector2.Distance(newPath.spawnPosition, nearestPath.spawnPosition);

            // Create intermediate paths along the path between them
            int intermediateCount = Mathf.FloorToInt(distance / 1.0f); // grid size
            for (int i = 1; i <= intermediateCount; i++)
            {
                Vector2 intermediatePosition = newPath.spawnPosition + direction * i * 1.0f;
                intermediatePosition.x = Mathf.Round(intermediatePosition.x);
                intermediatePosition.y = Mathf.Round(intermediatePosition.y);

                // Create the intermediate path
                Path intermediatePath = new Path(intermediatePosition);
                paths.Add(intermediatePath);
                intermediatePath.obj = Instantiate(pathObject, intermediatePath.spawnPosition, Quaternion.identity);

                // Connect the intermediate path to the newPath and nearestPath
                newPath.neighbors.Add(intermediatePath);
                intermediatePath.neighbors.Add(newPath);

                nearestPath.neighbors.Add(intermediatePath);
                intermediatePath.neighbors.Add(nearestPath);
            }

            // Now connect the original path and the nearest path
            newPath.neighbors.Add(nearestPath);
            nearestPath.neighbors.Add(newPath);
        }
    }



    // Helper method to find the nearest existing path to the new path
    private Path FindNearestPath(Path newPath)
    {
        Path nearest = null;
        float minDistance = float.MaxValue;

        // Iterate through all paths to find the nearest one (this could be more efficient)
        foreach (Path path in paths)
        {
            if (path == newPath) continue;

            float distance = Vector2.Distance(newPath.spawnPosition, path.spawnPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = path;
            }
        }

        return nearest;
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

    public void setHour(int hour)
    {
        this.hour = hour;
        updateView();
    }
    public int getHour()
    {
        return this.hour;
    }

    public void setDay(int day)
    {
        this.day = day;
        updateView();
    }
    public int getDay()
    {
        return this.day;
    }

    public void setWeek(int week)
    {
        this.week = week;
        updateView();
    }
    public int getWeek()
    {
        return this.week;
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
