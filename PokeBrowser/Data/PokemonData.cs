namespace PokeBrowser.Data
{
    public class PokemonData
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 英語名
        /// </summary>
        public string EnglishName { get; set; }
        
        /// <summary>
        /// フォルム名
        /// </summary>
        public string Form { get; set; }

        /// <summary>
        /// 全国図鑑NO
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// ガラルID
        /// </summary>
        public int? GalarID { get; set; }
        
        /// <summary>
        /// アローラID
        /// </summary>
        public int? AlolaID { get; set; }
        public int? AlolaID2 { get; set; }

        /// <summary>
        /// パラメータ
        /// </summary>
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        
        /// <summary>
        /// タイプ
        /// </summary>
        public string Type1 { get; set; }
        public string Type2 { get; set; }

        /// <summary>
        /// タマゴグループ
        /// </summary>
        public string Group1 { get; set; }
        public string Group2 { get; set; }

        /// <summary>
        /// 重さ
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 特性
        /// </summary>
        public string Ability1 { get; set; }
        public string Ability2 { get; set; }
        public string Ability3 { get; set; }

        /// <summary>
        /// おぼえられるわざ
        /// </summary>
        public string[] Moves { get; set; }
        
        /// <summary>
        /// 過去作わざ
        /// </summary>
        public string[] Obsoletes { get; set; }
    }
}