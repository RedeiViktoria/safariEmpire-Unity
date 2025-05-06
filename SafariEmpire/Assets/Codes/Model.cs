using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Codes.animal;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Codes.Security;
using JetBrains.Annotations;
using NUnit.Framework;
public class Model : MonoBehaviour
{
    //win conditions -> balanceolni kell
    public const int herbivoreNeed = 5;
    public const int carnivoreNeed = 5;
    public const int visitorsNeed = 10;
    public const int moneyNeed = 1000;
    public int weeksUntilWin;
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
    public List<Jeep> jeeps;
    public List<SecuritySystem> security;
    public GameObject airballoonObject;
    public GameObject droneObject;
    public GameObject cameraObject;

    //vadőrök
    public List<Ranger> rangers;
    public GameObject rangerObject;

    //orvvadászok
    public List<Poacher> poachers;
    public GameObject poacherObject;
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
    //utak
    public List<Path> paths;
    public List<List<Path>> validPaths;
    public GameObject pathObject;
    public GameObject startObj;
    public GameObject endObj;
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
    public TextMeshProUGUI elegedettsegText;
    public TextMeshProUGUI visitorsText;
    //win conditions page
    public TextMeshProUGUI latogatokText;
    public TextMeshProUGUI novenyevokText;
    public TextMeshProUGUI ragadozokText;
    public TextMeshProUGUI penzText;
    public TextMeshProUGUI hetekText;

