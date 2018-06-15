using FluxInfo.Metier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluxInfoMobile.ViewModels
{
    public class RssVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] String property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public Rss Model { get; }

        public ChannelVM ChannelVM { get; }

        public RssVM(Rss model)
        {
            if(model != null)
            {
                Model = model;
                ChannelVM = new ChannelVM(Model.Channel);
            } else
            {
                Rss rss = new Rss
                {
                    Channel = new Channel()
                };
                model = rss;
                ChannelVM = new ChannelVM(model.Channel);
            }
        }

    }
}
