using System.Collections;
using System.Collections.Concurrent;

namespace TomLonghurst.AdventOfCode._2022.Day6;

public class RollingCollection<T> : IEnumerable<T>
{
    private readonly ConcurrentQueue<T> _queue = new();

    public RollingCollection(int maximumCount)
    {
        if (maximumCount <= 0)
        {
            throw new ArgumentException(null, nameof(maximumCount));
        }

        MaximumCount = maximumCount;
    }

    public int MaximumCount { get; }
    public int Count => _queue.Count;

    public void Add(T value)
    {
        if (_queue.Count == MaximumCount)
        {
            _queue.TryDequeue(out _);
        }

        _queue.Enqueue(value);
    }

    public IEnumerator<T> GetEnumerator() => _queue.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}