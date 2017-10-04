using System;

namespace ViewCellHeight {
    // item model
    public class GroupModel {
        public int ID { get; set; }
        public string ScoreUs { get; set; }
        public string ScoreThem { get; set; }
        public string Points { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }

    // sub group model
    // to store listview item source model
    public class SubGroup {
        public GroupModel Group { get; set; }
    }

    // screen model
    // to store screen size and orientation
    public class ScreenModel {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int DPI { get; set; } = 0;
        public bool IsLandscape { get; set; } = false;
    }
}
