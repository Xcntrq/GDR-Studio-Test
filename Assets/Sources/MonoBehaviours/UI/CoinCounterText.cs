using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinCounterText : MonoBehaviour
{
    [SerializeField] private CollectableSpawner _collectableSpawner;
    [SerializeField] private string _preText;

    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _collectableSpawner.OnCountersChanged += CollectableSpawner_OnCountersChanged;
    }

    private void OnDestroy()
    {
        _collectableSpawner.OnCountersChanged -= CollectableSpawner_OnCountersChanged;
    }

    private void CollectableSpawner_OnCountersChanged(int collected, int totalCount)
    {
        _textMeshPro.SetText(string.Concat(_preText, collected, '/', totalCount));
    }
}
