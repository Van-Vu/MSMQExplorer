using System;
using System.ComponentModel;
using System.Windows;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;

namespace MSMQWpf.AvalonWrapper
{
    /// <summary>
    /// AvalonEdit wrapper
    /// http://stackoverflow.com/questions/14855304/two-way-binding-in-avalonedit-doesnt-work
    /// http://stackoverflow.com/questions/12344367/making-avalonedit-mvvm-compatible
    /// </summary>
    public class MvvmTextEditor : TextEditor, INotifyPropertyChanged
    {
        private FoldingManager _foldingManager;
        protected FoldingManager FoldingManager
        {
            get { return _foldingManager ?? (_foldingManager = FoldingManager.Install(TextArea)); }
            set { _foldingManager = value; }
        }

        private AbstractFoldingStrategy _foldingStrategy;
        protected AbstractFoldingStrategy FoldingStrategy
        {
            get { return _foldingStrategy ?? (_foldingStrategy = new XmlFoldingStrategy()); }
            set { _foldingStrategy = value; }
        }

        public static DependencyProperty DocumentTextProperty =
            DependencyProperty.Register("DocumentText", typeof(string), typeof(MvvmTextEditor),
            new PropertyMetadata((obj, args) =>
            {
                var target = (MvvmTextEditor)obj;
                if (target.DocumentText != null && target.DocumentText != (string) args.NewValue)
                {
                    target.DocumentText = (string) args.NewValue;
                }
            })
        );

        public string DocumentText
        {
            get { return Text; }
            set {
                Text = value;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            //Document.UndoStack.SizeLimit = 0;den
            //EndUndoGroup();
            //Document.BeginUpdate();
            SetCurrentValue(DocumentTextProperty, base.Text);
            //SetValue(DocumentTextProperty, base.Text);
            //FoldingStrategy.UpdateFoldings(FoldingManager, Document);
            base.OnTextChanged(e);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