    //constructor
    void Start()
    {
        this.jeeps = new List<Jeep>();
        this.paths = new List<Path>();
        this.validPaths = new List<List<Path>>();
        Path startPath = new Path((Vector2)startObj.transform.position);
        startPath.obj = startObj;
        Path endPath = new Path((Vector2)endObj.transform.position);
        endPath.obj = endObj;
        Path between = new Path(new Vector2(0, -1));
        between.obj = Instantiate(pathObject, between.spawnPosition, Quaternion.identity);
        paths.Add(startPath);
        paths.Add(endPath);
        paths.Add(between); //Start és end között




        this.difficulty = PlayerPrefs.GetInt("difficulty");
        this.timeSpeed = 1; //viewból setTimeSpeed-del módosítódik
        this.ticketPrice = 100; //viewból setTicketPrice-szal módosítódik
        this.hour = 0;
        this.day = 0;
        this.week = 0;

        //terepi akadályok generálása
        //HEGYEK
        hills = new List<Hill>();
        Hill hill1 = new Hill(new Vector2(8, 8));
        Hill hill2 = new Hill(new Vector2(-12, 5));
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
        River river2 = new River(new Vector2(-9, 6));
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
        Bush plant1 = new Bush(new Vector2(4, 8));
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
        AnimalGroup animal1 = new AnimalGroup(new Vector2(10, 0), Codes.animal.AnimalType.Gepard);
        AnimalGroup animal2 = new AnimalGroup(new Vector2(0, 10), Codes.animal.AnimalType.Crocodile);
        AnimalGroup animal3 = new AnimalGroup(new Vector2(-10, 0), Codes.animal.AnimalType.Hippo);
        AnimalGroup animal4 = new AnimalGroup(new Vector2(0, -10), Codes.animal.AnimalType.Gazella);
        animalGroups.Add(animal1);
        animalGroups.Add(animal2);
        animalGroups.Add(animal3);
        animalGroups.Add(animal4);
        foreach (AnimalGroup animal in animalGroups)
        {
            //Debug.Log(animal.animals[0].GetType());
            if (animal.animals[0].GetType() == typeof(Crocodile))
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
        this.poachers = new List<Poacher>();
        Poacher poacher1 = new Poacher(new Vector2(UnityEngine.Random.Range(-15, 16), UnityEngine.Random.Range(-15, 16)));
        this.poachers.Add(poacher1);
        poacher1.obj = Instantiate(poacherObject, poacher1.spawnPosition, Quaternion.identity);
        InvokeRepeating("makePoacher", 0f, 30f);

        //VADŐRÖK
        this.rangers = new List<Ranger>();
        /*Ranger ranger1 = new Ranger(new Vector2(UnityEngine.Random.Range(-15, 16), UnityEngine.Random.Range(-15, 16)), "0");
        this.rangers.Add(ranger1);
        ranger1.obj = Instantiate(rangerObject, ranger1.spawnPosition, Quaternion.identity);*/

        //MEGFIGYELŐ RENDSZER
        List<Vector2> waypoints1 = new List<Vector2>();
        waypoints1.Add(new Vector2(1, 1));
        waypoints1.Add(new Vector2(2, 1));
        waypoints1.Add(new Vector2(1, 2));
        List<Vector2> waypoints2 = new List<Vector2>();
        waypoints2.Add(new Vector2(0, 2));
        waypoints2.Add(new Vector2(1, 2));
        waypoints2.Add(new Vector2(2, 1));
        Vector2 charger = new Vector2(0, 1);

        
        security = new List<SecuritySystem>();
        Codes.Security.Camera camera = new Codes.Security.Camera(new Vector2(5, 1));
        AirBalloon airballon = new AirBalloon(new Vector2(-3, 3), waypoints1);
        Drone drone = new Drone(charger, waypoints2, charger);
        security.Add(camera);
        security.Add(airballon);
        security.Add(drone);
        foreach (SecuritySystem s in security)
        {
            if (s.GetType() == typeof(AirBalloon))
            {
                s.obj = Instantiate(airballoonObject, s.spawnPosition, Quaternion.identity);

            }
            if (s.GetType() == typeof(Codes.Security.Camera))
            {
                s.obj = Instantiate(cameraObject, s.spawnPosition, Quaternion.identity);

            }
            if (s.GetType() == typeof(Drone))
            {
                s.obj = Instantiate(droneObject, s.spawnPosition, Quaternion.identity);

            }

        }
        
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
        //metódus tesztelések
        buy("pond", new Vector2(0, 0));
        //move(hill1, new Vector2(-2, -2));


        StartCoroutine(AnimalTimeCoroutine());
        //idő telés:
        StartCoroutine(TimerCoroutine());
        StartCoroutine(sendJeep());
        updateView();
    }
   
    public void chooseSecurityPath(char i, List<Vector2> waypoints)
    {
        switch(i)
        {
            case 'a':
                {
                    waypoints.Add(new Vector2(1, 1));
                    waypoints.Add(new Vector2(2, 1));
                    waypoints.Add(new Vector2(1, 2));
                 
                }
                break;
            case 'b':
                {
                    waypoints.Add(new Vector2(0, 2));
                    waypoints.Add(new Vector2(1, 2));
                    waypoints.Add(new Vector2(2, 1));
                    waypoints.Add(new Vector2(2, 2));

                }
                break;
            case 'c':
                {
                    waypoints.Add(new Vector2(0, 1));
                    waypoints.Add(new Vector2(1, 1));
                    waypoints.Add(new Vector2(1, 2));
                    waypoints.Add(new Vector2(2, 2));
                    waypoints.Add(new Vector2(2, 1));
                }
                break;
        }
    }
    //TIME SYSTEM
    public void updateTime()
    {
        if (this.hour >= 24)
        {
            this.hour = 0;
            this.day++;
            this.visitorsWaiting += (int)Math.Round((decimal)newVisitors()/4);
        }
        if (this.day >= 7)
        {
            this.day = 0;
            this.week++;
            //havonta=4 hetente fizess�k ki a vad�r�ket
            if (this.week % 4 == 0)
            {
                payCheck();
            }
            checkWin();
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
                case 2: this.day++; this.visitorsWaiting += newVisitors(); break;
                case 3:
                    this.week++;
                    this.visitorsWaiting += 7*newVisitors();
                    //havonta=4 hetente fizess�k ki a vad�r�ket
                    if (this.week % 4 == 0)
                    {
                        payCheck();
                    }
                    checkWin();
                    break;
            }
            updateTime();
            yield return new WaitForSeconds(1f); // 1 másodperces várakozás
        }
    }
    IEnumerator sendJeep()
    {
        while (true) // Végtelen ciklus
        {
            if (visitorsWaiting >= 4)
            {
                int i = 0;
                while (i < jeeps.Count && jeeps[i].moving == true)
                {
                    i += 1;
                }

                if (i < jeeps.Count && validPaths.Count > 0)
                {
                    jeeps[i].chooseRandomPath(validPaths);
                    jeeps[i].moving = true;
                    jeeps[i].isFinished = false;
                    jeeps[i].encounteredAnimals.Clear();
                    visitorsWaiting -= 4;

                    //bevétel a jegyvásárlásból
                    money += ticketPrice * 4;
                }
            }
            yield return new WaitForSeconds(0.5f); // 4 másodperces várakozás
        }
    }
    public int newVisitors()
    {
        //based on: ticketprice, popularity
        //ticketprice: százas/ezres nagyságrend (feltételezés kb. 100-10.000)
        //popularity: folyton nő max. kb 40-nel (a calulateSatisfaction() alapján)
        //cél: naponta 1-30 látogató 

        //normalizálás: kisebb ticketprice -> nagyobb embi szám
        //1, ha a ticketprice minimális (100); 0, ha maximális (10000)
        double normA = 1.0 - ((ticketPrice - 100.0) / (10000.0 - 100.0)); 

        //normalizálás: ugyanúgy 0 és 1 közé
        //ez egy smooth növekedés, 200 felett stagnálni kezd (hogy ne várakozzon túl sok embi folyamatosan)
        double normB = Math.Tanh(popularity / 100.0); 

        //magasabb ticketprice + alacsonyabb populatiry -> kevesebb ember
        //alacsonyabb ticketprice + magasabb popularity -> több ember
        //a 0 és 1 közötti számok kihozzák az arányokat
        double c = 4 + 16 * normA * normB;

        //4 a minimum látogató, 20 a max, aki egy nap alatt jön
        return (int)Math.Round(c);
    }

