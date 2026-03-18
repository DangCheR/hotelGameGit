using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Hud
{
    public sealed class SplashScreenHudView : BaseHud
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fillBarImage;
        [SerializeField] private TMP_Text _appVersionText;
        [SerializeField] private TMP_Text _deviceIDText;

        public Image Icon => _icon;
        public Image FillBarImage => _fillBarImage;
        public TMP_Text AppVersionText => _appVersionText;

        protected override void OnEnable()
        {
            var deviceID = SystemInfo.deviceUniqueIdentifier;
            _deviceIDText.text = deviceID;
        }

        protected override void OnDisable()
        {
            
        }
    }
}