using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMonsterTeamView : BaseMonoBehaviour, IStartInit
{
    [SerializeField] private Button _btnClose;
    [SerializeField] private List<ButtonSelectMonsterInfor> _monsterTeamInfors = new();

    private ButtonSelectMonsterInfor _currentMonsterSelected = null;
    private HUDMonsterTeamViewData _hUDMonsterTeamViewData;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void SetData(HUDMonsterTeamViewData hUDMonsterTeamViewData)
    {
        _hUDMonsterTeamViewData = hUDMonsterTeamViewData;
    }

    public void Initialize()
    {
        _btnClose.onClick.AddListener(() =>
        {
            transform.gameObject.SetActive(false);
            ResetValue();
        });
        _monsterTeamInfors.ForEach(btn => btn.Button.onClick.AddListener(() => SelectMonster(btn)));
    }

    public void UpdateMonsterTeam()
    {
        int monsterCount = _hUDMonsterTeamViewData.MonsterTeams.Count;
        for(int i = 0; i < _monsterTeamInfors.Count; i++)
        {
            if(i < monsterCount)
            {
                MonsterViewData monsterViewData = _hUDMonsterTeamViewData.MonsterTeams[i];
                _monsterTeamInfors[i].gameObject.SetActive(true);
                _monsterTeamInfors[i].MonsterNameText.text = monsterViewData.MonsterName;
                _monsterTeamInfors[i].MonsterAnimator.runtimeAnimatorController = monsterViewData.UIAnimator;
                _monsterTeamInfors[i].LevelText.text = monsterViewData.Level.ToString();
                _monsterTeamInfors[i].HealthText.text = monsterViewData.Health + "/" + monsterViewData.MaxHealth;
                _monsterTeamInfors[i].HealthBar.value = (float)monsterViewData.Health / monsterViewData.MaxHealth;
            }
            else
            {
                _monsterTeamInfors[i].gameObject.SetActive(false);
            }
        }
    }

    private void SelectMonster(ButtonSelectMonsterInfor btnInfor)
    {
        if(_currentMonsterSelected == null)
        {
            _currentMonsterSelected = btnInfor;
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(true);
        }
        else
        {
            if (_currentMonsterSelected != btnInfor)
            {
                _currentMonsterSelected.ImgSelected.gameObject.SetActive(false);
                _currentMonsterSelected = btnInfor;
                _currentMonsterSelected.ImgSelected.gameObject.SetActive(true);
            }
        }
    }

    private void ResetValue()
    {
        if(_currentMonsterSelected != null)
        {
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(false);
            _currentMonsterSelected = null;
        }
    }
}