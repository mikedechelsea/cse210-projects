using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        foreach (string w in text.Split(' '))
            _words.Add(new Word(w));
    }

    // Hides 'count' randomly selected words that are not already hidden.
    // Stretch challenge: only selects from visible words.
    public void HideRandomWords(int count)
    {
        List<int> visibleIndexes = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
                visibleIndexes.Add(i);
        }

        Random random = new Random();
        int toHide = Math.Min(count, visibleIndexes.Count);
        for (int i = 0; i < toHide; i++)
        {
            int pick = random.Next(visibleIndexes.Count);
            _words[visibleIndexes[pick]].Hide();
            visibleIndexes.RemoveAt(pick);
        }
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
                return false;
        }
        return true;
    }

    public string GetDisplayText()
    {
        List<string> parts = new List<string>();
        foreach (Word word in _words)
            parts.Add(word.GetDisplayText());
        return $"{_reference.GetDisplayText()}\n{string.Join(" ", parts)}";
    }
}
