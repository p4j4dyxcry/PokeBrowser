using System.Linq;
using PokeBrowser.Data;

namespace PokeBrowser.Models
{
    public class PokemonParameterCalculator
    {
        /// <summary>
        /// 種族値
        /// </summary>
        public ParameterData<int> Bv { get; } = new ParameterData<int>();

        /// <summary>
        /// 個体値
        /// </summary>
        public ParameterData<int> Iv { get; } = new ParameterData<int>();

        /// <summary>
        /// 種族値
        /// </summary>
        public ParameterData<int> Ev { get; } = new ParameterData<int>();

        /// <summary>
        /// 性格補正
        /// </summary>
        public ParameterData<double> Personality { get; } = new ParameterData<double>(1,1,1,1,1,1);

        /// <summary>
        /// 努力値を設定する
        /// ex "4-252-0-0-0-252"
        /// </summary>
        /// <param name="str"></param>
        public void SetEv(string str)
        {
            Ev.Set(str);
        }

        /// <summary>
        /// 個体値を設定する
        /// ex "4-252-0-0-0-0"
        /// </summary>
        /// <param name="str"></param>
        public void SetIv(string str)
        {
            Iv.Set(str);
        }

        /// <summary>
        /// 個体値を設定する
        /// ex "31-31-31-31-31-0"
        /// </summary>
        /// <param name="str"></param>
        public void SetPersonality(string str)
        {
            var personality = DataBaseService.DataBase.FindPersonality(str);
            Personality.Set(1,1,1,1,1,1);

            if(personality.Up == PersonalityParameter.None)
                return;

            switch (personality.Up)
            {
                case PersonalityParameter.Atack:     Personality.Attack = 1.1; break;
                case PersonalityParameter.Defence:   Personality.Attack = 1.1; break;
                case PersonalityParameter.SpAtack:   Personality.Attack = 1.1; break;
                case PersonalityParameter.SpDefence: Personality.Attack = 1.1; break;
                case PersonalityParameter.Speed:     Personality.Attack = 1.1; break;
            }

            switch (personality.Down)
            {
                case PersonalityParameter.Atack:     Personality.Attack = 0.9; break;
                case PersonalityParameter.Defence:   Personality.Attack = 0.9; break;
                case PersonalityParameter.SpAtack:   Personality.Attack = 0.9; break;
                case PersonalityParameter.SpDefence: Personality.Attack = 0.9; break;
                case PersonalityParameter.Speed:     Personality.Attack = 0.9; break; 
            }
        }

        /// <summary>
        /// ポケモンを設定する
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="form">フォルム(省略可能)</param>
        public void SetPokemon(string name , string form = null)
        {
            var pokemon = DataBaseService.DataBase.FindPokemon(name,form);
            Bv.Set(pokemon.Hp,pokemon.Attack,pokemon.Defense,pokemon.SpecialAttack,pokemon.SpecialDefense,pokemon.Speed);
        }

        /// <summary>
        /// ポケモンの実数値を計算する
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public ParameterData<int> Calc(int level)
        {
            var result = new ParameterData<int>();
            result.Hp = StatusCalculator.CalcHitPoint(Bv.Hp, Iv.Hp, Ev.Hp,level);

            // HP以外の計算式は同じなのでループで計算する
            foreach (var i in Enumerable.Range(1, 6))
            {
                var bv = Bv.GetByIndex(i); // 種族値
                var iv = Iv.GetByIndex(i); // 個体値
                var ev = Ev.GetByIndex(i); // 努力値
                var person = Personality.GetByIndex(i); // 性格補正
                var param = StatusCalculator.CalcParameter(bv, iv, ev, person, level);
                result.SetByIndex(i,param);
            }

            return result;
        }
    }
}