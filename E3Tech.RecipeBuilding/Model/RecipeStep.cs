using System.ComponentModel;

namespace E3Tech.RecipeBuilding.Model
{
    public class RecipeStep : INotifyPropertyChanged
    {
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        private IRecipeBlock blockOne;
        public IRecipeBlock BlockOne
        {
            get => blockOne;
            set
            {
                blockOne = value;
                RaisePropertyChanged("BlockOne");
            }
        }
        private IRecipeBlock blockTwo;
        public IRecipeBlock BlockTwo
        {
            get => blockTwo;
            set
            {
                blockTwo = value;
                RaisePropertyChanged("BlockTwo");
            }
        }
        
        private IRecipeBlock blockThree;
        public IRecipeBlock BlockThree
        {
            get => blockThree;
            set
            {
                blockThree = value;
                RaisePropertyChanged("BlockThree");
            }
        }
        
        private IRecipeBlock blockFour;
        public IRecipeBlock BlockFour
        {
            get => blockFour;
            set
            {
                blockFour = value;
                RaisePropertyChanged("BlockFour");
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
