using System;
using System.Diagnostics;

class Script
{
    static public void Main(string[] args)
    {
        string RepoDir = "d:\\Repositories\\";
        string BackupDir = "d:\\Backup\\";

        string[] proj = new string[]{
            "PcSite",
            "H5Site"
            };

        string d = DateTime.Now.ToString("yyyyMMdd_HHmm");
        RunCom("mkdir " + BackupDir + d);

        foreach (string r in proj)
        {
            Console.WriteLine("备份" + r + "中...");
            RunCom("svnadmin dump " + RepoDir + r + " > " + BackupDir + d + "\\" + r + ".dump");
        }

        RunCom("7z a -t7z " + BackupDir + d + ".7z " + BackupDir + d);
        RunCom("rmdir /S /Q " + BackupDir + d);
    }

    static void RunCom(string scriptFileCmd)
    {
        Process proc = new Process();
        proc.StartInfo.FileName = "cmd.exe";
        proc.StartInfo.Arguments = "/C" + scriptFileCmd;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.CreateNoWindow = false;
        proc.Start();

        string line = null;

        while (null != (line = proc.StandardOutput.ReadLine()))
        {
            Console.WriteLine(line);
        }
        proc.WaitForExit();
    }
}
