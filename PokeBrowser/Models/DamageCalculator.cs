using System;
using System.Collections.Generic;
using System.Linq;
using PokeBrowser.Data;

namespace PokeBrowser.Models
{
    public enum Field
    {
        None,
        Electric,
        Mist,
        Psycho,
        Glass,
    }

    public enum WeatherType
    {
        None,
        Sun,
        Rain,
        Rock,
        Ice,
    }

    public enum Wall
    {
        None = 0x00,
        Special = 0x01,
        Physical  = 0x10,
    }

    public class BattleFieldInformation
    {
        public Field Field { get; set; }
        public WeatherType Weather { get; set; }
        
        public Wall Wall { get; set; }
    }

    public enum Gender
    {
        None,
        Male,   
        Female,
    }

    public class DamageResult
    {
        public int Max { get; set; }
        public int Min { get; set; }
    }

    public class PokemonInformation
    {
        public string Name { get; set; }
        // パラメータ
        public ParameterData<int> Parameter { get; set; }

        public int Level { get; set; } = 50;
        
        public PokemonType Type1 { get; set; }
        
        public PokemonType Type2 { get; set; }
        
        public AbilityData Ability { get; set; }
        
        public Item Item { get; set; }
        
        public StatusAilment StatusAilment { get; set; }
        
        public Gender Gender { get; set; }

        public bool ContainType(string type)
        {
            return Type1.Name == type || Type2.Name == type;
        }
    }

    public class Strategy
    {
        private Func<bool> _canExecute;
        private Func<double> _execute;
        
        public Strategy(Func<bool> canExecute,Func<double> execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        
        public bool QueryEnabled()
        {
            return _canExecute.Invoke();
        }

        public double Calc()
        {
            return _execute.Invoke().real();
        }
    }

    public class DamageCalculator
    {
        private BattleFieldInformation _battleField = new BattleFieldInformation();
        private bool _isCritical = false;
        private bool _isFirst; // 先行
        private bool _isPokemonChanged;
        private MoveData _originalMoveData;
        private MoveData _moveData;
        private PokemonInformation _attackInfo;
        private PokemonInformation _defenceInfo;

        private readonly IList<Strategy> _powerStrategies;
        private IList<Strategy> AttackStrategies = new List<Strategy>();

        private IList<Strategy> DefenseStrategies = new List<Strategy>();
        private IList<Strategy> DamageStrategies = new List<Strategy>();

        public DamageCalculator()
        {
            _powerStrategies = CreatePowerStrategies().ToList();
        }
        
