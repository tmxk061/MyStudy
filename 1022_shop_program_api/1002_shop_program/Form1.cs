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

        #region 컬렉션
        List<BuyDate> buylist = new List<BuyDate>();
        List<BlogDate> bloglist = new List<BlogDate>();
        #endregion

        #region 속성 및 프로퍼티
        Form2 form2;
        FolderBrowserDialog folderBrowser;
        Setting setting;
        int y =0; //상품리스트 좌표
        int x =0; //상품리스트 좌표
        int searchNum; //검색번호
        string SaveLink; //저장경로
        int display = 10; //상품출력 개수
        int Money = 0; //예산
        bool MoneyMode = false;//예산모드

        #endregion

        #region 생성자
        public Form1()
        {
            InitializeComponent();
            comboBox2.Text = "관련도";
            folderBrowser = new FolderBrowserDialog();
            setting = new Setting(this);
            SaveLink = string.Empty;
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
                bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString(),display.ToString());
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
            bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString(), display.ToString());
            PlintSearchList();
        }

        //<<이전
        private void button3_Click(object sender, EventArgs e)
        {
            if (searchNum <= 1)
                return;

            searchNum--;
            textBox2.Text = searchNum.ToString();
            bd.SearchBuy(textBox1.Text, comboBox2.Text, searchNum.ToString(), display.ToString());
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

        Loading lo = new Loading();

        
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
            if (MoneyMode == true)
            {
                if ((AllMoney() + bd.buydateList[idx - 1].Lprice) > Money)
                {
                    string str = string.Format("예산초과 : 예산 : {0}원", Money);
                    MessageBox.Show(str);
                    return;
                }


            }

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
        private int AllMoney()
        {
            int all = 0;
            foreach (BuyDate bu in buylist)
            {
                all += bu.Lprice;
            }
            textBox7.Text = all.ToString();

            return all;
        }

        //버튼_저장
        private void button4_Click(object sender, EventArgs e)
        {
            if (SaveLink.Equals(string.Empty))
                GetSaveURL();

            try
            {
                exelSave();
                string path = SaveLink + "\\MyShoping.txt";
                //@"C:\Users\USER\Desktop\이것저것\MyShoping.txt";
                string save = string.Empty;
                save += "합계 :" + textBox7.Text + "원" + "\r\n" +
                "===============================" + "\r\n";
                foreach (BuyDate bu in buylist)
                {
                    save += "상품명 :" + bu.Title + "\r\n" +
                            "URL :" + bu.Link + "\r\n" +
                            "최고가 :" + bu.Hprice + "\r\n" +
                            "최저가 :" + bu.Lprice + "\r\n" +
                            "===============================" + "\r\n";
                }
                System.IO.File.WriteAllText(path, save, Encoding.Default);

                MessageBox.Show("저장완료");
            }
            catch(Exception)
            {
                MessageBox.Show("저장실패");
            }
            
        }

        private void exelSave()
        {
          
            lo.ShowDialog();
            Application app = new Application();
            Workbook workbook = app.Workbooks.Add();
            //workbook.SaveAs(Filename: @"C:\Users\USER\Desktop\이것저것\MyShoping.xlsx");
            workbook.SaveAs(Filename: SaveLink+"\\MyShoping.xlsx");

            //workbook = app.Workbooks.Open(Filename: @"C:\Users\USER\Desktop\이것저것\test2.xlsx");
            workbook.SaveAs(Filename: SaveLink + "\\MyShoping.xlsx");

            Worksheet worksheet = workbook.Worksheets.Add();

            int r = 1;

            worksheet.Cells[r, 1] = "상품명";
            worksheet.Cells[r, 2] = "최저가";
            worksheet.Cells[r, 3] = "URL";
            r++;

            foreach (BuyDate d in buylist)
            {
                worksheet.Cells[r, 1] = d.Title;
                worksheet.Cells[r, 2] = d.Lprice;
                worksheet.Cells[r, 3] = d.Link;
                r+=2;
            }
            worksheet.Columns.AutoFit();
            workbook.Save();
            //workbook.SaveAs(Filename: @"C:\Users\USER\Desktop\이것저것\test2.xlsx");
            workbook.Close();


        }

        #endregion


        #region 메뉴
        private void 저장경로ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetSaveURL();
        }

        private void 프로그램설정SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setting.ShowDialog() == DialogResult.OK)
            {
                display = setting.Display;
                Money = setting.Money;
                MoneyMode = setting.MoneyMode;
            }
        }

        private void GetSaveURL()
        {
            DialogResult dr = folderBrowser.ShowDialog();
            if (dr == DialogResult.OK)
            {
               SaveLink = folderBrowser.SelectedPath;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("이용해주셔서 감사합니다.");
        }
        #endregion

       
    }
}
