using System.Collections.Generic; 
namespace ProductsList{ 

    public class Status
    {
        public bool showCase { get; set; }
        public bool availableOnlyInRetailStore { get; set; }
        public bool soldOut { get; set; }
        public bool isCannotBeExchanged { get; set; }
    }

}