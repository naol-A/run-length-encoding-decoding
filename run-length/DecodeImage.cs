using System.Drawing;

namespace run_length
{
    class DecodeImage
    {
        public static string decodeImg(string imageFile)
        {
            string reslt = "Run-length for image: decoded image size: ";
            string fileName = System.Environment.CurrentDirectory + "\\image.bmp";
            Bitmap bmp = new Bitmap(1,1);
            Color clr=new Color();
            string w="", h="";
            int iw = 0, ih = 0,tmpw=0,tmph=0;
            int prs = 0;
            int encps = 0;
            string scnt = "", sa = "", sr = "", sg = "", sb = "";
            int icnt = 0,ia = 0,ir = 0,ig = 0,ib = 0;
            bool[] dmreach = new bool[2] {false,false};
            bool[] pxreach = new bool[5] { false, false, false, false, false };

            foreach(char ch in imageFile)
            {
                if (ch != '|' && dmreach[0] == false)
                {
                    if (int.TryParse(ch.ToString(), out prs) && dmreach[1] == false)
                    {
                        w += ch.ToString();
                    }
                    else if (ch == ',')
                    {
                        dmreach[1] = true;
                    }
                    else if (int.TryParse(ch.ToString(), out prs) && dmreach[1] == true)
                    {
                        h += ch.ToString();
                    }
                }
                else if (ch == '|' && dmreach[0] == false)
                {
                    iw = int.Parse(w);
                    ih = int.Parse(h);
                    bmp = new Bitmap(iw, ih);
                    reslt += iw * ih+" pixels ";
                    dmreach[0] = true;
                }
                else if (ch != '|' && dmreach[0] == true)
                {
                    if(int.TryParse(ch.ToString(),out prs))
                    {
                        if (!pxreach[0])
                        {
                            scnt +=ch.ToString();
                        }
                        else if (pxreach[0] && !pxreach[1])
                        {
                            sa += ch.ToString();
                        }
                        else if (pxreach[1] && !pxreach[2])
                        {
                            sr += ch.ToString();
                        }
                        else if (pxreach[2] && !pxreach[3])
                        {
                            sg += ch.ToString();
                        }
                        else if (pxreach[3] && !pxreach[4])
                        {
                            sb += ch.ToString();
                        }
                    }
                    else
                    {
                        if (ch == ',' && !pxreach[0])
                        {
                            pxreach[0] = true;
                            icnt = int.Parse(scnt);
                        }
                        else if (ch == ',' && pxreach[0] && !pxreach[1])
                        {
                            pxreach[1] = true;
                            ia = int.Parse(sa);
                        }
                        else if (ch == ',' && pxreach[1] && !pxreach[2])
                        {
                            pxreach[2] = true;
                            ir = int.Parse(sr);
                        }
                        else if (ch == ',' && pxreach[2] && !pxreach[3])
                        {
                            pxreach[3] = true;
                            ig = int.Parse(sg);
                        }
                        else if (ch == ';' && pxreach[3] && !pxreach[4])
                        {
                            encps++;
                            pxreach[4] = true;
                            ib = int.Parse(sb);
                            clr = Color.FromArgb(ia, ir, ig, ib);
                            for (int i = 0; i < icnt; i++)
                            {
                                if (tmpw < iw)
                                {
                                    bmp.SetPixel(tmpw, tmph, clr);
                                }
                                else
                                {
                                    tmpw = 0;
                                    tmph++;
                                    bmp.SetPixel(tmpw, tmph, clr);
                                }
                                tmpw++;
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                pxreach[i] = false;
                            }
                            scnt = sa = sr = sg = sb = "";
                        }
                    }
                }
            }
            reslt += " encoded image size: " + encps;
            bmp.Save(fileName);
            return reslt;
        }
    }
}
