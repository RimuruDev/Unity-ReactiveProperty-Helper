using System;

namespace RimuruDev
{
    [Serializable]
    public class ReactiveProperty<T>
    {
        private T value;
        [NonSerialized] private Action<T> onChanged;

        public event Action<T> OnChanged
        {
            add
            {
                onChanged += value;
                value?.Invoke(this.value);
            }
            remove => onChanged -= value;
        }

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                onChanged?.Invoke(value);
            }
        }

        public ReactiveProperty(T initialValue = default)
        {
            value = initialValue;
        }
    }
}
