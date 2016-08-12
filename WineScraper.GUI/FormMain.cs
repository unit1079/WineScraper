using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

using Microsoft.Data.ConnectionUI;

using WineScraper.Data;
using WineScraper.Web;

namespace WineScraper.GUI
{
    public partial class FormMain : Form
    {
        private BackgroundWorker _bwLB;
        private Engine _lbEngine = new Engine();
        private bool _bRunLBScrape = true;
        private LB_Inventory _lbCurrentItem;

        private BackgroundWorker _bwWTSO;
        private Engine _wtsoEngine = new Engine();
        private bool _bRunWTSOScrape = true;
        private WTSO_Inventory _wtsoCurrentItem;

        private BackgroundWorker _bwWTSOHistorical;
        private Engine _wtsoHistoricalEngine = new Engine();
        private WTSO_Inventory _wtsoCurrentHistoricalItem;
        private bool _bHistoricalCrawlRunning = false;

        private Size _previousSize;
        private Size _previousLBSize;
        private Size _previousWTSOSize;

        private ToolTip _ttLB = new ToolTip();
        private ToolTip _ttWTSO = new ToolTip();
        private ToolTip _ttWTSOHistorical = new ToolTip();

        public FormMain()
        {
            InitializeComponent();
            WireUpLastBottleWorker();
            WireUpWTSOWorker();
        }        

        private void FormMain_Load(object sender, EventArgs e)
        {
            ToggleLastBottleWorker();
            ToggleWTSOWorker();
            _previousSize = this.Size;
            _previousLBSize = pbLastBottleItem.Size;
            _previousWTSOSize = pbWTSOItem.Size;
        }
        

        #region Last Bottle Scraper
        private void WireUpLastBottleWorker()
        {
            _bwLB = new BackgroundWorker();
            _bwLB.WorkerSupportsCancellation = true;
            _bwLB.WorkerReportsProgress = true;
            _bwLB.DoWork += new DoWorkEventHandler(_lbEngine.RunLastBottleLoop);
            _bwLB.ProgressChanged += new ProgressChangedEventHandler(this.LastBottleProgressUpdated);
        }
        
        protected void LastBottleProgressUpdated(object oSender, ProgressChangedEventArgs e)
        {            
            var oCurrentItem = e.UserState as WineScraper.Data.LB_Inventory;
            this._lbCurrentItem = oCurrentItem;
            lblLBTitle.Text = oCurrentItem.Title;
            lblLBPrice.Text = string.Format("${0}", oCurrentItem.Price);

            pbLastBottleItem.Image = Engine.GetResizedImageFromUrl(oCurrentItem.Picture_Url, pbLastBottleItem);
            
            var oShipRegex = new Regex("(Free ground shipping on [\\d]+ or more bottles)");  
            var oMatch = oShipRegex.Match(oCurrentItem.Description);
            if( oMatch.Success )
            {
                lblLBShipping.Text = oMatch.Groups[0].Value;
            }           
            else
            {
                lblLBShipping.Text = "--";
            }
            
            var oRegex = new Regex("(.{50}\\s)");
            var oWrappedMessage = oRegex.Replace(oCurrentItem.Description, "$1\n");
            oWrappedMessage = Regex.Replace(oWrappedMessage, "(<br[\\s\\n\\r]+/>)", Environment.NewLine);
            _ttLB.SetToolTip(pbLastBottleItem, oWrappedMessage);
        }
        
        private void ToggleLastBottleWorker()
        {
            if (_bwLB != null)
            {
                _bwLB.CancelAsync();
            }
            if (_bRunLBScrape)
            {
                WireUpLastBottleWorker();

                _lbEngine.LastBottleLoopInterval = Convert.ToInt32(nudLBInterval.Value) * 1000;
                _bwLB.RunWorkerAsync();
            }
        }

        #region Event Handlers
        private void btnToggleLBScrape_Click(object sender, EventArgs e)
        {
            _bRunLBScrape = !_bRunLBScrape;
            btnToggleLBScrape.Text = (_bRunLBScrape ? "Stop Scrape" : "Start Scrape");

            ToggleLastBottleWorker();
            //ToggleLastBottleThread();            
        }

