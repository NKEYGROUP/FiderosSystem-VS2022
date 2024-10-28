
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using dotless.Core.Parser.Infrastructure.Nodes;
using System.Text.RegularExpressions;

namespace App.Mvc.Invest.Fideros.Utils
{
    public class StaticFunctions
    {
        public static IEnumerable<SelectListItem> GetMessageTypeList(string group, string defaultValue = "", bool isRequired = false)
        {
            var lsLabelCodes = new List<SelectListItem>();

            IList<SelectListItem> list = new List<SelectListItem>();
            if (isRequired)
                list.Add(new SelectListItem() { Text = "* Type de message", Value = "" });
            else
                list.Add(new SelectListItem() { Text = "* Type de message", Value = "" });

            if (group == "WEB_CONTACT_MSG_TYPE_INVEST")
            {
                list.Add(new SelectListItem()
                {
                    Value = "INFOS_INVEST",
                    Text = "Information Investissement"
                });
				list.Add(new SelectListItem()
				{
					Value = "INFOS_PARTENAIRE",
					Text = "Information partenaire"
				});
				list.Add(new SelectListItem()
				{
					Value = "INFOS_CLIENT",
					Text = "Demande de rendez-vous"
				});
			}
            else
            {
                list.Add(new SelectListItem()
                {
                    Value = "INFOS_INVEST",
                    Text = "Information Investissement"
                });
                list.Add(new SelectListItem()
                {
                    Value = "INFOS_PARTENAIRE",
                    Text = "Information partenaire"
                });
                list.Add(new SelectListItem()
                {
                    Value = "INFOS_COMMERCE",
                    Text = "Information commerciale"
                });
                list.Add(new SelectListItem()
                {
                    Value = "INFOS_CLIENT",
                    Text = "Demande de rendez-vous"
                });
                list.Add(new SelectListItem()
                {
                    Value = "INFOS_GENERALES",
                    Text = "Information générale"
                });
                list.Add(new SelectListItem()
                {
                    Value = "INFOS_CANDIDATURE",
                    Text = "Information Candidature"
                });
            }
            return list;
        }

        public static String ReplaceDataWithValues(String contentData, Dictionary<string, string> dictDataValues)
        {
            string strResult = string.Empty;
            if (dictDataValues == null || dictDataValues.Count == 0 || string.IsNullOrEmpty(contentData))
                return contentData;
            try
            {
                foreach (KeyValuePair<string, string> elmt in dictDataValues)
                {
                    contentData = contentData.Replace(elmt.Key, elmt.Value);
                }

                string pattern = @"$\w*$";
                contentData = Regex.Replace(contentData, pattern, string.Empty);
            }
            catch (Exception e)
            {
                throw e;
            }
            return contentData;
        }

        public static IEnumerable<SelectListItem> GetCountryList(string zoneCode, string defaultValue = "", bool isRequired = false, bool isSearch = false)
        {
            var Countries = new List<SelectListItem>();
            IList<SelectListItem> list = new List<SelectListItem>();
            if (isRequired)
            {
                if (isSearch)
                    list.Add(new SelectListItem() { Text = "Tous les pays*", Value = "0" });
                else
                    list.Add(new SelectListItem() { Text = "Pays*", Value = "" });
            }
            else
            {
                if (isSearch)
                    list.Add(new SelectListItem() { Text = "Tous les pays", Value = "0" });
                else
                    list.Add(new SelectListItem() { Text = "Pays", Value = "" });
            }
            list.Add(new SelectListItem() { Text = "France", Value = "FR" });
            list.Add(new SelectListItem() { Text = "Suisse", Value = "CH" });
            list.Add(new SelectListItem() { Text = "Belgique", Value = "BE" });
            list.Add(new SelectListItem() { Text = "Europe", Value = "EU" });
            list.Add(new SelectListItem() { Text = "Afrique", Value = "AFIQUE" });
            list.Add(new SelectListItem() { Text = "Asie", Value = "ASIE" });
            list.Add(new SelectListItem() { Text = "Amérique", Value = "AMERIQUE" });

            return list;
        }

