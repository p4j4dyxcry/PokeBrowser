using PokeBrowser.Data;
using PokeBrowser.Models;
using PokeBrowser.ViewModels;

namespace PokeBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
        public MainWindow()
        {
            DataContext = new MainWindowVm();

            DamageTest();
            InitializeComponent();
        }

        /// <summary>
        /// ダメージ計算テスト
        /// </summary>
        public void DamageTest()
        {
            var damageCalculator = new DamageCalculator();

            damageCalculator
                .AttackPokemon(new PokemonInformation()
                {
                    Level = 50,
                    Parameter = new ParameterData<int>()
                    {
                        Attack = 182,
                    },
                    Ability = DataBaseService.DataBase.FindAbility("すなのちから"),
                    Type1 = DataBaseService.DataBase.FindType("ドラゴン"),
                    Type2 = DataBaseService.DataBase.FindType("じめん"),
                })
                .DefencePokemon(new PokemonInformation()
                {
                    Level = 50,
                    Parameter = new ParameterData<int>()
                    {
                        Defense = 189,
                    },
                    Ability = DataBaseService.DataBase.FindAbility("ふゆう"),
                    Type1 = DataBaseService.DataBase.FindType("エスパー"),
                })
                .天気(天気.砂)
                .Move(new MoveData()
                {
                    Name = "ストーンエッジ",
                    Power = 100,
                    Type = "いわ",
                });
            
            var result = damageCalculator.Calc();
        }
    }
}
