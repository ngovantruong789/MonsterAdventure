using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBattleMonsterView : LifetimeScope, IStartInit
{
    [Header("Infor current monster")]
    [SerializeField] private MonsterBattleInforUI _playerMonster;
    [SerializeField] private MonsterBattleInforUI _opponentMonster;
    [SerializeField] private RectTransform _battleHUDCanvas;
    [SerializeField] private int monsterTeamNumber;

    [Header("Skill")]
    [SerializeField] private Button _btnSkill;
    [SerializeField] private Button _btnCloseSkill;
    [SerializeField] private RectTransform _skillPanel;
    [SerializeField] private List<BattleButtonSkillInfor> _btnBattleSkills;
    private BattleButtonSkillInfor _currentButtonSkillSelected;
    public Action OnShowSkillsEvent { get; set; }
    public Action<bool, int> OnActiveAttack { get; set; }

    [Header("Item")]
    [SerializeField] private Button _btnItem;
    [SerializeField] private Button _btnCloseItem;
    [SerializeField] private RectTransform _itemChoosePanel;
    public Action OnShowItemsEvent { get; set; }

    [Header("Monster")]
    [SerializeField] private Button _btnMonster;
    [SerializeField] private Button _btnCloseMonster;
    [SerializeField] private RectTransform _monsterChoosePanel;
    [SerializeField] private List<ButtonSelectMonsterInfor> _btnSelectMonsters;
    private ButtonSelectMonsterInfor _currentMonsterSelected;
    public Action OnShowPlayerTeamEvent { get; set; }
    public Action<bool, int> OnSwapMonster { get; set; }

    [Header("Run")]
    [SerializeField] private Button _btnRun;
    public Action OnOutBattleEvent { get; set; }

    [SerializeField] private bool _isBattleButtonClicked;

    private HUDBattleMonsterViewData _hUDBattleMonsterViewData;
    public bool IsInteract { get; set; } = true;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        //Skill
        _btnSkill.onClick.AddListener(() => OnClickedBattleHUD(OnShowSkillsEvent));
        _btnCloseSkill.onClick.AddListener(() =>
        {
            ResetValue();
            _battleHUDCanvas.gameObject.SetActive(true);
        });

        //Item
        _btnItem.onClick.AddListener(() => OnClickedBattleHUD(OnShowItemsEvent));
        _btnCloseItem.onClick.AddListener(() =>
        {
            ResetValue();
        });
        _btnBattleSkills.ForEach(skill => skill.BtnSkill.onClick.AddListener(() => SelectSkill(skill)));

        //Monster
        _btnMonster.onClick.AddListener(() => OnClickedBattleHUD(OnShowPlayerTeamEvent));
        _btnCloseMonster.onClick.AddListener(() =>
        {
            ResetValue();
        });
        _btnSelectMonsters.ForEach(infor => infor.Button.onClick.AddListener(() => SelectMonster(infor)));

        //Run
        _btnRun.onClick.AddListener(() =>
        {
            if(_isBattleButtonClicked) return;

            OnClickedBattleHUD(OnOutBattleEvent);
            ResetValue();
        });
    }

    public void SetData(HUDBattleMonsterViewData hUDBattleMonsterViewData)
    {
        _hUDBattleMonsterViewData = hUDBattleMonsterViewData;
    }

    public void UpdateStatsInforText(bool isPlayer, string name, int level)
    {
        MonsterBattleInforUI currentBattleInfor = isPlayer ? _playerMonster : _opponentMonster;
        currentBattleInfor.MonsterNameText.text = name;
        currentBattleInfor.LevelText.text = "Lv." + level.ToString();
    }

    public void UpdateMonsterStats(bool isPlayer, EStatType eStatType, float value, float maxValue = 0)
    {
        MonsterBattleInforUI currentBattleInfor = isPlayer ? _playerMonster : _opponentMonster;
        switch(eStatType)
        {
            case EStatType.Health:
                currentBattleInfor.HealthValueText.text = value.ToString() + " / " + maxValue;
                currentBattleInfor.HealthSlider
                    .DOValue(value / maxValue, 1f)
                    .SetEase(Ease.OutQuad);
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

    public void UpdateBattteMonsterSkill(int index)
    {
        for(int i = 0; i < 4; i++)
        {
            if(i >= _hUDBattleMonsterViewData.PlayerTeamDatas[index].BatlleSkills.Count)
            {
                _btnBattleSkills[i].gameObject.SetActive(false);
            }
            else
            {
                _btnBattleSkills[i].gameObject.SetActive(true);
                _btnBattleSkills[i].SkillNameText.text = _hUDBattleMonsterViewData.PlayerTeamDatas[index].BatlleSkills[i].FullName;
                _btnBattleSkills[i].SkillType = _hUDBattleMonsterViewData.PlayerTeamDatas[index].BatlleSkills[i].SkillType;
                _btnBattleSkills[i].ESkillId = _hUDBattleMonsterViewData.PlayerTeamDatas[index].BatlleSkills[i].ESkillId;
            }
        }
    }

    public void ShowPlayerTeam()
    {
        _monsterChoosePanel.gameObject.SetActive(true);
        for (int i = 0; i < monsterTeamNumber; i++)
        {
            _btnSelectMonsters[i].MonsterNameText.text = _hUDBattleMonsterViewData.PlayerTeamDatas[i].MonsterName;
            _btnSelectMonsters[i].HealthText.text = _hUDBattleMonsterViewData.PlayerTeamDatas[i].Health + "/" + _hUDBattleMonsterViewData.PlayerTeamDatas[i].MaxHealth;
            _btnSelectMonsters[i].LevelText.text = _hUDBattleMonsterViewData.PlayerTeamDatas[i].Level.ToString();
            _btnSelectMonsters[i].HealthBar.value = (float)_hUDBattleMonsterViewData.PlayerTeamDatas[i].Health / _hUDBattleMonsterViewData.PlayerTeamDatas[i].MaxHealth;
        }
    }

    public void ShowSkillBattleMonster()
    {
        _skillPanel.gameObject.SetActive(true);
        _battleHUDCanvas.gameObject.SetActive(false);
    }

    public void ShowItem()
    {
        _itemChoosePanel.gameObject.SetActive(true);
    }

    public void CurrentMonsterSelectedConstructor()
    {
        _currentMonsterSelected = _btnSelectMonsters[0];
    }

    private void SelectMonster(ButtonSelectMonsterInfor buttonSelectMonsterInfor)
    {
        if (!IsInteract) return;
        if (_currentMonsterSelected != null && _currentMonsterSelected != buttonSelectMonsterInfor)
        {
            _currentMonsterSelected = buttonSelectMonsterInfor;
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(true);
        }
        else if (_currentMonsterSelected != null && _currentMonsterSelected == buttonSelectMonsterInfor)
        {
            SwapMonster(true, _currentMonsterSelected.MonsterIndex);
            ResetValue();
        }
        else
        {
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(false);
            _currentMonsterSelected = buttonSelectMonsterInfor;
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(true);
        }
    }

    private void SwapMonster(bool isPlayer, int index)
    {
        OnSwapMonster?.Invoke(isPlayer, index);
    }

    private void OnClickedBattleHUD(Action action)
    {
        if (!IsInteract) return;
        if (_isBattleButtonClicked) return;

        _isBattleButtonClicked = true;
        action?.Invoke();
    }

    private void SelectSkill(BattleButtonSkillInfor battleButtonSkillInfor)
    {
        if (!IsInteract) return;
        if (_currentButtonSkillSelected == null)
        {
            _currentButtonSkillSelected = battleButtonSkillInfor;
            _currentButtonSkillSelected.ImgSelected.gameObject.SetActive(true);
        }
        else if (_currentButtonSkillSelected != null && _currentButtonSkillSelected == battleButtonSkillInfor)
        {
            ActiveSKill(true, _currentButtonSkillSelected.SkillIndex);
            ResetValue();
        }
        else
        {
            _currentButtonSkillSelected.ImgSelected.gameObject.SetActive(false);
            _currentButtonSkillSelected = battleButtonSkillInfor;
            _currentButtonSkillSelected.ImgSelected.gameObject.SetActive(true);
        }
    }

    private void ActiveSKill(bool isPlayer, int skillIndex)
    {
        OnActiveAttack?.Invoke(isPlayer, skillIndex);
    }

    private void ResetValue()
    {
        _isBattleButtonClicked = false;
        _monsterChoosePanel.gameObject.SetActive(false);
        _itemChoosePanel.gameObject.SetActive(false);
        _skillPanel.gameObject.SetActive(false);
        _battleHUDCanvas.gameObject.SetActive(true);
        if (_currentMonsterSelected)
        {
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(false);
        }

        if (_currentButtonSkillSelected)
        {
            _currentButtonSkillSelected.ImgSelected.gameObject.SetActive(false);
            _currentButtonSkillSelected = null;
        }
    }
}