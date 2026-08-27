using UnityEngine;
using UnityEngine.UI;

public class HUDMonsterTeamView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private Button _btnCloseMenu;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _btnCloseMenu.onClick.AddListener(() => transform.gameObject.SetActive(false));
    }
}