        private void nudLBInterval_ValueChanged(object sender, EventArgs e)
        {
            ToggleLastBottleWorker();
            //ToggleLastBottleThread();
        }

        private void pbLastBottleItem_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["LB_URL"].ToString());
        }
        #endregion
        #endregion

        #region WTSO Scraper
        private void WireUpWTSOWorker()
        {
            _bwWTSO = new BackgroundWorker();
            _bwWTSO.WorkerSupportsCancellation = true;
            _bwWTSO.WorkerReportsProgress = true;
            _bwWTSO.DoWork += new DoWorkEventHandler(_wtsoEngine.RunWTSOLoop);
            _bwWTSO.ProgressChanged += new ProgressChangedEventHandler(this.WTSOProgressUpdated);
        }

        protected void WTSOProgressUpdated(object oSender, ProgressChangedEventArgs e)
        {
            var oCurrentItem = e.UserState as WineScraper.Data.WTSO_Inventory;
            if (oCurrentItem == null)
            {
                return;
            }
            this._wtsoCurrentItem = oCurrentItem;
            lblWTSOTitle.Text = oCurrentItem.Title;

            var oHistory = oCurrentItem.WTSO_Histories.OrderByDescending(h => h.OfferDate).FirstOrDefault();
            if (oHistory != null)
            {
                lblWTSOPrice.Text = string.Format("${0:0.00}", oHistory.Price);
                lblWTSOShipping.Text = oHistory.ShippingOffer;
            }
            else
            {
                lblWTSOPrice.Text = "--";
                lblWTSOShipping.Text = "--";
            }

            pbWTSOItem.Image = Engine.GetResizedImageFromUrl(oCurrentItem.Picture_Url, pbWTSOItem);
            if (oCurrentItem.WTSOMemberRating != null)
            {
                lblWTSORatingText.Visible = true;
                lblWTSOMemberRating.Visible = true;
                lblWTSOMemberRating.Text = oCurrentItem.WTSOMemberRating.ToString();
            }
            else
            {
                lblWTSORatingText.Visible = false;
                lblWTSOMemberRating.Visible = false;
            }


            _ttWTSO.SetToolTip(pbWTSOItem, BuildWTSOToolTip(oCurrentItem));
        }

        private string BuildWTSOToolTip(WineScraper.Data.WTSO_Inventory oItem)
        {
            var oBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(oItem.UnitSize))
            {
                oBuilder.Append(string.Format("Unit Size:\t{0}", oItem.UnitSize));
                oBuilder.Append(Environment.NewLine);
            }
            if (!string.IsNullOrEmpty(oItem.Varietal))
            {
                oBuilder.Append(string.Format("Varietal:\t\t{0}", oItem.Varietal));
                oBuilder.Append(Environment.NewLine);
            }
            if (oItem.Vintage != null)
            {
                oBuilder.Append(string.Format("Vintage:\t\t{0}", oItem.Vintage));
                oBuilder.Append(Environment.NewLine);
            }
            if (!string.IsNullOrEmpty(oItem.Grape))
            {
                oBuilder.Append(string.Format("Grape:\t\t{0}", oItem.Grape));
                oBuilder.Append(Environment.NewLine);
            }
            if (!string.IsNullOrEmpty(oItem.Country))
            {
                oBuilder.Append(string.Format("Country:\t\t{0}", oItem.Country));
                oBuilder.Append(Environment.NewLine);
            }
            if (!string.IsNullOrEmpty(oItem.Appellation))
            {
                oBuilder.Append(string.Format("Appellation:\t{0}", oItem.Appellation));
                oBuilder.Append(Environment.NewLine);
            }
            if (!string.IsNullOrEmpty(oItem.Region))
            {
                oBuilder.Append(string.Format("Region:\t\t{0}", oItem.Region));
                oBuilder.Append(Environment.NewLine);
            }
            if (!string.IsNullOrEmpty(oItem.ABV))
            {
                oBuilder.Append(string.Format("ABV:\t\t{0}", oItem.ABV));
                oBuilder.Append(Environment.NewLine);
            }

            if (oItem.WTSO_Ratings.HasLoadedOrAssignedValues && oItem.WTSO_Ratings.Any())
            {
                oBuilder.Append(Environment.NewLine);
                oBuilder.Append(Environment.NewLine);
                oBuilder.Append(Environment.NewLine);
                oBuilder.Append("RATINGS:");
                foreach (var oRating in oItem.WTSO_Ratings.AsQueryable() )
                {
                    oBuilder.Append(Environment.NewLine);
                    oBuilder.Append(string.Format("  {0}", oRating.Rating));
                }
            }

            return oBuilder.ToString();
        }

        private void ToggleWTSOWorker()
        {
            if (_bwWTSO != null)
            {
                _bwWTSO.CancelAsync();
            }
            if (_bRunWTSOScrape)
            {
                WireUpWTSOWorker();

                _wtsoEngine.WTSOLoopInterval = Convert.ToInt32(nudWTSOInterval.Value) * 1000;
                _bwWTSO.RunWorkerAsync();
            }
        }

        #region Event Handlers
        private void btnToggleWTSOScrape_Click(object sender, EventArgs e)
        {
            _bRunWTSOScrape = !_bRunWTSOScrape;
            btnToggleWTSOScrape.Text = (_bRunWTSOScrape ? "Stop Scrape" : "Start Scrape");

            ToggleWTSOWorker();
            //ToggleLastBottleThread();            
        }

        private void nudWTSOInterval_ValueChanged(object sender, EventArgs e)
        {
            ToggleWTSOWorker();
            //ToggleLastBottleThread();
        }

        private void pbWTSOItem_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["WTSO_URL"].ToString());
        }
        #endregion
        #endregion

        

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_threadLastBottle.Abort();
            _bwLB.CancelAsync();
            this.Dispose();
        }

        

        private void tcTabs_SizeChanged(object sender, EventArgs e)
        {

        }

        private void FormMain_Layout(object sender, LayoutEventArgs e)
        {
            if (_previousSize != null && _previousSize.Height > 0)
            {
                //int iHeightChange = (this.ClientSize.Height - _previousSize.Height);
                //int iWidthChange = (this.ClientSize.Width - _previousSize.Width);

                //gbLastBottle.Height += Convert.ToInt32(iHeightChange * 0.5);
                //gbWTSO.Top -= Convert.ToInt32(iHeightChange * 0.5);
                //gbWTSO.Height += Convert.ToInt32(iHeightChange * 0.5);
                //gbLastBottle.Top = 0;
                //gbLastBottle.ClientSize = new Size(gbLastBottle.ClientSize.Width, Convert.ToInt32(0.49 *  tpScraper.ClientSize.Height));
                //gbWTSO.Top = Convert.ToInt32(tpScraper.ClientSize.Height * 0.51);
                //gbWTSO.ClientSize = gbLastBottle.ClientSize;

                if (tpWTSOHistorical.ClientSize.Width > 0)
                {
                    lblWTSOHistoricalTitle.MaxWidth = Convert.ToInt32(tpWTSOHistorical.ClientSize.Width * 0.4);
                }
            }
            _previousSize = this.ClientSize;            
        }

        private void pbWTSOItem_SizeChanged(object sender, EventArgs e)
        {
            if (this._wtsoCurrentItem != null)
            {
                try
                {
                    pbWTSOItem.Image = Engine.GetResizedImageFromUrl(this._wtsoCurrentItem.Picture_Url, pbWTSOItem);
                }
                catch (Exception ex) { }
            }

            int iWidthChange = (pbWTSOItem.Size.Width - _previousWTSOSize.Width);

            //lblWTSOTitle.Left += iWidthChange;
            //lblWTSOPrice.Left += iWidthChange;
            //lblWTSOMemberRating.Left += iWidthChange;
            //lblWTSORatingText.Left += iWidthChange;
            //lblWTSOShipping.Left += iWidthChange;

            _previousWTSOSize = pbWTSOItem.Size;
        }

        private void pbLastBottleItem_SizeChanged(object sender, EventArgs e)
        {
            if (this._lbCurrentItem != null)
            {
                try
                {
                    pbLastBottleItem.Image = Engine.GetResizedImageFromUrl(this._lbCurrentItem.Picture_Url, pbLastBottleItem);
                }
                catch (Exception ex) { }
            }

            int iWidthChange = (pbLastBottleItem.Size.Width - _previousLBSize.Width);
            int iHeightChange = (pbLastBottleItem.Size.Height - _previousLBSize.Height);
            int iNewLeft = (pbLastBottleItem.Left + pbLastBottleItem.Width + 10);

            //lblLBPrice.Left = iNewLeft;
            //lblLBTitle.Left = iNewLeft;
            //lblLBShipping.Left = iNewLeft;

            _previousLBSize = pbLastBottleItem.Size;
        }

        #region WTSO Historical
        private void WireUpWTSOHistoricalWorker()
        {
            _bwWTSOHistorical = new BackgroundWorker();
            _bwWTSOHistorical.WorkerSupportsCancellation = true;
            _bwWTSOHistorical.WorkerReportsProgress = true;
            _bwWTSOHistorical.DoWork += new DoWorkEventHandler(WTSOHistorical_DoWorkEventHandler);
            _bwWTSOHistorical.ProgressChanged += new ProgressChangedEventHandler(this.WTSOHistoricalProgressUpdated);
            _bwWTSOHistorical.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.WTSOHistorical_Finished);
        }

        protected void WTSOHistorical_DoWorkEventHandler(object sender, DoWorkEventArgs e)
        {
            _bHistoricalCrawlRunning = true;
            //txtWTSOPassword.Enabled = false;
            //txtWTSOUser.Enabled = false;

            //if (_wtsoHistoricalEngine.LoginToWTSO(txtWTSOUser.Text, txtWTSOPassword.Text))
            {
                int iStart = Convert.ToInt32(nudWTSOHistoricalStart.Value);
                int iEnd = Convert.ToInt32(nudWTSOHistoricalEnd.Value);
                for (int i = iStart; i <= iEnd; i++)
                {
                    if (_bwWTSOHistorical.CancellationPending)
                    {                        
                        e.Result = new Tuple<int, int,bool>(iStart, i-1,true);
                        //lblWTSOHistoricalProcessItem.Text = string.Format("Historical Load was canceled. Processed products {0} to {1}", iStart, i - 1);
                        return;
                    }
                    //lblWTSOHistoricalProcessItem.Text = string.Format("Processing WTSO Product with ID {0}", i);
                    var oItem = _wtsoHistoricalEngine.UpsertWTSOItemByID(i, true);
                    //if (oItem != null)
                    {
                        _bwWTSOHistorical.ReportProgress(i, oItem);
                    }
                }
                e.Result = new Tuple<int, int,bool>(iStart, iEnd,false);
            }
            //else
            {
                //MessageBox.Show("Login to WTSO Failed. Cannot run historical load.");
                //lblWTSOHistoricalProcessItem.Text = "Login to WTSO Failed. Cannot run historical load.";
            }
        }

        protected void WTSOHistoricalProgressUpdated(object oSender, ProgressChangedEventArgs e)
        {
            lblWTSOHistoricalProcessItem.ForeColor = Color.Black;
            lblWTSOHistoricalProcessItem.Text = string.Format("Processing WTSO Product with ID {0}", e.ProgressPercentage);            
        
            var oCurrentItem = e.UserState as WineScraper.Data.WTSO_Inventory;
            if (oCurrentItem != null)
            {
                this._wtsoCurrentItem = oCurrentItem;
                lblWTSOHistoricalTitle.Text = oCurrentItem.Title;

                if (oCurrentItem.Image != null)
                {
                    pbWTSOHistorical.Image = Engine.ResizeImage(oCurrentItem.Image, pbWTSOHistorical, false);
                }
                //var oImage = Engine.GetResizedImageFromUrl(oCurrentItem.Picture_Url, pbWTSOItem);
                //if (oImage != null)
                //{
                //    pbWTSOHistorical.Image = oImage;
                //}

                _ttWTSOHistorical.SetToolTip(pbWTSOHistorical, BuildWTSOToolTip(oCurrentItem));
            }
        }

        protected void WTSOHistorical_Finished(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && e.Result != null)
            {
                var oResultData = (e.Result as Tuple<int, int, bool>);
                if (oResultData != null)
                {
                    if (oResultData.Item3)
                    {
                        lblWTSOHistoricalProcessItem.Text = string.Format("Historical Crawl Canceled by User. Processed Items {0} to {1}", oResultData.Item1, oResultData.Item2);
                        lblWTSOHistoricalProcessItem.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblWTSOHistoricalProcessItem.Text = string.Format("Historical Crawl Completed! Processed Items {0} to {1}", oResultData.Item1, oResultData.Item2);
                        lblWTSOHistoricalProcessItem.ForeColor = Color.Black;
                    }
                }
            }
            btnHistoricalCrawl.Text = "Start Historical Crawl";
            _bHistoricalCrawlRunning = false;
            txtWTSOUser.Enabled = true;
            txtWTSOPassword.Enabled = true;
            nudWTSOHistoricalStart.Enabled = true;
            nudWTSOHistoricalEnd.Enabled = true;
        }

        private void btnHistoricalCrawl_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWTSOPassword.Text) || string.IsNullOrEmpty(txtWTSOUser.Text))
            {
                MessageBox.Show("You must provide a valid user name and password to perform a historical WTSO crawl.");
                return;
            }

            if (_bHistoricalCrawlRunning)
            {
                _bwWTSOHistorical.CancelAsync();
            }
            else
            {
                if (_bwWTSOHistorical == null)
                {
                    WireUpWTSOHistoricalWorker();
                }


                if (_wtsoHistoricalEngine.LoginToWTSO(txtWTSOUser.Text, txtWTSOPassword.Text))
                {
                    txtWTSOPassword.Enabled = false;
                    txtWTSOUser.Enabled = false;
                    nudWTSOHistoricalEnd.Enabled = false;
                    nudWTSOHistoricalStart.Enabled = false;

                    btnHistoricalCrawl.Text = "Stop Historical Crawl";
                    _bwWTSOHistorical.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Login to WTSO Failed. Cannot run historical load.");
                }
            }
        }


        #endregion

        private void tpWTSOHistorical_SizeChanged(object sender, EventArgs e)
        {
            lblWTSOHistoricalTitle.MaxWidth = Convert.ToInt32(tpWTSOHistorical.ClientSize.Width * 0.4);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tpScraper_SizeChanged(object sender, EventArgs e)
        {
            gbLastBottle.Top = 0;
            gbLastBottle.ClientSize = new Size(gbLastBottle.ClientSize.Width, Convert.ToInt32(0.49 * tpScraper.ClientSize.Height));
            gbWTSO.Top = Convert.ToInt32(tpScraper.ClientSize.Height * 0.51);
            gbWTSO.ClientSize = gbLastBottle.ClientSize;
        }

        private void databaseWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strConnectString;
            var bConnection = TryGetDataConnectionStringFromUser(out strConnectString);
        }

        [STAThread]
        private bool TryGetDataConnectionStringFromUser(out string outConnectionString)
        {
            using (var dialog = new DataConnectionDialog())
            {
                // If you want the user to select from any of the available data sources, do this:
                DataSource.AddStandardDataSources(dialog);

                // OR, if you want only certain data sources to be available
                // (e.g. only SQL Server), do something like this instead: 
                //dialog.DataSources.Add(DataSource.SqlDataSource);
                //dialog.DataSources.Add(DataSource.SqlFileDataSource);               

                // The way how you show the dialog is somewhat unorthodox; `dialog.ShowDialog()`
                // would throw a `NotSupportedException`. Do it this way instead:
                DialogResult userChoice = DataConnectionDialog.Show(dialog);

                // Return the resulting connection string if a connection was selected:
                if (userChoice == DialogResult.OK)
                { 
                    outConnectionString = dialog.ConnectionString;
                    return true;
                }
                else
                {
                    outConnectionString = null;
                    return false;
                }
            }
        }



    }
}

        



