using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class HUDBattleMonsterView : BaseMonoBehaviour, IStartInit
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

    [Header("Item")]
    [SerializeField] private Button _btnItem;
    [SerializeField] private Button _btnCloseItem;
    [SerializeField] private RectTransform _itemChoosePanel;

    [Header("Monster")]
    [SerializeField] private Button _btnMonster;
    [SerializeField] private Button _btnCloseMonster;
    [SerializeField] private RectTransform _monsterChoosePanel;
    [SerializeField] private List<ButtonSelectMonsterInfor> _btnSelectMonsters;
    private ButtonSelectMonsterInfor _currentMonsterSelected;

    [Header("Run")]
    [SerializeField] private Button _btnRun;

    [SerializeField] private bool _isBattleButtonClicked;

    private HUDBattleMonsterViewData _hUDBattleMonsterViewData;
    public HUDBattleMonsterViewData HUDBattleMonsterViewData { get => _hUDBattleMonsterViewData; set => _hUDBattleMonsterViewData = value; }
    public bool IsInteract { get; set; } = true;

    protected override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        //Skill
        _btnSkill.onClick.AddListener(() =>
        {
            if (!CheckClickedBattleHUD()) return;
            _onShowSkill.OnNext(default);
        });

        _btnCloseSkill.onClick.AddListener(() =>
        {
            ResetValue();
            _battleHUDCanvas.gameObject.SetActive(true);
        });

        //Item
        _btnItem.onClick.AddListener(() =>
        {
            if (!CheckClickedBattleHUD()) return;
            _onShowItem.OnNext(default);
        });

        _btnCloseItem.onClick.AddListener(() =>
        {
            ResetValue();
        });
        _btnBattleSkills.ForEach(skill => skill.BtnSkill.onClick.AddListener(() => SelectSkill(skill)));

        //Monster
        _btnMonster.onClick.AddListener(() =>
        {
            if (!CheckClickedBattleHUD()) return;
            _onShowPlayerTeam.OnNext(default);
        });

        _btnCloseMonster.onClick.AddListener(() =>
        {
            ResetValue();
        });

        _btnSelectMonsters.ForEach(infor => infor.Button.onClick.AddListener(() => SelectMonster(infor)));

        //Run
        _btnRun.onClick.AddListener(() =>
        {
            if (_isBattleButtonClicked) return;

            _onOutBattleEvent.OnNext(default);
            ResetValue();
        });
    }

    public void SetData(HUDBattleMonsterViewData hUDBattleMonsterViewData)
    {
        _hUDBattleMonsterViewData = hUDBattleMonsterViewData;
    }

    public void UpdateStatsInforText(EMonsterSide eMonsterSide, string name, int level)
    {
        MonsterBattleInforUI currentBattleInfor = eMonsterSide == EMonsterSide.Player ? _playerMonster : _opponentMonster;
        currentBattleInfor.MonsterNameText.text = name;
        currentBattleInfor.LevelText.text = "Lv." + level.ToString();
    }

    public void UpdateMonsterStats(EMonsterSide eMonsterSide, EStatType eStatType, float value, float maxValue = 0)
    {
        MonsterBattleInforUI currentBattleInfor = eMonsterSide == EMonsterSide.Player ? _playerMonster : _opponentMonster;
        switch (eStatType)
        {
            case EStatType.Health:
                currentBattleInfor.HealthValueText.text = value.ToString() + " / " + maxValue;
                currentBattleInfor.HealthSlider
                    .DOValue(value / maxValue, 2f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() => _onUpdateMonsterStatCompleted.OnNext(new UpdateMonsterStatCompletedViewData(eMonsterSide, eStatType)));
                break;
        }
    }

    public void UpdatePlayerTeamAnimator()
    {
        for (int i = 0; i < monsterTeamNumber; i++)
        {
            _btnSelectMonsters[i].MonsterAnimator.runtimeAnimatorController = _hUDBattleMonsterViewData.PlayerTeamDatas[i].UIAnimator;
        }
    }

    public void UpdateMonsterNumber(int number)
    {
        _monsterChoosePanel.gameObject.SetActive(true);
        monsterTeamNumber = number;
        for (int i = 0; i < _btnSelectMonsters.Count; i++)
        {
            _btnSelectMonsters[i].gameObject.SetActive(i < number);
        }
        _monsterChoosePanel.gameObject.SetActive(false);
    }

    public void UpdateBattteMonsterSkill(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i >= _hUDBattleMonsterViewData.PlayerTeamDatas[index].BatlleSkills.Count)
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
            SwapMonster(EMonsterSide.Player, _currentMonsterSelected.MonsterIndex);
            ResetValue();
        }
        else
        {
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(false);
            _currentMonsterSelected = buttonSelectMonsterInfor;
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(true);
        }
    }

    private void SwapMonster(EMonsterSide eMonsterSide, int index)
    {
        _onSwapMonster.OnNext(new SwapMonsterViewData(eMonsterSide, index));
    }

    private bool CheckClickedBattleHUD()
    {
        if (!IsInteract) return false;
        if (_isBattleButtonClicked) return false;

        _isBattleButtonClicked = true;
        return true;
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
            ActiveSKill(EMonsterSide.Player, _currentButtonSkillSelected.SkillIndex);
            ResetValue();
        }
        else
        {
            _currentButtonSkillSelected.ImgSelected.gameObject.SetActive(false);
            _currentButtonSkillSelected = battleButtonSkillInfor;
            _currentButtonSkillSelected.ImgSelected.gameObject.SetActive(true);
        }
    }

    private void ActiveSKill(EMonsterSide eMonsterSide, int skillIndex)
    {
        _onActiveAttack.OnNext(new ActiveAttackViewData(eMonsterSide, skillIndex));
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