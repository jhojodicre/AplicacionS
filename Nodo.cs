using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace AplicacionS
{
    class Nodo
    {
        public int Zone_A;
        public int Zone_B;

        public bool Zone_A_ACK;
        public bool Zone_B_ACK;

        public bool Zone_A_ERR;
        public bool Zone_B_ERR;

        public bool Zone_A_FAL;
        public bool Zone_B_FAL;

        public bool Zone_A_ALR;
        public bool Zone_B_ALR;

        public bool Zone_A_OK;
        public bool Zone_B_OK;

        public bool Zone_Check;

        Point Zone_A_Ini;
        Point Zone_A_Fin;
        Point Zone_B_Ini;
        Point Zone_B_Fin;

        public string nodeStatus;

        public string nodeNumber;
        public string nodeZone;

        int node;

        Pen ZoneGreen = new Pen(Color.Green, 5);
        Pen ZoneRed = new Pen(Color.Red, 5);
        Pen ZoneGray = new Pen(Color.Gray, 5);
        Pen ZoneBlue = new Pen(Color.Blue, 5);
        Pen ZoneYellow = new Pen(Color.Yellow, 5);
        Pen ZoneWhite = new Pen(Color.White, 5);

        Graphics Perimetro;

        static SoundPlayer SoundAlarm = new SoundPlayer();
        //SoundAlarm =  new SoundPlayer();

        public Nodo()
        {

        }
        public Nodo(int _nodeNumber)
        {
            SoundAlarm.SoundLocation = "C:/Users/ILATINA/source/repos/jhojodicre/AplicacionS/Resources/sound_alarm_Zones.wav";
            this.node = _nodeNumber;
            this.Zone_B = _nodeNumber * 2;
            this.Zone_A = Zone_B - 1;
        }
        public Nodo(int _nodeNumber, Point A_Ini, Point A_Fin, Point B_Ini, Point B_Fin)
        {
            this.node = _nodeNumber;
            Zone_A_Ini = A_Ini;
            Zone_A_Fin = A_Ini;
            Zone_B_Ini = B_Ini;
            Zone_B_Fin = B_Fin;


        }
        public void NodeUpdate(string status, string _node, string _zone)
        {
            this.nodeStatus = status;
            this.nodeNumber = _node;
            this.nodeZone = _zone;
            NodeChange();
        }
        private void NodeChange()
        {
            switch (nodeStatus)
            {
                case "BOK":
                    if (nodeZone == "A")
                    {
                        //Perimetro.DrawLine(ZoneGreen, Zone_A_Ini, Zone_A_Fin);
                        Zone_A_OK = true;
                        Zone_A_ALR = false;
                        Zone_A_ACK = false;
                        Zone_A_FAL = false;
                        Zone_A_ERR = false;

                    }
                    else if (nodeZone == "B")
                    {
                        //Perimetro.DrawLine(ZoneGreen, Zone_B_Ini, Zone_B_Fin);
                        Zone_B_OK = true;
                        Zone_B_ALR = false;
                        Zone_B_ACK = false;
                        Zone_B_FAL = false;
                        Zone_B_ERR = false;
                    }
                    break;

                case "NOK":
                    if (nodeZone == "A")
                    {
                        Zone_A_ERR = false;
                        Zone_A_FAL = false;
                        Zone_A_ALR = true;
                        Zone_A_OK = false;

                    }
                    else if (nodeZone == "B")
                    {
                        Zone_B_ERR = false;
                        Zone_B_FAL = false;
                        Zone_B_ALR = true;
                        Zone_B_OK = false;
                    }
                    break;

                case "ERR":
                    if (nodeZone == "A")
                    {
                        //Perimetro.DrawLine(ZoneRed, Zone_A_Ini, Zone_A_Fin);
                        Zone_A_ERR = true;
                        Zone_A_ACK = false;
                        Zone_A_FAL = false;
                        Zone_A_ALR = false;
                        Zone_A_OK = false;

                    }
                    else if (nodeZone == "B")
                    {
                        //Perimetro.DrawLine(ZoneRed, Zone_B_Ini, Zone_B_Fin);
                        Zone_B_ERR = true;
                        Zone_B_ACK = false;
                        Zone_B_FAL = false;
                        Zone_B_ALR = false;
                    }
                    break;

                case "FAL":
                    //Perimetro.DrawLine(ZoneRed, Zone_B_Ini, Zone_B_Fin);
                    //Perimetro.DrawLine(ZoneRed, Zone_A_Ini, Zone_A_Fin);
                    Zone_A_FAL = true;
                    Zone_A_ERR = false;
                    Zone_A_ACK = false;
                    Zone_A_ALR = false;
                    Zone_A_OK = false;


                    Zone_B_FAL = true;
                    Zone_B_ERR = false;
                    Zone_B_ACK = false;
                    Zone_B_ALR = false;
                    Zone_B_OK = false;


                    break;

                case "ACK":
                    //Perimetro.DrawLine(ZoneRed, Zone_B_Ini, Zone_B_Fin);
                    //Perimetro.DrawLine(ZoneRed, Zone_A_Ini, Zone_A_Fin);
                    if (Zone_A_ALR == true)
                    {
                        Zone_A_ACK = true;

                    }
                    if (Zone_B_ALR == true)
                    {
                        Zone_B_ACK = true;
                    }

                    break;

            }
            if (Zone_A_ALR && !Zone_A_ACK)
            {
                SoundAlarm.Play();
                Zone_Check = false;
            }
            if(Zone_B_ALR && !Zone_B_ACK)
            {
                SoundAlarm.Play();
                Zone_Check = false;
            }
            if (Zone_A_ALR && Zone_A_ACK && Zone_Check)
            {
                SoundAlarm.Stop(); 
                SoundAlarm.Dispose();
                Zone_Check = true;
            }
            if(Zone_B_ALR && Zone_B_ACK && Zone_Check)
            {
                SoundAlarm.Stop();
                SoundAlarm.Dispose();
                Zone_Check = true;
            }
        }
    }
}
