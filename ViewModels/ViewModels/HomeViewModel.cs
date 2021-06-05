using BusinessLogic;

namespace ViewModels.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        public HomeViewModel() : base("Home", "Super Awesome Amazing Tour Planner"){}
        public string Logo => Config.Instance.LogoPath;
    }
}
