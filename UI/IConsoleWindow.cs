using System;

namespace Durak
{
    interface IConsoleWindow
    {
        void Show();

        void Update(ConsoleKeyInfo key);

        void OnClose();


        WindowState State { get; }

        WindowContext Context { set; }
    }
}