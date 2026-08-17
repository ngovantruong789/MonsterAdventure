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
    [SerializeField] private List<SelectItemInfor> _selectItemRestore;
    [SerializeField] private List<SelectItemInfor> _selectItemCapture;
    [SerializeField] private SelectItemInfor _itemPrefab;
    [SerializeField] private RectTransform _itemRestoreParent;
    [SerializeField] private RectTransform _itemCaptureParent;
    [SerializeField] private Button _btnItemRestore;
    [SerializeField] private Button _btnItemCapture;
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
            ShowItemRestore();
        });

        _btnItemCapture.onClick.AddListener(() =>
        {
            ShowItemCapture();
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

    public void CreateItemButtons()
    {
        int restoreDataCount = _hUDBattleMonsterViewData.RestoreInventoryModel.Items.Count;
        int captureDataCount = _hUDBattleMonsterViewData.CaptureInventoryModel.Items.Count;
        
        for (int i = 0; i < restoreDataCount; i++)
        {
            SelectItemInfor newItemUI = SpawnItemButton(_itemRestoreParent);
            _selectItemRestore.Add(newItemUI);
        }
        for (int i = 0; i < captureDataCount; i++)
        {
            SelectItemInfor newItemUI = SpawnItemButton(_itemCaptureParent);
            _selectItemCapture.Add(newItemUI);
        }
        _selectItemRestore.ForEach(item => item.BtnItem.onClick.AddListener(() => SelectItem(item)));
        _selectItemCapture.ForEach(item => item.BtnItem.onClick.AddListener(() => SelectItem(item)));
    }

    private SelectItemInfor SpawnItemButton(RectTransform parent)
    {
        return Instantiate(_itemPrefab, parent);
    }

    public void UpdateItemButtons()
    {
        List<ItemModel> restoreData = _hUDBattleMonsterViewData.RestoreInventoryModel.Items;
        List<ItemModel> captureData = _hUDBattleMonsterViewData.CaptureInventoryModel.Items;
        UpdateItemButtons(restoreData, _selectItemRestore);
        UpdateItemButtons(captureData, _selectItemCapture);
    }

    private void UpdateItemButtons(List<ItemModel> data, List<SelectItemInfor> listItemData)
    {
        int i = 0;
        foreach (ItemModel item in data)
        {
            listItemData[i].IdItem = item.Id;
            listItemData[i].ItemNameText.text = item.Name;
            listItemData[i].DescriptionText.text = item.Description;
            listItemData[i].QuantityText.text = item.Quantity.ToString();
            if (item.Image != null) listItemData[i].ImgIcon.sprite = item.Image;
            i++;
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
        _itemRestoreParent.gameObject.SetActive(true);
        _itemCaptureParent.gameObject.SetActive(false);
    }

    public void ShowItemRestore()
    {
        _itemRestoreParent.gameObject.SetActive(true);
        _itemCaptureParent.gameObject.SetActive(false);
        if (_currentItemselected != null)
        {
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(false);
            _currentItemselected = null;
        }

    }

    public void ShowItemCapture()
    {
        _itemRestoreParent.gameObject.SetActive(false);
        _itemCaptureParent.gameObject.SetActive(true);
        if (_currentItemselected != null)
        {
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(false);
            _currentItemselected = null;
        }
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
            _onActiveItem.OnNext(_currentItemselected.IdItem);
        }
        else
        {
            _currentItemselected = buttonSelectItemInfor;
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(true);
        }
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
        if (_currentItemselected != null)
        {
            _currentItemselected.ImgSelectedItem.gameObject.SetActive(false);
            _currentItemselected = null;
        }
    }
}