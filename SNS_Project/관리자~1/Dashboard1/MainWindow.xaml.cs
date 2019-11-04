using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard1
{
    public class Allinfo : INotifyPropertyChanged
    {
        private string result;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("result");
            }
        }

        protected void OnPropertyChanged(string user)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(user));
        }
    }
    public class CommentList : ObservableCollection<Post_Com>
    {
        public CommentList()
        {

        }
    }

    [DataContract]
    public class PostList : ObservableCollection<Post>
    {
        public PostList()
        {

        }
    }

    public class AccList : ObservableCollection<Account>
    {
        public AccList()
        {

        }
    }
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {

        DataBaseCtr dbctr = null;
        ServiceHost host = null;
        public MainWindow()
        {
            InitializeComponent();
            dbctr = new DataBaseCtr();
         
            Consumo consumo = new Consumo();
            DataContext = new ConsumoViewModel(consumo);
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ServerConnect_Click(object sender, RoutedEventArgs e)
        {
            host = new ServiceHost(typeof(DataBaseCtr));
            host.Open();

            //dbctr.ConnectDB();

        }
        private void ServerDisConnect_Click(object sender, RoutedEventArgs e)
        {
            host.Close();
            //dbctr.DisConnectDB();
        }

    }
    internal class ConsumoViewModel
    {
        public List<Consumo> Consumo { get; private set; }

        public ConsumoViewModel(Consumo consumo)
        {
            Consumo = new List<Consumo>();
            Consumo.Add(consumo);
        }
    }
    //파이차트
    internal class Consumo
    {
        public string Titulo { get; private set; }
        public int Porcentagem { get; private set; }

        public Consumo()
        {
            Titulo = "현재 활동/비활동 비율";
            Porcentagem = CalcularPorcentagem();
        }

        private int CalcularPorcentagem()
        {
            return 70; //Calculo da porcentagem de consumo
        }
    }

}