        /// <summary>
        /// 威力計算に用いる補正式一覧を取得する
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Strategy> CreatePowerStrategies()
        {
            // TODO ローカライズを考慮
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "とうそうしん" &&
                      _attackInfo.Gender != Gender.None &&
                      _defenceInfo.Gender != Gender.None,
                () => _attackInfo.Gender == _defenceInfo.Gender ? 1.25 : 0.75);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "エレキスキン" &&
                      _moveData.Type == "でんき",
                () => 1.2);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "ノーマルスキン" &&
                      _moveData.Type == "ノーマル",
                () => 1.2);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "フェアリースキン" &&
                      _moveData.Type == "フェアリー",
                () => 1.2);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "フリーズスキン" &&
                      _moveData.Type == "こおり",
                () => 1.2);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "てつのこぶし" &&
                      (
                        _moveData.Name.EndsWith("パンチ") ||
                        new []{"アームハンマー","アイスハンマー","スカイアッパー"}.Any(x=>x == _moveData.Name)
                      ),
                () => 1.2);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "すてみ" &&
                       DataBaseService.RecoilMoves.Contains(_moveData.Name),
                () => 1.2);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "ちからずく" &&
                      (
                          DataBaseService.SheerForceTarget.Contains(_moveData.Name) || 
                          _attackInfo.Item?.Name == "いのちのたま"
                      ),　
                () => 1.2);

            yield return new Strategy(
                () => _attackInfo.Ability.Name == "すなのちから" &&
                      _battleField.Weather == WeatherType.Rock && 
                       new []{"いわ","はがね","じめん"}.Any( x=>x == _moveData.Type),　
                () => 1.3);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "アナライズ" &&
                      (
                        _isFirst is false || _isPokemonChanged
                      ),
                () => 1.3);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "かたいツメ" &&
                      _moveData.IsContact,
                      () => 1.3);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "パンクロック" &&
                      DataBaseService.VoiceMoves.Contains(_moveData.Name),
                () => 1.3);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "テクニシャン" &&
                      _moveData.Power <= 60,
                () => 1.5);

            yield return new Strategy(
                () => _attackInfo.Ability.Name == "ねつぼうそう" &&
                      _moveData.Class == "特殊" && 
                      _attackInfo.StatusAilment.Name == "やけど",
                () => 1.5);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "どくぼうそう" &&
                      _moveData.Class == "物理" && 
                      _attackInfo.StatusAilment.Name == "どく",
                () => 1.5);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "がんじょうあご" &&
                      (
                          _isFirst is false || _isPokemonChanged
                      ) && 
                      DataBaseService.FangMoves.Any(x=>x == _moveData.Name),
                () => 1.5);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "メガランチャー" &&
                      _moveData.Name.Contains("はどう"),
                () => 1.5);
            
            yield return new Strategy(
                () => _attackInfo.Ability.Name == "はがねのせいしん" &&
                      _moveData.Type == "はがね",
                () => 1.5);
            
            yield return new Strategy(
                () => _attackInfo.Item?.Name == "ノーマルジュエル" &&
                      _moveData.Type == "ノーマル",
                () => 1.5);
            
            yield return new Strategy(
                () => _defenceInfo.Ability?.Name == "もふもふ" &&
                      _moveData.IsContact,
                () => 0.5);
            
            yield return new Strategy(
                () => _defenceInfo.Ability?.Name == "もふもふ" &&
                      _moveData.Type == "ほのお",
                () => 2.0);
            
            yield return new Strategy(
                () => _defenceInfo.Ability?.Name == "たいねつ" &&
                      _moveData.Type == "ほのお",
                () => 0.5);
            
            yield return new Strategy(
                () => _defenceInfo.Ability?.Name == "ハードロック" &&
                      TypeEffectBonus() > 1.1,
                () => 0.75);
            yield return new Strategy(
                () => _defenceInfo.Ability?.Name == "フィルター" &&
                      TypeEffectBonus() > 1.1,
                () => 0.75);
            
            // TODO パワースポット、オーラブレイク、フェアリーオーラ、ダークオーラ
        }
        
        public DamageCalculator AttackPokemon(PokemonInformation info)
        {
            _attackInfo = info;
            return this;
        }
        
        public DamageCalculator DefencePokemon(PokemonInformation info)
        {
            _defenceInfo = info;
            return this;
        }
        
        public DamageCalculator Wall(Wall w)
        {
            _battleField.Wall = w;
            return this;
        }
        
        public DamageCalculator PokemonChanged(bool flag = true)
        {
            _isPokemonChanged = flag;
            return this;
        }
        
        public DamageCalculator FirstAttack(bool flag = true)
        {
            _isFirst = flag;
            return this;
        }

        public DamageCalculator Move(MoveData move)
        {
            _originalMoveData = move;
            return this;
        }

        public DamageCalculator ResetBattleField()
        {
            _battleField = new BattleFieldInformation();
            return this;
        }
            
        public DamageCalculator Field(Field f)
        {
            _battleField.Field = f;
            return this;
        }

        public DamageCalculator Critical(bool critical)
        {
            _isCritical = critical;
            return this;
        }
        
        public DamageCalculator Weather(WeatherType w)
        {
            _battleField.Weather = w;
            return this;
        }

        public DamageResult Calc()
        {
            _moveData = CreateMoveDataFromAbility();
            var baseDamage = (int)(_attackInfo.Level * 2 / 5d + 2d);

            // 攻め手の特性・アイテムを考慮した攻撃力 or 特攻 の補正計算
            var damage = baseDamage * CalcAttack();
            
            // 攻め手の特性・アイテムを考慮した技威力の補正計算
            damage *= CalcPower();
            
            // 受け手側の特性・アイテムを考慮した防御 or 特防を計算
            damage *= CalcDefence();

            damage = damage.truncation();

            damage = damage / 50d + 2;
            damage = damage.truncation();

            // 天気補正を計算
            damage *= CalcWeather().real();

            // タイプ一致補正の計算
            damage *= TypeMatchBonus().real();

            // タイプ相性補正の計算
            damage *= TypeEffectBonus().real();
            
            // タイプ相性が無効の場合は0ダメージとする。
            if ((int) damage is 0)
            {
                return new DamageResult()
                {
                    Min = 0,
                    Max = 0,
                };
            }

            // 状態異常補正(やけど)の計算
            damage *= StatusAilmentBonus().real();
            
            var minDamage = damage * 0.85;

            return new DamageResult()
            {
                Min = (int)minDamage.clamp1(),
                Max = (int)damage.clamp1(),
            };
        }

        /// <summary>
        /// 状態異常計算
        /// </summary>
        /// <returns></returns>
        private double StatusAilmentBonus()
        {
            if (_attackInfo.StatusAilment?.Name == "やけど")
            {
                return 0.5d;
            }

            return 1.0d;
        }

        private MoveData CreateMoveDataFromAbility()
        {
            var result = _originalMoveData.Clone();
            if (result.Type == "ノーマル")
            {
                switch (_attackInfo.Ability.Name)
                {
                    case "エレキスキン":
                        result.Type = "でんき";
                        break;
                    case "スカイスキン":
                        result.Type = "ひこう";
                        break;
                    case "フリーズスキン":
                        result.Type = "こおり";
                        break;
                }
            }
            else if (_attackInfo.Ability.Name == "ノーマルスキン")
                result.Type = "ノーマル";

            return result;
        }
        
        /// <summary>
        /// タイプ相性計算
        /// </summary>
        /// <returns></returns>
        private double TypeEffectBonus()
        {
            var moveType = DataBaseService.DataBase.FindType(_moveData.Type);
            var bonus = 1.0d;
            if (moveType.SupperEffective.Contains(_defenceInfo.Type1))
                bonus *= 2.0d;
            if (moveType.SupperEffective.Contains(_defenceInfo.Type2))
                bonus *= 2.0d;
            if (moveType.BadEffective.Contains(_defenceInfo.Type1))
                bonus /= 2.0d;
            if (moveType.BadEffective.Contains(_defenceInfo.Type2))
                bonus /= 2.0d;
            if (moveType.NoEffective.Contains(_defenceInfo.Type1))
                bonus = 0;
            if (moveType.NoEffective.Contains(_defenceInfo.Type2))
                bonus = 0;

            return bonus;
        }

        /// <summary>
        /// タイプ一致計算
        /// </summary>
        /// <returns></returns>
        private double TypeMatchBonus()
        {
            if (_moveData.Type == _attackInfo.Type1.Name ||
                _moveData.Type == _attackInfo.Type2.Name)
            {
                if (_attackInfo.Ability?.Name == "てきおうりょく")
                {
                    return 2.0d;
                }

                return 1.5d;
            }

            return 1.0d;
        }
        
        /// <summary>
        /// 天気計算
        /// </summary>
        /// <returns></returns>
        private double CalcWeather()
        {
            switch (_battleField.Weather)
            {
                case WeatherType.None:
                    return 1d;
                case WeatherType.Sun when _moveData.Type == "ほのお":
                case WeatherType.Rain when _moveData.Type == "みず":
                    return 1.5d;
                case WeatherType.Sun when _moveData.Type == "みず":
                case WeatherType.Rain when _moveData.Type == "ほのお":
                    return 0.5d;
                default:
                    return 1d;
            }
        }

        // internals
        
        private double CalcPower()
        {
            var power = (double)_moveData.Power;

            foreach (var strategy in _powerStrategies)
            {
                if (strategy.QueryEnabled())
                    power *= strategy.Calc();
            }

            return power;
        }

        private double CalcAttack()
        {
            if (_moveData?.Class == "特殊")
            {
                if (_attackInfo.Item.Name == "でんきだま" && _attackInfo.Name == "ピカチュウ")
                    return _attackInfo.Parameter.SpecialAttack * 2.0;
                
                return _attackInfo.Parameter.SpecialAttack;                
            }

            if (_attackInfo.Item.Name == "でんきだま" && _attackInfo.Name == "ピカチュウ")
                return _attackInfo.Parameter.Attack * 2.0;
            
            if (_attackInfo.Item.Name == "ふといほね" && (_attackInfo.Name == "カラカラ" || _attackInfo.Name == "ガラガラ"))
                return _attackInfo.Parameter.Attack * 2.0;

            if (_attackInfo.Ability.Name == "ちからもち")
                return _attackInfo.Parameter.Attack * 2.0;

            if (_attackInfo.Ability.Name == "ヨガパワー")
                return _attackInfo.Parameter.Attack * 2.0;
            
            return _attackInfo.Parameter.Attack;
        }

        private double CalcDefence()
        {
            double def = _attackInfo.Parameter.SpecialDefense;
            if (_moveData?.Class == "特殊")
            {
                if (_defenceInfo.Item.Name == "しんかのきせき")
                {
                    def *= 1.5;
                }
                
                if (_defenceInfo.Item.Name == "とつげきチョッキ")
                {
                    def *= _attackInfo.Parameter.SpecialDefense * 1.5d;
                }

                if (_battleField.Weather == WeatherType.Rock && _defenceInfo.ContainType("いわ"))
                {
                    def *= 1.5;
                }
                
                return def;                
            }

            def = _defenceInfo.Parameter.Defense;

            
            if (string.IsNullOrEmpty(_defenceInfo.StatusAilment?.Name) is false && _defenceInfo.Ability.Name == "ふしぎなうろこ")
            {
                def *= 1.5;
            }
            
            if (_defenceInfo.Item.Name == "しんかのきせき")
            {
                def *= 1.5;
            }

            if (_defenceInfo.Ability.Name == "ファーコート")
            {
                def *= 2.0;
            }

            if (_defenceInfo.Ability.Name == "くさのけがわ" && _battleField.Field == Models.Field.Glass) 
            {
                def *= 1.5;
            }

            return def;
        }
    }

    internal static class MathExtensions
    {
        internal static double real(this double p)
        {
            if (p <= 0)
                return 0;
            return (4096d * p) / 4096d;
        }

        internal static double celling(this double d)
        {
            return Math.Ceiling(d);
        }

        internal static double truncation(this double d)
        {
            return Math.Truncate(d);
        }

        internal static double round(this double d)
        {
            return Math.Round(d);
        }
        
        internal static double clamp1(this double d)
        {
            return d <= 1 ? 1 : d;
        }
    }
}
