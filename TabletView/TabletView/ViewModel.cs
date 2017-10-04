using System;
using System.Collections.ObjectModel;

namespace TabletView {
    public class ViewModel : BaseViewModel {
        public ObservableCollection<ListModel> SourceData { get; private set; } = new ObservableCollection<ListModel>();

        public ViewModel() {
            for (int i = 1; i < 9; i++) {
                var subData = new ObservableCollection<SubGroup>();

                for (int j = 1; j < 4; j++) {
                    subData.Add(new SubGroup {
                        Group = new GroupModel {
                            ID = j,
                            Name = "Name " + j,
                            Date = DateTime.Now,
                            ScoreUs = "-",
                            ScoreThem = "32",
                            Points = "52"
                        }
                    });
                }

                var parent = new GroupModel {
                    ID = i,
                    Name = "Title " + i,
                    Date = DateTime.Now,
                    ScoreUs = "25",
                    ScoreThem = "35",
                    Points = "45"
                };

                SourceData.Add(new ListModel(parent, subData));
            }
        }
    }

	// List model derived from ObservableCollection<T> class
    // to store header and sub data
	public class ListModel : ObservableCollection<SubGroup>{
        // group header ID
        public double ID {
            get {
                if (Group == null)
                    return -1;

                return Group.ID;
            }
        }

        // real header data
        public GroupModel Group { get; private set; }

        // header model constructor
        // set subModel to parent
        public ListModel(GroupModel model, ObservableCollection<SubGroup> subModel) : base(subModel) {
            Group = model;
        }
    }
}
