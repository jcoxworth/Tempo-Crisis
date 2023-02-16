using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public Image redScreen;
    public Color redFlash;
    public PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {
        if (!health)
            health = FindObjectOfType<PlayerHealth>();
        health.onDamage += FlashRed;
    }
    private void FlashRed()
    {
        redScreen.color = redFlash;
    }
    // Update is called once per frame
    void Update()
    {
        redScreen.color = Color.Lerp(redScreen.color, Color.clear, Time.deltaTime);
        healthSlider.value = health.health;
    }
}
