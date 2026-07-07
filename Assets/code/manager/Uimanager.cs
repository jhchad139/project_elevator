using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Uimanager : MonoBehaviour
{

    public TMP_Text hptxt;
    public TMP_Text floortxt;
    public TMP_Text dashtxt;
    public TMP_Text ammotxt;

    public Slider hpSlider;
    public Slider dashSlider;
    public Slider ammoSlider;

    public Player player;
    public player_status status;

    private void Awake()
    {
        
    }
    private void Update()
    {
        //text
        hptxt.text = $"HP : {status.hp} / {status.maxHp}";
        floortxt.text = $"Floor : {Gamemanager.Instance.floor.currentFloor}";
        if (!player.is_reroading)
            ammotxt.text = $"{status.ammo} / {status.bullet_count}";
        else
            ammotxt.text = $"Reloading...";
        dashtxt.text = $"{status.dashCount}  / {dashSlider.maxValue}";


        //slider
        ammoSlider.value = status.ammo;
        hpSlider.value = status.hp;
        dashSlider.value = status.dashCount;

        if (status.max_ammo != ammoSlider.maxValue)
            ammoSlider.maxValue = status.max_ammo;
        if(status.maxHp != hpSlider.maxValue)
            hpSlider.maxValue = status.maxHp;
        if(status.maxdashCount != dashSlider.maxValue)
            dashSlider.maxValue = status.maxdashCount;

        
    }
}
