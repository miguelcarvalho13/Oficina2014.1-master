
var isQuitButton=false; //

var onBattle1=false; // 7

var onPage1=false; // 4

var onPage2=false; // 5



var onCustomizar = false; // 2

var onCreditos = false; // 3

var onCutscene1=false; // 6 

var onReturnMenu=false; // 



function OnMouseEnter()
	{
		renderer.material.color = Color.green;
	}

function OnMouseExit()

	{
		renderer.material.color = Color.white;
	}

function OnMouseUp()

	{
		renderer.material.color = Color.green;
	}



function OnMouseDown()
{
 if(isQuitButton)
 {
 Application.Quit();
 }
 
 if(onCustomizar)
 {
 Application.LoadLevel(2);
 }
 
 if(onCreditos)
 {
 Application.LoadLevel(3);
 }
 
 if(onBattle1)
 {
 Application.LoadLevel(7);
 }
 if(onCutscene1)
 {
 Application.LoadLevel(6);
 }
 if(onReturnMenu)
 {
 Application.LoadLevel(1);
 }
 
if(onPage1)
{
Application.LoadLevel(4);
}
 
if(onPage2)
{
 Application.LoadLevel(5);
}
 
renderer.material.color = Color.green;
}
