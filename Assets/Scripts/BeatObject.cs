using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    /// <summary>
    /// Beat Objects can also be described as the information held in each spot
    /// that a column has
    /// They have four possible obstacle spots in each, and each of those spots has 
    /// one of five states.
    /// 
    /// Blank, white, black, Black-White Double where we have two objects per beat,
    /// White-Black double which is the same but the white beat comes first.
    /// 
    /// They also have a second state of being high or not. Simply marked with 
    /// "true" or "false"
    /// 
    /// NOTE: When exporting to XML, for each beat we will have an x-pos. So we
    /// take that beat object and create an obstacle on the XML map for each
    /// beat object in that column, at that x-pos.
    /// </summary>
    public class BeatObject
    {
        public string state;
        public bool high;
        public float pos;

        //constructor
        public BeatObject()
        {
            this.state = "blank";
            this.high = false;
            this.pos = 0.0f;
        }

        public void ChangeInfo(string newState, bool newHigh, float newPos)
        {
            this.state = newState;
            this.high = newHigh;
            this.pos = newPos;
        }
    }
}
