using System;
using System.Threading.Tasks;
using UnityEngine;
using Utils;


public abstract class ValuesBase<T> : ScriptableObject, IAwaitable<T>
{
    private T _currentValue;

    public T Value => _currentValue;

    public Action<T> OnSelected;

    //public Task<T> GetNextValue() - фиговый метод
    //{
    //    var task = new Task<T>(() => 
    //    {
    //        OnSelected = null;
    //        return _currentValue; 
    //    });

    //    OnSelected += (position) => task.Start();
    //    return task;
    //}

    //public Task<T> GetNextValue() - Просто посмотреть, так делать можно
    //{
    //    var task = new TaskCompletionSource<T>();
    //    OnSelected += (value) =>
    //    {
    //        task.SetResult(_currentValue);
    //        OnSelected = null;
    //    };
    //    return task.Task;
    //}

    public IAwaiter<T> GetAwaiter() => new ValueChangedNotifier<T>(this);

    public void SetValue(T item)
    {
        _currentValue = item;
        OnSelected?.Invoke(_currentValue);
    }

    public class ValueChangedNotifier<TAwaited> : IAwaiter<TAwaited>
    {
        private ValuesBase<TAwaited> _valueContainer;
        private TAwaited _result;

        private bool _isCompleted;

        public bool IsCompleted => _isCompleted;

        private Action _continuation;

        public ValueChangedNotifier(ValuesBase<TAwaited> valueContainer)
        {
            _valueContainer = valueContainer;
            _valueContainer.OnSelected += HandleValueChanged;
        }        

        private void HandleValueChanged(TAwaited awaited)
        {
            _valueContainer.OnSelected -= HandleValueChanged;
            awaited = _valueContainer.Value;
            _isCompleted = true;            
            _result = awaited;            
            _continuation?.Invoke();
        }

        public TAwaited GetResult() => _result;

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;

            if (_isCompleted) _continuation?.Invoke();            
        }
    }
}
