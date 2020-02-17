using System;
using System.Linq;
using System.Reflection.Metadata;
using Livet;

namespace PokeBrowser.Models
{
    /// <summary>
    /// パラメータデータクラス
    /// </summary>
    public class ParameterData<T> : NotificationObject where T : struct, IComparable<T>
    {
        private T _hp;
        public T Hp
        {
            get => _hp;
            set => RaisePropertyChangedIfSet(ref _hp, value);
        }

        private T _attack;
        public T Attack
        {
            get => _attack;
            set => RaisePropertyChangedIfSet(ref _attack, value);
        }

        private T _defense;
        public T Defense
        {
            get => _defense;
            set => RaisePropertyChangedIfSet(ref _defense, value);
        }

        private T _specialAttack;
        public T SpecialAttack
        {
            get => _specialAttack;
            set => RaisePropertyChangedIfSet(ref _specialAttack, value);
        }

        private T _specialDefense;
        public T SpecialDefense
        {
            get => _specialDefense;
            set => RaisePropertyChangedIfSet(ref _specialDefense, value);
        }

        private T _speed;
        public T Speed
        {
            get => _speed;
            set => RaisePropertyChangedIfSet(ref _speed, value);
        }

        /// <summary>
        /// デフォルトコンストラクタ
        /// メンバを0初期化します。
        /// </summary>
        public ParameterData() : this(default, default, default, default, default, default)
        {

        }

        /// <summary>
        /// 直接値を指定するコンストラクタ
        /// </summary>
        /// <param name="h"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="s"></param>
        public ParameterData(T h, T a, T b, T c, T d, T s)
        {
            Set(h,a,b,c,d,s);
        }

        /// <summary>
        /// 文字列から値を設定するコンストラクタ
        /// 文字列は' ', ',', ';', '-'のいずれかを区切り文字とみなします。
        /// </summary>
        /// <param name="parameters"></param>
        public ParameterData(string parameters)
        {
            Set(parameters);
        }

        /// <summary>
        /// 値をまとめて設定します。
        /// </summary>
        /// <param name="h"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="s"></param>
        public void Set(T h, T a, T b, T c, T d, T s)
        {
            Hp = h;
            Attack = a;
            Defense = b;
            SpecialAttack = c;
            SpecialDefense = d;
            Speed = s;
        }

        /// <summary>
        /// 文字列から値を設定します。
        /// 文字列は' ', ',', ';', '-'のいずれかを区切り文字とみなします。
        /// </summary>
        /// <param name="parameters"></param>
        public void Set(string parameters)
        {
            char[] separators = { ' ', ',', ';', '-' };
            int index = 0;
            foreach (var value in parameters.Split(separators).Take(6))
            {
                if (typeof(T) == typeof(int))
                {
                    SetByIndex(index++, (T)(object)int.Parse(value));
                }
                else if (typeof(T) == typeof(double))
                {
                    SetByIndex(index++, (T)(object)double.Parse(value));
                }
                else if (typeof(T) == typeof(float))
                {
                    SetByIndex(index++, (T)(object)float.Parse(value));
                }                
                else if (typeof(T) == typeof(decimal))
                {
                    SetByIndex(index++, (T)(object)decimal.Parse(value));
                }
                else if (typeof(T) == typeof(long))
                {
                    SetByIndex(index++, (T)(object)long.Parse(value));
                }
                else if (typeof(T) == typeof(uint))
                {
                    SetByIndex(index++, (T)(object)uint.Parse(value));
                }   
                else if (typeof(T) == typeof(short))
                {
                    SetByIndex(index++, (T)(object)short.Parse(value));
                } 
                else if (typeof(T) == typeof(ushort))
                {
                    SetByIndex(index++, (T)(object)ushort.Parse(value));
                }                   
            }
        }

        /// <summary>
        /// 引数で渡されたパラメータをコピーして設定する
        /// </summary>
        /// <param name="refData"></param>
        public void CopyFrom(ref ParameterData<T> refData)
        {
            var refArray = refData.ToArray();

            foreach (var i in Enumerable.Range(0, refArray.Length))
            {
                SetByIndex(i, refArray[i]);
            }
        }


        /// <summary>
        /// Indexアクセスで値を設定します。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetByIndex(int index, T value)
        {
            switch (index)
            {
                case 0: Hp = value; break;
                case 1: Attack = value; break;
                case 2: Defense = value; break;
                case 3: SpecialAttack = value; break;
                case 4: SpecialDefense = value; break;
                case 5: Speed = value; break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Indexアクセスで値を取得します。
        /// </summary>
        /// <param name="index"></param>
        public T GetByIndex(int index)
        {
            switch (index)
            {
                case 0: return Hp;
                case 1: return Attack;
                case 2: return Defense;
                case 3: return SpecialAttack;
                case 4: return SpecialDefense;
                case 5: return Speed;
            }
            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// 配列化する
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return new[]
            {
                Hp,
                Attack,
                Defense,
                SpecialAttack,
                SpecialDefense,
                Speed
            };

        }
    }
}