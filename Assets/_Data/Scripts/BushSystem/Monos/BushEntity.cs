using UnityEngine;
using VContainer;

public class BushEntity : BaseMonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _touchedSprite;
    [SerializeField] private Sprite _exitTouchedSprite;

    [Inject] private BattleManager _battleManager;

    private IPlayer _player;

    private void EnterBattle()
    {
        if (_player == null) return;
        if (!CanBattle()) return;

        _battleManager.EnterBattle();
    }

    private bool CanBattle()
    {
        int rand = Random.Range(1, 101);
        return rand > 80;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out IPlayer player)) return;

        _player = player;
        _spriteRenderer.sprite = _touchedSprite;
        EnterBattle();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_player == null) return;
        _player = null;
        _spriteRenderer.sprite = _exitTouchedSprite;
    }
}