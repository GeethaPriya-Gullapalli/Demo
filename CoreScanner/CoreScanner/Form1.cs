using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreScanner;
namespace CoreScanner
{
    public partial class Form1 : Form
    {
        CCoreScannerClass cCoreScannerClass;
        public Form1()
        {
            InitializeComponent();
        }
        void OnBarcodeEvent(short eventType, ref string scanData)
        {
            textBox1.Text = scanData;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cCoreScannerClass = new CCoreScannerClass();
                //Call Open API
                short[] scannerTypes = new short[1];//Scanner Types you are interested in
                scannerTypes[0] = 1; // 1 for all scanner types
                short numberOfScannerTypes = 1; // Size of the scannerTypes array
                int status; // Extended API return code
                cCoreScannerClass.Open(0, scannerTypes, numberOfScannerTypes, out status);
                // Subscribe for barcode events in cCoreScannerClass
                cCoreScannerClass.BarcodeEvent += new
                _ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
                // Let's subscribe for events
                int opcode = 6005; // Method for Subscribe events
                string outXML; // XML Output
                string inXML = "<inArgs>" +
                    "<scannerID>1</scannerID>"+
                "<cmdArgs>" +
                "<arg-string>XUA-45001-11</arg-string>"+
                "<arg-int>1</arg-int>" + // Number of events you want to subscribe
                "<arg-int>0</arg-int>" + // Comma separated event IDs
                "</cmdArgs>" +
                "</inArgs>";
                cCoreScannerClass.ExecCommand(opcode, ref inXML, out outXML, out status);
               
            }
            catch (Exception exp)
            {
                Console.WriteLine("Something wrong please check... " + exp.Message);
            }

        }

    }
}