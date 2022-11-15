using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Image img_spGauge;

    public Image GetImage_SpGauge()
    {
        return img_spGauge;
}
}
