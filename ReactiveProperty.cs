// ********************************************************************
//
//   Copyright (c) RimuruDev
//   Contact information:
//       Email:    rimuru.dev@gmail.com
//       GitHub:   https://github.com/RimuruDev
//       LinkedIn: https://www.linkedin.com/in/rimuru/
//
// ********************************************************************

using System;
using UnityEngine;

namespace RimuruDev
{
    [Serializable]
    [HelpURL("https://github.com/RimuruDev/Unity-ReactiveProperty-Helper.git")]
    public class ReactiveProperty<T>
    {
        [SerializeField] private T value;

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

        public void OnNext(T nextValue)
        {
            Value = nextValue;
        }

        public ReactiveProperty(T initialValue = default)
        {
            value = initialValue;
        }
    }
}
