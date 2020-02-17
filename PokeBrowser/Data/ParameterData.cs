using System;
using System.Linq;

namespace PokeBrowser.Data
{
    /// <summary>
    /// パラメータデータクラス
    /// </summary>
    public class ParameterData
    {
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// メンバを0初期化します。
        /// </summary>
        public ParameterData() : this(0,0,0,0,0,0)
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
        public ParameterData(int h, int a, int b, int c, int d, int s)
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
            char[] separators = {' ', ',', ';', '-'};
            int index = 0;
            foreach (var value in parameters.Split(separators).Take(6).Select(int.Parse))
            {
                SetByIndex( index++ , value );
            }
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
        public void Set(int h, int a, int b, int c, int d, int s)
        {
            Hp = h;
            Attack = a;
            Defense = b;
            SpecialAttack = c;
            SpecialDefense = d;
            Speed = s;
        }

        /// <summary>
        /// Indexアクセスで値を設定します。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetByIndex(int index, int value)
        {
            switch (index)
            {
                case 0: Hp = value; break;
                case 1: Attack = value; break;
                case 2: Defense = value; break;
                case 3: SpecialAttack = value; break;
                case 4: SpecialDefense = value; break;
                case 5: Speed = value; break;
            }
            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Indexアクセスで値を取得します。
        /// </summary>
        /// <param name="index"></param>
        public int GetByIndex(int index)
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
    }
}