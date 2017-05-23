using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using System.ComponentModel;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

using HtmlAgilityPack;

using WineScraper.Data;

namespace WineScraper.Web
{
    //public class LastBottlePercentEventArgs : ProgressChangedEventArgs
    //{
    //    //public LastBottlePercentEventArgs() : base(0, null) { }

    //    //public LastBottlePercentEventArgs(int iPercent, object oUserState)
    //    //    : base(iPercent, oUserState)
    //    //{
    //    //}

    //    public LB_Inventory CurrentItem
    //    {
    //        get;
    //        set;
    //    }
    //}

    public class LastBottleEventArgs : EventArgs
    {
        public LB_Inventory CurrentItem
        {
            get;
            set;
        }
    }
    public delegate void LastBottleEventHandler( object sender, LastBottleEventArgs args);


    public class Engine
    {
        public event LastBottleEventHandler LastBottleItemChanged; 

        public volatile bool _stopLBLoop = false;
        public volatile bool _stopWTSOLoop = false;

        private WebClient _client = new WebClient();
        private CookieAwareWebClient _cookieClient;

        public Engine()
        {
        }

        public static Image GetOriginalSizedImageFromUrl(string strUrl)
        {
            if (string.IsNullOrEmpty(strUrl))
            {
                return null;
            }
            var request = WebRequest.Create(strUrl);

            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var oImg = Bitmap.FromStream(stream);
                        return oImg;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Image GetResizedImageFromUrl(string strUrl, System.Windows.Forms.PictureBox canvas)
        {
            var oImg = GetOriginalSizedImageFromUrl(strUrl);
            if( oImg != null )
            {
                return ResizeImage(oImg, canvas, false);
            }
            return null;
        }

        /// <summary>
        /// Takes in an image, scales it maintaining the proper aspect ratio of the image such it fits in the PictureBox's canvas size and loads the image into picture box.
        /// Has an optional param to center the image in the picture box if it's smaller then canvas size.
        /// </summary>
        /// <param name="image">The Image you want to load, see LoadPicture</param>
        /// <param name="canvas">The canvas you want the picture to load into</param>
        /// <param name="centerImage"></param>
        /// <returns></returns>

        public static Image ResizeImage(Image image, System.Windows.Forms.PictureBox canvas, bool centerImage)
        {
            if (image == null || canvas == null)
            {
                return null;
            }

            int canvasWidth = canvas.Size.Width;
            int canvasHeight = canvas.Size.Height;
            int originalWidth = image.Size.Width;
            int originalHeight = image.Size.Height;

            if (canvasHeight == 0 || canvasWidth == 0)
            {
                return image;
            }

            System.Drawing.Image thumbnail =
                new Bitmap(canvasWidth, canvasHeight); // changed parm names
            System.Drawing.Graphics graphic =
                         System.Drawing.Graphics.FromImage(thumbnail);

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            /* ------------------ new code --------------- */

            // Figure out the ratio
            double ratioX = (double)canvasWidth / (double)originalWidth;
            double ratioY = (double)canvasHeight / (double)originalHeight;
            double ratio = ratioX < ratioY ? ratioX : ratioY; // use whichever multiplier is smaller

            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);

            // Now calculate the X,Y position of the upper-left corner 
            // (one of these will always be zero)
            int posX = Convert.ToInt32((canvasWidth - (image.Width * ratio)) / 2);
            int posY = Convert.ToInt32((canvasHeight - (image.Height * ratio)) / 2);

            if (!centerImage)
            {
                posX = 0;
                posY = 0;
            }
            graphic.Clear(Color.White); // white padding
            graphic.DrawImage(image, posX, posY, newWidth, newHeight);

            /* ------------- end new code ---------------- */

            System.Drawing.Imaging.ImageCodecInfo[] info =
                             ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters;
            encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality,
                             100L);

            var s = new System.IO.MemoryStream();
            thumbnail.Save(s, info[1],
                              encoderParameters);

            return Image.FromStream(s);
        }

