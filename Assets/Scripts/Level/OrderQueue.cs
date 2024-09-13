using System.Collections;
using System.Collections.Generic;
using Overcooked.Data;
using Overcooked.UI;
using UnityEngine;
using System.Linq;

namespace Overcooked.Level
{
    public class OrderQueue : MonoBehaviour
    {
        [SerializeField] private GameObject _orderUIPrefab;
        [SerializeField][Range(0, 2)] private float _newOrderRewardMultiply;
        [SerializeField][Range(0, 2)] private float _oldOrderRewardMultiply;
        [SerializeField][Range(0, 2)] private float _lastChanceOrderOrderRewardMultiply;
        [SerializeField][Range(-2, 0)] private float _punishMultiply;
        private Transform _orderParent;
        private LevelTime _levelTime;
        private List<Order> _orders = new List<Order>();
        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = GetComponent<LevelManager>();
            _levelTime = GetComponent<LevelTime>();
            _orderParent = GameObject.FindGameObjectWithTag("OrderParent").transform;
            EventManager.StartListening(EventType.Delivery, DeliveryPlate);
        }

        private void Start()
        {
            CreateOrder(2);
            StartCoroutine(CheckQueue());
        }

        private void OnDestroy()
        {
            EventManager.StopListening(EventType.Delivery, DeliveryPlate);
        }

        private void DeliveryPlate(Dictionary<EventMessageType, object> data)
        {
            List<InteractiveSO> deliveryOrder = data[EventMessageType.UnitedObjects] as List<InteractiveSO>;
            for (int i = 0; i < _orders.Count; i++)
            {
                IEnumerable<InteractiveSO> commonInteractiveObj = deliveryOrder.Intersect(_orders[i].OrderData.StartInteractiveObj);
                if (deliveryOrder.Count == commonInteractiveObj.Count() && _orders[i].OrderData.StartInteractiveObj.Count == commonInteractiveObj.Count())
                {
                    CompleteOrder(i);
                    return;
                }
            }
        }

        private void CompleteOrder(int index)
        {
            float rewardMultiply = 0;
            switch (_orders[index].OrderStatus)
            {
                case OrderStatus.New:
                    rewardMultiply = _newOrderRewardMultiply;
                    break;
                case OrderStatus.Old:
                    rewardMultiply = _oldOrderRewardMultiply;
                    break;
                case OrderStatus.LastChanceOrder:
                    rewardMultiply = _lastChanceOrderOrderRewardMultiply;
                    break;
            }
            RemoveOrder(_orders[index]);
            EventManager.TriggerEvent(EventType.AddPoints, new Dictionary<EventMessageType, object> { { EventMessageType.Points, rewardMultiply } });
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

        private void RemoveOrder(Order order)
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
                        EventManager.TriggerEvent(EventType.AddPoints, new Dictionary<EventMessageType, object> { { EventMessageType.Points, _punishMultiply } });
                        RemoveOrder(_orders[i]);
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
