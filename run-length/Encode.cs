namespace run_length
{
    class Encode
    {
        public static string encode(string data)
        {
            string str="";
            string rep = "";
            string final = "";
            char prev=(char)0;
            int num=1;
            bool start = true;

            foreach(char c in data)
            {
                if (start == true)
                {
                    prev = c;
                    start = false;
                }
                else if (c == prev)
                {
                    prev = c;
                    num++;
                }
                else
                {
                    str += prev;
                    rep += num.ToString()+",";
                    num = 1;
                    prev = c;
                }
            }
            str += prev;
            rep += num.ToString()+",";
            
            final = rep.Length + "," + str.Length + ";" + rep + str;
            return final;
        }
    }
}
