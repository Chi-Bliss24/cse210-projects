using System;
using System.Collections.Generic;

public class PromptGenerator
{
    // List of prompts for journaling
    private List<string> _prompts = new List<string>()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What am I most grateful for today?",
        "What is one thing I learned today?",
        "How did I help someone today?",
        "What made me smile today?",
        "What do I want to remember about today?"
    };

    private Random _random = new Random();

    // Returns a random prompt from the list
    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}
