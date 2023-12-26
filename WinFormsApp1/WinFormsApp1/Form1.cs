using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private List<string> convertKeyWord(string keyword)
        {
            //bool hasDiacritics = RemoveDiacritics(keyword) != keyword;

            char[][] arr1 = new char[][] {
               new char[] { 'a', 'á', 'à', 'ả', 'ã', 'ạ', 'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ' , 'â', 'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ'},
               new char[] { 'â', 'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ' },
               new char[] { 'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ' },
               new char[] { 'd', 'đ' },
               new char[] { 'e', 'é', 'è','ẻ','ẽ','ẹ', 'ê','ế','ề','ể','ễ','ệ' },
               new char[] { 'ê', 'ế', 'ề','ể','ễ','ệ' },
               new char[] { 'i', 'í', 'ì','ỉ','ĩ','ị' },
               new char[] { 'o', 'ó', 'ò','ỏ','õ','ọ', 'ô','ô','ố','ồ','ổ','ỗ','ộ', 'ơ','ớ','ờ','ở','ỡ','ợ' },
               new char[] { 'ô', 'ô', 'ố','ồ','ổ','ỗ','ộ' },
               new char[] { 'ơ', 'ớ', 'ờ','ở','ỡ','ợ' },
               new char[] { 'u', 'ú', 'ù','ủ','ũ','ụ', 'ư','ứ','ừ','ử','ữ','ự' },
               new char[] { 'ư', 'ứ', 'ừ','ử','ữ','ự' },
               new char[] { 'y', 'ý', 'ỳ','ỷ','ỹ','ỵ' }};
            List<(int index, char value)> t = new List<(int, char)>();
            for (int i = 0; i < keyword.Length; i++)
            {
                foreach (var item in arr1)
                {
                    if (keyword[i] == item[0])
                    {
                        t.Add((i, item[0]));
                    }
                }
            }
            var a = new List<string>();
            //if (hasDiacritics)
            BackTrackingMethod(0, keyword, arr1, a);
            if (!a.Contains(keyword))
                a.Insert(0, keyword);
            return a;
        }

        private string RemoveDiacritics(string keyword)
        {
            var normalizedString = keyword.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private void BackTrackingMethod(int z, string keyword, char[][] arr1, List<string> a)
        {
            if (z == keyword.Length)
                return;
            var x = arr1.Where(p => p[0] == keyword[z]).ToArray();
            if (x.Length > 0)
            {
                var x1 = x[0];
                for (int j = 0; j < x1.Length; j++)
                {
                    string k = keyword.Remove(z, 1).Insert(z, x1[j].ToString());
                    if (!a.Contains(k))
                        a.Add(k);
                    BackTrackingMethod(z + 1, keyword.Remove(z, 1).Insert(z, x1[j].ToString()), arr1, a);
                }
            }
            else
            {
                BackTrackingMethod(z + 1, keyword, arr1, a);
            }
        }

        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            var a = convertKeyWord(textBox1.Text.ToLower());
            foreach (var item in a)
            {
                if (!string.IsNullOrEmpty(item))
                    listBox1.Items.Add(item);
            }

        }
    }
}
