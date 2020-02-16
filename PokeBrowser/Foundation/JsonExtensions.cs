using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokeBrowser.Foundation
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Jsonファイルへの出力
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void SerializeToFile<T>(this T obj, string path)
        {
            // JSON データにシリアライズ
            var jsonData = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);

            var directory = Directory.GetParent(path).FullName;
            Directory.CreateDirectory(directory);

            using (var sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                // JSON データをファイルに書き込み
                sw.Write(jsonData);
            }
        }

        /// <summary>
        /// Jsonファイルからの読み込み
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string path)
        {
            if (File.Exists(path) is false)
                return default;

            using (var file = File.OpenRead(path))
            {
                using (var sr = new StreamReader(file))
                {
                    return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                }
            }
        }
    }
}
