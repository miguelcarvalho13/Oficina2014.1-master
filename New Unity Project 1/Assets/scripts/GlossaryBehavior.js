
var showingPopUp1 = false;
var showingPopUp2 = false;

var bloc1:GameObject;


function Start () {
	  
    //startTime = Time.time;
	print("I'm attached to " + transform.name);
	 
}



function OnMouseEnter(){
renderer.material.color = Color.green;
}

function OnMouseExit(){
renderer.material.color = Color.white;

}

function OnMouseDown(){
renderer.material.color = Color.green;
showingPopUp1 = !showingPopUp1;
MovingGlossary();

}
function MovingGlossary(){


/*showingPopUp2 = !showingPopUp2;

if(showingPopUp2) {
 
bloc1.transform.position.x = bloc1.transform.position.x - 3;

	}

if(!showingPopUp2) {
 
bloc1.transform.position.x = bloc1.transform.position.x + 3;

	}

*/
if(showingPopUp1) {
 
bloc1.transform.position.y = bloc1.transform.position.y - 3;

	}

if(!showingPopUp1) {
 
bloc1.transform.position.y = bloc1.transform.position.y + 3;

	}
}

function OnMouseUp(){
renderer.material.color = Color.white;
}
