using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenuManager : SingletonMonoBehaviour<MainMenuManager>
{
    [SerializeField]
    private GameObject _moneyPanel;

    [SerializeField]
    private Vector2 _moneyPosition;

    [SerializeField]
    private Text _moneyValue;

    private Vector3 _safeAreaShift;

    protected override void OnAwake()
    {
        base.OnAwake();

        var safeAreaRect = VectorHelper.GetRectForSafeArea();
        _safeAreaShift = new Vector3(safeAreaRect.x, -safeAreaRect.y, 0);

        SetupMoneyPanel();
    }

    private void SetupMoneyPanel()
    {
        _moneyPanel.gameObject.transform.position = Camera.main.ViewportToScreenPoint(
            new Vector3(
                _moneyPosition.x,
                _moneyPosition.y,
                -Camera.main.transform.position.z) +
            _safeAreaShift);

        MoneyManager.Instance.MyWalletUpdated -= SetMoneyValue;
        MoneyManager.Instance.MyWalletUpdated += SetMoneyValue;

        SetMoneyValue(this, MoneyManager.Instance.GetCurrentAmount(Player.MyPlayer));
    }

    private void SetMoneyValue(object sender, float money)
    {
        _moneyValue.text = money.ToString();
    }

    private void OnDestroy()
    {
        MoneyManager.Instance.MyWalletUpdated -= SetMoneyValue;
    }
}
