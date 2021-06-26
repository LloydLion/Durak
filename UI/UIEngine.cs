using System;

namespace Durak
{
    class UIEngine
    {
        private WindowContext context = new WindowContext();


        public IConsoleWindow Window { get; private set; }


        public UIEngine(IConsoleWindow startWindow)
        {
            Window = startWindow;
            Window.Show();
            Window.Context = context;
        }


        public bool Update()
        {
            var key = Console.ReadKey(true);
            Window.Update(key);

            if(Window.State.IsGoClosing)
            {
                Window.OnClose();
                if(context.NextWindow == null) return false;

                Console.Clear();

                Window = context.NextWindow;
                context.SetNextWindow(context.DefaultWindow);
                Window.Context = context;
                Window.Show();
            }

            return true;
        }
    }
}