using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeBrowser.Data
{
    public class DataBase
    {
        /// <summary>
        /// 特性データ
        /// </summary>
        public AbilityData[] Abilities { get; private set; }

        /// <summary>
        /// タイプデータ
        /// </summary>
        public PokemonType[] Types { get; private set; }
        
        /// <summary>
        /// 性格データ
        /// </summary>
        public PersonalityData[] Personalities { get; private set; }

        /// <summary>
        /// ポケモンデータ
        /// </summary>
        public PokemonData[] Pokemons { get; private set; }

        /// <summary>
        /// フォルムデータ
        /// </summary>
        public PokemonData[] Forms { get; private set; }

        /// <summary>
        /// 技データ
        /// </summary>
        public MoveData[] Moves { get; private set; }

        private IDictionary<int, PokemonData> _pokemonDictionary = null;
        private IDictionary<int, PokemonData> _pokemonDictionaryWithId = null;
        private IDictionary<int, PokemonData[]> _formDictionary = null;
        private IDictionary<string, PokemonType> _typeDictionary = null;
        private IDictionary<string, AbilityData>     _abliDictionary = null;
        private IDictionary<string, PersonalityData> _persDictionary = null;
        private IDictionary<string, MoveData> _moveDictionary = null;

        public PokemonType FindType(string name) => _typeDictionary[name];
        public AbilityData FindAbility(string name) => _abliDictionary[name];
        public PersonalityData FindPersonality(string name) => _persDictionary.ContainsKey(name) ? _persDictionary[name] : default;
        public PokemonData FindPokemon(string name , string form = null) => _pokemonDictionary[pokemon_hash(name,form)];
        public PokemonData FindPokemon(int id) => _pokemonDictionaryWithId[id];

        public PokemonData[] GetForms(int id) => _formDictionary.ContainsKey(id) ? _formDictionary[id] : Array.Empty<PokemonData>();
        
        public bool AnyPersonality(string name) => _persDictionary.ContainsKey(name);
        public bool AnyPokemon(string name,string form = null) => _pokemonDictionary.ContainsKey(pokemon_hash(name,form));

        public static DataBase Build(IEnumerable<AbilityData> abilities,
                                     IEnumerable<TypeData> types,
                                     IEnumerable<PersonalityData> personalities,
                                     IEnumerable<PokemonData> pokemonData,
                                     IEnumerable<MoveData> moves,
                                     IEnumerable<PokemonData> forms)
        {
            var db = new DataBase();
            db.Abilities = abilities.ToArray();
            db.Personalities = personalities.ToArray();
            db.Moves = moves.ToArray();
            db.Forms = forms.ToArray();
            db.Pokemons = pokemonData.ToArray();

            db.Types = types.Select(x => new PokemonType(x)).ToArray();
            db._typeDictionary = db.Types.ToDictionary(x=>x.Name,x=>x);
            db._abliDictionary = db.Abilities.ToDictionary(x => x.Name, x => x);
            db._persDictionary = db.Personalities.ToDictionary(x => x.Name, x => x);
            db._moveDictionary = db.Moves.ToDictionary(x => x.Name, x => x);
            db._pokemonDictionary = db.Pokemons.Concat(db.Forms).ToDictionary(x => pokemon_hash(x.Name, x.Form), x => x);
            db._pokemonDictionaryWithId = db.Pokemons.ToDictionary(x => x.Id, x => x);
            db._formDictionary = db.Forms.GroupBy(x => x.Id).ToDictionary(x=>x.Key,x=>x.ToArray());

            foreach (var type in db.Types)
            {
                type.SetEffectiveSource(db._typeDictionary);
            }



            return db;
        }

        private static int pokemon_hash(string name, string form)
        {
            if (string.IsNullOrEmpty(form))
                return name.GetHashCode();

            if (name == form)
                return form.GetHashCode();

            return name.GetHashCode() ^ form.GetHashCode();
        }
    }

    public static class DataBaseService
    {
        public static DataBase DataBase { get; private set; }


        public static IEnumerable<string> Personalities => DataBase.Personalities.Select(x => x.Name);

        public static IEnumerable<string> Moves => DataBase.Moves.Select(x => x.Name);

        public static IEnumerable<string> Pokemons => DataBase.Pokemons.Select(x => x.Name);

        public static IEnumerable<string> RecoilMoves { get; } = new[]
        {
            "すてみタックル",
            "ウッドハンマー",
            "とっしん",
            "じごくぐるま",
            "ボルテッカー",
            "フレアドライブ",
            "もろはのずつき",
            "とびげり",
            "とびひざげり",
            "アフロブレイク",
            "ワイルドボルト",
            "ブレイブバード"
        };

        public static IEnumerable<string> SheerForceTarget { get; } = new[]
        {
            // どく
            "どくばり", "スモッグ", "ポイズンテール", "クロスポイズン", "ヘドロこうげき", "どくづき", "ヘドロばくだん", "ヘドロウェーブ", "ダストシュート", "ダブルニードル",

            // もうどく
            "どくどくのキバ",

            // やけど
            "ひのこ", "かえんぐるま", "ほのおのパンチ", "ふんえん", "ブレイズキック", "かえんほうしゃ", "ねっぷう", "れんごく", "せいなるほのお", "かえんだん", "だいもんじ",
            "フレアドライブ", "あおいほのお", "ねっとう", "スチームバースト", "コールドフレア",

            //　こおり
            "こなゆき", "れいとうパンチ", "フリーズドライ", "れいとうビーム", "ふぶき",

            // まひ
            "のしかかり", "ほっぺすりすり", "でんきショック", "スパーク", "かみなりパンチ", "ほうでん", "10まんボルト", "かみなり", "でんじほう", "ボルテッカー", "らいげき",
            "フリーズボルト", "はっけい", "とびはねる", "したでなめる", "りゅうのいぶき", "ライトニングサーフライド",

            // ねむり
            "いにしえのうた",

            // こおり / やけど / まひ
            "トライアタック",

            // こんらん
            "ピヨピヨパンチ", "ロッククライム", "みずのはどう", "ばくれつパンチ", "ぼうふう", "おしゃべり", "ねんりき", "サイケこうせん", "シグナルビーム",

            // ひるみ
            "ねこだまし", "いびき", "ふみつけ", "ずつき", "ひっさつまえば", "たきのぼり", "ニードルアーム", "びりびりちくちく", "つららおとし", "まわしげり", "ホネこんぼう",
            "エアスラッシュ", "ゴッドバード", "ハートスタンプ", "しねんのずつき", "じんつうりき", "ハードローラー", "いわなだれ", "おどろかす", "たつまき", "ドラゴンダイブ", "かみつく",
            "あくのはどう", "アイアンヘッド",

            // ひるみ　＆ ( こおり / やけど / まひ )
            "ほのおのキバ", "かみなりのキバ", "こおりのキバ",

            // Aダウン
            "トロピカルキック", "オーロラビーム", "とびかかる", "じゃれつく",

            // Bダウン
            "ブレイククロー", "ほのおのムチ", "シェルブレード", "アクアブレイク", "いわくだき", "シャドーボーン", "かみくだく", "アイアンテール",

            // Cダウン
            "マジカルフレイム", "ミストボール", "むしのていこう", "バークアウト", "ムーンフォース",

            // Dダウン
            "エナジーボール", "シードフレア", "きあいだま", "ようかいえき", "アシッドボム", "だいちのちから", "ラスターパージ", "サイコキネシス", "むしのさざめき", "シャドーボール",
            "ラスターカノン",

            // Sダウン
            "からみつく", "あわ", "バブルこうせん", "エレキネット", "こごえるかぜ", "こごえるせかい", "ローキック", "マッドショット", "じならし", "がんせきふうじ",

            // 命中ダウン
            "オクタンほう", "だくりゅう", "グラスミキサー", "どろかけ", "どろばくだん", "ナイトバースト", "ミラーショット",

            // A上昇
            "グロウパンチ", "メタルクロー", "コメットパンチ", "はがねのつばさ", "ダイヤストーム", "チャージビーム", "ほのおのまい", "ニトロチャージ",

            //全能力上昇
            "ぎんいろのかぜ", "げんしのちから", "あやしいかぜ", "ブレイジングソウルビート",

            "ひみつのちから",
            "かげぬい,アンカーショット",
            "じごくづき",
            "うたかたのアリア,オリジンスーパーノヴァ"
        };

        public static IEnumerable<string> VoiceMoves => new[]
        {
            "いびき", "うたかたのアリア", "エコーボイス", "さわぐ", "スケイルノイズ", "バークアウト", "ハイパーボイス", "ばくおんば", "むしのさざめき", "りんしょう",
        };

        public static IEnumerable<string> FangMoves { get; } = new[]
        {
            "かみつく","かみくだく","どくどくのキバ","ほのおのキバ","かみなりのキバ","こおりのキバ","ひっさつまえば","サイコファング","エラがみ"
        };

        public static void Initialize(DataBase dataBase)
        {
            DataBase = dataBase;
        }
    }
}
