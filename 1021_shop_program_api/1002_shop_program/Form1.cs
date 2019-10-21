using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Application = Microsoft.Office.Interop.Excel.Application;
using Microsoft.Office.Interop.Excel;

namespace _1002_shop_program
{
    public partial class Form1 : Form
    {

        #region API
        BuyDateSearcher bd = new BuyDateSearcher();
        BlogDateSearcher blogd = new BlogDateSearcher();
        #endregion

        #region 엑셀저장
        
        #endregion

        #region 컬렉션
        List<BuyDate> buylist = new List<BuyDate>();
        List<BlogDate> bloglist = new List<BlogDate>();
        #endregion

        #region 속성 및 프로퍼티
        Form2 form2;
        int y =0; //상품리스트 좌표
        int x =0; //상품리스트 좌표
        int searchNum; //검색번호  
        #endregion

        #region 생성자
        public Form1()
        {
            InitializeComponent();
            comboBox2.Text = "관련도";
        
        }
        #endregion

        #region 상품검색
        //검색
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                searchNum = 1;
                textBox2.Text = searchNum.ToString();
                bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString());
                button1.Enabled = true;
                button3.Enabled = true;
                PlintSearchList();
            }
            catch (Exception)
            {
                MessageBox.Show("검색할 수 없습니다.");
            }
        }

        //다음>>
        private void button1_Click(object sender, EventArgs e)
        {
            searchNum++;
            textBox2.Text = searchNum.ToString();
            bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString());
            PlintSearchList();
        }

        //<<이전
        private void button3_Click(object sender, EventArgs e)
        {
            if (searchNum <= 1)
                return;

            searchNum--;
            textBox2.Text = searchNum.ToString();
            bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString());
            PlintSearchList();
        }

        //출력
        private void PlintSearchList()
        {
            panel1.Controls.Clear();
            x = 0;
            y = 0;

            Loading lo = new Loading();

            lo.Function = (() =>
            {
                for (int i = 0; i < bd.buydateList.Count; i++)
                {
                    string filepath = bd.buydateList[i].Image;
                    byte[] data = new System.Net.WebClient().DownloadData(filepath);
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(data);
                    Image img = Image.FromStream(ms);

                    shopingItem item =
                        new shopingItem(img, bd.buydateList[i].Title,
                                        bd.buydateList[i].Lprice.ToString(),
                                        bd.buydateList[i].Link.ToString(),
                                        this);

                    this.Invoke(new MethodInvoker(
                         delegate ()
                         {
                             panel1.Controls.Add(item);
                         }
                     )
                    );

                    if (x == 0 && y == 0)
                    {
                        this.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                item.Location = new System.Drawing.Point(x, y + 10);
                            }
                          )
                        );
                        x = item.Location.X;
                        y = item.Location.Y;
                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(
                            delegate ()
                            {
                                item.Location = new System.Drawing.Point(x, y + 150);
                            }
                          )
                        );
                        x = item.Location.X;
                        y = item.Location.Y;
                    }

                    item.num.Text = (i + 1).ToString();
                }
            });
            lo.ShowDialog();
        }

        //상세검색(shopingItem 에서 호출)
        public void CallSearchItemModal(string name)
        {
            //검색 모달 호출
            form2 = new Form2(name);
            form2.Show();
        }
        #endregion

        #region 정보출력
        private void PrintBuylist()
        {
            foreach (BuyDate bu in buylist)
            {
                listBox1.Items.Add(bu.Title);
            }
        }
        #endregion

        #region 장바구니

        //장바구니 추가(shopingitem에서 호출)
        public void AddList(int idx)
        {
            buylist.Add(bd.buydateList[idx - 1]);
            listBox1.Items.Clear();
            PrintBuylist();
            AllMoney();
        }
        //장바구니 물건 삭제
        private void button2_Click(object sender, EventArgs e)
        {
            if (buylist.Count <= 0 || listBox1.SelectedIndex ==-1)
                return;

            int a = listBox1.SelectedIndices.Count;
            BuyDate[] buys = new BuyDate[a];

            for (int i = 0; i < a; i++)
            {
                buys[i] = buylist[listBox1.SelectedIndices[i]];
            }

            for (int i = 0; i < a; i++)
            {
                buylist.Remove(buys[i]);
            }

            listBox1.Items.Clear();
            foreach (BuyDate bu in buylist)
            {
                listBox1.Items.Add(bu.Title);
            }
            AllMoney();
        }

        //장바구니 전체 돈 출력
        private void AllMoney()
        {
            int all = 0;
            foreach (BuyDate bu in buylist)
            {
                all += bu.Lprice;
            }
            textBox7.Text = all.ToString();
        }

        //버튼_txt저장
        private void button4_Click(object sender, EventArgs e)
        {
            exelSave();
            string path =
                @"C:\Users\USER\Desktop\이것저것\text.txt";
            string save = string.Empty;
            save += "합계 :" + textBox7.Text +"원"+ "\r\n"+
            "===============================" + "\r\n";
            foreach (BuyDate bu in buylist)
            {
                save += "상품명 :" + bu.Title + "\r\n" +
                        "URL :" + bu.Link + "\r\n" +
                        "최고가 :" + bu.Hprice + "\r\n" +
                        "최저가 :" + bu.Lprice + "\r\n" +
                        "===============================" + "\r\n";
            }
            System.IO.File.WriteAllText(path,save, Encoding.Default);

            MessageBox.Show("저장완료");
        }

        private void exelSave()
        {
            Application app = new Application();
            Workbook workbook = app.Workbooks.Add();
            workbook.SaveAs(Filename: @"C:\Users\USER\Desktop\이것저것\test2.xlsx");

            workbook = app.Workbooks.Open(Filename: @"C:\Users\USER\Desktop\이것저것\test2.xlsx");
            Worksheet worksheet = workbook.Worksheets.Add();

            int r = 1;
            foreach (BuyDate d in buylist)
            {
                worksheet.Cells[r, 1] = d.Title;
                worksheet.Cells[r, 2] = d.Lprice;
                worksheet.Cells[r, 3] = d.Link;
                r++;
            }
            workbook.Save();
            //workbook.SaveAs(Filename: @"C:\Users\USER\Desktop\이것저것\test2.xlsx");
            workbook.Close();


        }







        #endregion


    }
}
