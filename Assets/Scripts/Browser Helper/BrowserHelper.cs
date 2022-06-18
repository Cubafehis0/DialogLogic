using System;
using System.Runtime.InteropServices;
using UnityEngine;
public static class BrowserHelper
{

    #region Window

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    private static extern bool GetOpenFileName([In, Out] OpenFileName ofn);

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    private static extern bool GetSaveFileName([In, Out] OpenFileName ofn);

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    private static extern bool GetOpenFileName([In, Out] OpenFileDialog ofn);

    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    private static extern bool GetSaveFileName([In, Out] OpenFileDialog ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    private static extern IntPtr SHBrowseForFolder([In, Out] FolderBrowserDialog ofn);

    [DllImport("shell32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    private static extern bool SHGetPathFromIDList([In] IntPtr pidl, [In, Out] char[] fileName);

    #endregion
    public const string IMAGEFILTER = "ͼƬ�ļ�(*.jpg;*.png)\0*.jpg;*.png";
    public const string ALLFILTER = "�����ļ�(*.*)\0*.*";
    public const string EXCELFILTER = "Excel�ļ�(*.xls;*.xlsx)\0*.xls;*.xlsx";
    /// <summary>
    /// ����Ŀ
    /// </summary>
    public static string OpenProject(string title, string filter)
    {
        OpenFileDlg pth = new OpenFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = filter;
        pth.file = new string(new char[256]);
        pth.maxFile = 256;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = 64;
        pth.initialDir = UnityEngine.Application.dataPath.Replace("/", "\\") + "\\Resources"; //Ĭ��·��
        pth.title = title;
        pth.defExt = "dat";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (OpenFileDialog.GetOpenFileName(pth))
        {
            string filepath = pth.file; //ѡ����ļ�·��;  
            return filepath;
        }
        return null;
    }


    /// <summary>
    /// �����ļ���Ŀ
    /// </summary>
    public static string SaveProject(string filename, string title, string filter)
    {
        SaveFileDlg pth = new SaveFileDlg();
        pth.structSize = Marshal.SizeOf(pth);
        pth.filter = filter;
        pth.file = filename;
        pth.maxFile = 256;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = 64;
        pth.initialDir = UnityEngine.Application.dataPath; //Ĭ��·��
        pth.title = title;
        pth.defExt = "dat";
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (SaveFileDialog.GetSaveFileName(pth))
        {
            string filepath = pth.file; //ѡ����ļ�·��;  
            return filepath;
        }
        return null;
    }
    /// <summary>
    /// ѡ���ļ�����
    /// </summary>
    /// <returns></returns>
    public static string SaveProjects(string title)
    {
        try
        {
            FolderBrowserDialog ofn2 = new FolderBrowserDialog();
            ofn2.pszDisplayName = new string(new char[2048]);
            ; // ���Ŀ¼·��������  
            ofn2.lpszTitle = title; // ����  
            ofn2.ulFlags = 0x00000040; // �µ���ʽ,���༭��  
            IntPtr pidlPtr = SHBrowseForFolder(ofn2);

            char[] charArray = new char[2048];

            for (int i = 0; i < 2048; i++)
            {
                charArray[i] = '\0';
            }
            SHGetPathFromIDList(pidlPtr, charArray);
            string res = new string(charArray);
            res = res.Substring(0, res.IndexOf('\0'));
            return res;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return null;
    }

}