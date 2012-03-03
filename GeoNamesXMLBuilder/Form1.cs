using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenjaminSchroeter.GeoNames;
using GeoNamesXMLBuilder.Model;
using System.IO;
using System.Threading;

namespace GeoNamesXMLBuilder
{
    public partial class Form1 : Form
    {
        public int CurrentLevel { get; set; }
        public int ItemCount { get; set; }

        public Form1()
        {
            InitializeComponent();
            cmbLevels.SelectedIndex = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            ItemCount = 0;
            CurrentLevel = cmbLevels.SelectedIndex;

            ThreadStart t = GetNodes;
            t.BeginInvoke(Finished, null);
        }
        public void Finished(IAsyncResult result)
        {
            BeginInvoke((MethodInvoker)
                        delegate
                            {
                                btnGo.Enabled = true;
                                btnGo.Text = "Get XML";
                                MessageBox.Show(@"File saved to c:\ouput.xml");
                            });
        }


        private void GetNodes()
        {
            int id;
            try
            {
                id = int.Parse(this.txtGenonameId.Text);
            }
            catch { return; }

            try
            {
                var g = GeoNamesOrgWebservice.Get(id);
                var sg = new SimpleGeoName(g);
                GetChildren(sg, g);

                var xml = XmlHelper.ToXml<SimpleGeoName>(sg);
                File.WriteAllText(@"c:\output.xml", xml);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateCounterUI()
        {
            this.BeginInvoke((MethodInvoker)delegate { btnGo.Text = string.Format("{0} items processed...", ItemCount); });
        }

        private void GetChildren(SimpleGeoName sg, Geoname g)
        {
            CurrentLevel--;

            try
            {
                foreach (var n in g.Children(GeoNamesDataStyle.Full))
                {
                    ItemCount++;
                    UpdateCounterUI();

                    var nsg = new SimpleGeoName(n);
                    sg.Children.Add(nsg);
                    if (CurrentLevel > 0)
                        GetChildren(nsg, n);
                }
            }
            catch{}
            CurrentLevel++;
        }
    }
}
