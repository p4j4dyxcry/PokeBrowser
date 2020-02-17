using System.Linq;
using PokeBrowser.Data;

namespace PokeBrowser.Models
{
    /// <summary>
    /// ステータス計算クラス
    /// </summary>
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
        /// 努力値を設定する
        /// </summary>
        /// <param name="data"></param>
        public void SetEv(ParameterData<int> data)
        {
            Ev.CopyFrom(ref data);
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
        /// </summary>
        /// <param name="data"></param>
        public void SetIv(ParameterData<int> data)
        {
            Iv.CopyFrom(ref data);
        }
 
        /// <summary>
        /// 性格を設定する　/ 存在しない性格を指定した場合は上昇下降無しとなる
        /// ex : "おくびょう"
        /// </summary>
        /// <param name="str"></param>
        public void SetPersonality(string str)
        {
            Personality.Set(1,1,1,1,1,1);            
            
            if(DataBaseService.DataBase.AnyPersonality(str) is false)
                return;
            
            var personality = DataBaseService.DataBase.FindPersonality(str);
            if(personality.Up == PersonalityParameter.None)
                return;

            switch (personality.Up)
            {
                case PersonalityParameter.Atack:     Personality.Attack         = 1.1; break;
                case PersonalityParameter.Defence:   Personality.Defense        = 1.1; break;
                case PersonalityParameter.SpAtack:   Personality.SpecialAttack  = 1.1; break;
                case PersonalityParameter.SpDefence: Personality.SpecialDefense = 1.1; break;
                case PersonalityParameter.Speed:     Personality.Speed          = 1.1; break;
            }

            switch (personality.Down)
            {
                case PersonalityParameter.Atack:     Personality.Attack         = 0.9; break;
                case PersonalityParameter.Defence:   Personality.Defense        = 0.9; break;
                case PersonalityParameter.SpAtack:   Personality.SpecialAttack  = 0.9; break;
                case PersonalityParameter.SpDefence: Personality.SpecialDefense = 0.9; break;
                case PersonalityParameter.Speed:     Personality.Speed          = 0.9; break; 
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
            foreach (var i in Enumerable.Range(1,5))
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
        
        /// <summary>
        /// 実数値から努力値を逆算する
        /// </summary>
        /// <param name="level"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ParameterData<int> CalcEv(int level , ParameterData<int> value)
        {
            var result = new ParameterData<int>();
            result.Hp = StatusCalculator.CalcHitPointEv(Bv.Hp, Iv.Hp, value.Hp,level);

            // HP以外の計算式は同じなのでループで計算する
            foreach (var i in Enumerable.Range(1, 5))
            {
                var bv = Bv.GetByIndex(i); // 種族値
                var iv = Iv.GetByIndex(i); // 個体値
                var p   = value.GetByIndex(i);
                var person = Personality.GetByIndex(i); // 性格補正
                var param = StatusCalculator.CalcEv(bv, iv, p, person, level);
                result.SetByIndex(i,param);
            }
            return result;
        }
    }
}