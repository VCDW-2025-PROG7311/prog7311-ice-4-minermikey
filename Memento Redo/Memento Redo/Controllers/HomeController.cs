using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MementoMVC.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Stack<Memento> _history = new();
        private static List<string> _savedTexts = new();
        private static string _text = "";

        public IActionResult Index()
        {
            ViewBag.SavedTexts = _savedTexts;
            return View((object)_text);
        }

        [HttpPost]
        public IActionResult Save(string newText)
        {
            _history.Push(new Memento(_text)); // Save current state
            _text = newText;
            _savedTexts.Add(newText);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Undo()
        {
            if (_history.Count > 0)
            {
                _text = _history.Pop().State;
                if (_savedTexts.Count > 0)
                {
                    _savedTexts.RemoveAt(_savedTexts.Count - 1);
                }
            }
            return RedirectToAction("Index");
        }
    }

    public class Memento
    {
        public string State { get; }
        public Memento(string state) => State = state;
    }
}
