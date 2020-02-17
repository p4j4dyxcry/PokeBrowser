using System.Linq;

namespace PokeBrowser.Models
{
    public class Filters
    {
        /// <summary>
        /// メガシンカするポケモンをフィルタリングする
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsMegashinka(Data.PokemonData data)
        {
            if (data.Form is null)
                return false;
            return data.Form.StartsWith("メガ");
        }

        /// <summary>
        /// 禁止級伝説をフィルタする
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsBanLegend(Data.PokemonData data)
        {
            var ids = new []
            {
                493, //アルセウス
                890, //ムゲンダイナ
                384, //レックウザ
                643, //レシラム
                644, //ゼクロム	
                791, //ソルガレオ
                150, //ミュウツー
                716, //ゼルネアス
                717, //イベルタル	
                484, //パルキア
                487, //ギラティナ
                483, //ディアルガ
                250, //ホウオウ
                792, //ルナアーラ
                249, //ルギア
                888, //ザシアン
                889, //ザマゼンタ
                383, //グラードン
                382, //カイオーガ	
                646, //キュレム	
                491, //ダークライ
                492, //シェイミ
                385, //ジラーチ
                492, //シェイミ	
                386, //デオキシス
                490, //マナフィ	
                151, //ミュウ
                721, //ボルケニオン
                489, //フィオネ
                718, //ジガルデ
                720, //フーパ
                719, //ディアンシー
                494, //ビクティニ
                648, //メロエッタ
                807, //ゼラオラ
                809, //メルメタル
                808, //メルタン
                789, //コスモッグ
                790, //コスモウム
                649, //ゲノセクト
                251, //セレビィ
                802, //マーシャドー
                801, //マギアナ
                800  //ネクロズマ
            };
            return ids.Contains(data.Id);
        }
    }
}
