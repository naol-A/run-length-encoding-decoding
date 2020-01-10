using System.Drawing;

namespace run_length
{
    class EncodeImage
    {
        public string imageSize = "run-length for image: original size: "; 
        public string encodeImg(Bitmap bitmp)
        {
            string final="";
            Color prevclr=new Color();
            int cnt = 1;
            int ensz = 0;
            int h = bitmp.Height;
            int w = bitmp.Width;
            int j = 0, i = 0;
            final = w + "," + h + "|";
            for (i = 0; i < h; i++)
            {
                for(j = 0; j < w; j++)
                {
                    if (j == 0 && i==0)
                    {
                        prevclr = bitmp.GetPixel(j,i);
                    }
                    else
                    {
                        if (prevclr == bitmp.GetPixel(j, i)){
                            cnt++;
                        }
                        else
                        {
                            final +=cnt+","+prevclr.A.ToString()+
                                ","+prevclr.R.ToString()+
                                ","+prevclr.G.ToString()+
                                ","+prevclr.B.ToString()+";";
                            cnt = 1;
                            ensz++;
                        }
                        prevclr = bitmp.GetPixel(j, i);
                    }
                }
            }
            final += cnt + "," + prevclr.A.ToString() +
                                "," + prevclr.R.ToString() +
                                "," + prevclr.G.ToString() +
                                "," + prevclr.B.ToString() + ";";
            ensz++;
            imageSize += w * h +" pixels"+" encoded image size: "+ensz+" pixels";
            return final;
        }
    }
}
