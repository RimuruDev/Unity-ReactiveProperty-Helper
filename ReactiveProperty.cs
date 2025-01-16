// ********************************************************************
//
//   Copyright (c) RimuruDev
//   Contact information:
//       Email:    rimuru.dev@gmail.com
//       GitHub:   https://github.com/RimuruDev
//       LinkedIn: https://www.linkedin.com/in/rimuru/
//
// ********************************************************************

// ReSharper disable CheckNamespace

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

        public ReactiveProperty(T initialValue = default)
        {
            value = initialValue;
        }

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

        /// <summary>
        /// Подписка на изменения с поддержкой IDisposable.
        /// <example>
        /// <code>
        /// Пример:
        /// 1:
        /// private readonly List-IDisposable> subscriptions = new();
        ///
        /// 2:
        /// subscriptions.Add(UserName.Subscribe(value => Origin.UserName = value));
        ///
        /// 3:
        ///  foreach (var subscription in subscriptions)
        ///     subscription.Dispose();
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="callback">Метод обратного вызова.</param>
        /// <returns>Объект IDisposable для отписки.</returns>
        public IDisposable Subscribe(Action<T> callback)
        {
            OnChanged += callback;

            // NOTE: Возвращаем объект, который при Dispose выполнит отписку.
            return new DisposableAction(() => OnChanged -= callback);
        }

        private class DisposableAction : IDisposable
        {
            private readonly Action disposeAction;
            private bool isDisposed;

            public DisposableAction(Action disposeAction)
            {
                this.disposeAction = disposeAction;
            }

            public void Dispose()
            {
                if (!isDisposed)
                {
                    disposeAction?.Invoke();
                    isDisposed = true;
                }
            }
        }
    }
}
