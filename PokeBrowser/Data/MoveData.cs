namespace PokeBrowser.Data
{
    /// <summary>
    /// わざデータ
    /// </summary>
    public class MoveData
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 詳細
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// タイプ
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// 接触わざかどうか
        /// </summary>
        public bool IsContact { get; set; }
        
        /// <summary>
        /// 威力
        /// </summary>
        public int Power { get; set; }
        
        /// <summary>
        /// PP
        /// </summary>
        public int PowerPoint { get; set; }

        /// <summary>
        /// ダイマックス時の威力
        /// </summary>
        public int DaiMaxPower { get; set; }
        
        /// <summary>
        /// まもるが有効か
        /// </summary>
        public bool IsWall { get; set; }
        
        /// <summary>
        /// 命中率
        /// </summary>
        public string Acc { get; set; }
        
        /// <summary>
        /// 物理 / 特殊 /変化
        /// </summary>
        public string Class { get; set; }
        
        /// <summary>
        /// 自分 / 相手 /　場 / 味方 ... 等
        /// </summary>
        public string Taget { get; set; }

        public MoveData Clone()
        {
            return new MoveData()
            {
                Name = Name,
                Description = Description,
                Type =  Type,
                IsContact = IsContact,
                Power = Power,
                PowerPoint = PowerPoint,
                DaiMaxPower = DaiMaxPower,
                IsWall = IsWall,
                Acc = Acc,
                Class = Class,
                Taget = Taget
            };
        }
        
    }
}
