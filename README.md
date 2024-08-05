# Unity ReactiveProperty Helper

# How to use

```csharp
[Serializable]
public class CurrencyData
{
    public ReactiveProperty<int> SoftCurrency { get; private set; }
    public ReactiveProperty<int> HardCurrency { get; private set; }

    public CurrencyData()
    {
        SoftCurrency = new ReactiveProperty<int>();
        HardCurrency = new ReactiveProperty<int>();
    }
}
```
