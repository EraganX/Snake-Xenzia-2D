using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    public GameObject bonusFood;
    public BoxCollider2D gridArea;
    public Slider sliderBonusFood;

    PlayerScript playerScript;
    private bool alreadySpawned = false;

    public float bonusValue;

    void Awake()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        sliderBonusFood.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerScript.countFoodEat % 5 == 0 && alreadySpawned == false && playerScript.countFoodEat>4)
        {
            bonusFoodRandomSpawnArea();
            alreadySpawned = true;
            
        }

        if (playerScript.countFoodEat % 5 == 3)
        {
            alreadySpawned = false;
        }
    }

    

    private void bonusFoodRandomSpawnArea()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        GameObject bonusFoodInstance = Instantiate(bonusFood, new Vector3(Mathf.Round(x), Mathf.Round(y), 0), Quaternion.identity);
        sliderBonusFood.value = 1;
        sliderBonusFood.gameObject.SetActive(true);
        StartCoroutine(UpdateSliderValue(bonusFoodInstance));
        StartCoroutine(DestroyBonusInstance(bonusFoodInstance));

    }

    IEnumerator DestroyBonusInstance(GameObject instanceGameObject)
    {
        yield return new WaitForSeconds(3f);
        Destroy(instanceGameObject);
        sliderBonusFood.gameObject.SetActive(false);

    }

    private IEnumerator UpdateSliderValue(GameObject instanceGameObject)
    {
        while (sliderBonusFood.value > 0 && instanceGameObject!=null)
        {
            sliderBonusFood.value -= Time.deltaTime / 3;
            bonusValue = sliderBonusFood.value;
            yield return null;
        }
    }

}