        public HtmlDocument GetHtmlDocumentFromUrl(string strUrl)
        {
            var oDocument = new HtmlDocument();
            oDocument.LoadHtml(_client.DownloadString(strUrl));

            return oDocument;
        }

        public string GetValueFromXpath(HtmlDocument oDoc, string strXpathSetting)
        {
            var oNavigator = oDoc.CreateNavigator();
            return  GetValueFromNavigator(oNavigator, strXpathSetting);
        }

        public string ExtractHtmlBetweenStrings(HtmlDocument oDoc, string strStartString, string strEndString)
        {
            string strDocBody = oDoc.DocumentNode.InnerHtml;
            int iStartIndex = strDocBody.IndexOf(strStartString);
            if (iStartIndex > 0)
            {
                int iEndIndex = strDocBody.IndexOf(strEndString);
                if (iEndIndex < 0)
                {
                    iEndIndex = strDocBody.Length;
                }
                if (iEndIndex < iStartIndex)
                {
                    return null;
                }
                return strDocBody.Substring(iStartIndex + strStartString.Length, iEndIndex - iStartIndex - strEndString.Length - 2).Trim();
            }
            return null;
        }

        public XPathNavigator GetNavigatorFromXpath(HtmlDocument oDoc, string strXpathSetting)
        {
            try
            {
                var oNavigator = oDoc.CreateNavigator();
                return oNavigator.SelectSingleNode(ConfigurationManager.AppSettings[strXpathSetting].ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string[] GetCollectionFromXpathNavigator(XPathNavigator oNavigator, string strXpathSetting)
        {
            var aList = new List<string>();
            var result = oNavigator.Evaluate(ConfigurationManager.AppSettings[strXpathSetting].ToString());
            if (result is XPathNodeIterator)
            {
                var iterator = result as XPathNodeIterator;
                while (iterator.MoveNext())
                {
                    if (!aList.Contains(iterator.Current.Value.ToString()))
                    {
                        aList.Add(iterator.Current.Value.ToString());
                    }
                }
            }
            return aList.ToArray();
        }

        private string BuildHtmlFromXpathCollection(XPathNavigator oNavigator, string strXpathSetting)
        {
            var oBuilder = new StringBuilder();
            var result = oNavigator.Evaluate(ConfigurationManager.AppSettings[strXpathSetting].ToString());
            if (result is XPathNodeIterator)
            {
                var iterator = result as XPathNodeIterator;
                while (iterator.MoveNext())
                {
                    oBuilder.Append(iterator.Current.OuterXml);
                    oBuilder.Append(iterator.Current.InnerXml);
                }
            }
            return oBuilder.ToString();
        }

        private string GetValueFromNavigator(XPathNavigator oNavigator, string strXpathSetting, Encoding oEncoding = null)
        {
            var result = oNavigator.Evaluate(ConfigurationManager.AppSettings[strXpathSetting].ToString());
            if (result is string)
            {
                return result.ToString().Trim();
            }
            if (result is XPathNodeIterator)
            {
                var iterator = result as XPathNodeIterator;
                if (iterator.MoveNext())
                {
                    var strValue = (strXpathSetting.EndsWith("InnerXML") ? iterator.Current.InnerXml.Trim() : iterator.Current.Value.ToString().Trim());

                    bool bHasMatch = false;
                    // do regex parsing for weird encoding 
                    while( Regex.Matches(strValue, @"&#\d+(;|.+)").Count > 0 )
                    {
                        Match oMatch = Regex.Matches(strValue, @"&#\d+(;|.+)")[0];
                    
                        bHasMatch = true;
                        var iIndex = oMatch.Index;
                        var oVal = oMatch.Value;

                        var strReplacement = Convert.ToChar(int.Parse(oMatch.Value.Replace("&#", string.Empty).Replace(";", string.Empty)));
                        strValue = strValue.Substring(0, oMatch.Index) + strReplacement.ToString() + strValue.Substring(oMatch.Index + oMatch.Value.Length);
                    }
                    if (bHasMatch)
                    {
                        return strValue;
                    }
                    var aBytes = Encoding.Default.GetBytes(strValue);

                    //var aUnicodeBytes = Encoding.Unicode.GetBytes(strValue);
                    //var aUTF8Bytes = Encoding.UTF8.GetBytes(strValue);

                    //var aConvertUnicodeToDefault = Encoding.Convert(Encoding.Unicode, Encoding.Default, aUnicodeBytes);
                    //var strUnicodeToDefault = Encoding.Default.GetString(aConvertUnicodeToDefault);

                    //var strUTF8 = Encoding.UTF8.GetString(aUTF8Bytes);
                    //var aUTF8Conv = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, aUTF8Bytes);
                    //var strUTF8ToUnicode = Encoding.Unicode.GetString(aUTF8Conv);                    
                    
                    if (oEncoding == null)
                    {
                        return Encoding.UTF8.GetString(aBytes);
                    }
                    return oEncoding.GetString(aBytes);
                    //return (strXpathSetting.EndsWith("InnerXML") ? iterator.Current.InnerXml.Trim() : iterator.Current.Value.ToString().Trim());
                }
                //return null;
            }
            return null;
        }

       

        #region Last Bottle
        public int LastBottleLoopInterval
        {
            get;
            set;
        }

        public void RunLastBottleLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            LB_Inventory oLastItem = null;
            while (!_stopLBLoop)
            {
                Console.WriteLine("Running last bottle loop");
                try
                {
                    var oItem = UpsertLastBottleItem();
                    if (oLastItem == null || oLastItem.lbid != oItem.lbid)
                    {
                        //if (LastBottleItemChanged != null)
                        //{
                        //    LastBottleItemChanged(this, new LastBottleEventArgs() { CurrentItem = oItem });
                        //}
                        worker.ReportProgress(0, oItem);
                        oLastItem = oItem;
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                { }
                System.Threading.Thread.Sleep(this.LastBottleLoopInterval);
            }
        }

        public void RunLastBottleLoop()
        {
            //int iLoopTime = 3000;
            //int.TryParse(ConfigurationManager.AppSettings["LB_Refresh_Interval"].ToString(), out iLoopTime);
            LB_Inventory oLastItem = null;
            while (!_stopLBLoop)
            {
                Console.WriteLine("Running last bottle loop");
                try
                {
                    var oItem = UpsertLastBottleItem();
                    if (oLastItem == null || oLastItem.lbid != oItem.lbid)
                    {
                        if (LastBottleItemChanged != null)
                        {
                            LastBottleItemChanged(this, new LastBottleEventArgs() { CurrentItem = oItem });
                        }
                        oLastItem = oItem;
                    }
                }
                catch (Exception ex)
                { }
                System.Threading.Thread.Sleep(this.LastBottleLoopInterval);
            }
        }

        private LB_Inventory UpsertLastBottleItem()
        {
            var oDoc = GetHtmlDocumentFromUrl(ConfigurationManager.AppSettings["LB_URL"].ToString());

            if (oDoc != null)
            {
                var prodID = GetValueFromXpath(oDoc, "LB_XP_ProductID");
                if (prodID != null) // TODO - do something here, this would break everything
                {
                    using (var oContext = WineDataContext.GetNewContextFromConfigFile())
                    {
                        var oLBItem = oContext.LB_Inventories.SingleOrDefault(x => x.lbid == prodID);
                        if (oLBItem == null)
                        {
                            oLBItem = new LB_Inventory() { lbid = prodID, CreatedDate = DateTime.Now };
                            oContext.LB_Inventories.InsertOnSubmit(oLBItem);
                        }

                        var strTitle = GetValueFromXpath(oDoc, "LB_XP_Title");
                        var strPrice = GetValueFromXpath(oDoc, "LB_XP_Price");
                        var strPictureUrl = GetValueFromXpath(oDoc, "LB_XP_PictureUrl");

                        var strDescription = GetValueFromXpath(oDoc, "LB_XP_Description_InnerXML");

                        // update the title, price and picture URL
                        oLBItem.Title = strTitle;
                        int iPrice = -1;
                        if (int.TryParse(strPrice, out iPrice))
                        {
                            oLBItem.Price = iPrice;
                        }
                        oLBItem.Picture_Url = (strPictureUrl.Contains("//") ? strPictureUrl :
                            string.Format("{0}{1}", ConfigurationManager.AppSettings["LB_URL"].ToString(), strPictureUrl.Replace("https:","")));
                        if (string.IsNullOrEmpty(oLBItem.Description) || strDescription.Length > 0 )
                        {
                            oLBItem.Description = strDescription;
                        }
                        oLBItem.Date_last_seen = DateTime.Now;

                        oContext.SubmitChanges();
                        return oLBItem;
                    }
                }

            }
            return null;
        }

        #endregion

        #region WTSO
        public int WTSOLoopInterval
        {
            get;
            set;
        }

        public void RunWTSOLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            WTSO_Inventory oLastItem = null;
            while (!_stopWTSOLoop)
            {
                Console.WriteLine("Running WTSO loop");
                try
                {
                    var oItem = UpsertWTSOItem();
                    if (oLastItem == null || oLastItem.ProductID != oItem.ProductID)
                    {
                        worker.ReportProgress(0, oItem);
                        oLastItem = oItem;
                    }
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                { }
                System.Threading.Thread.Sleep(this.WTSOLoopInterval);
            }
        }

        public void RunWTSOLoop()
        {
            
            int iLoopTime = 3000;
            int.TryParse(ConfigurationManager.AppSettings["WTSO_Refresh_Interval"].ToString(), out iLoopTime);
            while (!_stopWTSOLoop)
            {
                Console.WriteLine("Running WTSO loop");
                UpsertWTSOItem();
                System.Threading.Thread.Sleep(iLoopTime);
            }
        }

        private void PopulateWTSOItemFromTable(WTSO_Inventory oWTSOItem, XPathNavigator oTable)
        {
            var strUnitSize = GetValueFromNavigator(oTable, "WTSO_XP_UnitSize");
            var strVarietal = GetValueFromNavigator(oTable, "WTSO_XP_Varietal");
            var strVintage = GetValueFromNavigator(oTable, "WTSO_XP_Vintage");
            var strGrape = GetValueFromNavigator(oTable, "WTSO_XP_Grape");
            var strCountry = GetValueFromNavigator(oTable, "WTSO_XP_Country");
            var strAppellation = GetValueFromNavigator(oTable, "WTSO_XP_Appellation");
            var strRegion = GetValueFromNavigator(oTable, "WTSO_XP_Region");
            var strABV = GetValueFromNavigator(oTable, "WTSO_XP_ABV");

            int iVintage = 0;
            if (int.TryParse(strVintage, out iVintage))
            {
                oWTSOItem.Vintage = iVintage;
            }

            oWTSOItem.UnitSize = strUnitSize;
            oWTSOItem.Varietal = strVarietal;
            oWTSOItem.Grape = strGrape;
            oWTSOItem.Country = strCountry;
            oWTSOItem.Appellation = strAppellation;
            oWTSOItem.Region = strRegion;
            oWTSOItem.ABV = strABV;
        }

        private WTSO_Inventory UpsertWTSOItem()
        {
            var oDoc = GetHtmlDocumentFromUrl(ConfigurationManager.AppSettings["WTSO_URL"].ToString());

            if (oDoc != null)
            {
                int iProdID = 0;
                if (int.TryParse(GetValueFromXpath(oDoc, "WTSO_XP_ProductID"), out iProdID))
                {
                    using (var oContext = WineDataContext.GetNewContextFromConfigFile())
                    {
                        var oWTSOItem = oContext.WTSO_Inventories.SingleOrDefault(x => x.ProductID == iProdID );
                        if (oWTSOItem == null)
                        {
                            oWTSOItem = new WTSO_Inventory() { ProductID = iProdID, CreatedDate = DateTime.Now };
                            oContext.WTSO_Inventories.InsertOnSubmit(oWTSOItem);
                        }

                        var strTitle = GetValueFromXpath(oDoc, "WTSO_XP_Title");
                        var strPictureUrl = GetValueFromXpath(oDoc, "WTSO_XP_PictureUrl");

                        var oTable = GetNavigatorFromXpath(oDoc, "WTSO_XP_Details_Table_Node");
                        PopulateWTSOItemFromTable(oWTSOItem, oTable);

                        //var strFullDescription = BuildHtmlFromXpathCollection(oDoc.CreateNavigator(), "WTSO_XP_ProductDescription_Collection");'
                        //var strFullDescription = ExtractHtmlBetweenStrings(oDoc,
                        //    ConfigurationManager.AppSettings["WTSO_Product_Description_Start_Comment"].ToString(),
                        //    ConfigurationManager.AppSettings["WTSO_Product_Description_End_Comment"].ToString());

                        var strFullDescription = string.Empty;
                        var oRegEx = new Regex(ConfigurationManager.AppSettings["WTSO_ProductDescription_Regex"].ToString());
                        var oMatch = oRegEx.Match(oDoc.DocumentNode.InnerHtml);
                        if (oMatch.Success)
                        {
                            strFullDescription = oMatch.Value.Trim();
                        }                        

                        int iMemberRating = -1;
                        if (int.TryParse(GetValueFromXpath(oDoc, "WTSO_XP_WTSO_Member_Rating"), out iMemberRating))
                        {
                            oWTSOItem.WTSOMemberRating = iMemberRating;
                        }
                        else
                        {
                            //var oRatingRegex = new Regex(ConfigurationManager.AppSettings["WTSO_WTSO_Member_Rating_Regex"].ToString());
                            var oRatingRegex = new Regex(ConfigurationManager.AppSettings["WTSO_WTSO_Member_Rating_Text_Regex"].ToString());
                            var oRatingMatch = oRatingRegex.Match(oDoc.DocumentNode.InnerText);
                            if (oRatingMatch.Success)
                            {
                                oWTSOItem.WTSOMemberRating = int.Parse(oRatingMatch.Groups[0].Value);
                            }
                        }

                        oWTSOItem.Title = strTitle.Trim();
                        oWTSOItem.Picture_Url = strPictureUrl;
                        oWTSOItem.FullDescription = strFullDescription;

                        // Add ratings data
                        var aRatings = GetCollectionFromXpathNavigator(oTable, "WTSO_XP_Ratings_Collection");
                        foreach (var strRating in aRatings)
                        {
                            if (!oWTSOItem.WTSO_Ratings.Any(r => r.Rating == strRating))
                            {
                                var oRating = new WTSO_Rating() { Rating = strRating, WTSO_Inventory = oWTSOItem };                                
                            }
                        }

                        // Add pricing history data
                        var strPrice = GetValueFromXpath(oDoc, "WTSO_XP_Price");
                        var strOffer = GetValueFromXpath(oDoc, "WTSO_XP_ShippingOffer");

                        decimal dPrice = -1;
                        if( !decimal.TryParse( strPrice.Replace("$",""), out dPrice) )
                        {
                            dPrice = -1;
                        }
                        var oHistoryItem = oWTSOItem.WTSO_Histories.OrderByDescending( h => h.OfferDate ).SingleOrDefault();
                        if( oHistoryItem == null || (oHistoryItem.OfferDate - DateTime.Now).TotalDays > 1  )
                        {
                            oHistoryItem = new WTSO_History();
                            oHistoryItem.WTSO_Inventory = oWTSOItem;                            
                        }
                        oHistoryItem.Price = dPrice;
                        oHistoryItem.ShippingOffer = strOffer;
                        oHistoryItem.OfferDate = DateTime.Now;
                                                                        

                        oContext.SubmitChanges();
                        return oWTSOItem;
                    }
                }

            }
            return null;
        }

        public bool LoginToWTSO(string strUserName, string strPassword)
        {
            var loginData = new NameValueCollection
                {
                    { "username", strUserName },
                    { "password", strPassword },
                    { "stayloggedin", "true" }
                       
                };

            _cookieClient = new CookieAwareWebClient();
            _cookieClient.Login(ConfigurationManager.AppSettings["WTSO_Login_URL"].ToString(), loginData);

            var strPage = _cookieClient.DownloadString(ConfigurationManager.AppSettings["WTSO_URL"].ToString());
            var oDoc = new HtmlAgilityPack.HtmlDocument();

            oDoc.LoadHtml(strPage);
            var oCheckElement = GetNavigatorFromXpath(oDoc, "WTSO_XP_AccountContent");
            if (oCheckElement == null)
            {
                return false;
            }
            return true;
        }

        //protected bool ShowLoginDialog(WTSOLoginForm oForm)
        //{
        //    var oResult = oForm.ShowDialog();

        //    if (oResult == System.Windows.Forms.DialogResult.OK)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public void RunWTSOHistorical()
        //{
        //    //using( var oDlg = new WTSOLoginForm() )
        //    {
        //        //if (ShowLoginDialog(oDlg))
        //        {
        //            var loginUrl = "https://www.wtso.com/login";
        //            var loginData = new NameValueCollection
        //            {
        //                //{ "username", oDlg.UserName },
        //                //{ "password", oDlg.Password },
        //                //{ "stayloggedin", "true" }

        //                { "username", "bradleymichaelharris@gmail.com" },
        //                { "password", "79brad10"},
        //                { "stayloggedin", "true" }
        //            };

        //            var oClient = new CookieAwareWebClient();
        //            oClient.Login(loginUrl, loginData);

        //            var strPage = oClient.DownloadString(ConfigurationManager.AppSettings["WTSO_URL"].ToString());
        //            var oDoc = new HtmlDocument();

        //            oDoc.LoadHtml(strPage);
        //            var oCheckElement = GetNavigatorFromXpath(oDoc, "WTSO_XP_AccountContent");
        //            if (oCheckElement == null)
        //            {
        //                Console.WriteLine("Login to WTSO failed, no historical load running.");
        //                return;
        //            }

        //            var oRegex = new Regex("({[\\d]+,[\\d]+})");
        //            var oMatch = oRegex.Match(ConfigurationManager.AppSettings["WTSO_Historical_Ranges"].ToString());
        //            //if (oMatch.Success)
        //            {

        //                //foreach (var oRange in oMatch.Groups)
        //                foreach( var oRange in ConfigurationManager.AppSettings["WTSO_Historical_Ranges"].ToString().Split(';') )
        //                {
        //                    var strRange = oRange.ToString().Replace("{", "").Replace("}", "");
        //                    int iStart = int.Parse(strRange.Split(',')[0]);
        //                    int iEnd = int.Parse(strRange.Split(',')[1]);
        //                    for (int i = iStart; i <= iEnd; i++)
        //                    {
        //                        UpsertWTSOItemByID(oClient, i);
        //                    }
        //                }
        //            }
        //            //else
        //            //{
        //            //    Console.WriteLine("No Valid Ranges found");
        //            //}

        //        }
        //    }
        //}
        public WTSO_Inventory UpsertWTSOItemByID(int iProductID, bool bFetchImage)
        {
            return UpsertWTSOItemByID(_cookieClient, iProductID, bFetchImage);
        }

        private WTSO_Inventory UpsertWTSOItemByID(CookieAwareWebClient oClient, int iProductID, bool bFetchImage)
        {
            var strUrl = string.Format(ConfigurationManager.AppSettings["WTSO_CellarDetailBase_URL"].ToString(), iProductID);
            Console.WriteLine("Processing page: " + strUrl);
            var strPage = oClient.DownloadString(strUrl);
            var oDoc = new HtmlDocument();

            oDoc.LoadHtml(strPage); 

            var strTitle = GetValueFromXpath(oDoc, "WTSO_XP_CD_Title");
            if( strTitle != null && strTitle.Trim().Length > 0 )
            {
                using (var oContext = WineDataContext.GetNewContextFromConfigFile())
                {
                    var oWTSOItem = oContext.WTSO_Inventories.SingleOrDefault(x => x.ProductID == iProductID );
                    if (oWTSOItem == null)
                    {
                        oWTSOItem = new WTSO_Inventory() { ProductID = iProductID, Title = strTitle, CreatedDate = DateTime.Now };
                        oContext.WTSO_Inventories.InsertOnSubmit(oWTSOItem);
                    }

                    var oTable = GetNavigatorFromXpath(oDoc, "WTSO_XP_CD_Details_Table_Node");
                    var strPictureUrl = GetValueFromXpath(oDoc, "WTSO_XP_PictureUrl");

                    oWTSOItem.Picture_Url = strPictureUrl;
                    if (bFetchImage)
                    {
                        oWTSOItem.Image = Engine.GetOriginalSizedImageFromUrl(strPictureUrl);
                    }

                    PopulateWTSOItemFromTable(oWTSOItem, oTable);

                    if (oWTSOItem.FullDescription == null || oWTSOItem.FullDescription.Length == 0)
                    {
                        var strFullDescription = string.Empty;
                        var oRegEx = new Regex(ConfigurationManager.AppSettings["WTSO_XP_CD_ProductDescription_Regex"].ToString());
                        var oMatch = oRegEx.Match(oDoc.DocumentNode.InnerHtml);
                        if (oMatch.Success)
                        {
                            strFullDescription = oMatch.Value.Trim();
                        }
                        oWTSOItem.FullDescription = strFullDescription;
                    }

                    //int iMemberRating = -1;
                    //if (int.TryParse(GetValueFromXpath(oDoc, "WTSO_XP_WTSO_Member_Rating"), out iMemberRating))
                    //{
                    //    oWTSOItem.WTSOMemberRating = iMemberRating;
                    //}

                    //oWTSOItem.Title = strTitle.Trim();                        
                    //oWTSOItem.FullDescription = strFullDescription;

                    //// Add ratings data
                    //var aRatings = GetCollectionFromXpathNavigator(oTable, "WTSO_XP_Ratings_Collection");
                    //foreach (var strRating in aRatings)
                    //{
                    //    if (!oWTSOItem.WTSO_Ratings.Any(r => r.Rating == strRating))
                    //    {
                    //        var oRating = new WTSO_Rating() { Rating = strRating, WTSO_Inventory = oWTSOItem };                                
                    //    }
                    //}

                    //// Add pricing history data
                    //var strPrice = GetValueFromXpath(oDoc, "WTSO_XP_Price");
                    //var strOffer = GetValueFromXpath(oDoc, "WTSO_XP_ShippingOffer");

                    //decimal dPrice = -1;
                    //if( !decimal.TryParse( strPrice.Replace("$",""), out dPrice) )
                    //{
                    //    dPrice = -1;
                    //}
                    //var oHistoryItem = oWTSOItem.WTSO_Histories.OrderByDescending( h => h.OfferDate ).SingleOrDefault();
                    //if( oHistoryItem == null || (oHistoryItem.OfferDate - DateTime.Now).TotalDays > 1  )
                    //{
                    //    oHistoryItem = new WTSO_History();
                    //    oHistoryItem.WTSO_Inventory = oWTSOItem;                            
                    //}
                    //oHistoryItem.Price = dPrice;
                    //oHistoryItem.ShippingOffer = strOffer;
                    //oHistoryItem.OfferDate = DateTime.Now;

                    Console.WriteLine(string.Format("   Adding Product: ID={0}, Title={1}", iProductID, strTitle));
                    oContext.SubmitChanges();
                    return oWTSOItem;

                }
            }
            return null;
        }
        #endregion

    }
}