    //MOZGÁS
    /*
     * moves an entity to a position
     */
    private float fspeed = 1f;
    //a move jelenleg az animalGroupokat mozgatja random p hely felé
    public void move(Entity entity, Vector2 p)
    {
        entity.obj.transform.position = Vector2.MoveTowards(entity.obj.transform.position, p, fspeed * Time.deltaTime);
        if ((Vector2)entity.obj.transform.position == p)
        {
            entity.targetPosition = new Vector2(UnityEngine.Random.Range(-14, 15), UnityEngine.Random.Range(-14, 15));
            changeSortingOrder(entity);
        }
    }
    public void movePoacher(Poacher poacher, Vector2 p)
    {
        //mozogjon a p position felé
        poacher.obj.transform.position = Vector2.MoveTowards(poacher.obj.transform.position, p, fspeed * Time.deltaTime);
        if (Vector2.Distance(poacher.obj.transform.position, p) < 0.01f) //ha elérte a p positiont
        {
            //melyik animalgroup van a közelében ami targetanimal type-pal megfelel
            AnimalGroup group = detectAnimal(poacher.targetAnimal, poacher.obj.transform.position, poacher.visionRange);
            if (null!=group)
            {
                //ha volt a k�zel�ben target type animalGroup akkor meg�l bel�le egy �llatot, majd elt�nik � maga is
                int survivors = group.killAnimal();
                Debug.Log("poacher killed");
                if (survivors <= 0)
                {
                    this.animalGroups.Remove(group);
                    Destroy(group.obj);
                }
                this.poachers.Remove(poacher);
                Destroy(poacher.obj);
            } else
            {
                //ha nem volt a közelében target type animalGroup akkor új random pozíció irányába indul cxy
                poacher.targetPosition = new Vector2(poacher.obj.transform.position.x + UnityEngine.Random.Range(-poacher.visionRange, poacher.visionRange+1), poacher.obj.transform.position.y + UnityEngine.Random.Range(-poacher.visionRange, poacher.visionRange+1));
                changeSortingOrder(poacher);
            }
        }
    }
    public void moveRanger(Ranger ranger, Vector2 p)
    {
        //mozogjon a p position felé
        ranger.obj.transform.position = Vector2.MoveTowards(ranger.obj.transform.position, p, fspeed * Time.deltaTime);
        if (Vector2.Distance(ranger.obj.transform.position, p) < 0.01f)  //ha elérte a p positiont
        {
            if (ranger.target == 0) //ha poacher a targetje
            {
                Poacher poacher = detectPoacher(ranger.obj.transform.position, ranger.visionRange);
                if (null != poacher)
                {
                    Destroy(poacher.obj);
                    //Debug.Log("ranger");
                    this.poachers.Remove(poacher);
                }
                //ha talált poachert, ha nem, új targetPosition-t kap
                ranger.targetPosition = new Vector2(ranger.obj.transform.position.x + UnityEngine.Random.Range(-ranger.visionRange, ranger.visionRange + 1), ranger.obj.transform.position.y + UnityEngine.Random.Range(-ranger.visionRange, ranger.visionRange + 1));
                changeSortingOrder(ranger);
            } else //ha valamilyen állat a targetje
            {
                AnimalGroup group = detectAnimal(ranger.targetAnimal, ranger.obj.transform.position, ranger.visionRange);
                if (null != group)
                {
                    //ha volt a közelében target type animalGroup akkor megöl belőle egy állatot, majd eltűnik ő maga is
                    //killAnimal();
                    int survivors = group.killAnimal();
                    this.money += 500; //amit a kil�tt �llat�rt kapunk
                    if (survivors <= 0)
                    {
                        Destroy(group.obj);
                        this.animalGroups.Remove(group);
                    }
                    //ranger.target = 0; //legyen megint poacher a targetje
                }
                //mindenképp új targetPosition-t kap
                ranger.targetPosition = new Vector2(ranger.obj.transform.position.x + UnityEngine.Random.Range(-ranger.visionRange, ranger.visionRange + 1), ranger.obj.transform.position.y + UnityEngine.Random.Range(-ranger.visionRange, ranger.visionRange + 1));
                changeSortingOrder(ranger);
            }
        }
    }
    public void changeSortingOrder(Entity entity)
    {
        SpriteRenderer sr = entity.obj.GetComponent<SpriteRenderer>();
        if (entity.targetPosition.y < entity.obj.transform.position.y)
        {
            sr.sortingOrder = 0;
        }
        else
        {
            sr.sortingOrder = 10;
        }
    }
    private void FixedUpdate()
    {
        tempMove();
        jeepMove();
        poacherVisibility();
    }
    //ideiglenes mozgás
    public void tempMove()
    {
        if (this.animalGroups.Count > 0)
        {
            for (int i = 0; i < this.animalGroups.Count; i++)
            {
                if (animalGroups[i] != null && animalGroups[i].Panicked == 0 && animalGroups[i].obj != null)
                {
                    move(animalGroups[i], animalGroups[i].targetPosition);
                }

            }
        }

        if (this.poachers.Count > 0)
        {
            for (int i = 0; i < this.poachers.Count; i++)
            {
                Poacher poacher = this.poachers[i];
                movePoacher(poacher, poacher.targetPosition);
            }
        }
        if (this.rangers.Count > 0)
        {
            for (int i = 0; i < this.rangers.Count; i++)
            {
                Ranger ranger = this.rangers[i];
                moveRanger(ranger, ranger.targetPosition);
            }
        }
        
        if (this.security.Count > 0)
        {
            foreach (SecuritySystem securitySystem in this.security)
            {
                //detectPoacher(securitySystem.Position, securitySystem.Range);
                if (securitySystem.GetType() == typeof(AirBalloon))
                {
                    AirBalloon airBalloon = (AirBalloon)securitySystem;
                    airBalloon.Travel();
                }
                if (securitySystem.GetType() == typeof(Drone))
                {
                    Drone drone = (Drone)securitySystem;
                    float x = drone.obj.transform.position.x;
                    float y = drone.obj.transform.position.y;
                    if ((x != drone.charger.x || y != drone.charger.y) && drone.battery > 1 ||
                        (x == drone.charger.x && y == drone.charger.y) && drone.battery >= drone.MAX_BATTERY)
                    {
                        drone.Travel();
                    }
                    else if (x != drone.charger.x && y != drone.charger.y)
                    {
                        drone.GoBack();
                    }
                    else
                    {
                        if (drone.battery < drone.MAX_BATTERY) drone.Charge();
                    }
                }
            }
        }
    }

