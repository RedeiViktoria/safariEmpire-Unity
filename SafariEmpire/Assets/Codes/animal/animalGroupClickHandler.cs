using Codes.animal;
using UnityEngine;

public class AnimalGroupClickHandler : MonoBehaviour
{
    public AnimalGroup animalGroup;

    void OnMouseDown()
    {
        Debug.Log("Kattintás az állatcsoportra!");
        if (animalGroup != null)
        {
            // Pl. jelenítsd meg az adatait UI-on
            Debug.Log($"Állattípus: {animalGroup.animalType}, Egyedek száma: {animalGroup.animals.Count}");
        }
    }
}