using System;
using System.Windows.Forms;
using System.Xml;

namespace Editor
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            XmlTextReader xmlReader = new XmlTextReader(@"Config\Shop.xml");
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        listBox1.Items.Add("<" + xmlReader.Name + ">");
                        break;
                    case XmlNodeType.Text:
                        listBox1.Items.Add(xmlReader.Value);
                        break;
                    case XmlNodeType.EndElement:
                        listBox1.Items.Add("");
                        break;
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}
