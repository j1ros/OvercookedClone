using Overcooked.Data;
using Overcooked.UI;

namespace Overcooked.Level
{
    public class Order
    {
        public Order(UnitedStruct orderData, float startTime, OrderUI orderUI)
        {
            this._orderData = orderData;
            this._startTime = startTime;
            this._orderUI = orderUI;
            this._orderStatus = OrderStatus.New;
            _orderUI.Init(this);
        }

        private UnitedStruct _orderData;
        private float _startTime;
        private OrderStatus _orderStatus;
        private OrderUI _orderUI;
        public UnitedStruct OrderData => _orderData;
        public OrderStatus OrderStatus => _orderStatus;
        public float StartTime => _startTime;
        public OrderUI OrderUI => _orderUI;

        public void ChangeStatusOrder(OrderStatus status)
        {
            _orderStatus = status;
            _orderUI.ChangeView(this);
        }
    }
}
