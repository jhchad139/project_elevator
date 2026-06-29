using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Uimanager : MonoBehaviour
{
    public TMP_Text hptxt;
    public TMP_Text floortxt;
    public TMP_Text ammotxt;

    public Player player;
    public player_status status;
    private void Update()
    {
        hptxt.text = $"HP : {status.hp} / {status.maxHp}";
        floortxt.text = $"Floor : {Gamemanager.Instance.floor.currentFloor}";
        if (!player.is_reroading)
            ammotxt.text = $"{status.ammo} / {status.bullet_count}";
        else
            ammotxt.text = $"Reloading...";

    }
}
