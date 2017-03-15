using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is to work out whether the user has solo'd a track or not, for the purpose of the tutorial section
//Inherits from EasyController
public class ControllerSoloOverride : EasyController
{
    bool padDownClicked;

	void PadClick(object sender, ClickedEventArgs e)
    { 
        //CODE FOR SOLOING PART!
        //


        //Get co-ordinates of where the pad is being pressed
        //Thankfully, the object e already stores them, how about that!
        touchpadCoordinates = new Vector2(e.padX, e.padY);

        //SOLOING FUNCTIONALITY
        //If the user is touching the below area and that area is active
        if (padBottomActive == true && touchpadCoordinates.y< 0)
        {
            print(touchpadCoordinates.y);

            //If they're touching a musical block
            if (objectTouching != null)
            {
                //Mute all the other blocks (apart from those marked as temp active
               // MuteAllOthers();
            }
        }

        //GRABBING/MOVING FUNCTIONALITY
        else if (padTopActive == true && touchpadCoordinates.y > 0)
        {

        } 
    }

    public bool getPadDownClicked()
    {
        return padDownClicked;
    }
}
