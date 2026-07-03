using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDBattleMonsterView : LifetimeScope
{
    [SerializeField] private MonsterBattleInforUI _playerMonster;
    [SerializeField] private MonsterBattleInforUI _opponentMonster;

    public void UpdateMonsterName(bool isPlayer, string name)
    {
        MonsterBattleInforUI currentBattleInfor = isPlayer ? _playerMonster : _opponentMonster;
        currentBattleInfor.MonsterNameText.text = name;
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
}
