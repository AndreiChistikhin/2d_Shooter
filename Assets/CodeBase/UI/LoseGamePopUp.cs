using CodeBase.Configs;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI
{
    public class LoseGamePopUp : PopUpBase
    {
        public override void PlayMusic() =>
            _audioService.PlayMusic(AudioId.LoseSound, false).Forget();
    }
}