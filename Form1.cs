using System.Diagnostics.Metrics;

namespace XAXB視窗版
{
    public partial class Form1 : Form
    {
        private XaXbEngine xaxb; // XAXB 遊戲引擎
        private int counter; // 猜測次數的計數器
        public Form1()
        {
            InitializeComponent();
            xaxb = new XaXbEngine(); // 初始化 XAXB 遊戲引擎
            label1.Text = "請輸入3位數字  (0~9) "; // 設置提醒文字
            counter = 0; // 將猜測次數的計數器設為 0，表示從 0 開始計算
        }
        // 設定點下"猜"按鈕的事件處理方式
        private void button1_Click(object sender, EventArgs e)
        {
            string userNum = textBox1.Text; // 獲取玩家輸入的 3 位數字
            if (xaxb.IsLegal(userNum)) // 檢查玩家輸入的數字有沒有符合規定
            {
                counter++; // 猜測次數加一
                string result = xaxb.GetResult(userNum); // 獲取猜測結果
                listBox1.Items.Add($"{userNum} : {result}，猜測次數 : {counter}"); // 將猜測結果顯示在列表框中
                if (result == "3A0B") // 如果猜對了
                {
                    listBox1.Items.Add("恭喜你，猜對了 !"); // 顯示恭喜訊息
                    button1.Enabled = false; // 禁止使用猜測按鈕
                }
            }
            else
            {
                MessageBox.Show("輸入的資料不對，或長度不對!!請重新輸入 !!"); // 提示玩家重新輸入 3 位數字
            }
            textBox1.Clear(); // 清空玩家輸入框
        }
    }
    // XAXB 遊戲引擎 ( 類別 )
    public class XaXbEngine
    {
        string luckynum; // 幸運數字
        // 遊戲引擎的建構函式
        public XaXbEngine()
        {
            // 隨機數生成器
            Random random = new Random();

            // 創建一個 3 元素的整數數組，用於存放生成的數字
            int[] tem = new int[3];

            // 隨機生成 3 個 0 到 9 之間的數字
            tem[0] = random.Next(0, 9);
            tem[1] = random.Next(0, 9);
            tem[2] = random.Next(0, 9);

            // 循環檢查數組中的 3 個數字是否有重複的數字
            while (tem[0] == tem[1] ^ tem[1] == tem[2] ^ tem[0] == tem[2])
            {
                // 如果有重複的數字，重新生成第二和第三個數字
                tem[1] = random.Next(0, 9);
                tem[2] = random.Next(0, 9);
            }

            // 將 3 個數字組合成一個字符串作為幸運數字
            luckynum = $"{tem[0]}{tem[1]}{tem[2]}";
        }
        // 設定數字
        public bool SetLuckyNumber(string newLuckyNum)
        {
            // 如果新設定的幸運數字合法
            if (IsLegal(newLuckyNum))
            {
                // 將幸運數字設置為新設定的值
                this.luckynum = newLuckyNum;
                return true; // 返回 true，表示設置成功
            }
            else
            {
                return false; // 返回 false，表示設置失敗
            }
        }
        // 獲取現在幸運數字的值
        public string GetLuckyNumber()
        {
            // 返回現在幸運數字的值
            return this.luckynum;
        }
        // 判斷數字是否符合規定
        public Boolean IsLegal(string theNumber)
        {
            // 將輸入的字符串轉換為字符數組
            char[] tem = theNumber.ToCharArray();

            // 檢查輸入的字符串長度是否為 3（必須是 3 位數字）
            if (tem.Length == 3)
            {
                // 檢查 3 位數字中的每一位是否都不同
                if (tem[0] != tem[1] ^ tem[1] != tem[2] ^ tem[0] != tem[2])
                {
                    // 如果每一位都不同，返回 true
                    return true;
                }
                else
                {
                    // 否則，返回 false
                    return false;
                }
            }
            else
            {
                // 如果字符串長度不是 3，返回 false
                return false;
            }
        }
        // 獲取猜數字的結果 ( XAXB )
        public string GetResult(string userNumber)
        {
            // 將玩家的猜測和幸運數字轉換為字符數組
            char[] user = userNumber.ToCharArray();
            char[] ans = this.luckynum.ToCharArray();

            // 初始化 a 和 b 計數器
            int a = 0; // 正確位置且正確數字的計數
            int b = 0; // 錯誤位置但正確數字的計數

            // 遍歷玩家的猜測數字
            for (int i = 0; i < user.Length; i++)
            {
                // 遍歷幸運數字
                for (int j = 0; j < ans.Length; j++)
                {
                    // 如果玩家的猜測數字和幸運數字的某個字符匹配
                    if (user[i] == ans[j])
                    {
                        // 檢查位置是否正確
                        if (i == j)
                        {
                            a++; // 增加 a 計數器（正確位置且正確數字）
                        }
                        else
                        {
                            b++; // 增加 b 計數器（錯誤位置但正確數字）
                        }
                    }
                }
            }
            // 返回猜測結果的字符串格式
            return $"{a}A{b}B";
        }
        // 檢查遊戲是否結束
        public Boolean IsGameover(string userNumber)
        {
            // 檢查玩家的猜測結果是否為 "3A0B"
            if (GetResult(userNumber) == "3A0B")
            {
                // 如果猜測結果是 "3A0B"，表示玩家已經猜中了幸運數字
                return true; // 遊戲結束，返回 true
            }
            else
            {
                // 否則，遊戲未結束
                return false; // 遊戲未結束，返回 false
            }
        }
    }
}