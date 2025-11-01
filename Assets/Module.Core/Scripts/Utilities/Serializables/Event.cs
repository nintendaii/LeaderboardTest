using System;

namespace Module.Core.Utilities {
    public class Event<T> {
        private event Action<T> ActionEvent;
        
        public void Subscribe(Action<T> action) => ActionEvent += action;

        public void Unsubscribe(Action<T> action) {
            if (ActionEvent != null) {
                ActionEvent -= action;
            }
        }
        
        public void Invoke(T value) => ActionEvent?.Invoke(value);
    }
}