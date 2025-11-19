using System;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words = new List<Word>();
    private Random _rnd = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        foreach (string word in text.Split(" "))
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        for (int i = 0; i < numberToHide; i++)
        {
            int index = _rnd.Next(_words.Count);
            _words[index].Hide();   //allow re-hiding
        }
    }

    public string GetDisplayText()
    {
        string result = _reference.GetDisplayText() + "\n\n";
        foreach (Word word in _words)
        {
            result += word.GetDisplayText() + " ";
        }
        return result;
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
}
