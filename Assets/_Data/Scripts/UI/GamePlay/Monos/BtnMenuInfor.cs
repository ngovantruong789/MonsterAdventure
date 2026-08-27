using UnityEngine;
using UnityEngine.UI;

public class BtnMenuInfor : BaseMonoBehaviour
{
    [SerializeField] private Button _button;
    public Button Button => _button;

    [SerializeField] private RectTransform _menuRect;
    public RectTransform MenuRect => _menuRect;

    [SerializeField] private RectTransform _menuCanvas;
    public RectTransform MenuCanvas => _menuCanvas;
}