using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Windows.Forms;

namespace 升级程序
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        WebClient webClient; //下载文件使用
        Stopwatch sw = new Stopwatch(); //用于计算下载速度
        public string finish;

        
        #region 下载文件版本文件
        public void DownloadFile2(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed2);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged2);

                try
                {
                    Uri URL;
                    // 先判断是否包括http://
                    if (!urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        URL = new Uri("http://" + urlAddress);
                    else
                        URL = new Uri(urlAddress);
                    sw.Start();
                    // 开始异步下载
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProgressChanged2(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                
                // 进度条
                if (progressBar1.Value != e.ProgressPercentage)
                    progressBar1.Value = e.ProgressPercentage;

                // 当前比例
                if (label1.Text != e.ProgressPercentage.ToString() + "%")
                    label1.Text = e.ProgressPercentage.ToString() + "%";

                // 下载了多少 还剩余多少
                label1.Text = (Convert.ToDouble(e.BytesReceived) / 1024 / 1024).ToString("0.00") + " Mb's" + "  /  " + (Convert.ToDouble(e.TotalBytesToReceive) / 1024 / 1024).ToString("0.00") + " Mb's";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // The event that will trigger when the WebClient is completed
        private void Completed2(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if (e.Cancelled == true)
            {
                string time = DateTime.Now.ToLongTimeString().ToString();
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "文件下载失败，请检查网络后再试"; //下载未完成 //下载未完成
            }
            else
            {
                string time = DateTime.Now.ToLongTimeString().ToString();
                
                    string versionnow = System.IO.File.ReadAllText(@"version.txt");
 
                string version = System.IO.File.ReadAllText(@"temp\version.txt");
               
                if (version == versionnow)
                {
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "当前为最新版本";
                    DelectDir("updata");
                    DelectDir("temp");
                }
                else
                {
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "检测到更新，当前版本" + versionnow + "目标版本" + version;
                    string dis = System.IO.File.ReadAllText(@"temp\dis.txt");
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "版本介绍：" + "\r\n" + dis;
                    richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "开始下载...";
                    DownloadFile("http://updata.hong-fu.top:666/Debug.zip", @"temp\Debug.zip");
                }
            }
        }
        #endregion

        #region 下载介绍文件
        public void DownloadFile1(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed1);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged1);

                try
                {
                    Uri URL;
                    // 先判断是否包括http://
                    if (!urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        URL = new Uri("http://" + urlAddress);
                    else
                        URL = new Uri(urlAddress);
                    sw.Start();
                    // 开始异步下载
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProgressChanged1(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
               

                // 进度条
                if (progressBar1.Value != e.ProgressPercentage)
                    progressBar1.Value = e.ProgressPercentage;

                // 当前比例
                if (label1.Text != e.ProgressPercentage.ToString() + "%")
                    label1.Text = e.ProgressPercentage.ToString() + "%";

                // 下载了多少 还剩余多少
                label1.Text = (Convert.ToDouble(e.BytesReceived) / 1024 / 1024).ToString("0.00") + " Mb" + "  /  " + (Convert.ToDouble(e.TotalBytesToReceive) / 1024 / 1024).ToString("0.00") + " Mb";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // The event that will trigger when the WebClient is completed
        private void Completed1(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if (e.Cancelled == true)
            {
                string time = DateTime.Now.ToLongTimeString().ToString();
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "文件下载失败，请检查网络后再试"; //下载未完成 //下载未完成
            }
            else
            {

            }
        }
        #endregion

        #region 下载文件更新文件
        public void DownloadFile(string urlAddress, string location)
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                try
                {
                    Uri URL;
                    // 先判断是否包括http://
                    if (!urlAddress.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                        URL = new Uri("http://" + urlAddress);
                    else
                        URL = new Uri(urlAddress);
                    sw.Start();
                    // 开始异步下载
                    webClient.DownloadFileAsync(URL, location);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
              

                // 进度条
                if (progressBar1.Value != e.ProgressPercentage)
                    progressBar1.Value = e.ProgressPercentage;

                // 当前比例
                if (label1.Text != e.ProgressPercentage.ToString() + "%")
                    label1.Text = e.ProgressPercentage.ToString() + "%";

                // 下载了多少 还剩余多少
                label1.Text = (Convert.ToDouble(e.BytesReceived) / 1024 / 1024).ToString("0.00") + " Mb" + "  /  " + (Convert.ToDouble(e.TotalBytesToReceive) / 1024 / 1024).ToString("0.00") + " Mb";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // The event that will trigger when the WebClient is completed
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if (e.Cancelled == true)
            {
                string time = DateTime.Now.ToLongTimeString().ToString();
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "文件下载失败，请检查网络后再试"; //下载未完成
            }
            else
            {
                string time = DateTime.Now.ToLongTimeString().ToString();
                string str2 = Environment.CurrentDirectory;
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "开始解压...";
                System.IO.Compression.ZipFile.ExtractToDirectory(@"temp\Debug.zip", "updata"); //解压
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "开始覆盖...";
                string 应用目录 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                string 应用目录updata = 应用目录+ @"updata\";

                string cmd = "/C xcopy /y /e /k " + 应用目录updata + "*.*  " + 应用目录;
                ExecCMD(cmd);
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "开始清除升级缓存文件";
                DelectDir("updata");
                DelectDir("temp");
                richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "升级完成";




            }
        }
        #endregion

        #region 清空文件
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region 检测权限
        /// <summary>
        /// 判断程序是否是以管理员身份运行。
        /// </summary>
        public bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public void RunAdmin()
        {
            try
            {
                //判断是否以管理员身份运行，不是则提示
                if (!IsAdministrator())
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.WorkingDirectory = Environment.CurrentDirectory;
                    psi.FileName = Application.ExecutablePath;
                    psi.UseShellExecute = true;
                    psi.Verb = "runas";
                    Process p = new Process();
                    p.StartInfo = psi;
                    p.Start();
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message+"请使用管理员权限启动");
                Application.Exit();
            }
        }

        #endregion

        #region 覆盖文件
        public static void ExecCMD(string command)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.UseShellExecute = false;   //是否使用操作系统shell启动 
            process.StartInfo.CreateNoWindow = false;   //是否在新窗口中启动该进程的值 (不显示程序窗口)
            process.Start();
            process.WaitForExit();  //等待程序执行完退出进程
            process.Close();
            


        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 初始化

            string time = DateTime.Now.ToLongTimeString().ToString();
            richTextBox1.Text = "[" + time + "]" + "正在初始化";
            RunAdmin();

            if (Directory.Exists("temp"))//检测是否有文件夹
            {
                DelectDir("temp");//有则清空
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("temp");//无则创建
                directoryInfo.Create();
            }


            if (Directory.Exists("updata"))//检测是否有文件夹
            {
                DelectDir("updata");//有则清空
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo("updata");//无则创建
                directoryInfo.Create();
            }
            richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "初始化完成";
            richTextBox1.Text = richTextBox1.Text + "\r\n" + "[" + time + "]" + "开始检查更新";
            DownloadFile2("http://updata.hong-fu.top:666/version.txt", @"temp\version.txt");
            DownloadFile1("http://updata.hong-fu.top:666/dis.txt", @"temp\dis.txt");
            ;

            #endregion

        }
    }
}
