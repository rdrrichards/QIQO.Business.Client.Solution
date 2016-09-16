using QIQO.Business.Client.Wrappers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General
{
    /// <summary>
    /// Interaction logic for CommentControl.xaml
    /// </summary>
    public partial class CommentControl : UserControl
    {
        public CommentControl()
        {
            InitializeComponent();
        }

        public IEnumerable<CommentWrapper> Comments
        {
            get { return (IEnumerable<CommentWrapper>)GetValue(CommentsProperty); }
            set { SetValue(CommentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Comments.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommentsProperty =
            DependencyProperty.Register("Comments", typeof(IEnumerable<CommentWrapper>), typeof(CommentControl));


    }
}
