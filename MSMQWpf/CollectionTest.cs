using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSMQWpf
{
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public class CollectionTest
    {
        private ObservableCollection<string> testCollection = new ObservableCollection<string>();
        public CollectionTest()
        {
            testCollection.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs e)
                {
                    // Subscribe event
                    switch (e.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            var a = "we are here";
                            break;

                        case NotifyCollectionChangedAction.Remove:
                            break;
                    }
                };


            testCollection.Add("something");

        }
    }
}
