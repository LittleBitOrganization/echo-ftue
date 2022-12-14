using System.Collections.Generic;
using System.Linq;
using LittleBit.Modules.UI;
using LittleBitGames.FTUE.Configs;
using ILayout = LittleBit.Modules.CoreModule.MonoInterfaces.ILayout;

namespace LittleBitGames.FTUE.DialogSystem
{
    public class DialogFactory
    {
        private readonly ICreator _creator;
        private readonly DialogsConfigSO _dialogsConfigSo;
        private readonly ILayoutBuilderService _layoutBuilderService;
        private readonly ILayout _root;

        public DialogFactory(ICreator creator, ILayoutBuilderService layoutBuilderService, ILayout root, DialogsConfigSO dialogsConfigSo)
        {
            _root = root;
            _layoutBuilderService = layoutBuilderService;
            _dialogsConfigSo = dialogsConfigSo;
            _creator = creator;
        }

        public DialogModel Create(string key, bool autoHide)
        {
            var config = GetConfig(key);
            var queue = GetPhrasesQueue(config);
            var viewPrefab = _dialogsConfigSo.ViewPrefab.GetComponent<ILayout>();

            var args = new object[] {queue, autoHide};

            var view = _layoutBuilderService.BuildLayout<DialogView>(_root, viewPrefab);
            var model = _creator.Instantiate<DialogModel>(args);
            var pres = _creator.Instantiate<DialogPresenter>(model, view);

            return model;
        }

        private DialogConfig GetConfig(string key) =>
            _dialogsConfigSo.DialogConfigs.First(c => c.GetKey() == key);

        private static Queue<string> GetPhrasesQueue(DialogConfig config)
        {
            var queue = new Queue<string>();

            foreach (var phrase in config.GetPhrases()) queue.Enqueue(phrase);
            
            return queue;
        }
    }
}