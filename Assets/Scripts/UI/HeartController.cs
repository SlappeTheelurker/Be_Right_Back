using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour {

    public int startHearts, maxHeartAmount, healthPerHeart, curHealth;
    public Image[] healthImages;
    public Sprite[] healthSprites;

    private int maxHealth;

    // Use this for initialization
    void Start () {
        curHealth = startHearts * healthPerHeart;
        CheckHealthAmount();
        UpdateHearts();
	}

    void CheckHealthAmount()
    {
        for (int i = 0; i < maxHeartAmount; i++)
        {
            if (startHearts <= i)
            {
                healthImages[i].enabled = false;
            }
            else
            {
                healthImages[i].enabled = true;
            }
        }
    }

    void UpdateHearts()
    {
        bool empty = false;
        int i = 0;
        foreach (Image image in healthImages)
        {
            if (empty)
            {
                image.sprite = healthSprites[0];
            }
            else
            {
                i++;
                if (curHealth >= i * 4)
                {
                    image.sprite = healthSprites[healthSprites.Length - 1];
                }
                else
                {
                    int currentHeartHealth = (int)(4 - (4 * i - curHealth));
                    int healthPerImage = healthPerHeart / (healthSprites.Length - 1);
                    int imageIndex = currentHeartHealth / healthPerImage;
                    image.sprite = healthSprites[imageIndex];
                    empty = true;
                }
            }
        }
    }

    public void ChangeHeartAmount(int amount)
    {
        curHealth += amount;

        if ((float)curHealth / healthPerHeart > startHearts)
        {
            AddHeartcontainer();
        }

        curHealth = Mathf.Clamp(curHealth, 0, startHearts * healthPerHeart);
        UpdateHearts();
    }

    public void AddHeartcontainer()
    {
        startHearts++;
        startHearts = Mathf.Clamp(startHearts, 0, maxHeartAmount);

        CheckHealthAmount();
    }
}
