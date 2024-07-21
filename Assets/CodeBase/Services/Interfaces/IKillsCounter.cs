using Cysharp.Threading.Tasks;

namespace CodeBase.Services
{
    public interface IKillsCounter
    {
        void AddKill();
        UniTask ResetCounter();
    }
}