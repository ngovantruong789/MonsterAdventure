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
    [SerializeField] private List<SelectItemInfor> _itemRestoreButtons;
    [SerializeField] private List<SelectItemInfor> _itemCaptureButtons;
    [SerializeField] private SelectItemInfor _itemPrefab;
    [SerializeField] private RectTransform _itemRestoreParent;
    [SerializeField] private RectTransform _itemCaptureParent;
    [SerializeField] private Button _btnItemRestore;
    [SerializeField] private Button _btnItemCapture;
    [SerializeField] private bool _isrestore = false;
    private SelectItemInfor _currentItemselected;

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
        //Skil
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

        _btnItemRestore.onClick.AddListener(() =>
        {
            ShowItemPanel(EItemType.Restore);
        });

        _btnItemCapture.onClick.AddListener(() =>
        {
            ShowItemPanel(EItemType.Capture);
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
            if (!IsInteract) return;

            _onOutBattleEvent.OnNext(default);
            ResetValue();
        });
    }

    public void SetData(HUDBattleMonsterViewData hUDBattleMonsterViewData)
    {
        _hUDBattleMonsterViewData = hUDBattleMonsterViewData;
    }

    #region Stats
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
    #endregion Stats

    #region Player team
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
        #endregion Player team

    #region Skill
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

    public void ShowSkillBattleMonster()
    {
        _skillPanel.gameObject.SetActive(true);
        _battleHUDCanvas.gameObject.SetActive(false);
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
    #endregion Skill

    #region Item
    public void CreateItemButtons()
    {
        SpawnItemInforButtons();
        RegisterSelectedItemInforButtons();
    }

    private void SpawnItemInforButtons()
    {
        for (int i = 0; i < _hUDBattleMonsterViewData.RestoreInventoryData.Items.Count; i++)
        {
            SelectItemInfor itemInfor = SpawnItemButton(_itemRestoreParent);
            UpdateInforItemButton(itemInfor, _hUDBattleMonsterViewData.RestoreInventoryData.Items[i]);
            _itemRestoreButtons.Add(itemInfor);
        }
        for (int i = 0; i < _hUDBattleMonsterViewData.CaptureInventoryData.Items.Count; i++)
        {
            SelectItemInfor itemInfor = SpawnItemButton(_itemCaptureParent);
            UpdateInforItemButton(itemInfor, _hUDBattleMonsterViewData.CaptureInventoryData.Items[i]);
            _itemCaptureButtons.Add(itemInfor);
        }
    }

    private SelectItemInfor SpawnItemButton(RectTransform parent)
    {
        return Instantiate(_itemPrefab, parent);
    }

    private void RegisterSelectedItemInforButtons()
    {
        _itemRestoreButtons.ForEach(item => item.BtnItem.onClick.AddListener(() => SelectItem(item)));
        _itemCaptureButtons.ForEach(item => item.BtnItem.onClick.AddListener(() => SelectItem(item)));
    }

    private void UpdateInforItemButton(SelectItemInfor itemInfor, ItemViewData itemViewData)
    {
        itemInfor.IdItem = itemViewData.Id;
        itemInfor.ItemType = itemViewData.ItemType;
        itemInfor.ItemNameText.text = itemViewData.Name;
        itemInfor.DescriptionText.text = itemViewData.Description;
        itemInfor.QuantityText.text = itemViewData.Quantity.ToString();
        itemInfor.ImgIcon.sprite = itemViewData.Image;
    }

    private void UpdateInforItemButtons(List<ItemViewData> items, List<SelectItemInfor> itemInfors, int id)
    {
        foreach(ItemViewData item in items)
        {
            foreach(SelectItemInfor infor in itemInfors)
            {
                if(item.Id == id)
                {
                    UpdateInforItemButton(infor, item);
                    break;
                }
            }
        }
    }

    public void UpdateInforItemButton(EItemType itemType, int itemId)
    {
        if (itemType == EItemType.Restore)
        {
            UpdateInforItemButtons(_hUDBattleMonsterViewData.RestoreInventoryData.Items, _itemRestoreButtons, itemId);
        }
        else
        {
            UpdateInforItemButtons(_hUDBattleMonsterViewData.CaptureInventoryData.Items, _itemCaptureButtons, itemId);
        }
    }

    public void ShowItemPanel(EItemType itemType)
    {
        _itemChoosePanel.gameObject.SetActive(true);
        if (itemType == EItemType.Restore)
        {
            _itemCaptureParent.gameObject.SetActive(false);
            _itemRestoreParent.gameObject.SetActive(true);
        }
        else if(itemType == EItemType.Capture)
        {
            _itemRestoreParent.gameObject.SetActive(false);
            _itemCaptureParent.gameObject.SetActive(true);
        }

        if (_currentItemselected != null)
        {
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(false);
            _currentItemselected = null;
        }
    }

    private void SelectItem(SelectItemInfor buttonSelectItemInfor)
    {
        if (!IsInteract) return;
        if (_currentItemselected != null && _currentItemselected != buttonSelectItemInfor)
        {
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(false);
            _currentItemselected = buttonSelectItemInfor;
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(true);
        }
        else if (_currentItemselected != null && _currentItemselected == buttonSelectItemInfor)
        {
            if (_currentItemselected.ItemType == EItemType.Capture) 
            {
                _onUseItem.OnNext(new UseItemHUDViewData(_currentItemselected.IdItem, _currentItemselected.ItemType));
                IsInteract = false;
                ResetValue();
            }
            else
            {
                _isrestore = true;
                IsInteract = false;
                _itemChoosePanel.gameObject.SetActive(false);
                ShowPlayerTeam();
            }
        }
        else
        {
            _currentItemselected = buttonSelectItemInfor;
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(true);
        }
    }
    #endregion Item

    #region Monster
    public void CurrentMonsterSelectedConstructor()
    {
        _currentMonsterSelected = _btnSelectMonsters[0];
    }

    private void SelectMonster(ButtonSelectMonsterInfor buttonSelectMonsterInfor)
    {
        if (_currentMonsterSelected != null && _currentMonsterSelected != buttonSelectMonsterInfor)
        {
            _currentMonsterSelected = buttonSelectMonsterInfor;
            _currentMonsterSelected.ImgSelected.gameObject.SetActive(true);
        }
        else if (_currentMonsterSelected != null && _currentMonsterSelected == buttonSelectMonsterInfor)
        {
            if (!_isrestore)
            {
                SwapMonster(EMonsterSide.Player, _currentMonsterSelected.MonsterIndex);
                ResetValue();
            }
            else
            {
                _onUseItem.OnNext(new UseItemHUDViewData(_currentItemselected.IdItem, _currentItemselected.ItemType));
                IsInteract = false;
                ResetValue();
            }
            
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
    #endregion Monster

    private bool CheckClickedBattleHUD()
    {
        if (!IsInteract) return false;
        if (_isBattleButtonClicked) return false;

        _isBattleButtonClicked = true;
        return true;
    }

    private void ResetValue()
    {
        _isBattleButtonClicked = false;
        _isrestore = false;
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
        if (_currentItemselected != null)
        {
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(false);
            _currentItemselected = null;
        }
    }
}