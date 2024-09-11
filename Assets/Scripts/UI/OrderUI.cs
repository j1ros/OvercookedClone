using System.Collections.Generic;
using Overcooked.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Overcooked.UI
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField] private Color _newStatusColor;
        [SerializeField] private Color _oldStatusColor;
        [SerializeField] private Color _lastChanceOrderStatusColor;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private List<Image> _IngredientImages;

        public void Init(Order order)
        {
            ChangeBackgroundColor(order.OrderStatus);
            for (int i = 0; i < _IngredientImages.Count; i++)
            {
                if (order.OrderData.StartInteractiveObj.Count > i)
                    _IngredientImages[i].sprite = order.OrderData.StartInteractiveObj[i].Sprite;
                else
                    _IngredientImages[i].enabled = false;
            }
        }

        public void ChangeView(Order order)
        {
            ChangeBackgroundColor(order.OrderStatus);
        }

        private void ChangeBackgroundColor(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.New:
                    _backgroundImage.color = _newStatusColor;
                    break;
                case OrderStatus.Old:
                    _backgroundImage.color = _oldStatusColor;
                    break;
                case OrderStatus.LastChanceOrder:
                    _backgroundImage.color = _lastChanceOrderStatusColor;
                    break;
            }
        }
    }
}
