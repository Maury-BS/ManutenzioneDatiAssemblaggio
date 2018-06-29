﻿using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    static class FormsLayout
    {
        public static void LockTextBoxes(Control Container, bool Lock)
        {
            foreach (Control c in Container.Controls)
            {
                LockTextBox(c, Lock);
            }
        }

        public static void LockTextBox(Control Ctrl, bool Lock)
        {
            if (Ctrl.GetType().ToString() == typeof(TextBox).ToString())
            {
                ((TextBox)Ctrl).ReadOnly = Lock;
            }
        }

    }
}
