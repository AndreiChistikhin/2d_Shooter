using CodeBase.Configs;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI
{
    public class WinGamePopUp : PopUpBase
    {
        public override void PlayMusic() =>
            _audioService.PlayMusic(AudioId.WinSound, false).Forget();
    }
}