    //POACHER F�GGV�NYEK
    public void poacherVisibility()
    {
        bool visible = false;
        foreach (Poacher poacher in this.poachers)
        {
            visible = detectRangerOrJeep(poacher.obj.transform.position, 5);
            poacher.setVisibility(visible);
        }
    }
    //poacher-nek kell hogy van-e a k�zel�ben jeep vagy ranger
    public bool detectRangerOrJeep(Vector2 position, int range)
    {
        foreach (Ranger r in this.rangers)
        {
            float x = r.obj.transform.position.x;
            float y = r.obj.transform.position.y;
            if ((x < position.x + range && x > position.x - range) && (y < position.y + range && y > position.y - range))
            {
                return true;
            }
        }
        foreach(Jeep j in this.jeeps)
        {
            float x = j.obj.transform.position.x;
            float y = j.obj.transform.position.y;
            if ((x < position.x + range && x > position.x - range) && (y < position.y + range && y > position.y - range))
            {
                return true;
            }
        }
        return false;
    }
    //POACHER GENERÁTOR
    public void makePoacher()
    {
        Poacher poacher = new Poacher(new Vector2(UnityEngine.Random.Range(-15,16), UnityEngine.Random.Range(-15, 16)));
        this.poachers.Add(poacher);
        poacher.obj = Instantiate(poacherObject, poacher.spawnPosition, Quaternion.identity);
    }

    //DETECT
    //paraméterek: milyen typeot keresünk, hol, milyen range-ben
    //visszatér a megtalált animalGroup-pal ha van, ha nincs akkor null-lal
    public AnimalGroup detectAnimal(Codes.animal.AnimalType type, Vector2 position, int range)
    {
        //leszűri a megfelelő type-ú animalGroupokat
        List<AnimalGroup> list = new List<AnimalGroup>();
        for(int i = 0; i<this.animalGroups.Count; i++)
        {
            if (this.animalGroups[i].animalType == type)
            {
                list.Add(animalGroups[i]);
            }
        }
        //végigmegy a leszűrt listán hogy van-e valamelyik a közelben
        foreach (AnimalGroup a in list)
        {
            
            float x = a.obj.transform.position.x;
            float y = a.obj.transform.position.y;
            if ((x<position.x+range && x>position.x-range) && (y<position.y+range && y > position.y - range))
            {
                return a;
            }
        }
        return null;
    }

    public AnimalGroup detectForSatisfaction(Jeep jeep, int range) 
    {
        foreach (AnimalGroup a in this.animalGroups)
        {
            float x = a.obj.transform.position.x;
            float y = a.obj.transform.position.y;
            float jeep_x = jeep.obj.transform.position.x;
            float jeep_y = jeep.obj.transform.position.y;
            if ((x < jeep_x + range && x > jeep_x - range) && (y < jeep_y + range && y > jeep_y - range))
            {
                if(!jeep.encounteredAnimals.Contains(a)) 
                return a;
            }
        }
        return null;
    }
    //paraméterek: hol, milyen rangeben keresünk
    //visszatér a megtalált poacher-rel ha van, ha nincs akkor null-lal
    public Poacher detectPoacher(Vector2 position, int range)
    {
        foreach (Poacher p in this.poachers)
        {
            float x = p.obj.transform.position.x;
            float y = p.obj.transform.position.y;
            if ((x < position.x + range && x > position.x - range) && (y < position.y + range && y > position.y - range))
            {
                return p;
            }
        }
        return null;
    }
    //általános detectAnimal
    public AnimalGroup detectAnyAnimal(Vector2 position)
    {
        int range = 1;
        foreach (AnimalGroup a in this.animalGroups)
        {

            float x = a.obj.transform.position.x;
            float y = a.obj.transform.position.y;
            if ((x < position.x + range && x > position.x - range) && (y < position.y + range && y > position.y - range))
            {
                return a;
            }
        }
        return null;
    }
    //Animal Stufff
    //------------------

