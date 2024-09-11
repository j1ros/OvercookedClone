using System.Collections;
using System.Collections.Generic;
using Overcooked.Data;
using Overcooked.UI;
using UnityEngine;

namespace Overcooked.Level
{
    public class OrderQueue : MonoBehaviour
    {
        [SerializeField] private GameObject _orderUIPrefab;
        private Transform _orderParent;
        private LevelTime _levelTime;
        private List<Order> _orders = new List<Order>();
        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = GetComponent<LevelManager>();
            _levelTime = GetComponent<LevelTime>();
            _orderParent = GameObject.FindGameObjectWithTag("OrderParent").transform;
        }

        private void Start()
        {
            CreateOrder(2);
            StartCoroutine(CheckQueue());
        }

        private void CreateOrder(int count)
        {
            if (_orderParent == null)
                return;

            if (_orders == null)
                _orders = new List<Order>();

            for (int i = 0; i < count; i++)
            {
                UnitedStruct newOrderData = GetRandomOrder();
                GameObject newOrderObj = Instantiate(_orderUIPrefab, _orderParent);
                OrderUI newOrderUI = newOrderObj.GetComponent<OrderUI>();
                Order newOrder = new Order(newOrderData, _levelTime.TimeFromStart, newOrderUI);
                _orders.Add(newOrder);
            }
        }

        private UnitedStruct GetRandomOrder()
        {
            return _levelManager.LevelSO.PossibleRecipes.UnitedRecipe[Random.Range(0, _levelManager.LevelSO.PossibleRecipes.UnitedRecipe.Count)];
        }

        private void DeclineOrder(Order order)
        {
            Destroy(order.OrderUI.gameObject);
            _orders.Remove(order);
        }

        IEnumerator CheckQueue()
        {
            for (; ; )
            {
                for (int i = 0; i < _orders.Count; i++)
                {
                    float procentToEndOrder = (_levelTime.TimeFromStart - _orders[i].StartTime) / _levelManager.LevelSO.TimeOnOrder;
                    if (procentToEndOrder > .5f)
                    {
                        _orders[i].ChangeStatusOrder(OrderStatus.Old);
                    }
                    if (procentToEndOrder > .75f)
                    {
                        _orders[i].ChangeStatusOrder(OrderStatus.LastChanceOrder);
                    }
                    if (procentToEndOrder > 1f)
                    {
                        DeclineOrder(_orders[i]);
                    }
                }
                if (_orders.Count < 2)
                {
                    CreateOrder(2);
                }
                yield return new WaitForSeconds(.15f);
            }
        }
    }
}
