using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBattleMonsterView : LifetimeScope, IStartInit
{
    [Header("Infor current pokemon")]
    [SerializeField] private MonsterBattleInforUI _playerMonster;
    [SerializeField] private MonsterBattleInforUI _opponentMonster;
    [SerializeField] private int monsterTeamNumber;

    [Header("Skill")]
    [SerializeField] private Button _btnSkill;
    [SerializeField] private Button _btnCloseSkill;
    [SerializeField] private RectTransform _skillPanel;
    public Action OnShowSkillsEvent { get; set; }

    [Header("Item")]
    [SerializeField] private Button _btnItem;

    [Header("Monster")]
    [SerializeField] private Button _btnMonster;
    [SerializeField] private Button _btnCloseMonster;
    [SerializeField] private RectTransform _monsterChoosePanel;
    [SerializeField] private List<ButtonSelectMonsterInfor> _btnSelectMonsters;
    public Action OnShowPlayerTeamEvent { get; set; }

    [Header("Run")]
    [SerializeField] private Button _btnRun;
    public Action OnOutBattleEvent { get; set; }

    [SerializeField] private bool _isBattleButtonClicked;

    private HUDBattleMonsterViewData _hUDBattleMonsterViewData;
    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        _btnSkill.onClick.AddListener(() => Debug.Log("_btnSkill"));
        _btnCloseSkill.onClick.AddListener(() =>
        {
            _isBattleButtonClicked = false;
            _skillPanel.gameObject.SetActive(false);
        });
        _btnItem.onClick.AddListener(() => Debug.Log("_btnItem"));
        _btnMonster.onClick.AddListener(() => OnClickedBattleHUD(OnShowPlayerTeamEvent));
        _btnRun.onClick.AddListener(() =>
        {
            if(_isBattleButtonClicked) return;

            OnClickedBattleHUD(OnOutBattleEvent);
            ResetValue();
        });
        _btnCloseMonster.onClick.AddListener(() =>
        {
            _isBattleButtonClicked = false;
            _monsterChoosePanel.gameObject.SetActive(false);
        });
    }

    public void SetData(HUDBattleMonsterViewData hUDBattleMonsterViewData)
    {
        _hUDBattleMonsterViewData = hUDBattleMonsterViewData;
    }

    public void UpdateStaticInforText(bool isPlayer, string name, int level)
    {
        MonsterBattleInforUI currentBattleInfor = isPlayer ? _playerMonster : _opponentMonster;
        currentBattleInfor.MonsterNameText.text = name;
        currentBattleInfor.LevelText.text = "Lv." + level.ToString();
    }

    public void UpdateMonsterStats(bool isPlayer, EStatType eStatType, int value, int maxValue = 0)
    {
        MonsterBattleInforUI currentBattleInfor = isPlayer ? _playerMonster : _opponentMonster;
        switch(eStatType)
        {
            case EStatType.Health:
                currentBattleInfor.HealthValueText.text = value.ToString() + " / " + maxValue;
                currentBattleInfor.HealthSlider.value = value / maxValue;
                break;
        }
    }

    public void UpdatePlayerTeamAnimator()
    {
        for(int i = 0; i < monsterTeamNumber; i++)
        {
            _btnSelectMonsters[i].MonsterAnimator.runtimeAnimatorController = _hUDBattleMonsterViewData.PlayerTeamDatas[i].UIAnimator;
        }
    }

    public void UpdateMonsterNumber(int number)
    {
        _monsterChoosePanel.gameObject.SetActive(true);
        monsterTeamNumber = number;
        for(int i = 0; i < _btnSelectMonsters.Count; i++)
        {
            _btnSelectMonsters[i].gameObject.SetActive(i < number);
        }
        _monsterChoosePanel.gameObject.SetActive(false);
    }

    public void ShowPlayerTeam()
    {
        _monsterChoosePanel.gameObject.SetActive(true);
        for (int i = 0; i < monsterTeamNumber; i++)
        {
            _btnSelectMonsters[i].MonsterNameText.text = _hUDBattleMonsterViewData.PlayerTeamDatas[i].MonsterName;
            _btnSelectMonsters[i].HealthText.text = _hUDBattleMonsterViewData.PlayerTeamDatas[i].Health + "/" + _hUDBattleMonsterViewData.PlayerTeamDatas[i].MaxHealth;
            _btnSelectMonsters[i].LevelText.text = _hUDBattleMonsterViewData.PlayerTeamDatas[i].Level.ToString();
            _btnSelectMonsters[i].HealthBar.value = _hUDBattleMonsterViewData.PlayerTeamDatas[i].Health / _hUDBattleMonsterViewData.PlayerTeamDatas[i].MaxHealth;
        }
    }

    private void OnClickedBattleHUD(Action action)
    {
        if (_isBattleButtonClicked) return;

        _isBattleButtonClicked = true;
        action?.Invoke();
    }

    private void ResetValue()
    {
        _isBattleButtonClicked = false;
    }
}
