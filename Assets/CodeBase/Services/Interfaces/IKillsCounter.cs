using Cysharp.Threading.Tasks;

namespace CodeBase.Services.Interfaces
{
    public interface IKillsCounter
    {
        void AddKill();
        UniTask ResetCounter();
    }
}