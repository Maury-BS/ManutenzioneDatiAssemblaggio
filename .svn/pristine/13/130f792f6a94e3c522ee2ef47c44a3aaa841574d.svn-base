using System;
using System.Drawing;
using System.Windows.Forms;

namespace Metra.ManutenzioneDatiAssemblaggio
{
    public partial class ucMenu : UserControl
    {
        int _btnCount = 0;
        int _newBtnTop = 0;

        //public event EventHandler Expand;

        //public event EventHandler Collapse;

        public new event EventHandler Click;

        public ucMenu()
        {
            InitializeComponent();

            //lblTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Height = lblTitle.Top + lblTitle.Height + 2;

            Refresh();
        }

        public override void Refresh()
        {
            lblTitle.BackColor = Skins.BackColor(Skins.ColorSet.Menu);
            lblTitle.ForeColor = Skins.ForeColor(Skins.ColorSet.Menu);
        }

        public int Items
        {
            get { return _btnCount; }
        }

        public void AddSpace(int Height)
        {
            this.Height += Height;
        }
        
        public void AddItem(string Text, string Tag, int Height)
        {
            if (_btnCount == 0) _newBtnTop = lblTitle.Height + 2;
            else _newBtnTop = this.Height;

            Button btn = new Button();

            btn.Text = Text;
            btn.Tag = Tag;
            btn.Height = Height;
            btn.Left = 2;
            btn.Font = new Font(lblTitle.Font, FontStyle.Regular);
           
            btn.Parent = this;
            btn.TabStop = false;
            btn.Top = _newBtnTop;// lblTitle.Height + 2 + (btn.Height) * (_btnCount);
            btn.Width = this.Width - 2 * btn.Left;
            btn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btn.BackColor = System.Drawing.SystemColors.Control;
            btn.UseVisualStyleBackColor = true;
            btn.Visible = true;
            btn.Click += new EventHandler(btn_Click);

            _btnCount += 1;
            this.Height = btn.Top + btn.Height + 2;
        }

        void btn_Click(object sender, EventArgs e)
        {
            Click(sender, e);
        }

        public string TitleText
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        public Color TitleBackColor
        {
            get
            { return lblTitle.BackColor; }
            set
            { lblTitle.BackColor = value; }
        }

        public Color TitleForeColor
        {
            get
            { return lblTitle.ForeColor; }
            set
            { lblTitle.ForeColor = value; }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
