using HobbyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyApp.VM
{
    public class HobbyItemViewModel : ViewModelBase
    {
        private readonly Hobby model;

        public HobbyItemViewModel(Hobby model)
        {
            this.model = model;
        }

        public string Name
        {
            get { return model.Name; }
            set { model.Name = value; }
        }

        public string Description
        {
            get { return model.Description; }
            set { model.Description = value; }
        }

    }
}