    public void clearAnimals()
    {
        for (int i = animalGroups.Count - 1; i >= 0; i--)
        {
            if (animalGroups[i] != null && animalGroups[i].Merged)
            {
              //  Debug.Log(animalGroups[i].animalType + " is merged!");
                if (animalGroups[i].obj != null)
                    Destroy(animalGroups[i].obj);

                animalGroups.RemoveAt(i);
            }
        }
    }
    IEnumerator AnimalTimeCoroutine()
    {
        while (true) // V�gtelen ciklus
        {
            for (int i = 0; i < this.animalGroups.Count; i++)
            {
                //Debug.Log("Group is starving: " + AnimalGroup.CountAverageHunger(animalGroups[i]) +"\n thirst = " + animalGroups[i].AverageThirst());
                
                animalGroups[i].Starve();
                animalGroups[i].Age();
                animalGroups[i].Die();
                
            }
            clearAnimals();
            for (int i = 0; i < this.animalGroups.Count; i++)
            {
                LookForObject(animalGroups[i]);
                
                if (animalGroups[i].Panicked > 0) animalGroups[i].Panicked--;
                else 
                {

                    DoAnimalCycle(animalGroups[i]);
                    //Move(animalGroups[i], animalGroups[i].targetPosition);
                }

            }
            for (int j = 0; j < this.plants.Count; j++)
            {
                plants[j].Grow();
            }
            yield return new WaitForSeconds(1f); // 1 m�sodperces v�rakoz�s
        }
    }
    public void DoAnimalCycle(AnimalGroup group)
    {
        if (group.Panicked > 0)
        { 
            return;
        }
        if (group.IsHungry())
        {
           // Debug.Log("Group is hungry");
            TryToEat(group);
        }
        else if (group.IsThirsty())
        {
        //    Debug.Log("Group is thirsty");
            GoDrink(group);
        }
        else
        {
            group.Mate();
        }
    }
    //GetGroupType() = animalType (adattag) Csak elfelejtettem hogy hivatkozhatunk r� 
    public void LookForObject(AnimalGroup group)
    {
        group.currentActivity = "A csoport mozog";
        if (group.IsHerbivore())
        {
            List<Plant> plants = detectPlants(group);
            if (plants != null)
            {
                foreach (Plant plant in plants)
                {
                    if (group.addPlantPlace(plant))
                    {
                       // Debug.Log(group.GetGroupType() +" found Plant");
                    }
                }
            }
            
        }
        for (int i = 0; i < this.animalGroups.Count; i++)
        {
            if (animalGroups[i] != group && animalGroups[i].GetGroupType() == group.GetGroupType())
            {
                if (FoodSourceCloseEnough(animalGroups[i].Position, group.Position, 1))
                {
                  //  Debug.Log(group.GetGroupType() + "Tried to merge");
                    if (animalGroups[i].MergeGroups(group))
                    {
                 //       Debug.Log("Merge success");
                    }
                }
            }
        }
        foreach (Pond pond in this.ponds)
        {
            if (Vector2.Distance(pond.Position, group.Position) <= group.Vision)
            {

                if (group.addWaterPlace(pond.Position))
                {
                //    Debug.Log(group.animalType + " found Pond");
                }
            }
        }
        foreach (River river in this.rivers)
        {
            if (Vector2.Distance(river.Position, group.Position) <= group.Vision)
            {
                if (group.addWaterPlace(river.Position))
                {
                //    Debug.Log(group.animalType + " found River");
                }
            }
        }
        
    }
    public AnimalGroup detectHervivore(Vector2 position, int range)
    {

        List<AnimalGroup> list = new List<AnimalGroup>();
        for (int i = 0; i < this.animalGroups.Count; i++)
        {
            if (this.animalGroups[i].animalType == AnimalType.Gazella || this.animalGroups[i].animalType == AnimalType.Hippo)
            {
                list.Add(animalGroups[i]);
            }
        }
        foreach (AnimalGroup a in list)
        {
            float x = a.obj.transform.position.x;
            float y = a.obj.transform.position.y;
            if ((x < position.x + range && x > position.x - range) && (y < position.y + range && y > position.y - range))
            {
            //    Debug.Log("Group found Herbivore");
                return a;
                
            }
        }
        return null;
    }
    public List<Plant> detectPlants(AnimalGroup group)
    {
        List<Plant> list = new List<Plant>();
        if (group == null || group.obj == null) return null;
        Vector2 position = group.obj.transform.position;
        int range = group.Vision;
        foreach (Plant plant in this.plants)
        {
            float x = plant.obj.transform.position.x;
            float y = plant.obj.transform.position.y;
            if ((x < position.x + range && x > position.x - range) && (y < position.y + range && y > position.y - range))
            {
                list.Add(plant);
                group.addPlantPlace(plant);
            //    Debug.Log("Group found plant");
            }
        }
        if (list.Count == 0) return null;
        return list;
       
    }
    public bool FoodSourceCloseEnough(Vector2 eater, Vector2 feeder, int distance)
    {

        if (Vector2.Distance(eater, feeder) <= distance)
        {
            return true;
        }
        return false;
    }