        public static String ReadFile(String fileName, bool htmlEncode = false)
        {
            string strResult = string.Empty;
            try
            {
                // Create an instance of StreamReader to read from a file.
                StreamReader streamReader = new StreamReader(fileName);
                strResult = streamReader.ReadToEnd();
                streamReader.Close();

                if (htmlEncode)
                {
                    strResult = System.Web.HttpUtility.HtmlEncode(strResult);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return strResult;
        }

        //Captcha Code generation
        const string Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ";
        public static string GenerateCaptchaCode()
        {
            Random rand = new Random();
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }

        // Make a captcha image for the text.
        public static Bitmap MakeCaptchaImge(string txt,
            int min_size, int max_size, int wid, int hgt)
        {
            // Make the bitmap and associated Graphics object.
            Bitmap bm = new Bitmap(wid, hgt);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.Clear(Color.White);

                // See how much room is available for each character.
                int ch_wid = (int)(wid / txt.Length);

                // Draw each character.
                for (int i = 0; i < txt.Length; i++)
                {
                    float font_size = Rand.Next(min_size, max_size);
                    using (Font the_font = new Font("Times New Roman",
                        font_size, FontStyle.Bold))
                    {
                        DrawCharacter(txt.Substring(i, 1), gr,
                            the_font, i * ch_wid, ch_wid, wid, hgt);
                    }
                }
            }

            return bm;
        }

        // Random var
        private static Random Rand = new Random();
        // Draw a deformed character at this position.
        private static int PreviousAngle = 0;
        private static void DrawCharacter(string txt, Graphics gr,
             Font the_font, int X, int ch_wid, int wid, int hgt)
        {
            // Center the text.
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Center;
                RectangleF rectf = new RectangleF(X, 0, ch_wid, hgt);

                // Convert the text into a path.
                using (GraphicsPath graphics_path = new GraphicsPath())
                {
                    graphics_path.AddString(txt,
                        the_font.FontFamily, (int)the_font.Style,
                        the_font.Size, rectf, string_format);

                    // Make random warping parameters.
                    float x1 = (float)(X + Rand.Next(ch_wid) / 2);
                    float y1 = (float)(Rand.Next(hgt) / 2);
                    float x2 = (float)(X + ch_wid / 2 +
                        Rand.Next(ch_wid) / 2);
                    float y2 = (float)(hgt / 2 + Rand.Next(hgt) / 2);
                    PointF[] pts = {
            new PointF(
                (float)(X + Rand.Next(ch_wid) / 4),
                (float)(Rand.Next(hgt) / 4)),
            new PointF(
                (float)(X + ch_wid - Rand.Next(ch_wid) / 4),
                (float)(Rand.Next(hgt) / 4)),
            new PointF(
                (float)(X + Rand.Next(ch_wid) / 4),
                (float)(hgt - Rand.Next(hgt) / 4)),
            new PointF(
                (float)(X + ch_wid - Rand.Next(ch_wid) / 4),
                (float)(hgt - Rand.Next(hgt) / 4))
        };
                    Matrix mat = new Matrix();
                    graphics_path.Warp(pts, rectf, mat,
                        WarpMode.Perspective, 0);

                    // Rotate a bit randomly.
                    float dx = (float)(X + ch_wid / 2);
                    float dy = (float)(hgt / 2);
                    gr.TranslateTransform(-dx, -dy, MatrixOrder.Append);
                    int angle = PreviousAngle;
                    do
                    {
                        angle = Rand.Next(-30, 30);
                    } while (Math.Abs(angle - PreviousAngle) < 20);
                    PreviousAngle = angle;
                    gr.RotateTransform(angle, MatrixOrder.Append);
                    gr.TranslateTransform(dx, dy, MatrixOrder.Append);

                    // Draw the text.
                    gr.FillPath(Brushes.Blue, graphics_path);
                    gr.ResetTransform();
                }
            }
        }
    }
}
