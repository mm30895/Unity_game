using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public void SetHP(int hp) { 
        slider.value = hp;
    }
}