    public void TryToEat(AnimalGroup group)
    {
       // Debug.Log("Group is trying to eat!");
        if (group.IsCarnivore())
        {
            AnimalGroup meal = detectHervivore(group.obj.transform.position, group.Vision);
            if (meal == null) return;
            if (FoodSourceCloseEnough(group.obj.transform.position, meal.obj.transform.position, 1))
            {
                for (int i = 0; i < group.AmountToEat(); i++)
                {
                 //   Debug.Log(group.animalType + " is eating " + meal.animalType);
                    group.Eat(150);
                    meal.Panicked = 2;
                    group.Panicked = 2;
                    int remaining = meal.killAnimal();
                    if (remaining <= 0)
                    {
                        return;
                    }

                }
            }
            else
            {
                meal.Panicked = 3;
                group.targetPosition = meal.Position;
                return;
            }
        }
        else if (group.IsHerbivore())
        {
            Plant target = Herbivore.SelectClosestPlantTarget(group);
            if (target == null) return;

            if (FoodSourceCloseEnough(group.Position, target.Position, 1))
            {
                group.Eat(target.GetEaten());
                group.Panicked = 2;
                return;
            }
            else
            {
                group.targetPosition = target.Position;
                return;
            }
        }
    }
    public void GoDrink(AnimalGroup group)
    {

        if (group.WaterPlaces.Count == 0)
        {
            return;
        }
        Vector2 goal = group.targetPosition;
        HashSet<Vector2> waterSources = group.WaterPlaces;
        float minDistance = float.MaxValue;
        foreach (Vector2 source in waterSources)
        {
            float distance = Vector2.Distance(source, group.Position);
            if(distance < 1)
            {
                group.Panicked = 3;
                group.Drink();
                return;
            }    
           
            if (distance < minDistance)
                {
                    minDistance = distance;
                    goal = source;
                }
                
        }
        group.targetPosition = goal;
    }
    //------------
    public int calculateSatisfaction(List<AnimalGroup> encounteredAnimals) 
    {
        //a legritkább állatfaj éri a legtöbbet
        //ennek aktuális állását (az egyszerűség kedvéért) a beérkezéskor számítjuk ki
        List<(AnimalType type, int count)> typeCounts = new List<(AnimalType type, int count)> ();
        int hippoCount = 0, gepardCount = 0, gazellaCount = 0, crocoCount = 0;
        
        foreach(AnimalGroup a in this.animalGroups)
        {
            switch(a.animalType)
            {
                case AnimalType.Hippo:     hippoCount += a.animals.Count(); break;
                case AnimalType.Gazella: gazellaCount += a.animals.Count(); break;
                case AnimalType.Gepard:   gepardCount += a.animals.Count(); break;
                case AnimalType.Crocodile: crocoCount += a.animals.Count(); break;
            }
        }

        typeCounts.Add(new (AnimalType.Hippo, hippoCount));
        typeCounts.Add(new (AnimalType.Gazella, gazellaCount));
        typeCounts.Add(new (AnimalType.Gepard, gepardCount));
        typeCounts.Add(new (AnimalType.Crocodile, crocoCount));

        //csökkenően sorba rendezi az állatok összesített száma alapján (kevesebb => ritkább)
        typeCounts.Sort((a, b) => b.count.CompareTo(a.count));

        int satisfaction = 0;

        if (encounteredAnimals.Count < 1)
        {
            return -(int)Math.Round(popularity * 0.2);
        }

        foreach (AnimalGroup a in encounteredAnimals)
        {
            for (int i = 0; i < typeCounts.Count; i++) 
            {
                //az elégedettség a látott állatok ritkasága alapján 1/2/3/4-gyel nő 
                if (a.animalType == typeCounts[i].type)
                {
                    satisfaction += i + 1 * a.animals.Count();
                }
            }
        }
        return satisfaction; 
    }

    public int idx = 0;
    public void jeepMove()
    {
        if (validPaths.Count > 0)
        {
            foreach (Jeep jeep in jeeps)
            {
                if (jeep.moving)
                {
                    //ha beért a jeep
                    if (jeep.move())
                    {
                        popularity += calculateSatisfaction(jeep.encounteredAnimals);
                        Debug.Log("Popularity: " + popularity);
                    }
                    else
                    {
                        AnimalGroup a = detectForSatisfaction(jeep, 5);
                        if (a != null) { jeep.encounteredAnimals.Add(a); }
                    }
                }
            }
        }
    }
    

