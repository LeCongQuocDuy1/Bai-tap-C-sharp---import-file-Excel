using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportAndExport_ExcelFile
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public class UserInfo
        {
            public string Name { get; set; }
            public DateTime Birthday { get; set; }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // tạo ra danh sách UserInfo rỗng để hứng dữ liệu.
            List<UserInfo> userList = new List<UserInfo>();
            try
            {
                // mở file excel
                var package = new ExcelPackage(new FileInfo("ImportData.xlsx"));

                // lấy ra sheet đầu tiên để thao tác
                ExcelWorksheet workSheet = package.Workbook.Worksheets[0];

                // duyệt tuần tự từ dòng thứ 2 đến dòng cuối cùng của file. lưu ý file excel bắt đầu từ số 1 không phải số 0
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        // biến j biểu thị cho một column trong file
                        int j = 1;

                        // lấy ra cột họ tên tương ứng giá trị tại vị trí [i, 1]. i lần đầu là 2
                        // tăng j lên 1 đơn vị sau khi thực hiện xong câu lệnh
                        string name = workSheet.Cells[i, j++].Value.ToString();

                        // lấy ra cột ngày sinh tương ứng giá trị tại vị trí [i, 2]. i lần đầu là 2
                        // tăng j lên 1 đơn vị sau khi thực hiện xong câu lệnh
                        // lấy ra giá trị ngày tháng và ép kiểu thành DateTime                      
                        var birthdayTemp = workSheet.Cells[i, j++].Value;
                        DateTime birthday = new DateTime();
                        if (birthdayTemp != null)
                        {
                            birthday = (DateTime)birthdayTemp;
                        }

                        /*                         

                        Đừng lười biến mà dùng đoạn code này sẽ gây ra lỗi nếu giá trị value không thỏa kiểu DateTime

                        DateTime birthday = (DateTime)workSheet.Cells[i, j++].Value;

                         */


                        // tạo UserInfo từ dữ liệu đã lấy được
                        UserInfo user = new UserInfo()
                        {
                            Name = name,
                            Birthday = birthday
                        };

                        // add UserInfo vào danh sách userList
                        userList.Add(user);

                    }
                    catch (Exception exe)
                    {

                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error!");
            }

            dataGridView1.DataSource = userList;

        }

        private void btnExport_Click(object sender, EventArgs e)
        { 

        }

    }    
}
