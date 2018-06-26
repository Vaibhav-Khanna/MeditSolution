using System;
namespace MeditSolution.Service
{
    public interface ICloseApp
    {
        void CloseApp(bool reopen = true);
    }
}
