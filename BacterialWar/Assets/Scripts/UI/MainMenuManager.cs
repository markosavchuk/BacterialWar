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

    [SerializeField]
    private GameObject _crystalPanel;

    [SerializeField]
    private Vector2 _crystalPosition;

    [SerializeField]
    private Text _crystalValue;
    
    private Vector3 _safeAreaShift;
    private float _initialCrystalHealth;

    protected override void OnAwake()
    {
        base.OnAwake();

        var safeAreaRect = VectorHelper.GetRectForSafeArea();
        _safeAreaShift = new Vector3(safeAreaRect.x, -safeAreaRect.y, 0);

        SetupMoneyPanel();
        SetupCrystalPanel();
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
        _moneyValue.text = Mathf.Round(money).ToString();
    }

    private void SetupCrystalPanel()
    {
        _crystalPanel.gameObject.transform.position = Camera.main.ViewportToScreenPoint(
            new Vector3(
                _crystalPosition.x,
                _crystalPosition.y,
                -Camera.main.transform.position.z) +
            _safeAreaShift);

        var crystal = MapManager.Instance.GetCrystal(Player.MyPlayer);
        _initialCrystalHealth = crystal.InitialHealth;

        crystal.HealthUpdated -= SetCrystalHealthValue;
        crystal.HealthUpdated += SetCrystalHealthValue;

        SetCrystalHealthValue(this, crystal.Health);
    }

    private void SetCrystalHealthValue(object sender, float health)
    {
        if (health > 0)
        {
            _crystalValue.text = $"{health}/{_initialCrystalHealth}";
        }
        else
        {
            _crystalValue.text = "(×_×)";
        }
    }
}
