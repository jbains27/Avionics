// Decompiled with JetBrains decompiler
// Type: WindowsFormsApplication2.Program
// Assembly: WindowsFormsApplication2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F26F28AE-C020-4D78-9CDC-CDBBF01F0617
// Assembly location: C:\Users\Sumit\Desktop\Please change the extension to .exe from .exe1 (1)\WindowsFormsApplication2.exe

using System;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Main());
    }
  }
}
