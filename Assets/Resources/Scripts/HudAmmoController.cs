using TMPro;
using UnityEngine;

namespace Resources.Scripts
{
    public class HudAmmoController : MonoBehaviour
    {
        public GameObject ammoAmount;
    
        public void SetAmount(int amount)
        {
            ammoAmount.GetComponent<TextMeshProUGUI>().text = amount.ToString();
        }
    }
}