    //VÁSÁRLÁS
    public bool canBuy(string obj)
    {
        //még nincs megcsinálva, hogy ne lehessen egymásra helyezni itemeket
        //a drón vásárlásnak előfeltétele a töltőállomás
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
            case "hippo": moneyNeeded = 100; break;
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
        if (obj == "path")
        {
            float pathSize = 1.0f;
            position.x = Mathf.Round(position.x / pathSize) * pathSize;
            position.y = Mathf.Round(position.y / pathSize) * pathSize;
        }
        if (IsPositionOccupied(position) && obj!="jeep")
        {
            Debug.Log("Arra a mezőre nem helyezhetünk le.");
            return;
        }
        if (canBuy(obj))
        {
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
                    AnimalGroup cheetah = new AnimalGroup(position, Codes.animal.AnimalType.Gepard);
                    animalGroups.Add(cheetah);
                    cheetah.obj = Instantiate(cheetahObject, cheetah.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "crocodile":
                    AnimalGroup crocodile = new AnimalGroup(position, Codes.animal.AnimalType.Crocodile);
                    animalGroups.Add(crocodile);
                    crocodile.obj = Instantiate(crocodileObject, crocodile.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "gazelle":
                    AnimalGroup gazelle = new AnimalGroup(position, Codes.animal.AnimalType.Gazella);
                    animalGroups.Add(gazelle);
                    gazelle.obj = Instantiate(gazelleObject, gazelle.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "hippo":
                    AnimalGroup hippo = new AnimalGroup(position, Codes.animal.AnimalType.Hippo);
                    animalGroups.Add(hippo);
                    hippo.obj = Instantiate(hippoObject, hippo.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "jeep":
                    Jeep jeep = new Jeep(startObj.transform.position);
                    jeeps.Add(jeep);
                    jeep.obj = Instantiate(jeepObject, jeep.spawnPosition, Quaternion.identity);
                    this.money -= 100;
                    break;
                case "path":
                    Path tempPath = new Path(position);
                    Path nearestPath = FindNearestPath(tempPath);
                    List<Vector2> fullPath = (nearestPath != null) ?
                        FindPathAvoidingObstacles(nearestPath.spawnPosition, position) : new List<Vector2>();

                    int totalCost = fullPath.Count * 100;
                    if (this.money < totalCost)
                    {
                        Debug.Log("Nincs elég pénz az út lerakásához!");
                        break;
                    }

                    Path path = new Path(position);
                    CreateIntermediatePaths(path);
                    Path start = paths.Find(p => p.spawnPosition == (Vector2)startObj.transform.position);
                    Path end = paths.Find(p => p.spawnPosition == (Vector2)endObj.transform.position);
                    var uniquePaths = new HashSet<string>();
                    validPaths = CreateValidPaths(startObj.transform.position, endObj.transform.position, paths);

                    Debug.Log(validPaths.Count);
                    break;
                case "camera":
                    /* SecuritySystem securityItem = new SecuritySystem(position, "camera");
                     * security.add(securityItem);
                     * securityItem.obj = Instantiate(securityObject, securityItem.spawnPosition, Quaternion.identity);
                     this.money -= 100;*/
                    //ellenőrizni kell még, hogy van-e hozzá már töltő
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

    //RANGER KEZELÉSEK
    public void buyRanger(string id)
    {
        Ranger ranger = new Ranger(new Vector2(UnityEngine.Random.Range(-15, 16), UnityEngine.Random.Range(-15, 16)), id);
        this.rangers.Add(ranger);
        ranger.obj = Instantiate(rangerObject, ranger.spawnPosition, Quaternion.identity);
    }

    public void sellRanger(string id)
    {
        foreach (Ranger ranger in this.rangers)
        {
            if(id == ranger.id)
            {
                this.rangers.Remove(ranger);
                Destroy(ranger.obj);
                break;
            }
        }
    }
    public int rangerTargetChange(string id)
    {
        foreach (Ranger ranger in this.rangers)
        {
            if (id == ranger.id)
            {
                return ranger.toggleTarget();
            }
        }
        return -1;
    }

    //VIEW FRISSÍTÉSE
    public void updateView()
    {
        moneyText.text = "|  Pénz: " + this.money;
        timeText.text = this.week + ". hét, " + this.day + ". nap, " + this.hour + ". óra";
        elegedettsegText.text = "Elégedettség: " + this.popularity;
        visitorsText.text = "|  Várakozó látogatók: " + this.visitorsWaiting;

        //winConditions page
        int herbivores = 0;
        int carnivores = 0;
        foreach (AnimalGroup group in this.animalGroups)
        {
            if (group.animalType == AnimalType.Crocodile || group.animalType == AnimalType.Gepard)
            {
                carnivores += group.animals.Count();
            }
            else
            {
                herbivores += group.animals.Count();
            }
        }
        int weeks = 0;
        switch (difficulty)
        {
            case 1: weeks = 3 * 4; break;
            case 2: weeks = 6 * 4; break;
            case 3: weeks = 12 * 4; break;
        }
        latogatokText.text = "Látogatók: " + this.visitorsWaiting + "/" + visitorsNeed;
        novenyevokText.text = "Növényevők: " + herbivores + "/" + herbivoreNeed;
        ragadozokText.text = "Ragadozók: " + carnivores + "/" + carnivoreNeed;
        penzText.text = "Pénz: " + this.money + "/" + moneyNeed;
        hetekText.text = "Teljesített hetek: " + this.weeksUntilWin + "/" + weeks;

        //colors:
        latogatokText.color = new Color(0.8941177f, 0.003921553f, 0.1770464f);
        novenyevokText.color = new Color(0.8941177f, 0.003921553f, 0.1770464f);
        ragadozokText.color = new Color(0.8941177f, 0.003921553f, 0.1770464f);
        penzText.color = new Color(0.8941177f, 0.003921553f, 0.1770464f);
        Color g = new Color(0.0352202f, 0.4276729f, 0f);
        if (this.visitorsWaiting >= visitorsNeed) { latogatokText.color = g; }
        if (herbivores >= herbivoreNeed) { novenyevokText.color = g; }
        if (carnivores >= carnivoreNeed) { ragadozokText.color = g; }
        if (this.money >= moneyNeed) { penzText.color = g; }

    }

    /*
     * pays for the rangers every month(?)
     * be kell még állítani hogy havi szinten hívódjon meg
     */
    public void payCheck()
    {
        int rangerPrice = 30;
        this.money -= this.rangers.Count * rangerPrice;
        updateView();
        if (this.money <= 0)
        {
            //lose()
            SceneManager.LoadScene("Lose");
        }
    }

   

    /*
     * checks if we win or lose
     */
    public void checkWin()
    {
        int carnivores = 0;
        int herbivores = 0;
        foreach (AnimalGroup group in this.animalGroups)
        {
            if (group.animalType == AnimalType.Crocodile || group.animalType == AnimalType.Gepard)
            {
                carnivores += group.animals.Count();
            }
            else
            {
                herbivores += group.animals.Count();
            }
        }
        if (this.money >= moneyNeed && this.visitorsWaiting >= visitorsNeed && carnivores >= carnivoreNeed && herbivores >= herbivoreNeed)
        {
            ++this.weeksUntilWin;
            int weeksToWin = 0;
            switch (this.difficulty)
            {
                case 1: weeksToWin = 3 * 4; break;
                case 2: weeksToWin = 6 * 4; break;
                case 3: weeksToWin = 12 * 4; break;
            }
            if (this.weeksUntilWin >= weeksToWin)
            {
                //win()
                PlayerPrefs.SetInt("weeks", this.week);
                PlayerPrefs.SetInt("days", this.day);
                PlayerPrefs.SetInt("hours", this.hour);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Win");
            }
        }
        else
        {
            this.weeksUntilWin = 0;
        }
    }

    public List<List<Path>> CreateValidPaths(Vector2 startPosition, Vector2 endPosition, List<Path> paths)
    {
        List<List<Path>> allPaths = new List<List<Path>>();
        HashSet<string> uniquePaths = new HashSet<string>(); // Egyediség ellenőrzés

        Path startPath = paths.Find(p => p.spawnPosition == startPosition);
        Path endPath = paths.Find(p => p.spawnPosition == endPosition);

        if (startPath == null || endPath == null)
            return allPaths; // Ha nincs kezdő vagy végpont, nincs érvényes út

        List<Path> currentPath = new List<Path>();
        HashSet<Path> visited = new HashSet<Path>();

        DFS(startPath, endPath, currentPath, visited, allPaths, uniquePaths);
        return FilterPaths(allPaths);
    }

    private void DFS(Path current, Path target, List<Path> currentPath, HashSet<Path> visited, List<List<Path>> allPaths, HashSet<string> uniquePaths)
    {
        // Hozzáadjuk az aktuális csomópontot az úthoz
        currentPath.Add(current);
        visited.Add(current);

        // Ha elértük a célt, elmentjük az útvonalat
        if (current == target)
        {
            string pathString = string.Join("->", currentPath.Select(p => p.spawnPosition.ToString()));

            // Csak akkor adjuk hozzá, ha még nincs benne
            if (!uniquePaths.Contains(pathString))
            {
                allPaths.Add(new List<Path>(currentPath)); // Mélymásolat
                uniquePaths.Add(pathString);
            }
        }
        else
        {
            // Továbbhaladunk a szomszédok mentén
            foreach (Path neighbor in current.neighbors)
            {
                if (!visited.Contains(neighbor)) // Ne menjünk vissza ugyanoda
                {
                    DFS(neighbor, target, currentPath, visited, allPaths, uniquePaths);
                }
            }
        }

        // Visszalépés: eltávolítjuk az utolsó elemet, hogy más utak is kereshetők legyenek
        currentPath.RemoveAt(currentPath.Count - 1);
        visited.Remove(current);
    }

    private List<List<Path>> FilterPaths(List<List<Path>> rawPaths)
    {
        var filtered = new List<List<Path>>();

        foreach (var path in rawPaths)
        {
            bool contains = false;
            foreach (var item in path)
            {
                if (item.obj == null || item.obj.transform == null || !item.obj.activeInHierarchy || item == paths[2])
                {
                    contains = true;
                }
            }

            if (contains)
            {
                continue;
            }
            filtered.Add(path);
            
        }

        return filtered;
    }


    private void connectNeighbourPaths(Path newPath)
    {
        foreach (Path path in paths)
        {
            if (path != newPath && path.IsAdjacent(newPath))
            {
                if (!newPath.neighbors.Contains(path))
                    newPath.neighbors.Add(path);

                if (!path.neighbors.Contains(newPath))
                    path.neighbors.Add(newPath);
            }
        }
    }

    private void CreateIntermediatePaths(Path newPath)
    {
        Path nearestPath = FindNearestPath(newPath);
        if (nearestPath != null)
        {
            Vector2 start = nearestPath.spawnPosition;
            Vector2 end = newPath.spawnPosition;

            List<Vector2> intermediatePositions = FindPathAvoidingObstacles(start, end);

            // Számoljuk ki, mennyibe kerülne az út, és ha nincs rá pénz, kilépünk
            int totalCost = intermediatePositions.Count * 100;
            if (this.money < totalCost)
            {
                Debug.Log("Nincs elég pénz az út építéséhez!");
                return;
            }

            // Most már biztosak vagyunk benne, hogy van elég pénz, elkezdhetjük az építést
            Path previousPath = nearestPath;
            foreach (Vector2 pos in intermediatePositions)
            {
                Path intermediatePath = new Path(pos);
                paths.Add(intermediatePath);
                intermediatePath.obj = Instantiate(pathObject, pos, Quaternion.identity);

                connectNeighbourPaths(intermediatePath);
                this.money -= 100; // Levonjuk a pénzt
            }
            connectNeighbourPaths(newPath);
        }
    }






    private List<Vector2> FindPathAvoidingObstacles(Vector2 start, Vector2 end)
    {
        List<Vector2> path = new List<Vector2>();
        HashSet<Vector2> visited = new HashSet<Vector2>();
        Queue<Vector2> queue = new Queue<Vector2>();

        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();

        queue.Enqueue(start);
        visited.Add(start);

        Vector2[] directions = new Vector2[]
        {
        new Vector2(1, 0),  // Jobbra
        new Vector2(-1, 0), // Balra
        new Vector2(0, 1),  // Felfelé
        new Vector2(0, -1)  // Lefelé
        };

        bool pathFound = false;

        while (queue.Count > 0)
        {
            Vector2 current = queue.Dequeue();

            if (current == end)
            {
                pathFound = true;
                break;
            }

            foreach (Vector2 dir in directions)
            {
                Vector2 next = current + dir;

                if (!visited.Contains(next) && !IsPositionOccupied(next))
                {
                    queue.Enqueue(next);
                    visited.Add(next);
                    cameFrom[next] = current;
                }
            }
        }

        if (pathFound)
        {
            Vector2 step = end;
            while (step != start)
            {
                path.Add(step);
                step = cameFrom[step];
            }
            path.Reverse();
        }

        return path;
    }

    private bool IsPositionOccupied(Vector2 position)
    {
        float checkRadius = 0.4f; // Ez legyen kisebb, mint a grid méret
        return Physics2D.OverlapCircle(position, checkRadius) != null;
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
            if (path == paths[2]) continue;
            if (path.obj.transform.position == endObj.transform.position) continue;

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
