namespace run_length
{
    class Decode
    {
        public static string decompress(string data)
        {
            string[] head =new string[2] {"",""};
            int[] size = new int[2] {0,0};
            string rep = "";
            string trep = "";
            int nrep = 0;
            int crat = 0;
            string str = "";
            string final = "";
            bool[] reach = new bool[2] { false, false};
            int rl=0;
            foreach(char ch in data){
                if (ch.ToString() != ";" && reach[0]==false)
                {
                    if (int.TryParse(ch.ToString(),out rl)&&reach[1]==false)
                    {
                        head[0] += ch.ToString();
                    }
                    else if(ch.ToString()==",")
                    {
                        reach[1] = true;
                    }
                    else
                    {
                        head[1] += ch.ToString();
                    }
                }
                else if (ch.ToString() == ";" && reach[0]==false)
                {
                    reach[0] = true;
                    size[0] = int.Parse(head[0]);
                    size[1] = int.Parse(head[1]);
                }
                else
                {
                    if (size[0] > 0)
                    {
                        rep += ch.ToString();
                        size[0]--;
                    }
                    else if (size[0] == 0 && size[1] > 0)
                    {
                        str += ch.ToString();
                        size[1]--;
                    }
                }
            }
            foreach(char r in rep)
            {
                if(int.TryParse(r.ToString(),out rl))
                {
                    trep += r.ToString();
                }
                else if(r.ToString() == ","){
                    nrep = int.Parse(trep);
                    for(int i = 0; i < nrep; i++)
                    {
                        final += str[crat];
                    }
                    crat++;
                    trep = "";
                }
            }
            return final;
        }
    }
}
